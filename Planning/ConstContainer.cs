using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public abstract class ConstContainer : IEquatable<ConstContainer>
    {
        #region Fields

        /// <summary>
        /// Store the array of variables
        /// </summary>
        private string[] _constArray;

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
            get { return _constArray.Length; }
        }

        /// <summary>
        /// Return the list of variables
        /// </summary>
        public IReadOnlyCollection<string> ConstArray
        {
            get { return _constArray; }
        }

        #endregion

        #region Constructors

        protected ConstContainer(string[] constList)
        {
            int count = constList.Length;
            _constArray = new string[count];
            Array.Copy(constList, _constArray, count);
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            string result = GetFullName(Name, _constArray);
            return result;
        }

        public static string GetFullName(string name, string[] constArray)
        {
            StringBuilder sb = new StringBuilder();

            if (constArray.Length != 0)
            {
                sb.AppendFormat("{0}(", name);

                for (int i = 0; i < constArray.Length - 1; i++)
                {
                    sb.AppendFormat("{0},", constArray[i]);
                }

                sb.AppendFormat("{0})", constArray[constArray.Length - 1]);
            }
            else
            {
                sb.AppendFormat("{0}()", name);
            }

            return sb.ToString();
        }

        public static string GetFullName(PlanningParser.TermAtomFormContext context, StringDictionary assignment)
        {
            string name = context.predicate().GetText();
            var termContextList = context.term();
            return GetFullName(name, termContextList, assignment);
        }

        public static string GetFullName(PlanningParser.ConstTermAtomFormContext context)
        {
            string name = context.predicate().GetText();
            var constTermContext = context.constTerm();
            int count = constTermContext.Count;
            string[] termArray = new string[count];
            Parallel.For(0, count, i => termArray[i] = constTermContext[i].GetText());
            return GetFullName(name, termArray);
        }

        public static string GetFullName(PlanningParser.TermEventFormContext context, StringDictionary assignment)
        {
            string name = context.eventSymbol().GetText();
            var termContextList = context.term();
            return GetFullName(name, termContextList, assignment);
        }

        public static string GetFullName(PlanningParser.ActionSymbolContext context,
            IReadOnlyList<PlanningParser.TermContext> termList)
        {
            string name = context.GetText();
            int count = termList.Count;
            string[] termArray = new string[count];
            Parallel.For(0, count, i => termArray[i] = termList[i].GetText());
            return GetFullName(name, termArray);
        }

        public static string GetFullName(string name, IReadOnlyList<PlanningParser.TermContext> termContextList, StringDictionary assignment)
        {
            int count = termContextList.Count;
            string[] constArray = new string[count];
            Parallel.For(0, count, i => constArray[i] = Globals.TermInterpreter.GetString(termContextList[i], assignment));
            return GetFullName(name, constArray);
        }

        #endregion

        public bool Equals(ConstContainer other)
        {
            return FullName == other.FullName;
        }
    }
}
