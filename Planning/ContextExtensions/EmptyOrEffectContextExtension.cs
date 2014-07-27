using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.ContextExtensions
{
    public static class EmptyOrEffectContextExtension
    {
        public static List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> GetEffect(this PlanningParser.EmptyOrEffectContext context,
            IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
        {
            var result = new List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>>();
            if (context != null)
            {
                PlanningParser.EffectContext effectContext = context.effect();
                if (effectContext != null)
                {
                    foreach (var cEffectContext in effectContext.cEffect())
                    {
                        CUDDNode initialCuddNode = CUDD.ONE;
                        CUDD.Ref(initialCuddNode);
                        var condEffect = cEffectContext.GetCondEffectList(initialCuddNode, predicateDict, assignment);
                        result.AddRange(condEffect);
                    }
                }
            }

            return result;
        }
    }
}
