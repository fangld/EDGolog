using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Clients
{
    public class ClientProblem : Problem<ClientDomain, ClientAction, ClientGroundAction>
    {
        #region Properties

        protected override int PredicateCuddIndexNumber
        {
            get { return 2; }
        }

        public CUDDNode Knowledge { get; set; }

        public CUDDNode Belief { get; set; }

        #endregion

        #region Methods

        public override void ShowInfo()
        {
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine(ClientDomain.BarLine);

            Console.WriteLine("Domain name: {0}", DomainName);
            Console.WriteLine(ClientDomain.BarLine);

            Console.WriteLine("Agents:");
            foreach (var agent in AgentList)
            {
                Console.WriteLine("  {0}", agent);
            }
            Console.WriteLine(ClientDomain.BarLine);

            Console.WriteLine("Variables:");
            foreach (var pair in ConstantTypeMap)
            {
                Console.WriteLine("  {0} - {1}", pair.Key, pair.Value);
            }
            Console.WriteLine(ClientDomain.BarLine);

            Console.WriteLine("Ground predicates:");
            Console.WriteLine("  Previous:");
            foreach (var pair in GroundPredicateDict)
            {
                Console.WriteLine("    Name: {0}, Previous index: {1}, Successsor index:{2}", pair.Key,
                    pair.Value.CuddIndexList[0], pair.Value.CuddIndexList[1]);
            }
            Console.WriteLine(ClientDomain.BarLine);

            Console.WriteLine("Initial state:");
            foreach (var pred in TruePredSet)
            {
                Console.WriteLine("  {0}", pred);
            }
            Console.WriteLine(ClientDomain.BarLine);

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

                //Console.WriteLine("  Successor state axiom:");
                //CUDD.Print.PrintMinterm(gndAction.SuccessorStateAxiom);
            }

            #endregion
        }


    }
}
