using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class ConditionalEffect
    {
        #region Fields

        private List<Predicate> _predicates;

        public static readonly ConditionalEffect ObservableEffect;

        #endregion

        #region Properties
        public CUDDNode Condition { get; set; }

        public IReadOnlyList<Predicate> Predicates
        {
            get { return _predicates; }
        }

        #endregion

        #region Constructors

        static ConditionalEffect()
        {
            ObservableEffect = new ConditionalEffect();
            ObservableEffect.Condition = CUDD.ONE;
        }

        public ConditionalEffect()
        {
            _predicates = new List<Predicate>();
        }

        #endregion

        #region Methods

        public void AddPredicate(Predicate predicate)
        {
            _predicates.Add(predicate);
        }

        #endregion

    }
}
