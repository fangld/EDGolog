using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Servers
{
    public class ServerAction : Action
    {

        #region Methods for creating an instance

        public override void FromContext(int initialCuddIndex, PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            CurrentCuddIndex = initialCuddIndex;
            Name = context.actionSymbol().GetText();
            GenerateVariableList(context.listVariable());
            GenerateAbstractPredicates(context.actionDefBody(), predDict);
            GeneratePrecondition(context, predDict);
            GenerateEffect(context, predDict);
        }

        #endregion

        #region Methods for generating cudd nodes

        protected override CUDDNode GetCuddNode(PlanningParser.GdContext context)
        {
            CUDDNode result = null;

            if (context.atomicFormulaTerm() != null)
            {
                result = GetCuddNode(context.atomicFormulaTerm());
            }
            else if (context.literalTerm() != null)
            {
                result = GetCuddNode(context.literalTerm());
            }
            else if (context.AND() != null)
            {
                result = GetCuddNode(context.gd()[0]);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gd()[i]);
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Ref(andNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = GetCuddNode(context.gd()[0]);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gd()[i]);
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    CUDD.Ref(orNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = GetCuddNode(context.gd()[0]);
                result = CUDD.Function.Not(gdNode);
                CUDD.Ref(result);
                CUDD.Deref(gdNode);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = GetCuddNode(context.gd()[0]);
                CUDDNode gdNode1 = GetCuddNode(context.gd()[1]);

                result = CUDD.Function.Implies(gdNode0, gdNode1);
                CUDD.Ref(result);
                CUDD.Deref(gdNode0);
                CUDD.Deref(gdNode1);
            }

            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.AtomicFormulaTermContext context)
        {
            AbstractPredicate abstractPredicate = GetAbstractPredicate(context);
            int index = abstractPredicate.CuddIndexList[0];
            CUDDNode result = CUDD.Var(index);
            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.LiteralTermContext context)
        {
            CUDDNode subNode = GetCuddNode(context.atomicFormulaTerm());
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

        //protected override void GenerateAbstractPredicates(PlanningParser.AtomicFormulaTermContext context, IReadOnlyDictionary<string, Predicate> predDict)
        //{
        //    var abstractPredicate = CreateAbstractPredicate(context, predDict);
        //    if (!_abstractPredDict.ContainsKey(abstractPredicate.ToString()))
        //    {
        //        abstractPredicate.CuddIndexList[0] = CurrentCuddIndex;
        //        CurrentCuddIndex++;
        //        _abstractPredDict.Add(abstractPredicate.ToString(), abstractPredicate);
        //    }
        //}

        #endregion

        protected override int PredicateCuddIndexNumber
        {
            get { return 1; }
        }
    }
}
