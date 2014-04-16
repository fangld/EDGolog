using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
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

        #endregion
    }
}
