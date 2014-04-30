//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LanguageRecognition;
//using PAT.Common.Classes.CUDDLib;

//namespace Planning.Servers
//{
//    public class ServerDomain : Domain<ServerAction>
//    {
//        #region Constructors

//        private ServerDomain(PlanningParser.DomainContext context)
//            : base(context)
//        {
//        }

//        #endregion

//        #region Methods

//        public static ServerDomain CreateInstance(PlanningParser.DomainContext context)
//        {
//            ServerDomain result = new ServerDomain(context);
//            return result;
//        }

//        public override void ShowInfo()
//        {
//            Console.WriteLine("Name: {0}", Name);
//            Console.WriteLine(BarLine);

//            Console.Write("Types: ");
//            for (int i = 0; i < TypeList.Count - 1; i++)
//            {
//                Console.Write("{0}, ", TypeList[i]);
//            }
//            Console.WriteLine("{0}", TypeList[TypeList.Count - 1]);
//            Console.WriteLine(BarLine);

//            Console.WriteLine("Predicates:");
//            foreach (var pred in PredicateDict.Values)
//            {
//                Console.WriteLine("  Name: {0}", pred.Name);
//                Console.WriteLine("  Variable: {0}", pred.Count);
//                for (int i = 0; i < pred.Count; i++)
//                {
//                    Console.WriteLine("    Index: {0}, Name: {1}, Type: {2}", i, pred.VariableList[i].Item1,
//                        pred.VariableList[i].Item2);
//                }
//                Console.WriteLine();
//            }
//            Console.WriteLine(BarLine);

//            Console.WriteLine("Actions:");
//            foreach (var action in ActionDict.Values)
//            {
//                Console.WriteLine("  Name: {0}", action.Name);
//                Console.WriteLine("  Variable: {0}", action.Count);
//                for (int i = 0; i < action.Count; i++)
//                {
//                    Console.WriteLine("    Index: {0}, Name: {1}, Type: {2}", i, action.VariableList[i].Item1,
//                        action.VariableList[i].Item2);
//                }

//                Console.WriteLine("    Abstract Predicates: ");
//                foreach (var pair in action.AbstractPredicateDict)
//                {
//                    Console.WriteLine("      Name: {0}, CuddIndex: {1}", pair.Key, pair.Value.CuddIndexList[0]);
//                }
//                Console.WriteLine("  Precondition:");
//                CUDD.Print.PrintMinterm(action.Precondition);

//                Console.WriteLine("  Effect:");
//                for (int i = 0; i < action.Effect.Count; i++)
//                {
//                    Console.WriteLine("      Index:{0}", i);
//                    Console.WriteLine("      Condition:");
//                    CUDD.Print.PrintMinterm(action.Effect[i].Item1);

//                    Console.Write("      Literals: { ");
//                    var literal = action.Effect[i].Item2[0];
//                    if (literal.Item2)
//                    {
//                        Console.Write("{0}", literal.Item1);
//                    }
//                    else
//                    {
//                        Console.Write("not {0}", literal.Item1);
//                    }

//                    for (int j = 1; j < action.Effect[i].Item2.Count; j++)
//                    {
//                        if (literal.Item2)
//                        {
//                            Console.Write(", {0}", literal.Item1);
//                        }
//                        else
//                        {
//                            Console.Write(", not {0}", literal.Item1);
//                        }
//                    }

//                    Console.WriteLine(" }");
//                }

//                Console.WriteLine();
//            }
//        }

//        #endregion
//    }
//}
