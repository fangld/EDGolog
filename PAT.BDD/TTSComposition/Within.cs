using System.Collections.Generic;
using PAT.Common.Classes.CUDDLib;
using PAT.Common.Classes.SemanticModels.LTS.BDD;
using PAT.Common.Classes.Expressions.ExpressionClass;

namespace PAT.Common.Classes.SemanticModels.TTS
{
    public partial class TimeBehaviors
    {
        public static AutomataBDD Within(AutomataBDD m0, int t, Model model)
        {
            AutomataBDD result = new AutomataBDD();

            List<string> varNames = WithinSetVariable(m0, t, model, result);
            WithinSetInit(varNames[0], m0, result);
            WithinEncodeTransitionChannel(varNames[0], m0, t, model, result);
            WithinEncodeTick(varNames[0], m0, t, model, result);

            //
            return result;
        }

        /// <summary>
        /// P.var : m0 ∪ {clk}
        /// </summary>
        private static List<string> WithinSetVariable(AutomataBDD m0, int t, Model model, AutomataBDD result)
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
        private static void WithinSetInit(string clk, AutomataBDD m0, AutomataBDD result)
        {
            result.initExpression = new PrimitiveApplication(PrimitiveApplication.AND, m0.initExpression,
                                        new PrimitiveApplication(PrimitiveApplication.EQUAL, new Variable(clk), new IntConstant(0)));
        }

        private static void WithinEncodeTransitionChannel(string clk, AutomataBDD m0, int t, Model model, AutomataBDD result)
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
        }

        private static void WithinEncodeTick(string clk, AutomataBDD m0, int t, Model model, AutomataBDD result)
        {
            Expression guard;
            List<CUDDNode> guardDD;

            //1. m0.Tick and [(clk >= 0 and clk < t and clk' = clk + 1) or (clk = -1 and clk' = -1)]
            guard = new PrimitiveApplication(PrimitiveApplication.OR, new PrimitiveApplication(PrimitiveApplication.AND, new PrimitiveApplication(PrimitiveApplication.AND,
                        new PrimitiveApplication(PrimitiveApplication.GREATER_EQUAL, new Variable(clk), new IntConstant(0)),
                            new PrimitiveApplication(PrimitiveApplication.LESS, new Variable(clk), new IntConstant(t))),
                            new Assignment(clk, new PrimitiveApplication(PrimitiveApplication.PLUS, new Variable(clk), new IntConstant(1)))), new PrimitiveApplication(PrimitiveApplication.AND,
                                new PrimitiveApplication(PrimitiveApplication.EQUAL, new Variable(clk), new IntConstant(-1)),
                                new Assignment(clk, new IntConstant(-1))));
            guardDD = guard.TranslateBoolExpToBDD(model).GuardDDs;
            guardDD = CUDD.Function.And(m0.Ticks, guardDD);
            result.Ticks.AddRange(guardDD);
        }
    }
}
