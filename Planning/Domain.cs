using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public abstract class Domain<TA> 
        where TA: Action, new()
    {
        #region Fields

        private List<string> _typeList;

        private Dictionary<string, Predicate> _predDict;

        private Dictionary<string, TA> _actionDict;

        internal const string BarLine = "----------------";

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

        public IReadOnlyDictionary<string, TA> ActionDict
        {
            get { return _actionDict; }
        }

        #endregion

        #region Constructors

        protected Domain(PlanningParser.DomainContext context)
        {
            _typeList = new List<string> { VariableContainer.DefaultType };
            _predDict = new Dictionary<string, Predicate>();
            _actionDict = new Dictionary<string, TA>();
            CurrentCuddIndex = 0;
            HandleDomain(context);
        }

        #endregion

        #region Methods

        public void AddToTypeList(string type)
        {
            _typeList.Add(type);
        }

        public void AddToPredicateDict(Predicate predicate)
        {
            _predDict.Add(predicate.Name, predicate);
        }

        public void AddToActionDict(TA action)
        {
            _actionDict.Add(action.Name, action);
            CurrentCuddIndex = action.CurrentCuddIndex;
        }

        public void FromContext(PlanningParser.DomainContext context)
        {
            
        }

        public abstract void ShowInfo();

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
                AddToTypeList(type.GetText());
            }
        }

        private void HandlePredicatesDefine(PlanningParser.PredicatesDefineContext context)
        {
            foreach (var atomicFormulaSkeleton in context.atomicFormulaSkeleton())
            {
                Predicate pred = Predicate.FromContext(atomicFormulaSkeleton);
                AddToPredicateDict(pred);
            }
        }

        private void HandleActionsDefine(IReadOnlyList<PlanningParser.ActionDefineContext> contexts)
        {
            foreach (var actionDefineContext in contexts)
            {
                TA action = new TA();
                action.FromContext(CurrentCuddIndex, actionDefineContext, PredicateDict);
                AddToActionDict(action);
            }
        }

        #endregion
    }
}
