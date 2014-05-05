using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Formulas
{
    public class BinaryOperatorFormula : Formula
    {
        #region Fields

        public Formula LeftSubFormula { get; set; }

        public Formula RightSubFormula { get; set; }

        #endregion
    }
}
