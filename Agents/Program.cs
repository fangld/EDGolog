using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agents.Network;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanguageRecognition;
//using Agents.HighLevelPrograms;
using PAT.Common.Classes.CUDDLib;
using Planning;

namespace Agents
{
    class Program
    {
        static void Main(string[] args)
        {
            //string programFileName = @"program.edp";
            //Test2(programFileName);
            Test1();
            //string domainFileName, problemFileName;
            //if (args.Length == 3)
            //{
            //    domainFileName = args[1];
            //    problemFileName = args[2];
            //}
            //else
            //{
            //    domainFileName = "d1.pddl";
            //    problemFileName = "a1.pddl";
            //}
            //Client client = new Client();
            //Test3(domainFileName, problemFileName);
            //Console.ReadLine();
        }

        static void Test3(string domainFileName, string problemFileName)
        {
            CUDD.InitialiseCUDD(256, 256, 262144, 0.1);

            // Create a TextReader that reads from a file
            TextReader tr = new StreamReader(domainFileName);

            // create a CharStream that reads from standard input
            AntlrInputStream input = new AntlrInputStream(tr);
            // create a lexer that feeds off of input CharStream

            PlanningLexer lexer = new PlanningLexer(input);
            // create a buffer of tokens pulled from the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // create a parser that feeds off the tokens buffer
            PlanningParser parser = new PlanningParser(tokens);

            //var domainContext = parser.domain();// begin parsing at init rule
            tr.Close();
            //var domain = Domain<ClientAction>.CreateInstance(domainContext);
            //ShowDomainInfo(domain);

            // Create a TextReader that reads from a file
            tr = new StreamReader(problemFileName);

            // create a CharStream that reads from standard input
            input = new AntlrInputStream(tr);
            // create a lexer that feeds off of input CharStream

            lexer = new PlanningLexer(input);
            // create a buffer of tokens pulled from the lexer
            tokens = new CommonTokenStream(lexer);
            // create a parser that feeds off the tokens buffer
            parser = new PlanningParser(tokens);

            var problemContext = parser.clientProblem();// begin parsing at init rule
            tr.Close();

            //ClientProblem problem = ClientProblem.CreateInstance(domain, problemContext);
            //problem.ShowInfo();

            //Server server = new Server(port, listenBacklog, problem);
            //server.Run();
            Console.ReadLine();
        }

        private static void Test1()
        {
            try
            {
                string domainFileName = "swDomain.pddl";
                string problomFileName = "swClientProblem.pddl";

                string programFileName = "program.pddl";
                Client client = new Client(domainFileName, problomFileName, programFileName);
                client.Connect();
                client.Handshake();
                Console.WriteLine("Start to execute program");
                Console.ReadLine();
                client.ExecuteProgram();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                Console.ReadLine();
            }

        }

        //public static void ShowDomainInfo(Domain<ClientAction> domain)
        //{
        //    Console.WriteLine("Name: {0}", domain.Name);
        //    Console.WriteLine(Domain<ClientAction>.BarLine);

        //    Console.Write("Types: ");
        //    for (int i = 0; i < domain.TypeList.Count - 1; i++)
        //    {
        //        Console.Write("{0}, ", domain.TypeList[i]);
        //    }
        //    Console.WriteLine("{0}", domain.TypeList[domain.TypeList.Count - 1]);
        //    Console.WriteLine(Domain<ClientAction>.BarLine);

        //    Console.WriteLine("Predicates:");
        //    foreach (var pred in domain.PredicateDict.Values)
        //    {
        //        Console.WriteLine("  Name: {0}", pred.Name);
        //        Console.WriteLine("  Variable: {0}", pred.Count);
        //        for (int i = 0; i < pred.Count; i++)
        //        {
        //            Console.WriteLine("    Index: {0}, Name: {1}, Type: {2}", i, pred.VariableList[i].Item1,
        //                pred.VariableList[i].Item2);
        //        }
        //        Console.WriteLine();
        //    }
        //    Console.WriteLine(Domain<ClientAction>.BarLine);

        //    Console.WriteLine("Actions:");
        //    foreach (var action in domain.ActionDict.Values)
        //    {
        //        Console.WriteLine("  Name: {0}", action.Name);
        //        Console.WriteLine("  Variable: {0}", action.Count);
        //        for (int i = 0; i < action.Count; i++)
        //        {
        //            Console.WriteLine("    Index: {0}, Name: {1}, Type: {2}", i, action.VariableList[i].Item1,
        //                action.VariableList[i].Item2);
        //        }

        //        Console.WriteLine("    Abstract Predicates: ");
        //        foreach (var pair in action.AbstractPredicateDict)
        //        {
        //            Console.WriteLine("      Name: {0}, Previous index: {1}, Successor index: {2}", pair.Key, pair.Value.CuddIndexList[0], pair.Value.CuddIndexList[1]);
        //        }

        //        Console.WriteLine("  Precondition:");
        //        CUDD.Print.PrintMinterm(action.Precondition);

        //        Console.WriteLine("  Effect:");
        //        for (int i = 0; i < action.Effect.Count; i++)
        //        {
        //            Console.WriteLine("      Index:{0}", i);
        //            Console.WriteLine("      Condition:");
        //            CUDD.Print.PrintMinterm(action.Effect[i].Item1);

        //            Console.Write("      Literals: { ");
        //            var literal = action.Effect[i].Item2[0];
        //            if (literal.Item2)
        //            {
        //                Console.Write("{0}", literal.Item1);
        //            }
        //            else
        //            {
        //                Console.Write("not {0}", literal.Item1);
        //            }

        //            for (int j = 1; j < action.Effect[i].Item2.Count; j++)
        //            {
        //                if (literal.Item2)
        //                {
        //                    Console.Write(", {0}", literal.Item1);
        //                }
        //                else
        //                {
        //                    Console.Write(", not {0}", literal.Item1);
        //                }
        //            }

        //            Console.WriteLine(" }");
        //        }

        //        Console.WriteLine("  Successor state axiom:");
        //        CUDD.Print.PrintMinterm(action.SuccessorStateAxiom);

        //        Console.WriteLine();
        //    }
        //}

       }
}
