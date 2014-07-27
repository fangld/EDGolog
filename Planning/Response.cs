using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.ContextExtensions;

namespace Planning
{
    public class Response : ConstContainer
    {

        #region Properties

        public EventModel EventModel { get; private set; }

        #endregion

        #region Constructors

        public Response(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict,
            StringDictionary assignment, string[] constArray) : base(constArray)
        {
            Name = context.responseSymbol().GetText();
            EventModel = context.eventModel().GetEventModel(eventDict, assignment);
        }

        public Response(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict,
            StringDictionary assignment) : this(context, eventDict, assignment, Globals.EmptyConstArray)
        {
        }

        #endregion
    }
}
