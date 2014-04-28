using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Servers
{
    public class ServerProblem : Problem<ServerDomain, ServerAction, ServerAbstractPredicate, ServerGroundAction>
    {
        public override void ShowInfo()
        {
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine();

            Console.WriteLine("Domain name: {0}", DomainName);
            Console.WriteLine(ServerDomain.BarLine);

            Console.WriteLine("Agents:");
            foreach (var agent in AgentList)
            {
                Console.WriteLine("  {0}", agent);
            }
            Console.WriteLine(ServerDomain.BarLine);

            Console.WriteLine("Variables:");
            foreach (var pair in ConstantTypeMap)
            {
                Console.WriteLine("  {0} - {1}", pair.Key, pair.Value);
            }
            Console.WriteLine(ServerDomain.BarLine);

            Console.WriteLine("Ground predicates:");
            foreach (var pair in GroundPredicateDict)
            {
                Console.WriteLine("  Name: {0}, Index: {1}", pair.Key, pair.Value.CuddIndex);
            }
            Console.WriteLine(ServerDomain.BarLine);

            Console.WriteLine("Initial state:");
            foreach (var pred in TruePredSet)
            {
                Console.WriteLine("  {0}", pred);
            }
            Console.WriteLine(ServerDomain.BarLine);

            Console.WriteLine("Ground actions:");
            foreach (var gndAction in GroundActionDict.Values)
            {
                Console.WriteLine("  Name: {0}", gndAction);
                //Console.WriteLine("  Variable: {0}", gndAction.Container.Count);
                //for (int i = 0; i < gndAction.ConstantList.Count; i++)
                //{
                    //Console.WriteLine("    Index: {0}, Name: {1}", i, gndAction.ConstantList[i]);
                //}
                Console.WriteLine("  Precondition:");
                //Console.WriteLine(gndAction.Precondition == null);
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
    }
}
