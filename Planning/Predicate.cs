using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class Predicate
    {
        #region Fields

        private List<Tuple<string, string>> _variableTypeList;

        #endregion

        #region Properties

        public string Name { get; set; }

        public int Count
        {
            get { return _variableTypeList.Count; }
        }

        public IReadOnlyList<Tuple<string, string>> VariableTypeList
        {
            get { return _variableTypeList; }
        }

        #endregion

        #region Constructors

        public Predicate()
        {
            _variableTypeList = new List<Tuple<string, string>>();
        }

        #endregion

        #region Methods

        public void AddVariable(string name, string type)
        {
            Tuple<string, string> tuple = new Tuple<string, string>(name, type);
            _variableTypeList.Add(tuple);
        }

        #endregion
    }
}