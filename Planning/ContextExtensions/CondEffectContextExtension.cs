using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning.ContextExtensions
{
    public static class CondEffectContextExtension
    {
        public static Tuple<Predicate, bool>[] GetLiteralArray(this PlanningParser.CondEffectContext context,
    IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
        {
            int count = context.termLiteral().Count;
            Tuple<Predicate, bool>[] result = new Tuple<Predicate, bool>[count];
            Parallel.For(0, count, i => result[i] = context.termLiteral()[i].GetLiteral(predicateDict, assignment));
            return result;
        }
    }
}
