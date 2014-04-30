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

        public CUDDNode Knowledge { get; set; }

        public CUDDNode Belief { get; set; }

        #endregion

        #region Constructors

        public ClientProblem(Domain<ClientAction> domain, PlanningParser.ServerProblemContext context)
            : base(domain, context)
        {

        }

        #endregion
        
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

                Console.WriteLine("  Effect:");
                for (int i = 0; i < gndAction.Effect.Count; i++)
                {
                    Console.WriteLine("      Index:{0}", i);
                    Console.WriteLine("      Condition:");
                    CUDD.Print.PrintMinterm(gndAction.Effect[i].Item1);

                    Console.Write("      Literals: { ");
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

        }




    }
}
