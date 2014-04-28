using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public abstract class Domain<TA, TAP> where TA: Action<TAP> where TAP: AbstractPredicate, new()
    {
        #region Fields

        private List<string> _typeList;

        private Dictionary<string, Predicate> _predDict;

        private Dictionary<string, TA> _actionDict;

        internal const string BarLine = "----------------";

        #endregion

        #region Properties

        public string Name { get; set; }

        public int CurrentCuddIndex { get; set; }

        public IReadOnlyList<string> TypeList
        {
            get { return _typeList; }
        }

        public IReadOnlyDictionary<string, Predicate> PredicateDict
        {
            get { return _predDict; }
        }

        public IReadOnlyDictionary<string, TA> ActionDict
        {
            get { return _actionDict; }
        }

        #endregion

        #region Constructors

        public Domain()
        {
            _typeList = new List<string> { VariableContainer.DefaultType };
            _predDict = new Dictionary<string, Predicate>();
            _actionDict = new Dictionary<string, TA>();
            CurrentCuddIndex = 0;
        }

        #endregion

        #region Methods

        public void AddToTypeList(string type)
        {
            _typeList.Add(type);
        }

        public void AddToPredicateDict(Predicate predicate)
        {
            _predDict.Add(predicate.Name, predicate);
        }

        public void AddToActionDict(TA action)
        {
            _actionDict.Add(action.Name, action);
            CurrentCuddIndex = action.CurrentCuddIndex;
        }

        public abstract void ShowInfo();

        #endregion
    }
}
