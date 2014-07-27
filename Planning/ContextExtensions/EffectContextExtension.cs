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
    public static class EffectContextExtension
    {
        public static List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> GetCondEffectList(this PlanningParser.EffectContext context,
            CUDDNode currentConditionNode, IReadOnlyDictionary<string, Predicate> predicateDict,
            StringDictionary assignment)
        {
            var result = new List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>>();

            foreach (var cEffectContext in context.cEffect())
            {
                var condEffect = cEffectContext.GetCondEffectList(currentConditionNode, predicateDict, assignment);
                result.AddRange(condEffect);
            }

            return result;
        }
    }
}
