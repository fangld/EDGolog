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
    public class ActionEnumerator: MixedRadixEnumeratorWithAssignment<PlanningParser.ActionDefineContext>
    {
        #region Fields
        
        private IReadOnlyDictionary<string, Event> _eventDict;

        private IDictionary<string, Action> _actionDict;

        #endregion

        #region Constructors

        public ActionEnumerator(PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Event> eventDict,
            IDictionary<string, Action> actionDict)
            : base(context, context.listVariable().GetCollection(), context.listVariable().GetVariableNameList())
        {
            _eventDict = eventDict;
            _actionDict = actionDict;
        }

        #endregion

        #region Overriden Methods

        public override void Execute()
        {
            Action action = new Action(_context, _eventDict, _scanArray, _assignment);
            _actionDict.Add(action.FullName, action);
        }

        #endregion
    }
}
