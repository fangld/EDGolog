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
    public class EventModel
    {
        #region Fields
        
        private Event[] _believeEventArray;

        private Event[] _knowEventArray;

        private HashSet<Predicate> _believeAffectedPredset;

        private HashSet<Predicate> _knowAffectedPredset;

        #endregion

        #region Properties

        public CUDDNode BelievePrecondition { get; private set; }

        public CUDDNode KnowPrecondition { get; private set; }

        public CUDDNode BelievePartialSsa { get; private set; }

        public CUDDNode KnowPartialSsa { get; private set; }

        public HashSet<Predicate> BelieveAffectedPredset
        {
            get { return _believeAffectedPredset; }
        }

        public HashSet<Predicate> KnowAffectedPredset
        {
            get { return _knowAffectedPredset; }
        }

        public IReadOnlyList<Event> BelieveEventList
        {
            get { return _believeEventArray; }
        }

        public IReadOnlyList<Event> KnowEventList
        {
            get { return _knowEventArray; }
        }

        public int MaxPlausibilityDegree { get; private set; }

        #endregion

        #region Constructors

        public EventModel(PlanningParser.EventModelContext context, IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        {
            MaxPlausibilityDegree = context.LB() == null ? 0 : 1;

            GenerateEventArray(context, eventDict, assignment);
            GenerateBelievePrecondition();
            GenerateBelievePssa();
            GenerateKnowPrecondition();
            GenerateKnowPssa();
        }

        #endregion

        #region Methods

        private void GenerateEventArray(PlanningParser.EventModelContext context, IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        {
            if (MaxPlausibilityDegree == 0)
            {
                _believeEventArray = context.gdEvent().ToEventCollection(eventDict, assignment);
                _knowEventArray = _believeEventArray;
            }
            else
            {
                _believeEventArray = context.plGdEvent(0).gdEvent().ToEventCollection(eventDict, assignment);
                var eventArray1 = context.plGdEvent(1).gdEvent().ToEventCollection(eventDict, assignment);
                int eventArray0Length = _believeEventArray.Length;
                int eventArray1Length = eventArray1.Length;

                _knowEventArray = new Event[eventArray0Length + eventArray1Length];
                Array.Copy(_believeEventArray, _knowEventArray, eventArray0Length);
                Array.Copy(eventArray1, 0, _knowEventArray, eventArray0Length, eventArray1Length);
            }
        }

        private void GenerateBelievePrecondition()
        {
            //OK
            BelievePrecondition = _believeEventArray[0].Precondition;
            CUDD.Ref(BelievePrecondition);

            for (int i = 1; i < _believeEventArray.Length; i++)
            {
                CUDDNode eventPreNode = _believeEventArray[i].Precondition;
                CUDD.Ref(eventPreNode);
                BelievePrecondition = CUDD.Function.Or(BelievePrecondition, eventPreNode);
            }
        }

        private void GenerateKnowPrecondition()
        {
            KnowPrecondition = BelievePrecondition;

            if (MaxPlausibilityDegree == 1)
            {
                CUDD.Ref(KnowPrecondition);

                for (int i = 0; i < _knowEventArray.Length; i++)
                {
                    CUDDNode eventPreNode = _knowEventArray[i].Precondition;
                    CUDD.Ref(eventPreNode);
                    KnowPrecondition = CUDD.Function.Or(KnowPrecondition, eventPreNode);
                }
            }
        }

        private void GenerateBelievePssa()
        {
            _believeAffectedPredset = new HashSet<Predicate>();
            BelievePartialSsa = CUDD.ZERO;
            CUDD.Ref(BelievePartialSsa);

            foreach (var e in _believeEventArray)
            {
                _believeAffectedPredset.UnionWith(e.AffectedPredicateSet);
            }

            foreach (var e in _believeEventArray)
            {
                CUDDNode eventPssa = e.PartialSsa;
                CUDD.Ref(eventPssa);
                CUDD.Ref(e.Precondition);
                eventPssa = CUDD.Function.And(eventPssa, e.Precondition);

                foreach (var pred in _believeAffectedPredset)
                {
                    if (!e.AffectedPredicateSet.Contains(pred))
                    {
                        CUDDNode prePredNode = CUDD.Var(pred.PreviousCuddIndex);
                        CUDDNode sucPredNode = CUDD.Var(pred.SuccessiveCuddIndex);
                        CUDDNode invariantNode = CUDD.Function.Equal(prePredNode, sucPredNode);
                        eventPssa = CUDD.Function.And(eventPssa, invariantNode);
                    }
                }

                BelievePartialSsa = CUDD.Function.Or(BelievePartialSsa, eventPssa);
            }
        }

        private void GenerateKnowPssa()
        {
            if (MaxPlausibilityDegree == 0)
            {
                KnowPartialSsa = BelievePartialSsa;
            }
            else
            {
                _knowAffectedPredset = new HashSet<Predicate>();
                KnowPartialSsa = CUDD.ZERO;
                CUDD.Ref(KnowPartialSsa);

                foreach (var e in _knowEventArray)
                {
                    _knowAffectedPredset.UnionWith(e.AffectedPredicateSet);
                }

                foreach (var e in _knowEventArray)
                {
                    CUDDNode eventPssa = e.PartialSsa;
                    CUDD.Ref(eventPssa);
                    CUDD.Ref(e.Precondition);
                    eventPssa = CUDD.Function.And(eventPssa, e.Precondition);

                    foreach (var pred in _knowAffectedPredset)
                    {
                        if (!e.AffectedPredicateSet.Contains(pred))
                        {
                            CUDDNode prePredNode = CUDD.Var(pred.PreviousCuddIndex);
                            CUDDNode sucPredNode = CUDD.Var(pred.SuccessiveCuddIndex);
                            CUDDNode invariantNode = CUDD.Function.Equal(prePredNode, sucPredNode);
                            eventPssa = CUDD.Function.And(eventPssa, invariantNode);
                        }
                    }

                    KnowPartialSsa = CUDD.Function.Or(BelievePartialSsa, eventPssa);
                }
            }
        }

        #endregion

    }
}
