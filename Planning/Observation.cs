using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class Observation : ConstContainer
    {
        #region Fields

        private EventCollection[] _eventCollectionArray;

        #endregion

        #region Properties

        public CUDDNode Precondition;

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

        public Observation(PlanningParser.ObservationDefineContext context, IReadOnlyDictionary<string, Event> eventDict, string[] constArray, Dictionary<string, string> assignment)
            : base(constArray)
        {
            Name = context.observationSymbol().GetText();
            Console.WriteLine(FullName);
            HandleEventModel(context.eventModel(), eventDict, assignment);
        }

        #endregion

        #region Methods

        private void HandleEventModel(PlanningParser.EventModelContext context,
            IReadOnlyDictionary<string, Event> eventDict, Dictionary<string, string> assignment)
        {

        }

        #endregion
    }
}
