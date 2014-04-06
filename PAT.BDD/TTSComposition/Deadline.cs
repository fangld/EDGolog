using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAT.Common.Classes.CUDDLib;
using PAT.Common.Classes.SemanticModels.LTS.BDD;
using PAT.Common.Classes.Expressions.ExpressionClass;

namespace PAT.Common.Classes.SemanticModels.TTS
{
    public partial class TimeBehaviors
    {
        public static AutomataBDD Deadline(AutomataBDD m0, int t, Model model)
        {
            AutomataBDD result = new AutomataBDD();

            List<string> varNames = DeadlineSetVariable(m0, t, model, result);
            DeadlineSetInit(varNames[0], m0, result);
            DeadlineEncodeTransitionChannel(varNames[0], m0, t, model, result);
            DeadlineEncodeTick(varNames[0], m0, t, model, result);

            //
            return result;
        }

        /// <summary>
        /// P.var : m0 ∪ {clk}
        /// </summary>
        private static List<string> DeadlineSetVariable(AutomataBDD m0, int t, Model model, AutomataBDD result)
        {
            result.variableIndex.AddRange(m0.variableIndex);
            //
            string clk = Model.GetNewTempVarName();
            model.AddLocalVar(clk, -1, t);
            result.variableIndex.Add(model.GetNumberOfVars() - 1);

            return new List<string>() { clk };
        }

        /// <summary>
        /// P.init: m0.init ^ clk = 0
        /// </summary>
        private static void DeadlineSetInit(string clk, AutomataBDD m0, AutomataBDD result)
        {
            result.initExpression = new PrimitiveApplication(PrimitiveApplication.AND, m0.initExpression,
                                                             new PrimitiveApplication(PrimitiveApplication.EQUAL,
                                                                                      new Variable(clk),
                                                                                      new IntConstant(0)));
        }

        private static void DeadlineEncodeTransitionChannel(string clk, AutomataBDD m0, int t, Model model, AutomataBDD result)
        {
            //1. (clk <= t) and m0.Trans/In/Out and [(event' = terminate and clk' = -1) or (event' != terminate and clk' = clk)]
            Expression guard = new PrimitiveApplication(PrimitiveApplication.AND,
                                                new PrimitiveApplication(PrimitiveApplication.LESS_EQUAL, new Variable(clk), new IntConstant(t)),
                                                new PrimitiveApplication(PrimitiveApplication.OR, new PrimitiveApplication(PrimitiveApplication.AND,
                                                    AutomataBDD.GetTerminateTransExpression(),
                                                    new Assignment(clk, new IntConstant(-1))), new PrimitiveApplication(PrimitiveApplication.AND,
                                                    AutomataBDD.GetNotTerminateTransExpression(),
                                                    new Assignment(clk, new Variable(clk)))));

            List<CUDDNode> guardDD = guard.TranslateBoolExpToBDD(model).GuardDDs;

            List<CUDDNode> transTemp = CUDD.Function.And(m0.transitionBDD, guardDD);
            result.transitionBDD.AddRange(transTemp);

            //1. (clk <= t & clk' = clk & In/Out)
            guard = new PrimitiveApplication(PrimitiveApplication.AND,
                                             new PrimitiveApplication(PrimitiveApplication.LESS_EQUAL, new Variable(clk),
                                                                      new IntConstant(t)),
                                             new Assignment(clk, new Variable(clk)));

            guardDD = guard.TranslateBoolExpToBDD(model).GuardDDs;

            //
            CUDD.Ref(guardDD);
            transTemp = CUDD.Function.And(m0.channelInTransitionBDD, guardDD);
            result.channelInTransitionBDD.AddRange(transTemp);

            //
            //CUDD.Ref(guardDD);
            transTemp = CUDD.Function.And(m0.channelOutTransitionBDD, guardDD);
            result.channelOutTransitionBDD.AddRange(transTemp);
        }

        private static void DeadlineEncodeTick(string clk, AutomataBDD m0, int t, Model model, AutomataBDD result)
        {
            //1. m0.Tick and [(0 <= clk < t and clk' = clk + 1) or (clk = -1 and clk' = -1)]
            Expression guard= new PrimitiveApplication(PrimitiveApplication.OR,
                                new PrimitiveApplication(PrimitiveApplication.AND, new PrimitiveApplication(PrimitiveApplication.AND,
                                    new PrimitiveApplication(PrimitiveApplication.LESS_EQUAL, new IntConstant(0), new Variable(clk)),
                                    new PrimitiveApplication(PrimitiveApplication.LESS, new Variable(clk), new IntConstant(t))),
                                    new Assignment(clk, new PrimitiveApplication(PrimitiveApplication.PLUS, new Variable(clk), new IntConstant(1)))),
                                new PrimitiveApplication(PrimitiveApplication.AND,
                                    new PrimitiveApplication(PrimitiveApplication.EQUAL, new Variable(clk), new IntConstant(-1)),
                                    new Assignment(clk, new IntConstant(-1))));

            List<CUDDNode> guardDD = guard.TranslateBoolExpToBDD(model).GuardDDs;
            guardDD = CUDD.Function.And(m0.Ticks, guardDD);
            result.Ticks.AddRange(guardDD);
        }
    }
}
