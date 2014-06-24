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
        
        private List<Event> _eventSets;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; set; }

        public int CurrentCuddIndex { get; set; }

        #endregion

        #region Constructors

        private Action()
        {
            
        }

        #endregion

        #region Methods for creating an instance

        public static Action From(int initialCuddIndex, PlanningParser.ActionDefineContext context,
            IReadOnlyDictionary<string, Predicate> predDict)
        {
            Action result = new Action();

            result.CurrentCuddIndex = initialCuddIndex;
            result.Name = context.actionSymbol().GetText();
            result.GenerateVariableList(context.listVariable());

            return result;
        }

        #endregion
    }
}
