using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Clients
{
    public class AbstractPredicate : IEquatable<AbstractPredicate>
    {
        #region Fields

        private List<string> _parameterList;

        #endregion

        #region Properties

        public Predicate Predicate { get; set; }

        public int PreviousCuddIndex { get; set; }

        public int SuccessorCuddIndex { get; set; }

        public IReadOnlyList<string> ParameterList
        {
            get { return _parameterList; }
        }

        #endregion

        #region Methods

        public AbstractPredicate(List<string> parameterList)
        {
            _parameterList = parameterList;
        }

        #endregion

        #region Overriden Methods

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
