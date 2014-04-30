using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class DomainLoader<TD, TA>
        where TD : Domain<TA>, new()
        where TA : Action, new()
    {
        #region Properties

        public TD Domain { get; set; }

        #endregion

        #region Constructors
        public DomainLoader()
        {
            Domain = new TD();
        }

        #endregion

        #region Overriden Methods

        public void HandleDomain(PlanningParser.DomainContext context)
        {
            Domain.Name = context.NAME().GetText();
            HandleTypeDefine(context.typeDefine());
            HandlePredicatesDefine(context.predicatesDefine());
            HandleActionsDefine(context.actionDefine());
        }

        private void HandleTypeDefine(PlanningParser.TypeDefineContext context)
        {
            foreach (var type in context.listName().NAME())
            {
                Domain.AddToTypeList(type.GetText());
            }
        }

        private void HandlePredicatesDefine(PlanningParser.PredicatesDefineContext context)
        {
            foreach (var atomicFormulaSkeleton in context.atomicFormulaSkeleton())
            {
                Predicate pred = Predicate.FromContext(atomicFormulaSkeleton);
                Domain.AddToPredicateDict(pred);
            }
        }

        private void HandleActionsDefine(IReadOnlyList<PlanningParser.ActionDefineContext> contexts)
        {
            foreach (var actionDefineContext in contexts)
            {
                TA action = new TA();
                action.FromContext(Domain.CurrentCuddIndex, actionDefineContext, Domain.PredicateDict);
                Domain.AddToActionDict(action);
            }
        }

        #endregion
    }
}
