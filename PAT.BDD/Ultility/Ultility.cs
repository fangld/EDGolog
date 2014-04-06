using System.Collections.Generic;
using PAT.Common.Classes.Expressions.ExpressionClass;

namespace PAT.Common.Classes.Ultility
{
    public class Ultility
    {
        #region Set operations

        /// <summary>
        /// return the union of the two lists: add the new elements into list1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static List<T> Union<T>(List<T> list1, List<T> list2)
        {
            for (int i = 0; i < list2.Count; i++)
            {
                T item = list2[i];
                if (!list1.Contains(item))
                {
                    list1.Add(item);
                }
            }
            return list1;
        }


        /// <summary>
        /// return the union of the two lists: add the new elements into list1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static List<T> Substract<T>(List<T> list1, List<T> list2)
        {
            List<T> returnList = new List<T>();

            for (int i = 0; i < list1.Count; i++)
            {
                T item = list1[i];
                if(list2.Contains(item))
                {
                    returnList.Add(item);
                }
            }
            return returnList;
        }



        public static List<T> Intersect<T>(List<T> list1, List<T> list2)
        {
            List<T> returnList = new List<T>(list1.Count);
            for (int i = 0; i < list2.Count; i++)
            {
                T item = list2[i];
                if (list1.Contains(item))
                {
                    returnList.Add(item);
                }
            }
            return returnList;
        }



        /// <summary>
        /// add list2 into list1 with no duplication
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        public static void AddList<T>(List<T> list1, List<T> list2)
        {
            foreach (T item in list2)
            {
                if (!list1.Contains(item))
                {
                    list1.Add(item);
                }
            }
        }






        //without modifying the original lists
        public static List<T> AddList2<T>(List<T> list1, List<T> list2)
        {
            List<T> result = new List<T>();

            result.AddRange(list1);

            for (int i = 0; i < list2.Count; i++)
            {
                T item = list2[i];
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static string PPStringList<T>(List<T> list)
        {
            if (list == null)
            {
                return "";
            }

            string s = "";

            for (int i = 0; i < list.Count; i++)
            {
                T item = list[i];
                s += "," + item;
            }
            return s.TrimStart(',');
        }

        public static string PPStringList<T>(List<T> list, string separator)
        {
            if (list == null)
            {
                return "";
            }

            string s = "";

            for (int i = 0; i < list.Count; i++)
            {
                T item = list[i];
                s += separator + item.ToString();
            }
            return s.Substring(separator.Length);
        }



        public static string PPStringListDot<T>(List<T> list)
        {
            if (list == null)
            {
                return "";
            }

            string s = "";
            foreach (T item in list)
            {
                s += "." + item;
            }
            return s;
        }

        public static string PPStringList<T>(T[] list)
        {
            if (list == null)
            {
                return "";
            }

            string s = "";
            for (int i = 0; i < list.Length; i++)
            {
                T item = list[i];
                if (item != null)
                {
                    s += "," + item;
                }
            }
            return s.TrimStart(',');
        }

        public static string PPStringListDot<T>(T[] list)
        {
            if (list == null)
            {
                return "";
            }

            string s = "";
            foreach (T item in list)
            {
                s += "." + item;
            }
            return s;
        }

        public static string PPIDListDot(Expression[] list)
        {
            if (list == null)
            {
                return "";
            }

            string s = "";
            foreach (Expression item in list)
            {
                s += "." + item.ExpressionID; //.GetID();
            }
            return s;
        }


        public static string ListIntsToString(List<int> listOfInts)
        {
            string result = "";
            foreach (int a in listOfInts)
            {
                result += a.ToString() + ",";
            }
            return result.TrimEnd(',');
        }

        #endregion
    }
}
