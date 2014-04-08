using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class FormulaVistor : PlanningBaseVisitor<CUDDNode>
    {
        private int _variablesCount = 0;

        public override CUDDNode VisitAtomicFormulaName(PlanningParser.AtomicFormulaNameContext context)
        {
            //Console.WriteLine(_variablesCount);
            CUDDNode result = CUDD.Var(_variablesCount);
            _variablesCount++;
            return result;
        }

        public override CUDDNode VisitLiteralName(PlanningParser.LiteralNameContext context)
        {
            CUDDNode subNode = Visit(context.atomicFormulaName());
            CUDDNode result;

            if (context.NOT() != null)
            {
                result = CUDD.Function.Not(subNode);
                CUDD.Ref(result);
            }
            else
            {
                result = subNode;
            }
            return result;
        }

        public override CUDDNode VisitGd(PlanningParser.GdContext context)
        {
            CUDDNode result = null;

            if (context.atomicFormulaTerm() != null || context.literalTerm() != null)
            {
                result = Visit(context);
            }
            else if (context.AND() != null)
            {
                result = Visit(context.gd()[0]);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = Visit(context.gd()[i]);
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Deref(result);
                    CUDD.Ref(andNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = Visit(context.gd()[0]);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = Visit(context.gd()[i]);
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    CUDD.Deref(result);
                    CUDD.Ref(orNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = Visit(context.gd()[0]);
                result = CUDD.Function.Not(gdNode);
                CUDD.Deref(gdNode);
                CUDD.Ref(result);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = Visit(context.gd()[0]);
                CUDDNode gdNode1 = Visit(context.gd()[1]);

                result = CUDD.Function.Implies(gdNode0, gdNode1);
                CUDD.Deref(gdNode0);
                CUDD.Deref(gdNode1);
                CUDD.Ref(result);
            }

            return result;
        }

        public override CUDDNode VisitPrefGD(PlanningParser.PrefGDContext context)
        {
            return Visit(context.gd());
        }

        public override CUDDNode VisitPreGD(PlanningParser.PreGDContext context)
        {
            CUDDNode result = null;
            if (context.prefGD() != null)
            {
                result = Visit(context);
            }
            else
            {
                result = Visit(context.preGD()[0]);
                for (int i = 1; i < context.preGD().Count; i++)
                {
                    CUDDNode gdNode = Visit(context.preGD()[i]);
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Ref(andNode);
                    CUDD.Deref(result);
                    result = andNode;
                }
            }

            return result;
        }
    }
}
