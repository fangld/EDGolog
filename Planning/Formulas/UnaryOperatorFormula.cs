using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Formulas
{
    public class UnaryOperatorFormula : Formula
    {
        #region Fields

        public Formula SubFormula { get; set; }

        #endregion
    }
}
