using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class Ground<T> where T : VariableContainer
    {
        #region Fields

        private List<string> _constantList;

        #endregion

        #region Properties

        public T Container { get; set; }

        public int CuddIndex { get; set; }

        public IReadOnlyList<string> ConstantList
        {
            get { return _constantList; }
        }

        #endregion

        #region Constructors

        public Ground(T container, IEnumerable<string> constantList)
        {
            Container = container;
            _constantList = new List<string>(constantList);
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return VariableContainer.GetFullName(Container.Name, _constantList);
        }

        #endregion
    }
}
