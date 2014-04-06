using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class Action : VariableContainer
    {
        #region Properties

        public string Name { get; set; }

        public string Precondition { get; set; }

        public string Effect { get; set; }
        
        #endregion
    }
}
