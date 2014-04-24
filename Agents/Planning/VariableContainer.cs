using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Agents.Planning
{
    public abstract class VariableContainer
    {
        #region Fields

        /// <summary>
        /// Store the list of variables
        /// </summary>
        private List<Tuple<string, string>> _variableList;

        #endregion

        #region Properties

        /// <summary>
        /// The name of default type;
        /// </summary>
        public const string DefaultType = "object";

        /// <summary>
        /// The name of variable container
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The count of variables
        /// </summary>
        public int Count
        {
            get { return _variableList.Count; }
        }

        /// <summary>
        /// Return the list of variables
        /// </summary>
        public IReadOnlyList<Tuple<string, string>> VariableList
        {
            get { return _variableList; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public VariableContainer()
        {
            _variableList = new List<Tuple<string, string>>();
        }

        #endregion

        #region Methods

        internal void GenerateVariableList(PlanningParser.ListVariableContext context)
        {
            do
            {
                if (context.VAR().Count != 0)
                {
                    string type = context.type() == null ? DefaultType : context.type().GetText();

                    foreach (var varNode in context.VAR())
                    {
                        var tuple = new Tuple<string, string>(varNode.GetText(), type);
                        _variableList.Add(tuple);
                    }
                }
                context = context.listVariable();
            } while (context != null);
        }

        public override string ToString()
        {
            List<string> variableNameList = new List<string>();
            foreach (var tuple in _variableList)
            {
                variableNameList.Add(tuple.Item1);
            }
            string result = GetFullName(Name, variableNameList);
            return result;
        }

        public static string GetFullName(string name, IReadOnlyList<string> termList)
        {
            StringBuilder sb = new StringBuilder();

            if (termList.Count != 0)
            {
                sb.AppendFormat("{0}(", name);

                for (int i = 0; i < termList.Count - 1; i++)
                {
                    sb.AppendFormat("{0},", termList[i]);
                }

                sb.AppendFormat("{0})", termList[termList.Count - 1]);
            }
            else
            {
                sb.AppendFormat("{0}()", name);
            }

            return sb.ToString();
        }

        public static string GetFullName(PlanningParser.AtomicFormulaTermContext context)
        {
            string name = context.predicate().GetText();
            List<string> termList = new List<string>();
            foreach (var termContext in context.term())
            {
                termList.Add(termContext.GetText());
            }
            return GetFullName(name, termList);
        }

        public static string GetFullName(PlanningParser.AtomicFormulaNameContext context)
        {
            string name = context.predicate().GetText();
            List<string> constantList = new List<string>();
            foreach (var nameContext in context.NAME())
            {
                constantList.Add(nameContext.GetText());
            }
            return GetFullName(name, constantList);
        }

        public static string GetFullName(PlanningParser.ActionDefineContext context)
        {
            string name = context.actionSymbol().GetText();
            List<string> termList = new List<string>();
            PlanningParser.ListVariableContext listVariableContext = context.listVariable();
            do
            {
                if (listVariableContext.VAR().Count != 0)
                {
                    foreach (var varNode in listVariableContext.VAR())
                    {
                        termList.Add(varNode.GetText());
                    }
                }
                listVariableContext = listVariableContext.listVariable();
            } while (listVariableContext != null);
            return GetFullName(name, termList);
        }

        #endregion
    }
}
