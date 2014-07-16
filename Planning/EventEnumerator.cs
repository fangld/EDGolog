using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.ContextExtensions;

namespace Planning
{
    public class EventEnumerator : MixedRadixEnumeratorWithAssignment<PlanningParser.EventDefineContext>
    {
        #region Fields
        
        private IReadOnlyDictionary<string, Predicate> _predicateDict;

        private IDictionary<string, Event> _eventDict;

        #endregion

        #region Properties

        public int InitialCuddIndex { get; private set; }

        public int CurrentCuddIndex { get; private set; }

        #endregion

        #region Constructors

        public EventEnumerator(PlanningParser.EventDefineContext context, IReadOnlyList<IList<string>> collection,
            IReadOnlyList<string> variableNameList, IReadOnlyDictionary<string, Predicate> predicateDict,
            IDictionary<string, Event> eventDict, int initialCuddIndex) : base(context, collection, variableNameList)
        {
            _predicateDict = predicateDict;
            _eventDict = eventDict;
            InitialCuddIndex = initialCuddIndex;
            CurrentCuddIndex = initialCuddIndex;
        }

        #endregion

        #region Overriden Methods

        public override void Initial(int[] index)
        {
            base.Initial(index);
            CurrentCuddIndex = InitialCuddIndex;
        }

        public override void Execute()
        {
            CUDDNode precondition = _context.emptyOrPreGD().ToPrecondition(_predicateDict, _assignment);
            if (!precondition.Equals(CUDD.ZERO))
            {
                Event e = new Event(_context, precondition, _predicateDict, _scanArray, _assignment, CurrentCuddIndex);
                _eventDict.Add(e.FullName, e);
                CurrentCuddIndex++;
            }
            else
            {
                CUDD.Deref(precondition);
            }
        }

        #endregion
    }
}
