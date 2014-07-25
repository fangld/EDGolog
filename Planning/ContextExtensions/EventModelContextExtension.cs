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
    public static class EventModelContextExtension
    {
        public static EventCollection[] ToEventCollectionArray(this PlanningParser.EventModelContext context,
            IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        {
            EventCollection[] result = context.LB() == null
                ? new[] {context.gdEvent().ToEventCollection(eventDict, assignment)}
                : new[]
                {
                    context.plGdEvent(0).gdEvent().ToEventCollection(eventDict, assignment),
                    context.plGdEvent(1).gdEvent().ToEventCollection(eventDict, assignment)
                };
            return result;
        }
    }
}
