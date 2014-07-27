using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.ContextExtensions;
using Planning.Servers;

namespace Planning.Collections
{
    public class ObservationEnumerator : MixedRadixEnumeratorWithAssignment<PlanningParser.ObservationDefineContext>
    {
        #region Fields

        private IReadOnlyDictionary<string, Predicate> _predicateDict;
        
        private IReadOnlyDictionary<string, Event> _eventDict;

        private IDictionary<string, Observation> _observationDict;

        private IDictionary<string, Agent> _agentDict;

        #endregion

        #region Constructors

        public ObservationEnumerator(PlanningParser.ObservationDefineContext context,
            IReadOnlyList<IList<string>> collection, IReadOnlyList<string> variableNameList,
            IReadOnlyDictionary<string, Predicate> predicateDict, IReadOnlyDictionary<string, Event> eventDict, IDictionary<string, Agent> agentDict,
            IDictionary<string, Observation> observationDict) : base(context, collection, variableNameList)
        {
            _predicateDict = predicateDict;
            _eventDict = eventDict;
            _observationDict = observationDict;
            _agentDict = agentDict;
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

                foreach (var pair in _agentDict)
                {
                    int startIndex = observation.FullName.IndexOf('(');
                    int startIndexAddOne = observation.FullName.IndexOf(pair.Key);
                    //Console.WriteLine("observation name: {0}, agent name: {1}, isContains: {2}", observation.FullName, pair.Key, startIndex == startIndexAddOne - 1);
                    //Console.ReadLine();
                    if (startIndex == startIndexAddOne - 1)
                    {
                        pair.Value.AddObservation(observation);
                        break;
                    }
                }
            }
            else
            {
                CUDD.Deref(precondition);
            }
        }

        #endregion
    }
}
