using System;
using System.Collections.Generic;
using System.Linq;

namespace PAT.Common.Classes.CUDDLib
{
    /// <summary>
    /// Manage variables and their lower-bound and upper-bound value
    /// </summary>
    public class VariableList
    {
        private Dictionary<string, List<int>> vars;

        public VariableList()
        {
            vars = new Dictionary<string, List<int>>();
        }

        /// <summary>
        /// Get lower-bound of a variable
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetVarLow(string name)
        {
            return vars[name][0];
        }

        /// <summary>
        /// Get upper_bound value of a variable
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetVarHigh(string name)
        {
            return vars[name][1];
        }

        /// <summary>
        /// Return variable's name based its index
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetVarName(int i)
        {
            return vars.Keys.ElementAt(i);
        }

        /// <summary>
        /// Return number of bits used to represent this variable
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetNumberOfBits(string name)
        {
            int range = GetVarHigh(name) - GetVarLow(name) + 1;
            return (int) Math.Ceiling(Math.Log(range, 2));
        }

        /// <summary>
        /// Return variable's index based on variable's name
        /// </summary>
        /// <param name="varName"></param>
        /// <returns></returns>
        public int GetVarIndex(string varName)
        {
            for (int i = 0; i < vars.Keys.Count; i++)
            {
                if (vars.Keys.ElementAt(i) == varName)
                {
                    return i;
                }
            }
            throw new Exception("Variable not found");
        }

        /// <summary>
        /// Add new variable to the list
        /// </summary>
        /// <param name="name">Variable's name</param>
        /// <param name="low">Variable's lower-bound value</param>
        /// <param name="high">Variable's upper-bound value</param>
        public void AddNewVariable(string name, int low, int high)
        {
            vars.Add(name, new List<int> { low, high });
        }

        /// <summary>
        /// Check whether a variable exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ContainsVar(string name)
        {
            return this.vars.Keys.Contains(name);
        }
    }
}