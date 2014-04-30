using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Servers
{
    public class ServerProblem : Problem<ServerAction, ServerGroundAction>
    {
        #region Properties

        protected override int PredicateCuddIndexNumber
        {
            get { return 1; }
        }

        public HashSet<string> TruePredSet { get; set; }

        #endregion

        #region Constructors

        private ServerProblem(Domain<ServerAction> domain, PlanningParser.ServerProblemContext context)
            : base(domain, context)
        {
            TruePredSet = new HashSet<string>();
            HandleServerProblem(context);

        }

        #endregion

        #region Methods

        public static ServerProblem CreateInstance(Domain<ServerAction> domain, PlanningParser.ServerProblemContext context)
        {
            ServerProblem result = new ServerProblem(domain, context);
            return result;
        }

        private void HandleServerProblem(PlanningParser.ServerProblemContext context)
        {
            Name = context.problemName().GetText();
            DomainName = context.domainName().GetText();
            HandleAgentDefine(context.agentDefine());
            HandleObjectDeclaration(context.objectDeclaration());
            HandleInit(context.init());
        }

        private void BuildTruePredicateSet(PlanningParser.InitContext context)
        {
            foreach (var atomicFormula in context.atomicFormulaName())
            {
                var nameNodes = atomicFormula.NAME();
                List<string> termList = new List<string>();
                foreach (var nameNode in nameNodes)
                {
                    termList.Add(nameNode.GetText());
                }
                string gndPredName = VariableContainer.GetFullName(atomicFormula.predicate().GetText(), termList);

                TruePredSet.Add(gndPredName);
            }
        }

        private void HandleInit(PlanningParser.InitContext context)
        {
            BuildTruePredicateSet(context);
        }

        public override void ShowInfo()
        {
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine();

            Console.WriteLine("Domain name: {0}", DomainName);
            Console.WriteLine(Domain<ServerAction>.BarLine);

            Console.WriteLine("Agents:");
            foreach (var agent in AgentList)
            {
                Console.WriteLine("  {0}", agent);
            }
            Console.WriteLine(Domain<ServerAction>.BarLine);

            Console.WriteLine("Variables:");
            foreach (var pair in ConstantTypeMap)
            {
                Console.WriteLine("  {0} - {1}", pair.Key, pair.Value);
            }
            Console.WriteLine(Domain<ServerAction>.BarLine);

            Console.WriteLine("Ground predicates:");
            foreach (var pair in GroundPredicateDict)
            {
                Console.WriteLine("  Name: {0}, Index: {1}", pair.Key, pair.Value.CuddIndexList[0]);
            }
            Console.WriteLine(Domain<ServerAction>.BarLine);

            Console.WriteLine("Initial state:");
            foreach (var pred in TruePredSet)
            {
                Console.WriteLine("  {0}", pred);
            }
            Console.WriteLine(Domain<ServerAction>.BarLine);

            Console.WriteLine("Ground actions:");
            foreach (var gndAction in GroundActionDict.Values)
            {
                Console.WriteLine("  Name: {0}", gndAction);
                Console.WriteLine("  Precondition:");
                CUDD.Print.PrintMinterm(gndAction.Precondition);

                Console.WriteLine("  Effect:");
                for (int i = 0; i < gndAction.Effect.Count; i++)
                {
                    Console.WriteLine("    Index: {0}", i);
                    Console.WriteLine("    Condition:");
                    CUDD.Print.PrintMinterm(gndAction.Effect[i].Item1);

                    Console.Write("    Literals: { ");
                    var literal = gndAction.Effect[i].Item2[0];
                    if (literal.Item2)
                    {
                        Console.Write("{0}", literal.Item1);
                    }
                    else
                    {
                        Console.Write("not {0}", literal.Item1);
                    }

                    for (int j = 1; j < gndAction.Effect[i].Item2.Count; j++)
                    {
                        if (literal.Item2)
                        {
                            Console.Write(", {0}", literal.Item1);
                        }
                        else
                        {
                            Console.Write(", not {0}", literal.Item1);
                        }
                    }

                    Console.WriteLine(" }");
                }
                Console.WriteLine();
            }
        }

        #endregion
    }
}
