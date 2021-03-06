﻿using System.Collections.Generic;
using PAT.Common.Classes.CUDDLib;
using PAT.Common.Classes.SemanticModels.LTS.BDD;
using PAT.Common.Classes.Expressions.ExpressionClass;

namespace PAT.Common.Classes.SemanticModels.TTS
{
    public partial class TimeBehaviors
    {
        public static AutomataBDD Timeout(AutomataBDD m0, AutomataBDD m1, int t, Model model)
        {
            AutomataBDD result = new AutomataBDD();

            List<string> varNames = TimeoutSetVariable(m0, m1, t, model, result);
            TimeoutSetInit(varNames[0], m0, m1, result);
            TimeoutEncodeTransitionChannel(varNames[0], m0, m1, t, model, result);
            TimeoutEncodeTick(varNames[0], m0, m1, t, model, result);

            //
            return result;
        }

        /// <summary>
        /// P.var : m0 ∪ m1 ∪ {clk}
        /// </summary>
        private static List<string> TimeoutSetVariable(AutomataBDD m0, AutomataBDD m1, int t, Model model, AutomataBDD result)
        {
            result.variableIndex.AddRange(m0.variableIndex);
            result.variableIndex.AddRange(m1.variableIndex);
            //
            string clk = Model.GetNewTempVarName();
            model.AddLocalVar(clk, -1, t + 1);
            result.variableIndex.Add(model.GetNumberOfVars() - 1);

            return new List<string>() { clk};
        }

        /// <summary>
        /// P.init: m0.init ^ clk = 0
        /// </summary>
        private static void TimeoutSetInit(string clk, AutomataBDD m0, AutomataBDD m1, AutomataBDD result)
        {
            result.initExpression = new PrimitiveApplication(PrimitiveApplication.AND, m0.initExpression,
                                        new PrimitiveApplication(PrimitiveApplication.EQUAL, new Variable(clk), new IntConstant(0)));
        }

        private static void TimeoutEncodeTransitionChannel(string clk, AutomataBDD m0, AutomataBDD m1, int t, Model model, AutomataBDD result)
        {
            Expression guard;
            List<CUDDNode> guardDD, transTemp;

            //1. (clk <= t) and m0.Trans/In/Out and [(event' = tau and clk' = clk) or (event' != tau and clk' = -1)]
            guard = new PrimitiveApplication(PrimitiveApplication.AND, new PrimitiveApplication(PrimitiveApplication.LESS_EQUAL, new Variable(clk), new IntConstant(t)),
                                                new PrimitiveApplication(PrimitiveApplication.OR, new PrimitiveApplication(PrimitiveApplication.AND,
                                                    AutomataBDD.GetTauTransExpression(),
                                                    new Assignment(clk, new Variable(clk))), new PrimitiveApplication(PrimitiveApplication.AND,
                                                    AutomataBDD.GetNotTauTransExpression(),
                                                    new Assignment(clk, new IntConstant(-1)))));
            guardDD = guard.TranslateBoolExpToBDD(model).GuardDDs;

            CUDD.Ref(guardDD);
            transTemp = CUDD.Function.And(m0.transitionBDD, guardDD);
            result.transitionBDD.AddRange(transTemp);

            //
            CUDD.Ref(guardDD);
            transTemp = CUDD.Function.And(m0.channelInTransitionBDD, guardDD);
            result.channelInTransitionBDD.AddRange(transTemp);

            //
            //CUDD.Ref(guardDD);
            transTemp = CUDD.Function.And(m0.channelOutTransitionBDD, guardDD);
            result.channelOutTransitionBDD.AddRange(transTemp);

            //2. clk = t and event' = tau and clk' = t + 1 and m1.Init
            guard = new PrimitiveApplication(PrimitiveApplication.AND, new PrimitiveApplication(PrimitiveApplication.EQUAL, new Variable(clk), new IntConstant(t)),
                        new PrimitiveApplication(PrimitiveApplication.AND, AutomataBDD.GetTauTransExpression(),
                                            new Assignment(clk, new IntConstant(t + 1))));
            guardDD = guard.TranslateBoolExpToBDD(model).GuardDDs;
            guardDD = CUDD.Function.And(m1.GetInitInColumn(model), CUDD.Function.Or(guardDD));
            guardDD = model.AddVarUnchangedConstraint(guardDD, model.GlobalVarIndex);
            result.transitionBDD.AddRange(guardDD);

            //3. clk = t + 1 and m1.Trans and clk' = t + 1
            guard = new PrimitiveApplication(PrimitiveApplication.AND, new PrimitiveApplication(PrimitiveApplication.EQUAL, new Variable(clk), new IntConstant(t + 1)),
                                            new Assignment(clk, new IntConstant(t + 1)));
            guardDD = guard.TranslateBoolExpToBDD(model).GuardDDs;

            //
            CUDD.Ref(guardDD);
            transTemp = CUDD.Function.And(m1.transitionBDD, guardDD);
            result.transitionBDD.AddRange(guardDD);

            //
            CUDD.Ref(guardDD);
            transTemp = CUDD.Function.And(m1.channelInTransitionBDD, guardDD);
            result.channelInTransitionBDD.AddRange(guardDD);

            //
            //CUDD.Ref(guardDD);
            transTemp = CUDD.Function.And(m1.channelOutTransitionBDD, guardDD);
            result.channelOutTransitionBDD.AddRange(guardDD);
        }

        private static void TimeoutEncodeTick(string clk, AutomataBDD m0, AutomataBDD m1, int t, Model model, AutomataBDD result)
        {
            Expression guard;
            List<CUDDNode> guardDD;

            //1. m0.Tick and [(clk >= 0 and clk < t and clk' = clk + 1) or (clk = -1 and clk' = -1)]
            guard= new PrimitiveApplication(PrimitiveApplication.OR, new PrimitiveApplication(PrimitiveApplication.AND, new PrimitiveApplication(PrimitiveApplication.AND,
                        new PrimitiveApplication(PrimitiveApplication.GREATER_EQUAL, new Variable(clk), new IntConstant(0)),
                            new PrimitiveApplication(PrimitiveApplication.LESS, new Variable(clk), new IntConstant(t))),
                            new Assignment(clk, new PrimitiveApplication(PrimitiveApplication.PLUS, new Variable(clk), new IntConstant(1)))), new PrimitiveApplication(PrimitiveApplication.AND,
                                new PrimitiveApplication(PrimitiveApplication.EQUAL, new Variable(clk), new IntConstant(-1)),
                                new Assignment(clk, new IntConstant(-1))));

            guardDD = guard.TranslateBoolExpToBDD(model).GuardDDs;
            guardDD = CUDD.Function.And(m0.Ticks, guardDD);
            result.Ticks.AddRange(guardDD);

            //2. clk = t + 1 and m1.Tick and clk' = t + 1
            guard = new PrimitiveApplication(PrimitiveApplication.AND, new PrimitiveApplication(PrimitiveApplication.EQUAL, new Variable(clk), new IntConstant(t + 1)),
                                            new Assignment(clk, new IntConstant(t + 1)));
            guardDD = guard.TranslateBoolExpToBDD(model).GuardDDs;
            guardDD = CUDD.Function.And(m1.Ticks, guardDD);
            result.Ticks.AddRange(guardDD);
        }
    }
}