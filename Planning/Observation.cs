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
        #region Properties

        public CUDDNode Precondition { get; private set; }

        public EventModel EventModel { get; private set; }

        #endregion

        #region Constructors

        public Observation(PlanningParser.ObservationDefineContext context, CUDDNode precondition,
            IReadOnlyDictionary<string, Event> eventDict, string[] constArray, StringDictionary assignment)
            : base(constArray)
        {
            Name = context.observationSymbol().GetText();
            Precondition = precondition;
            //Console.WriteLine(Name);
            EventModel = context.eventModel().GetEventModel(eventDict, assignment);

            foreach (var e in EventModel.KnowEventList)
            {
                e.AddObservation(this);
            }
        }

        #endregion
    }
}
