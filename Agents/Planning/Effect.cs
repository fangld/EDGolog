using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agents.Planning
{
    public class Effect
    {
        #region Fields

        private List<ConditionalEffect> _conditionalEffects;

        #endregion

        #region Properties

        public IReadOnlyList<ConditionalEffect> ConditionalEffects
        {
            get { return _conditionalEffects; }
        }

        #endregion

        #region Constructors

        public Effect()
        {
            _conditionalEffects = new List<ConditionalEffect>();
        }
        #endregion

        #region Methods

        public void AddConditionalEffect(ConditionalEffect conditionalEffect)
        {
            _conditionalEffects.Add(conditionalEffect);
        }

        #endregion
    }
}
