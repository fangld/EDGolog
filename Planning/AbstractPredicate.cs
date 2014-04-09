﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class AbstractPredicate : Predicate, IEquatable<AbstractPredicate>
    {
        #region Fields

        private List<string> _variableNameList;

        #endregion

        #region Properties

        public int CuddIndex { get; set; }

        public IReadOnlyList<string> VariableNameList
        {
            get { return _variableNameList; }
        }

        #endregion

        #region Constructors

        public AbstractPredicate(List<string> variableNameList)
        {
            _variableNameList = variableNameList;
        }

        #endregion

        #region Methods

        public bool Equals(AbstractPredicate other)
        {
            if (Name == other.Name)
            {
                if (_variableNameList.Count == other.VariableNameList.Count)
                {
                    for (int i = 0; i < _variableNameList.Count; i++)
                    {
                        if (_variableNameList[i] != other.VariableNameList[i])
                        {
                            return false;
                        }
                    }

                    for (int i = 0; i < VariableTypeList.Count; i++)
                    {
                        if (VariableTypeList[i] != other.VariableTypeList[i])
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
            StringBuilder sb = new StringBuilder();
            if (_variableNameList.Count != 0)
            {
                sb.AppendFormat("{0}(", Name);

                for (int i = 0; i < _variableNameList.Count - 1; i++)
                {
                    sb.AppendFormat("{0},", _variableNameList[i]);
                }

                sb.AppendFormat("{0})", _variableNameList[_variableNameList.Count - 1]);
            }
            else
            {
                sb.AppendFormat("{0}()", Name);
            }

            return sb.ToString();
        }

        #endregion
    }
}