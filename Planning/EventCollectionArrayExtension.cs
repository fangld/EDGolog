//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PAT.Common.Classes.CUDDLib;

//namespace Planning
//{
//    public static class EventCollectionArrayExtension
//    {
//        public static List<Event> GetEventList(this EventCollection[] eventCollectionArray)
//        {
//            List<Event>  result = new List<Event>();
//            for (int i = 0; i < eventCollectionArray.Length; i++)
//            {
//                result.AddRange(eventCollectionArray[i].EventList);
//            }
//            return result;
//        }

//        /// <summary>
//        /// [ REFS: 'result', DEREFS: none ]
//        /// </summary>
//        /// <returns></returns>
//        public static CUDDNode GetPrecondition(this IReadOnlyList<EventCollection> eventCollectionList)
//        {
//            //OK
//            CUDDNode result = eventCollectionList[0].Precondition;
//            CUDD.Ref(result);

//            if (eventCollectionList.Count == 2)
//            {
//                CUDDNode eventCollection2Precondition = eventCollectionList[1].Precondition;
//                CUDD.Ref(eventCollection2Precondition);
//                result = CUDD.Function.Or(result, eventCollection2Precondition);
//            }
//            return result;
//        }
//    }
//}
