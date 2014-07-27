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
        public static EventModel GetEventModel(this PlanningParser.EventModelContext context,
            IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        {
            EventModel result = new EventModel(context, eventDict, assignment);
            return result;
        }
    }
}
