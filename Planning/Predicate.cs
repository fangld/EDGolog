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

        private List<string> _varaiblesType;

        #endregion

        #region Properties

        public string Name { get; set; }

        public int Count
        {
            get { return _varaiblesType.Count; }
        }

        public IReadOnlyList<string> ListVariablesType
        {
            get { return _varaiblesType; }
        }

        #endregion

        #region Constructors

        public Predicate()
        {
            _varaiblesType = new List<string>();
        }

        #endregion

        #region Methods

        public void AddVariableTypes(string type, int number)
        {
            string[] types = new string[number];
            for (int i = 0; i < number; i++)
            {
                types[i] = type;
            }

            _varaiblesType.AddRange(types);
        }

        #endregion
    }
}