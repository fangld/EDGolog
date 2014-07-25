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
    public static class EmptyOrPreGDContextExtension
    {
        public static CUDDNode ToPrecondition(this PlanningParser.EmptyOrPreGDContext context, IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
        {
            CUDDNode result;

            if (context != null)
            {
                PlanningParser.GdContext gdContext = context.gd();
                if (gdContext != null)
                {
                    result = gdContext.GetCuddNode(predicateDict, assignment);
                }
                else
                {
                    result = CUDD.ONE;
                    CUDD.Ref(result);
                }
            }
            else
            {
                result = CUDD.ONE;
                CUDD.Ref(result);
            }

            return result;
        }
    }
}
