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
        #region Properties

        public CUDDNode Precondition { get; set; }

        public string Effect { get; set; }
        
        #endregion
    }
}
