using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class CEffectEnumerator : MixedRadixEnumeratorWithAssignment<PlanningParser.EffectContext>
    {
        #region Fields

        private IReadOnlyDictionary<string, Predicate> _predicateDict;
        
        private IReadOnlyDictionary<string, Event> _eventDict;

        private IDictionary<string, Observation> _observationDict;

        #endregion

        #region Constructors

        public CEffectEnumerator(PlanningParser.EffectContext context,
            IReadOnlyList<IList<string>> collection, IReadOnlyList<string> variableNameList,
            IReadOnlyDictionary<string, Predicate> predicateDict, IReadOnlyDictionary<string, Event> eventDict,
            IDictionary<string, Observation> observationDict) : base(context, collection, variableNameList)
        {
            _predicateDict = predicateDict;
            _eventDict = eventDict;
            _observationDict = observationDict;
        }

        #endregion

        #region Overriden Methods

        public override void Execute()
        {
            CUDDNode precondition = _context.emptyOrPreGD().ToPrecondition(_predicateDict, _assignment);
            if (!precondition.Equals(CUDD.ZERO))
            {
                Observation observation = new Observation(_context, precondition, _eventDict, _scanArray, _assignment);
                _observationDict.Add(observation.FullName, observation);
            }
            else
            {
                CUDD.Deref(precondition);
            }
        }

        #endregion
    }
}
