using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Clients
{
    public class DomainLoader : PlanningBaseListener
    {
        #region Properties

        public Domain Domain { get; set; }

        #endregion

        #region Constructors
        public DomainLoader()
        {
            Domain = new Domain();
        }

        #endregion

        #region Overriden Methods

        public override void EnterDomain(PlanningParser.DomainContext context)
        {
            Domain.Name = context.NAME().GetText();
        }

        public override void EnterTypeDefine(PlanningParser.TypeDefineContext context)
        {
            foreach (var type in context.listName().NAME())
            {
                Domain.AddToTypeList(type.GetText());
            }
        }

        public override void EnterPredicatesDefine(PlanningParser.PredicatesDefineContext context)
        {
            foreach (var atomicFormulaSkeleton in context.atomicFormulaSkeleton())
            {
                Predicate pred = Predicate.FromContext(atomicFormulaSkeleton);
                Domain.AddToPredicateDict(pred);
            }
        }

        public override void EnterActionDefine(PlanningParser.ActionDefineContext context)
        {
            Action action = Action.FromContext(Domain.CurrentCuddIndex, context, Domain.PredicateDict);
            Domain.AddToActionDict(action);
        }

        #endregion
    }
}
