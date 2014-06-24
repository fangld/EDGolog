using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class Action : VariableContainer
    {
        #region Fields

        private List<string> _constList;
        
        private Dictionary<string, List<Event>> _respDict;

        #endregion

        #region Properties
        
        public int CurrentCuddIndex { get; set; }

        #endregion

        #region Constructors

        private Action()
        {
            _constList = new List<string>();            
        }

        #endregion

        #region Methods for creating an instance

        public static Action From(PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Event> eventDict, string[] constArray)
        {
            Action result = new Action();

            result.GenerateVariableList(context.listVariable());
            result.SetConstList(constArray);
            string actionName = context.actionSymbol().GetText();
            result.Name = GetFullName(actionName, result._constList);
            return result;
        }

        private void SetConstList(string[] constArray)
        {
            _constList.AddRange(constArray);
        }

        #endregion
    }
}
