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

        #endregion

        #region Constructors

        public Response(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict, string[] constArray, StringDictionary assignment)
            : base(constArray)
        {
            Name = context.responseSymbol().GetText();
            Console.WriteLine(FullName);
            _eventCollectionArray = context.eventModel().ToEventCollectionArray(eventDict, assignment);
        }

        public Response(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
            : base(Globals.EmptyConstArray)
        {
            Name = context.responseSymbol().GetText();
            Console.WriteLine(FullName);
            _eventCollectionArray = context.eventModel().ToEventCollectionArray(eventDict, assignment);
        }

        #endregion

        //#region Methods

        //public void GenerateEventList(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        //{
        //    Console.WriteLine(FullName);
        //    var eventModelContext = context.eventModel();
        //    if (eventModelContext.LB() == null)
        //    {
        //        _eventCollectionArray = new EventCollection[1];
        //        _eventCollectionArray[0] = eventModelContext.gdEvent().ToEventCollection(eventDict, assignment);
        //        Console.WriteLine("Finishing event collection 0 precondition");
        //        Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[0].Precondition));
        //        Console.WriteLine("Finishing event collection 0 partial successor state axiom");
        //        Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[0].PartialSuccessorStateAxiom));
        //    }
        //    else
        //    {
        //        _eventCollectionArray = new EventCollection[2];
        //        _eventCollectionArray[0] = eventModelContext.plGdEvent(0)
        //            .gdEvent()
        //            .ToEventCollection(eventDict, assignment);

        //        Console.WriteLine("Finishing event collection 0 precondition");
        //        Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[0].Precondition));
        //        Console.WriteLine("Finishing event collection 0 partial successor state axiom");
        //        Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[0].PartialSuccessorStateAxiom));

        //        _eventCollectionArray[1] = eventModelContext.plGdEvent(1)
        //            .gdEvent()
        //            .ToEventCollection(eventDict, assignment);

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
