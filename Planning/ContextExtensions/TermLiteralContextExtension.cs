using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning.ContextExtensions
{
    public static class TermLiteralContextExtension
    {
        public static Tuple<Predicate, bool> GetLiteral(this PlanningParser.TermLiteralContext context,
    IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
        {
            string fullName = ConstContainer.GetFullName(context.termAtomForm(), assignment);
            Predicate predicate = predicateDict[fullName];
            bool isPositive = context.NOT() == null;
            return new Tuple<Predicate, bool>(predicate, isPositive);
        }
    }
}
