using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class Requirements
    {
        #region Properties
        public bool Strips { get; set; }
        public bool Typing { get; set; }

        #endregion

        #region Constructs
        public Requirements()
        {
            Strips = true;
            Typing = false;
        }

        #endregion
    }
}
