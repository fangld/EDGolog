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

        private List<PlanningType> _typeList;

        private Dictionary<string, Predicate> _predDict;

        private Dictionary<string, Action> _actionDict;

        public const string BarLine = "----------------";

        #endregion

        #region Properties

        public string Name { get; set; }

        public int CurrentCuddIndex { get; set; }

        public IReadOnlyList<PlanningType> TypeList
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

        private Domain()//PlanningParser.DomainContext context)
        {
            _typeList = new List<PlanningType> { PlanningType.ObjectType, PlanningType.AgentType };
            _predDict = new Dictionary<string, Predicate>();
            _actionDict = new Dictionary<string, Action>();
            CurrentCuddIndex = 0;
            //HandleDomain(context);
        }

        #endregion

        #region Methods for generating from context

        public static Domain CreateInstance(PlanningParser.DomainContext context)
        {
            Domain result = new Domain();
            result.HandleDomain(context);
            return result;
        }

        private void HandleDomain(PlanningParser.DomainContext context)
        {
            Name = context.NAME().GetText();
            HandleTypeDefine(context.typeDefine());
            //HandlePredicatesDefine(context.predicatesDefine());
            //HandleActionsDefine(context.actionDefine());
        }

        private void HandleTypeDefine(PlanningParser.TypeDefineContext context)
        {
            //foreach (var typeContext in context.typeDeclaration())
            //{
            //    PlanningType type;
            //    if (typeContext.LB() == null)
            //    {
            //        type = new PlanningType {Name = typeContext.GetText()};
            //    }
            //    else
            //    {
            //        string name = typeContext.GetText();
            //        int min = int.Parse(typeContext.constTerm(0).GetText());
            //        int max = int.Parse(typeContext.constTerm(1).GetText());
            //        type = new PlanningNumericType {Name = name, Min = min, Max = max};
            //    }

            //    _typeList.Add(type);
            //}
        }

        //private void HandlePredicatesDefine(PlanningParser.PredicatesDefineContext context)
        //{
        //    foreach (var atomicFormulaSkeleton in context.atomicFormulaSkeleton())
        //    {
        //        Predicate pred = Predicate.FromContext(atomicFormulaSkeleton);
        //        _predDict.Add(pred.Name, pred);
        //    }
        //}

        private void HandleActionsDefine(IReadOnlyList<PlanningParser.ActionDefineContext> contexts)
        {
            throw new NotImplementedException();
            //foreach (var actionDefineContext in contexts)
            //{
            //    Action action = Action.From(CurrentCuddIndex, actionDefineContext, PredicateDict);
            //    _actionDict.Add(action.Name, action);
            //    CurrentCuddIndex = action.CurrentCuddIndex;
            //}
        }

        #endregion
    }
}
