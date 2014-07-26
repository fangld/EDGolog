using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class EventCollection : IEnumerable<Event>
    {
        #region Fields

        private List<Event> _eventList;

        private HashSet<Predicate> _affectedPredicateSet;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; private set; }

        public CUDDNode PartialSuccessorStateAxiom { get; private set; }

        public IReadOnlyList<Event> EventList
        {
            get { return _eventList; }
        }

        public HashSet<Predicate> AffectedPredicateSet
        {
            get { return _affectedPredicateSet; }
        }

        #endregion

        #region Constructors

        public EventCollection(List<Event> eventList)
        {
            _eventList = eventList;
            GeneratePrecondition();
            GeneratePartialSuccessorStateAxiom();
        }

        #endregion

        #region Methods

        private void GeneratePrecondition()
        {
            //OK
            Precondition = _eventList[0].Precondition;
            CUDD.Ref(Precondition);

            for (int i = 1; i < _eventList.Count; i++)
            {
                CUDDNode eventPreNode = _eventList[i].Precondition;
                CUDD.Ref(eventPreNode);
                Precondition = CUDD.Function.Or(Precondition, eventPreNode);
            }
        }

        private void GeneratePartialSuccessorStateAxiom()
        {
            _affectedPredicateSet = new HashSet<Predicate>();
            PartialSuccessorStateAxiom = CUDD.ZERO;
            CUDD.Ref(PartialSuccessorStateAxiom);

            foreach (var e in _eventList)
            {
                _affectedPredicateSet.UnionWith(e.AffectedPredicateSet);
            }

            foreach (var e in _eventList)
            {
                CUDDNode eventPssa = e.ParitalSuccessorStateAxiom;
                CUDD.Ref(eventPssa);

                foreach (var pred in _affectedPredicateSet)
                {
                    if (!e.AffectedPredicateSet.Contains(pred))
                    {
                        CUDDNode prePredNode = CUDD.Var(pred.PreviousCuddIndex);
                        CUDDNode sucPredNode = CUDD.Var(pred.SuccessiveCuddIndex);
                        CUDDNode invariantNode = CUDD.Function.Equal(prePredNode, sucPredNode);
                        CUDD.Ref(invariantNode);

                        CUDDNode tmpNode = CUDD.Function.And(eventPssa, invariantNode);
                        CUDD.Ref(tmpNode);
                        CUDD.Deref(eventPssa);
                        CUDD.Deref(invariantNode);
                        eventPssa = tmpNode;
                    }
                }

                PartialSuccessorStateAxiom = CUDD.Function.Or(PartialSuccessorStateAxiom, eventPssa);
                //CUDDNode andNode = CUDD.Function.And(PartialSuccessorStateAxiom, eventPssa);
                //CUDD.Ref(andNode);
                //CUDD.Deref(PartialSuccessorStateAxiom);
                //PartialSuccessorStateAxiom = andNode;
            }
        }

        #endregion

        #region Overriden Methods

        public IEnumerator<Event> GetEnumerator()
        {
            return _eventList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _eventList.GetEnumerator();
        }

        #endregion
    }
}
