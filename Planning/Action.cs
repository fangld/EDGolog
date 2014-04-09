using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class Action : Predicate
    {
        #region Fields

        private List<Predicate> _usedPreviousPredicates;

        private List<Predicate> _usedSuccessorPredicates;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; set; }

        public CUDDNode Effect { get; set; }

        public IReadOnlyList<Predicate> UsedPreviousPredicates
        {
            get { return _usedPreviousPredicates; }
        }

        public IReadOnlyList<Predicate> UsedSuccessorPredicates
        {
            get { return _usedSuccessorPredicates; }
        }

        #endregion

        #region Constructors

        public Action()
        {
            _usedPreviousPredicates = new List<Predicate>();
            _usedSuccessorPredicates = new List<Predicate>();
        }

        #endregion

        #region Methods

        public void AddPredicateToUsedPreviousPredicates(Predicate predicate)
        {
            _usedPreviousPredicates.Add(predicate);
        }

        public void AddPredicateToUsedSuccessorPredicates(Predicate predicate)
        {
            _usedSuccessorPredicates.Add(predicate);
        }

        #endregion
    }
}
