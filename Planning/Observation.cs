using System;
using System.Collections;
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
    public class Observation : ConstContainer
    {
        #region Fields

        private EventCollection[] _eventCollectionArray;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; private set; }

        public int MaxPlausibilityDegree
        {
            get { return _eventCollectionArray.Length; }
        }

        public IReadOnlyList<EventCollection> EventCollectionList
        {
            get { return _eventCollectionArray; }
        }

        #endregion

        #region Constructors

        public Observation(PlanningParser.ObservationDefineContext context, CUDDNode precondition,
            IReadOnlyDictionary<string, Event> eventDict, string[] constArray, StringDictionary assignment)
            : base(constArray)
        {
            Name = context.observationSymbol().GetText();
            Precondition = precondition;
            _eventCollectionArray = context.eventModel().ToEventCollectionArray(eventDict, assignment);
        }

        #endregion
    }
}
