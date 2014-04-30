using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class AbstractPredicate : IEquatable<AbstractPredicate>
    {
        #region Fields

        private List<string> _parameterList;

        private int[] _cuddIndexList; 

        #endregion

        #region Properties

        public Predicate Predicate { get; set; }

        public IReadOnlyList<string> ParameterList
        {
            get { return _parameterList; }
        }

        public IReadOnlyList<int> CuddIndexList
        {
            get { return _cuddIndexList; }
        }

        #endregion

        #region Constructors

        public AbstractPredicate(int cuddIndexNumber, Predicate pred, List<string> parameterList)
        {
            _cuddIndexList = new int[cuddIndexNumber];
            Predicate = pred;
            _parameterList = parameterList;
        }

        #endregion

        #region Overriden Methods

        public void SetCuddIndex(int listIndex, int cuddIndex)
        {
            _cuddIndexList[listIndex] = cuddIndex;
        }

        //public void From(int cuddIndexNumber, List<string> parameterList)
        //{
        //    _cuddIndexs = new int[cuddIndexNumber];
        //    _parameterList = parameterList;
        //}

        //public void SetParameterList(List<string> parameterList)
        //{
        //    _parameterList = parameterList;
        //}

        public bool Equals(AbstractPredicate other)
        {
            if (Predicate.Name == other.Predicate.Name)
            {
                if (_parameterList.Count == other.ParameterList.Count)
                {
                    for (int i = 0; i < _parameterList.Count; i++)
                    {
                        if (_parameterList[i] != other.ParameterList[i])
                        {
                            return false;
                        }
                    }

                    for (int i = 0; i < Predicate.VariableList.Count; i++)
                    {
                        if (Predicate.VariableList[i] != other.Predicate.VariableList[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return VariableContainer.GetFullName(Predicate.Name, _parameterList);
        }

        #endregion
    }
}
