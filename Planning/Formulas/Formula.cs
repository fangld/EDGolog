using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Formulas
{
    public enum FormulaType { True, False, Atom, Not, And, Or, Implies, Equals, Xor}

    public abstract class Formula
    {
        public FormulaType Type { get; protected set; }
    }
}
