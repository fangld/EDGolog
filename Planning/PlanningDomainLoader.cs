using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class PlanningDomainLoader : PlanningDomainBaseListener
    {
        #region Fields

        private const string DefaultType = "object";

        #endregion

        #region Properties
        public string Name { get; set; }

        public Requirements Requirements { get; set; }

        public List<string> ListType { get; set; }

        public List<PredicateDefinition> PredicateDefinitions { get; set; }

        public List<ActionDefinition> ActionDefinitions { get; set; }


        #endregion

        #region Constructors
        public PlanningDomainLoader()
        {
            Requirements = new Requirements();
            ListType = new List<string>();
            ListType.Add(DefaultType);
            PredicateDefinitions = new List<PredicateDefinition>();
            ActionDefinitions = new List<ActionDefinition>();
        }

        #endregion

        #region Overriden Methods

        public override void EnterDomain(PlanningDomainParser.DomainContext context)
        {
            Name = context.NAME().GetText();
        }

        public override void EnterRequireDefine(PlanningDomainParser.RequireDefineContext context)
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

        public override void EnterTypeDefine(PlanningDomainParser.TypeDefineContext context)
        {
            foreach (var type in context.listName().NAME())
            {
                ListType.Add(type.GetText());
            }
        }

        public override void EnterPredicatesDefine(PlanningDomainParser.PredicatesDefineContext context)
        {
            foreach (var atomicFormulaSkeleton in context.atomicFormulaSkeleton())
            {
                PredicateDefinition predDef = new PredicateDefinition();
                predDef.Name = atomicFormulaSkeleton.predicate().GetText();
                var listVariableContext = atomicFormulaSkeleton.listVariable();

                do
                {
                    if (listVariableContext.VAR().Count != 0)
                    {
                        string type = listVariableContext.type() == null
                            ? DefaultType
                            : listVariableContext.type().GetText();
                        predDef.AddVariableTypes(type, listVariableContext.VAR().Count);
                    }
                    listVariableContext = listVariableContext.listVariable();
                } while (listVariableContext != null);

                PredicateDefinitions.Add(predDef);
            }
        }

        public override void EnterActionDefine(PlanningDomainParser.ActionDefineContext context)
        {
            ActionDefinition actDef = new ActionDefinition();
            actDef.Name = context.actionSymbol().GetText();

            var listVariableContext = context.listVariable();

            do
            {
                if (listVariableContext.VAR().Count != 0)
                {
                    string type = listVariableContext.type() == null
                        ? DefaultType
                        : listVariableContext.type().GetText();
                    actDef.AddVariableTypes(type, listVariableContext.VAR().Count);
                }
                listVariableContext = listVariableContext.listVariable();
            } while (listVariableContext != null);

            ActionDefinitions.Add(actDef);
        }

        #endregion
    }
}
