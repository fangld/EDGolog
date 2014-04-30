using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Clients
{
    public class ClientDomain : Domain<ClientAction>
    {
        #region Methods

        public override void ShowInfo()
        {
            const string barline = "----------------";

            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine(barline);

            Console.Write("Types: ");
            for (int i = 0; i < TypeList.Count - 1; i++)
            {
                Console.Write("{0}, ", TypeList[i]);
            }
            Console.WriteLine("{0}", TypeList[TypeList.Count - 1]);
            Console.WriteLine(barline);

            Console.WriteLine("Predicates:");
            foreach (var pred in PredicateDict.Values)
            {
                Console.WriteLine("  Name: {0}", pred.Name);
                Console.WriteLine("  Variable: {0}", pred.Count);
                for (int i = 0; i < pred.Count; i++)
                {
                    Console.WriteLine("    Index: {0}, Name: {1}, Type: {2}", i, pred.VariableList[i].Item1,
                        pred.VariableList[i].Item2);
                }
                Console.WriteLine();
            }
            Console.WriteLine(barline);

            Console.WriteLine("Actions:");
            foreach (var action in ActionDict.Values)
            {
                Console.WriteLine("  Name: {0}", action.Name);
                Console.WriteLine("  Variable: {0}", action.Count);
                for (int i = 0; i < action.Count; i++)
                {
                    Console.WriteLine("    Index: {0}, Name: {1}, Type: {2}", i, action.VariableList[i].Item1,
                        action.VariableList[i].Item2);
                }

                Console.WriteLine("    Abstract Predicates: ");
                foreach (var pair in action.AbstractPredicateDict)
                {
                    Console.WriteLine("      Name: {0}, Previous index: {1}, Successor index: {2}", pair.Key, pair.Value.CuddIndexList[0], pair.Value.CuddIndexList[1]);
                }

                Console.WriteLine("  Precondition:");
                CUDD.Print.PrintMinterm(action.Precondition);

                Console.WriteLine("  Effect:");
                for (int i = 0; i < action.Effect.Count; i++)
                {
                    Console.WriteLine("      Index:{0}", i);
                    Console.WriteLine("      Condition:");
                    CUDD.Print.PrintMinterm(action.Effect[i].Item1);

                    Console.Write("      Literals: { ");
                    var literal = action.Effect[i].Item2[0];
                    if (literal.Item2)
                    {
                        Console.Write("{0}", literal.Item1);
                    }
                    else
                    {
                        Console.Write("not {0}", literal.Item1);
                    }

                    for (int j = 1; j < action.Effect[i].Item2.Count; j++)
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

                Console.WriteLine("  Successor state axiom:");
                CUDD.Print.PrintMinterm(action.SuccessorStateAxiom);

                Console.WriteLine();
            }
        }

        #endregion
    }
}
