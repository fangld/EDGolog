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
            EventModel = context.eventModel().GetEventModel(eventDict, assignment);

            foreach (var e in EventModel.KnowEventList)
            {
                if (FullName == "noinfo(a2,a1)")
                {
                        Console.WriteLine(e.FullName);
                }
                e.AddObservation(this);
            }

            if (FullName == "noinfo(a2,a1)")
            {
                foreach (var predicate in EventModel.KnowAffectedPredSet)
                {
                    Console.WriteLine(predicate.FullName);
                }
            }
        }

        #endregion
    }
}
