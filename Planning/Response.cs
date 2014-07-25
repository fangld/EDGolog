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
        #region Fields

        private EventCollection[] _eventCollectionArray;

        private List<Event> _eventList;

        #endregion

        #region Properties

        public int MaxPlausibilityDegree
        {
            get { return _eventCollectionArray.Length; }
        }

        public IReadOnlyList<EventCollection> EventCollectionList
        {
            get { return _eventCollectionArray; }
        }

        public IReadOnlyList<Event> EventList { get { return _eventList; } }

        public CUDDNode EventPrecondition { get; private set; }

        #endregion

        #region Constructors

        public Response(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict,
            StringDictionary assignment, string[] constArray) : base(constArray)
        {
            Name = context.responseSymbol().GetText();
            _eventCollectionArray = context.eventModel().ToEventCollectionArray(eventDict, assignment);
            _eventList = _eventCollectionArray.GetEventList();
        }

        public Response(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict,
            StringDictionary assignment) : this(context, eventDict, assignment, Globals.EmptyConstArray)
        {
        }

        #endregion
    }
}
