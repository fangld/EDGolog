using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Agents.Planning
{
    public class ProblemLoader : PlanningBaseListener
    {
        #region Properties

        public Problem Problem { get; set; }

        #endregion

        #region Constructors

        public ProblemLoader(Domain domain)
        {
            Problem = new Problem(domain);
        }

        #endregion

        #region Overriden Methods

        public override void EnterClientProblem(PlanningParser.ClientProblemContext context)
        {
            Problem.Name = context.problemName().GetText();
            Problem.DomainName = context.domainName().GetText();
            Problem.AgentId = context.agentId().GetText();
        }

        public override void EnterAgentDefine(PlanningParser.AgentDefineContext context)
        {
            foreach (var nameNode in context.NAME())
            {
                Problem.AddAgent(nameNode.GetText());
            }
        }

        public override void EnterObjectDeclaration(PlanningParser.ObjectDeclarationContext context)
        {
            var listNameContext = context.listName();
            Problem.BuildConstantTypeMap(listNameContext);
            Problem.BuildGroundPredicate();
            Problem.BuildGroundAction();
        }

        public override void EnterInitKnowledge(PlanningParser.InitKnowledgeContext context)
        {
            Problem.GenerateKnowledge(context.gdName());
        }

        public override void EnterInitBelief(PlanningParser.InitBeliefContext context)
        {
            Problem.GenerateBelief(context.gdName());
        }

        #endregion
    }
}