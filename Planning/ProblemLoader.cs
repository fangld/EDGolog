using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class ProblemLoader<TP, TD, TA, TGA>
        where TP : Problem<TD, TA, TGA>, new()
        where TD : Domain<TA>, new()
        where TA : Action, new()
        where TGA : GroundAction<TA> , new()
    {
        #region Properties

        public TP Problem { get; set; }

        #endregion

        #region Constructors

        public ProblemLoader(TD domain)
        {
            Problem = new TP();
            Problem.From(domain);
        }

        #endregion

        #region Overriden Methods

        public void HandleServerProblem(PlanningParser.ServerProblemContext context)
        {
            Problem.Name = context.problemName().GetText();
            Problem.DomainName = context.domainName().GetText();
            HandleAgentDefine(context.agentDefine());
            HandleObjectDeclaration(context.objectDeclaration());
            HandleInit(context.init());
        }

        private void HandleAgentDefine(PlanningParser.AgentDefineContext context)
        {
            foreach (var nameNode in context.NAME())
            {
                Problem.AddAgent(nameNode.GetText());
            }
        }

        private void HandleObjectDeclaration(PlanningParser.ObjectDeclarationContext context)
        {
            var listNameContext = context.listName();
            Problem.BuildConstantTypeMap(listNameContext);
            Problem.BuildGroundPredicate();
            Problem.BuildGroundAction();
        }

        private void HandleInit(PlanningParser.InitContext context)
        {
            Problem.BuildTruePredicateSet(context);
        }

        #endregion
    }
}