using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning.Clients;

namespace Planning.HighLevelProgramExecution
{
    public class EmptyProgram : Program
    {
        #region Constructors

        internal EmptyProgram()
        {
            
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return "nil";
        }

        #endregion

    }
}
