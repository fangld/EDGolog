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

        public Observation(PlanningParser.ObservationDefineContext context,IReadOnlyDictionary<string, Predicate> predicateDict, IReadOnlyDictionary<string, Event> eventDict,
            string[] constArray, StringDictionary assignment)
            : base(constArray)
        {
            Name = context.observationSymbol().GetText();
            Precondition = context.emptyOrPreGD().ToPrecondition(predicateDict, assignment);
            //foreach (DictionaryEntry pair in assignment)
            //{
            //    Console.WriteLine("key: {0}, value: {1}",pair.Key, pair.Value);
            //}
            //Console.WriteLine("  Context: {0}", context.emptyOrPreGD().GetText());
            //Console.ReadLine();
            if (!Precondition.Equals(CUDD.ZERO))
            {
                //Console.ReadLine();

                //Console.WriteLine(FullName);
                _eventCollectionArray = context.eventModel().ToEventCollectionArray(eventDict, assignment);
            }
        }

        #endregion

        //#region Methods

        //private void HandleEventModel(PlanningParser.EventModelContext context,
        //    IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        //{
        //    Console.WriteLine(FullName);
        //    if (context.LB() == null)
        //    {
        //        _eventCollectionArray = new EventCollection[1];
        //        _eventCollectionArray[0] = context.gdEvent().ToEventCollection(eventDict, assignment);
        //        Console.WriteLine("Finishing event collection 0 precondition");
        //        Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[0].Precondition));
        //        Console.WriteLine("Finishing event collection 0 partial successor state axiom");
        //        Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[0].PartialSuccessorStateAxiom));
        //    }
        //    else
        //    {
        //        _eventCollectionArray = new EventCollection[2];
        //        _eventCollectionArray[0] = context.plGdEvent(0).gdEvent().ToEventCollection(eventDict, assignment);

        //        Console.WriteLine("Finishing event collection 0 precondition");
        //        Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[0].Precondition));
        //        Console.WriteLine("Finishing event collection 0 partial successor state axiom");
        //        Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[0].PartialSuccessorStateAxiom));

        //        _eventCollectionArray[1] = context.plGdEvent(1).gdEvent().ToEventCollection(eventDict, assignment);

        //        Console.WriteLine("Finishing event collection 1 precondition");
        //        Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[1].Precondition));
        //        Console.WriteLine("Finishing event collection 1 partial successor state axiom");
        //        Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[1].PartialSuccessorStateAxiom));
        //    }
        //    Console.WriteLine();
        //}

        //#endregion
    }
}
