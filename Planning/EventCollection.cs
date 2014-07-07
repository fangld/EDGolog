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

        private HashSet<Predicate> _effectPredSet;

        #endregion

        #region Properties

        public CUDDNode Precondition;

        public CUDDNode PartialSuccessorStateAxiom;

        #endregion

        #region Constructors

        public EventCollection(List<Event> eventList)
        {
            _eventList = eventList;
            Console.WriteLine(eventList.Count);
            GeneratePrecondition();
            GeneratePartialSuccessorStateAxiom();
        }

        #endregion

        #region Methods

        private void GeneratePrecondition()
        {
            Precondition = CUDD.ZERO;
            CUDD.Ref(Precondition);

            foreach (var e in _eventList)
            {
                CUDDNode eventPreNode = e.Precondition;
                CUDD.Ref(eventPreNode);
                CUDDNode orNode = CUDD.Function.Or(Precondition, eventPreNode);
                CUDD.Ref(orNode);
                CUDD.Deref(Precondition);
                Precondition = orNode;
            }
        }

        private void GeneratePartialSuccessorStateAxiom()
        {
            _effectPredSet = new HashSet<Predicate>();
            PartialSuccessorStateAxiom = CUDD.ONE;
            CUDD.Ref(PartialSuccessorStateAxiom);

            foreach (var e in _eventList)
            {
                _effectPredSet.UnionWith(e.EffectPredSet);
            }

            foreach (var e in _eventList)
            {
                CUDDNode eventPssa = e.ParitalSuccessorStateAxiom;
                CUDD.Ref(eventPssa);

                foreach (var pred in _effectPredSet)
                {
                    if (!e.EffectPredSet.Contains(pred))
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

                CUDDNode andNode = CUDD.Function.And(PartialSuccessorStateAxiom, eventPssa);
                CUDD.Ref(andNode);
                CUDD.Deref(PartialSuccessorStateAxiom);
                PartialSuccessorStateAxiom = andNode;
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
