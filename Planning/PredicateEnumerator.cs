using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class PredicateEnumerator : MixedRadixEnumerator<PlanningParser.AtomFormSkeletonContext>
    {
        #region Fields
        
        private IDictionary<string, Predicate> _predicateDict;

        #endregion

        #region Properties

        public int InitialCuddIndex { get; private set; }

        public int CurrentCuddIndex { get; private set; }

        #endregion

        #region Constructors

        public PredicateEnumerator(PlanningParser.AtomFormSkeletonContext context, IReadOnlyList<IList<string>> collection, IDictionary<string, Predicate> predicateDict, int initialCuddIndex)
            : base(context, collection)
        {
            _predicateDict = predicateDict;
            InitialCuddIndex = initialCuddIndex;
            CurrentCuddIndex = initialCuddIndex;
        }

        #endregion

        #region Overriden Methods

        public override void Initial(int[] index)
        {
            CurrentCuddIndex = InitialCuddIndex;
            base.Initial(index);
        }

        public override void Action()
        {
            Predicate predicate = new Predicate(_context, _scanArray, CurrentCuddIndex);
            CurrentCuddIndex = predicate.SuccessiveCuddIndex;
            _predicateDict.Add(predicate.FullName, predicate);
        }

        #endregion
    }
}
