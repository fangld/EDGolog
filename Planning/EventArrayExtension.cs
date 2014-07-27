//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PAT.Common.Classes.CUDDLib;

//namespace Planning
//{
//    public static class EventArrayExtension
//    {
//        public static CUDDNode GetPrecondition(this Event[] eventArray)
//        {
//            //OK
//            CUDDNode result = eventArray[0].Precondition;
//            CUDD.Ref(result);

//            for (int i = 1; i < eventArray.Length; i++)
//            {
//                CUDDNode eventPreNode = eventArray[i].Precondition;
//                CUDD.Ref(eventPreNode);
//                result = CUDD.Function.Or(result, eventPreNode);
//            }

//            return result;
//        }

//        public static CUDDNode GetPartialSsa(this Event[] eventArray)
//        {
//            //OK
//            CUDDNode result = eventArray[0].Precondition;
//            CUDD.Ref(result);

//            for (int i = 1; i < eventArray.Length; i++)
//            {
//                CUDDNode eventPreNode = eventArray[i].Precondition;
//                CUDD.Ref(eventPreNode);
//                result = CUDD.Function.Or(result, eventPreNode);
//            }

//            return result;
//        }
//    }
//}
