using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class Domain
    {
        #region Fields

        private List<string> _typeList;

        private Dictionary<string, Predicate> _predDict;

        private Dictionary<string, Action> _actionDict;

        public const string BarLine = "----------------";

        #endregion

        #region Properties

        public string Name { get; set; }

        public int CurrentCuddIndex { get; set; }

        public IReadOnlyList<string> TypeList
        {
            get { return _typeList; }
        }

        public IReadOnlyDictionary<string, Predicate> PredicateDict
        {
            get { return _predDict; }
        }

        public IReadOnlyDictionary<string, Action> ActionDict
        {
            get { return _actionDict; }
        }

        #endregion

        #region Constructors

        public Domain(PlanningParser.DomainContext context)
        {
            _typeList = new List<string> { VariableContainer.DefaultType };
            _predDict = new Dictionary<string, Predicate>();
            _actionDict = new Dictionary<string, Action>();
            CurrentCuddIndex = 0;
            HandleDomain(context);
        }

        #endregion

        #region Methods for generating from context

        private void HandleDomain(PlanningParser.DomainContext context)
        {
            Name = context.NAME().GetText();
            HandleTypeDefine(context.typeDefine());
            HandlePredicatesDefine(context.predicatesDefine());
            HandleActionsDefine(context.actionDefine());
        }

        private void HandleTypeDefine(PlanningParser.TypeDefineContext context)
        {
            foreach (var type in context.listName().NAME())
            {
                _typeList.Add(type.GetText());
            }
        }

        private void HandlePredicatesDefine(PlanningParser.PredicatesDefineContext context)
        {
            foreach (var atomicFormulaSkeleton in context.atomicFormulaSkeleton())
            {
                Predicate pred = Predicate.FromContext(atomicFormulaSkeleton);
                _predDict.Add(pred.Name, pred);
            }
        }

        private void HandleActionsDefine(IReadOnlyList<PlanningParser.ActionDefineContext> contexts)
        {
            foreach (var actionDefineContext in contexts)
            {
                Action action = Action.From(CurrentCuddIndex, actionDefineContext, PredicateDict);
                _actionDict.Add(action.Name, action);
                CurrentCuddIndex = action.CurrentCuddIndex;
            }
        }

        #endregion
    }
}
