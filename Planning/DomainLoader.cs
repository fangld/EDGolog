using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class DomainLoader : PlanningBaseListener
    {
        #region Fields

        private const string DefaultType = "object";

        #endregion

        #region Properties
        public string Name { get; set; }

        public Requirements Requirements { get; set; }

        public List<string> ListType { get; set; }

        public List<Predicate> PredicateDefinitions { get; set; }

        public List<Action> ActionDefinitions { get; set; }


        #endregion

        #region Constructors
        public DomainLoader()
        {
            Requirements = new Requirements();
            ListType = new List<string>();
            ListType.Add(DefaultType);
            PredicateDefinitions = new List<Predicate>();
            ActionDefinitions = new List<Action>();
        }

        #endregion

        #region Overriden Methods

        public override void EnterDomain(PlanningParser.DomainContext context)
        {
            Name = context.NAME().GetText();
        }

        public override void EnterRequireDefine(PlanningParser.RequireDefineContext context)
        {
            Requirements.Strips = false;

            foreach (var requirment in context.requireKey())
            {
                if (requirment.GetText() == ":typing")
                {
                    Requirements.Typing = true;
                }

                else if (requirment.GetText() == ":strips")
                {
                    Requirements.Strips = true;
                }
            }
        }

        public override void EnterTypeDefine(PlanningParser.TypeDefineContext context)
        {
            foreach (var type in context.listName().NAME())
            {
                ListType.Add(type.GetText());
            }
        }

        public override void EnterPredicatesDefine(PlanningParser.PredicatesDefineContext context)
        {
            foreach (var atomicFormulaSkeleton in context.atomicFormulaSkeleton())
            {
                Predicate predDef = new Predicate();
                predDef.Name = atomicFormulaSkeleton.predicate().GetText();
                AddVairablesToContainer(predDef, atomicFormulaSkeleton.listVariable());
                PredicateDefinitions.Add(predDef);
            }
        }

        public override void EnterActionDefine(PlanningParser.ActionDefineContext context)
        {
            Action actDef = new Action();
            actDef.Name = context.actionSymbol().GetText();
            AddVairablesToContainer(actDef, context.listVariable());

            actDef.Precondition = "true";
            if (context.actionDefBody().emptyOrPreGD() != null)
            {
                if (context.actionDefBody().emptyOrPreGD().preGD() != null)
                {
                    actDef.Precondition = context.actionDefBody().emptyOrPreGD().preGD().GetText();
                }
            }

            actDef.Effect = "true";
            if (context.actionDefBody().emptyOrEffect() != null)
            {
                if (context.actionDefBody().emptyOrEffect().effect() != null)
                {
                    actDef.Effect = context.actionDefBody().emptyOrEffect().effect().GetText();
                }
            }

            ActionDefinitions.Add(actDef);
        }

        private void AddVairablesToContainer(VariablesContainer container, PlanningParser.ListVariableContext context)
        {
            do
            {
                if (context.VAR().Count != 0)
                {
                    string type = context.type() == null ? DefaultType : context.type().GetText();
                    container.AddVariableTypes(type, context.VAR().Count);
                }
                context = context.listVariable();
            } while (context != null);
        }

        #endregion
    }
}
