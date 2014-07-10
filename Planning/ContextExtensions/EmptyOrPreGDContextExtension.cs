using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.ContextExtensions
{
    public static class EmptyOrPreGDContextExtension
    {
        public static CUDDNode ToPrecondition(this PlanningParser.EmptyOrPreGDContext context, IReadOnlyDictionary<string, Predicate> predicateDict, Dictionary<string, string> assignment)
        {
            CUDDNode result = CUDD.ONE;
            CUDD.Ref(result);

            if (context != null)
            {
                PlanningParser.GdContext gdContext = context.gd();
                if (gdContext != null)
                {
                    CUDD.Deref(result);
                    result = gdContext.GetCuddNode(predicateDict, assignment);
                        //GetCuddNode(gdContext, predicateDict, assignment);
                }
            }

            return result;
        }
    }
}
