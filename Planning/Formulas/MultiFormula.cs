using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Formulas
{
    public class MultiFormula
    {
        #region Fields

        private List<Formula> _subFormulas;

        #endregion

        #region Properties

        public IReadOnlyList<Formula> SubFormulas;

        #endregion

        #region Constructors

        public MultiFormula()
        {
            _subFormulas = new List<Formula>();
        }

        #endregion

        #region Methods

        public void AddSubFormula(Formula formula)
        {
            _subFormulas.Add(formula);
        }

        #endregion
    }
}
