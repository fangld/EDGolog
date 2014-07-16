using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class EventEnumerator : MixedRadixEnumerator<PlanningParser.EventDefineContext>
    {
        #region Fields
        
        private IDictionary<string, Predicate> _predicateDict;

        private IDictionary<string, Event> _eventDict;

        private StringDictionary _assignment;

        private IReadOnlyList<string> _variableNameList;

        #endregion

        #region Properties

        public int InitialCuddIndex { get; private set; }

        public int CurrentCuddIndex { get; private set; }

        #endregion

        #region Constructors

        public EventEnumerator(PlanningParser.EventDefineContext context, IReadOnlyList<IList<string>> collection, IReadOnlyList<string> variableNameList, IDictionary<string, Predicate> predicateDict, IDictionary<string, Event> eventDict, int initialCuddIndex)
            : base(context, collection)
        {
            _assignment = new StringDictionary();
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
            for (int i = 0; i < CollectionCount; i++)
            {
                
            }
            Parallel.For(0, CollectionCount, assignment.Add(variableNameList[i], value))
        }

        public override void Action()
        {
            Event e = new Event(_context, _predicateDict, _scanArray,
                
                (_context, _scanArray, CurrentCuddIndex);
            CurrentCuddIndex = e.CuddIndex;
            _predicateDict.Add(e.FullName, e);
        }

        #endregion
    }
}
