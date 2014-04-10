using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class Ground<T> where T : Predicate
    {
        #region Fields

        private List<string> _paramList;

        #endregion

        #region Properties

        public T VariableContainer { get; set; }

        public IReadOnlyList<string> ParamList
        {
            get { return _paramList; }
        }

        #endregion

        #region Constructors

        public Ground(T variableContainer, ICollection<string> paramList)
        {
            VariableContainer = variableContainer;
            _paramList = new List<string>(paramList);
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (_paramList.Count != 0)
            {
                sb.AppendFormat("{0}(", VariableContainer.Name);

                for (int i = 0; i < _paramList.Count - 1; i++)
                {
                    sb.AppendFormat("{0},", _paramList[i]);
                }

                sb.AppendFormat("{0})", _paramList[_paramList.Count - 1]);
            }
            else
            {
                sb.AppendFormat("{0}()", VariableContainer.Name);
            }

            return sb.ToString();
        }

        #endregion
    }
}
