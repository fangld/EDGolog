using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.Collections;

namespace Planning.ContextExtensions
{
    public static class CEffectContextExtension
    {
        public static List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> GetCondEffectList(
            this PlanningParser.CEffectContext context, CUDDNode currentConditionNode,
            IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
        {
            List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> result;
            if (context.FORALL() != null)
            {
                CEffectEnumerator enumerator = new CEffectEnumerator(context, currentConditionNode, predicateDict,
                    assignment);
                Algorithms.IterativeScanMixedRadix(enumerator);
                result = enumerator.CondEffectList;
            }
            else if (context.WHEN() != null)
            {
                result = new List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>>();
                CUDD.Ref(currentConditionNode);
                CUDDNode gdNode = context.gd().GetCuddNode(predicateDict, assignment);
                CUDDNode conditionNode = CUDD.Function.And(currentConditionNode, gdNode);
                if (!conditionNode.Equals(CUDD.ZERO))
                {
                    var literaCollection = context.condEffect().GetLiteralArray(predicateDict, assignment);
                    var condEffect = new Tuple<CUDDNode, Tuple<Predicate, bool>[]>(conditionNode, literaCollection);
                    result.Add(condEffect);
                }
                else
                {
                    CUDD.Deref(conditionNode);
                }
            }
            else
            {
                result = new List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>>();
                var literals = context.condEffect().GetLiteralArray(predicateDict, assignment);
                var condEffect = new Tuple<CUDDNode, Tuple<Predicate, bool>[]>(currentConditionNode, literals);
                result.Add(condEffect);
            }
            return result;
        }
    }
}
