using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class EventEnumerator : MixedRadixEnumerator<PlanningParser.AtomFormSkeletonContext>
    {
        #region Fields

        //private PlanningParser.AtomFormSkeletonContext _context;

        //private IReadOnlyList<IList<string>> _collection;
        
        private IDictionary<string, Predicate> _predicateDict;

        //private string[] _scanArray;

        private int _currentCuddIndex;

        #endregion

        #region Constructors

        public EventEnumerator(PlanningParser.AtomFormSkeletonContext context, IReadOnlyList<IList<string>> collection, IDictionary<string, Predicate> predicateDict, int currentCuddIndex)
            : base(context, collection)
        {
            _predicateDict = predicateDict;
            _currentCuddIndex = currentCuddIndex;
        }

        #endregion

        #region Overriden Methods

        public override void Action()
        {
            Predicate predicate = new Predicate(_context, _scanArray, _currentCuddIndex);
            _currentCuddIndex = predicate.SuccessiveCuddIndex;
            _predicateDict.Add(predicate.FullName, predicate);
        }

        #endregion
    }
}
