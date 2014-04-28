using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Servers
{
    public class ServerAbstractPredicate : AbstractPredicate, IEquatable<ServerAbstractPredicate>
    {
        #region Properties
        
        public int CuddIndex { get; set; }

        #endregion

        #region Overriden Methods

        public bool Equals(ServerAbstractPredicate other)
        {
            if (Predicate.Name == other.Predicate.Name)
            {
                if (ParameterList.Count == other.ParameterList.Count)
                {
                    for (int i = 0; i < ParameterList.Count; i++)
                    {
                        if (ParameterList[i] != other.ParameterList[i])
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

        #endregion
    }
}
