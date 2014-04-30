using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Clients
{
    public class ClientProblem : Problem<ClientAction, ClientGroundAction>
    {
        #region Properties

        protected override int PredicateCuddIndexNumber
        {
            get { return 2; }
        }

        public string AgentId { get; set; }

        public CUDDNode Knowledge { get; set; }

        public CUDDNode Belief { get; set; }

        #endregion

        #region Constructors

        private ClientProblem(Domain<ClientAction> domain, PlanningParser.ClientProblemContext context)
            : base(domain)
        {
            HandleClientProblem(context);
        }

        #endregion

        #region Methods

        public static ClientProblem CreateInstance(Domain<ClientAction> domain,
            PlanningParser.ClientProblemContext context)
        {
            ClientProblem result = new ClientProblem(domain, context);
            return result;
        }

        private void HandleClientProblem(PlanningParser.ClientProblemContext context)
        {
            Name = context.problemName().GetText();
            DomainName = context.domainName().GetText();
            HandleAgentDefine(context.agentDefine());
            HandleObjectDeclaration(context.objectDeclaration());
            HandleInitKnowledgeAndBelief(context.initKnowledge(), context.initBelief());
        }

        private void HandleInitKnowledgeAndBelief(PlanningParser.InitKnowledgeContext knowledgeContext, PlanningParser.InitBeliefContext beliefContext)
        {
            Knowledge = knowledgeContext != null ? GetCuddNode(knowledgeContext.gdName()) : CUDD.ONE;
            Belief = beliefContext != null ? GetCuddNode(beliefContext.gdName()) : Knowledge;
        }

        public override void ShowInfo()
        {
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine(Domain<ClientAction>.BarLine);

            Console.WriteLine("Domain name: {0}", DomainName);
            Console.WriteLine(Domain<ClientAction>.BarLine);

            Console.WriteLine("Agents:");
            foreach (var agent in AgentList)
            {
                Console.WriteLine("  {0}", agent);
            }
            Console.WriteLine(Domain<ClientAction>.BarLine);

            Console.WriteLine("Variables:");
            foreach (var pair in ConstantTypeMap)
            {
                Console.WriteLine("  {0} - {1}", pair.Key, pair.Value);
            }
            Console.WriteLine(Domain<ClientAction>.BarLine);

            Console.WriteLine("Ground predicates:");
            Console.WriteLine("  Previous:");
            foreach (var pair in GroundPredicateDict)
            {
                Console.WriteLine("    Name: {0}, Previous index: {1}, Successsor index:{2}", pair.Key,
                    pair.Value.CuddIndexList[0], pair.Value.CuddIndexList[1]);
            }
            Console.WriteLine(Domain<ClientAction>.BarLine);

            Console.WriteLine("Ground actions:");
            foreach (var gndAction in GroundActionDict.Values)
            {
                Console.WriteLine("  Name: {0}", gndAction.Container.Name);
                Console.WriteLine("  Variable: {0}", gndAction.Container.Count);
                for (int i = 0; i < gndAction.ConstantList.Count; i++)
                {
                    Console.WriteLine("    Index: {0}, Name: {1}", i, gndAction.ConstantList[i]);
                }
                Console.WriteLine("  Precondition:");
                CUDD.Print.PrintMinterm(gndAction.Precondition);

                Console.WriteLine("  Successor state axiom:");
                CUDD.Print.PrintMinterm(gndAction.SuccessorStateAxiom);
            }
        }

        #endregion

        #region Methods for generating knowledge and belief

        private CUDDNode GetCuddNode(PlanningParser.AtomicFormulaNameContext context)
        {
            GroundPredicate gndPred = GetGroundPredicate(context);

            int index = gndPred.CuddIndexList[0];

            CUDDNode result = CUDD.Var(index);
            return result;
        }

        private GroundPredicate GetGroundPredicate(PlanningParser.AtomicFormulaNameContext context)
        {
            string gndPredName = VariableContainer.GetFullName(context);
            GroundPredicate result = GroundPredicateDict[gndPredName];
            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.LiteralNameContext context)
        {
            CUDDNode subNode = GetCuddNode(context.atomicFormulaName());
            CUDDNode result;

            if (context.NOT() != null)
            {
                result = CUDD.Function.Not(subNode);
                CUDD.Ref(result);
            }
            else
            {
                result = subNode;
            }

            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.GdNameContext context)
        {
            CUDDNode result = null;

            if (context.atomicFormulaName() != null)
            {
                result = GetCuddNode(context.atomicFormulaName());
            }
            else if (context.literalName() != null)
            {
                result = GetCuddNode(context.literalName());
            }
            else if (context.AND() != null)
            {
                result = GetCuddNode(context.gdName()[0]);
                for (int i = 1; i < context.gdName().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gdName()[i]);
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Ref(andNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = GetCuddNode(context.gdName()[0]);
                for (int i = 1; i < context.gdName().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gdName()[i]);
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    CUDD.Ref(orNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = GetCuddNode(context.gdName()[0]);
                result = CUDD.Function.Not(gdNode);
                CUDD.Ref(result);
                CUDD.Deref(gdNode);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = GetCuddNode(context.gdName()[0]);
                CUDDNode gdNode1 = GetCuddNode(context.gdName()[1]);

                result = CUDD.Function.Implies(gdNode0, gdNode1);
                CUDD.Ref(result);
                CUDD.Deref(gdNode0);
                CUDD.Deref(gdNode1);
            }

            return result;
        }

        #endregion
    }
}
