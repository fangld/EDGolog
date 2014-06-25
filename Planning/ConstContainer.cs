using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public abstract class ConstContainer
    {
        #region

        /// <summary>
        /// Store the list of variables
        /// </summary>
        private string[] _constList;

        #endregion

        #region Properties

        /// <summary>
        /// The name of constant container
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The full name of constant container
        /// </summary>
        public string FullName { get { return ToString(); } }

        /// <summary>
        /// The count of variables
        /// </summary>
        public int Count
        {
            get { return _constList.Length; }
        }

        /// <summary>
        /// Return the list of variables
        /// </summary>
        public IReadOnlyList<string> ConstList
        {
            get { return _constList; }
        }

        #endregion

        #region Constructors

        protected ConstContainer(string[] constList)
        {
            int count = constList.Length;
            _constList = new string[count];
            Array.Copy(constList, _constList, count);
        }

        #endregion

        #region Methods

        //protected void CopyConstArray(string[] constList)
        //{
        //    int count = constList.Length;
        //    _constList = new string[count];
        //    Array.Copy(constList, _constList, count);
        //}

        //internal void GenerateVariableList(PlanningParser.ListVariableContext context)
        //{
        //    do
        //    {
        //        if (context.VAR().Count != 0)
        //        {
        //            string type = context.type() == null ? PlanningType.ObjectType.Name : context.type().GetText();

        //            foreach (var varNode in context.VAR())
        //            {
        //                var tuple = new Tuple<string, string>(varNode.GetText(), type);
        //                _constList.Add(tuple);
        //            }
        //        }
        //        context = context.listVariable();
        //    } while (context != null);
        //}

        public override string ToString()
        {
            string result = GetFullName(Name, _constList);
            return result;
        }

        public static string GetFullName(string name, IReadOnlyList<string> constList)
        {
            StringBuilder sb = new StringBuilder();

            if (constList.Count != 0)
            {
                sb.AppendFormat("{0}(", name);

                for (int i = 0; i < constList.Count - 1; i++)
                {
                    sb.AppendFormat("{0},", constList[i]);
                }

                sb.AppendFormat("{0})", constList[constList.Count - 1]);
            }
            else
            {
                sb.AppendFormat("{0}()", name);
            }

            return sb.ToString();
        }

        public static string GetFullName(PlanningParser.TermAtomFormContext context, Dictionary<string, string> assignment)
        {
            string name = context.pred().GetText();
            List<string> constList = new List<string>();
            foreach (var termContext in context.term())
            {
                string termString = Globals.TermHandler.GetString(termContext, assignment);
                constList.Add(termString);
            }
            return GetFullName(name, constList);
        }

        public static string GetFullName(PlanningParser.ConstTermAtomFormContext context)
        {
            string name = context.pred().GetText();
            List<string> termList = new List<string>();
            foreach (var constTermContext in context.constTerm())
            {
                termList.Add(constTermContext.GetText());
            }
            return GetFullName(name, termList);
        }

        public static string GetFullName(PlanningParser.TermEventFormContext context, Dictionary<string, string> assignment)
        {
            string name = context.eventSymbol().GetText();
            List<string> constList = new List<string>();
            foreach (var termContext in context.term())
            {
                string termString = Globals.TermHandler.GetString(termContext, assignment);
                constList.Add(termString);
            }
            return GetFullName(name, constList);
        }

        #endregion
    }
}
