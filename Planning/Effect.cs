using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class Effect
    {
        #region Fields

        private List<ConditionalEffect> _conditionalEffects;

        public static readonly Effect TrueEffect;

        #endregion

        #region Properties

        public IReadOnlyList<ConditionalEffect> ConditionalEffects
        {
            get { return _conditionalEffects; }
        }

        #endregion

        #region Constructors

        static Effect()
        {
            TrueEffect = new Effect();
            TrueEffect.AddConditionalEffect(ConditionalEffect.ObservableEffect);
        }

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
