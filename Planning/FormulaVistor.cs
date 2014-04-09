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
        #region Fields

        private int _variablesCount = 0;

        #endregion

        //public override CUDDNode VisitAtomicFormulaName(PlanningParser.AtomicFormulaNameContext context)
        //{
        //    Console.WriteLine(_variablesCount);
        //    CUDDNode result = CUDD.Var(_variablesCount);
        //    _variablesCount++;
        //    return result;
        //}

        //public override CUDDNode VisitLiteralName(PlanningParser.LiteralNameContext context)
        //{
        //    CUDDNode subNode = Visit(context.atomicFormulaName());
        //    CUDDNode result;

        //    if (context.NOT() != null)
        //    {
        //        result = CUDD.Function.Not(subNode);
        //        CUDD.Ref(result);
        //    }
        //    else
        //    {
        //        result = subNode;
        //    }
        //    return result;
        //}

        public override CUDDNode VisitEffect(PlanningParser.EffectContext context)
        {
            CUDDNode result = Visit(context.cEffect()[0]);
            for (int i = 1; i < context.cEffect().Count; i++)
            {
                CUDDNode gdNode = Visit(context.cEffect()[i]);
                CUDDNode andNode = CUDD.Function.And(result, gdNode);
                CUDD.Deref(result);
                CUDD.Ref(andNode);
                result = andNode;
            }
            return result;
        }

        public override CUDDNode VisitCEffect(PlanningParser.CEffectContext context)
        {
            CUDDNode result;

            if (context.WHEN() != null)
            {
                CUDDNode condPreNode = Visit(context.gd());
                CUDDNode condEffNode = Visit(context.condEffect());
                result = CUDD.Function.Implies(condPreNode, condEffNode);
                CUDD.Ref(result);
                CUDD.Deref(condPreNode);
                CUDD.Deref(condEffNode);
            }
            else
            {
                result = Visit(context.literalTerm());
            }
            return result;
        }

        public override CUDDNode VisitCondEffect(PlanningParser.CondEffectContext context)
        {
            CUDDNode result = Visit(context.literalTerm()[0]);
            for (int i = 1; i < context.literalTerm().Count; i++)
            {
                CUDDNode gdNode = Visit(context.literalTerm()[i]);
                CUDDNode andNode = CUDD.Function.And(result, gdNode);
                CUDD.Deref(result);
                CUDD.Ref(andNode);
                result = andNode;
            }
            return result;
        }

        public override CUDDNode VisitAtomicFormulaTerm(PlanningParser.AtomicFormulaTermContext context)
        {
            //Console.WriteLine("Before Atomic Formula: {0}, count: {1}", context.GetText(), _variablesCount);
            CUDDNode result = CUDD.Var(_variablesCount);
            _variablesCount++;
            //Console.WriteLine("After Atomic Formula: {0}, count: {1}", context.GetText(), _variablesCount);

            return result;
        }

        public override CUDDNode VisitLiteralTerm(PlanningParser.LiteralTermContext context)
        {
            //Console.WriteLine("Before Literal: {0}, count: {1}", context.GetText(), _variablesCount);
            CUDDNode subNode = Visit(context.atomicFormulaTerm());
            CUDDNode result;

            if (context.NOT() != null)
            {
                //Console.WriteLine("Before Not literal: {0}, count: {1}", context.GetText(), _variablesCount);
                result = CUDD.Function.Not(subNode);
                CUDD.Ref(result);
                //Console.WriteLine("After Not literal: {0}, count: {1}", context.GetText(), _variablesCount);
            }
            else
            {
                result = subNode;
            }

            //Console.WriteLine("After Literal: {0}, count: {1}", context.GetText(), _variablesCount);
            return result;
        }

        public override CUDDNode VisitGd(PlanningParser.GdContext context)
        {
            //Console.WriteLine("Before Gd: {0}", context.GetText());
            CUDDNode result = null;

            if (context.atomicFormulaTerm() != null) 
            {
                result = Visit(context.atomicFormulaTerm());
            }
            else if (context.literalTerm() != null)
            {
                //Console.WriteLine("Before Literal gd: {0}", context.GetText());
                result = Visit(context.literalTerm());
                //Console.WriteLine("After Literal gd: {0}", context.GetText());
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

            //Console.WriteLine("After Gd: {0}", context.GetText());
            return result;
        }
    }
}
