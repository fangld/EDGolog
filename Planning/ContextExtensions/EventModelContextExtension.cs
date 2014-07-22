using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.ContextExtensions
{
    public static class EventModelContextExtension
    {
        public static EventCollection[] ToEventCollectionArray(this PlanningParser.EventModelContext context,
            IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        {
            IEnumerator<int> x;
            IEnumerable<int> y;
            
            EventCollection[] result = context.LB() == null
                ? new[] {context.gdEvent().ToEventCollection(eventDict, assignment)}
                : new[]
                {
                    context.plGdEvent(0).gdEvent().ToEventCollection(eventDict, assignment),
                    context.plGdEvent(1).gdEvent().ToEventCollection(eventDict, assignment)
                };

            //if (context.LB() == null)
            //{
            //    result = new EventCollection[1];
            //    result[0] = context.gdEvent().ToEventCollection(eventDict, assignment);
            //    //
            
            //Console.WriteLine("Finishing event collection 0 precondition");
            //    //Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(result[0].Precondition));
            //    //Console.WriteLine("Finishing event collection 0 partial successor state axiom");
            //    //Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(result[0].PartialSuccessorStateAxiom));
            //}
            //else
            //{
            //    result = new EventCollection[2];
            //    result[0] = context.plGdEvent(0).gdEvent().ToEventCollection(eventDict, assignment);

            //    //Console.WriteLine("Finishing event collection 0 precondition");
            //    //Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(result[0].Precondition));
            //    //Console.WriteLine("Finishing event collection 0 partial successor state axiom");
            //    //Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(result[0].PartialSuccessorStateAxiom));

            //    result[1] = context.plGdEvent(1).gdEvent().ToEventCollection(eventDict, assignment);

            //    //Console.WriteLine("Finishing event collection 1 precondition");
            //    //Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(result[1].Precondition));
            //    //Console.WriteLine("Finishing event collection 1 partial successor state axiom");
            //    //Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(result[1].PartialSuccessorStateAxiom));
            //}
            //Console.WriteLine();
            return result;
        }
    }
}
