using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class GroundPredicate : Ground<Predicate>
    {
        #region Fields

        private int[] _cuddIndexList; 

        #endregion

        #region Properties

        public IReadOnlyList<int> CuddIndexList
        {
            get { return _cuddIndexList; }
        }

        #endregion

        #region Constructors

        public GroundPredicate(int cuddIndexNumber, Predicate container, IEnumerable<string> constantList)
        {
            _cuddIndexList = new int[cuddIndexNumber];
            Container = container;
            SetConstantList(constantList);
        }

        #endregion

        #region Methods

        //public void From(Predicate container, IEnumerable<string> constantList)
        //{
        //    Container = container;
        //    SetConstantList(constantList);
        //}

        public void SetCuddIndex(int listIndex, int cuddIndex)
        {
            _cuddIndexList[listIndex] = cuddIndex;
        }

        #endregion
    }
}
