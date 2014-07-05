using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdd
{
    public class VariableTreeNode
    {
        #region Properties

        public VariableTreeNode Left { get; set; }

        public VariableTreeNode Right { get; set; }

        public int Index { get; set; }

        #endregion
    }
}
