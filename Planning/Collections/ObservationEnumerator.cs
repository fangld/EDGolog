using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.ContextExtensions;

namespace Planning.Collections
{
    public class ObservationEnumerator : MixedRadixEnumeratorWithAssignment<PlanningParser.ObservationDefineContext>
    {
        #region Fields

        private IReadOnlyDictionary<string, Predicate> _predicateDict;
        
        private IReadOnlyDictionary<string, Event> _eventDict;

        private IDictionary<string, Observation> _observationDict;

        #endregion

        #region Constructors

        public ObservationEnumerator(PlanningParser.ObservationDefineContext context,
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
