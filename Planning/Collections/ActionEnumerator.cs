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
    public class ActionEnumerator: MixedRadixEnumeratorWithAssignment<PlanningParser.ActionDefineContext>
    {
        #region Fields
        
        private IReadOnlyDictionary<string, Event> _eventDict;

        private IDictionary<string, Action> _actionDict;

        private IDictionary<string, Agent> _agentDict;

        #endregion

        #region Constructors

        public ActionEnumerator(PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Event> eventDict, IDictionary<string, Agent> agentDict, IDictionary<string, Action> actionDict)
            : base(context, context.listVariable().GetCollection(), context.listVariable().GetVariableNameList())
        {
            _eventDict = eventDict;
            _actionDict = actionDict;
            _agentDict = agentDict;
        }

        #endregion

        #region Overriden Methods

        public override void Execute()
        {
            Action action = new Action(_context, _eventDict, _scanArray, _assignment);
            _actionDict.Add(action.FullName, action);
            foreach (var pair in _agentDict)
            {


                int startIndex = action.FullName.IndexOf('(');
                int startIndexAddOne = action.FullName.IndexOf(pair.Key);
                //Console.WriteLine("action name: {0}, agent name: {1}, isContains: {2}", action.FullName, pair.Key, startIndex == startIndexAddOne - 1);
                //Console.ReadLine();
                if (startIndex == startIndexAddOne - 1)
                {
                    pair.Value.AddAction(action);
                    break;
                }
            }
        }

        #endregion
    }
}
