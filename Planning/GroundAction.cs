using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class GroundAction : Ground<Action>
    {
        #region Properties

        public CUDDNode Precondition { get; set; }

        public CUDDNode Effect { get; set; }

        #endregion

        #region Constructors

        public GroundAction(Action action, ICollection<string> paramList):base(action, paramList)
        {
        }

        #endregion
    }
}
