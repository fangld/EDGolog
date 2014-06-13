using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanguageRecognition;
//using ObjectWorlds.Network;
using PAT.Common.Classes.CUDDLib;
using Planning;
//using Planning.Servers;

namespace ObjectWorlds
{
    public class Program
    {
        private const int port = 888;
        private const int listenBacklog = 1;

        public static void Main(string[] args)
        {
            string domainFileName, problemFileName;
            if (args.Length == 3)
            {
                domainFileName = args[1];
                problemFileName = args[2];
            }
            else
            {
                domainFileName = "swDomain.pddl";
                problemFileName = "swServerProblem.pddl";
            }


            Test1(domainFileName, problemFileName);
            Console.ReadLine();
        }

        static void Test1(string domainFileName, string problemFileName)
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

            var domainContext = parser.domain();// begin parsing at init rule
            Console.WriteLine(domainContext.ToStringTree(parser));
            tr.Close();
            var domain = Domain.CreateInstance(domainContext);
            ShowDomainInfo(domain);

            //// Create a TextReader that reads from a file
            //tr = new StreamReader(problemFileName);

            //// create a CharStream that reads from standard input
            //input = new AntlrInputStream(tr);
            //// create a lexer that feeds off of input CharStream

            //lexer = new PlanningLexer(input);
            //// create a buffer of tokens pulled from the lexer
            //tokens = new CommonTokenStream(lexer);
            //// create a parser that feeds off the tokens buffer
            //parser = new PlanningParser(tokens);

            //var serverProblem = parser.serverProblem();// begin parsing at init rule
            //tr.Close();
            //ServerProblem problem = ServerProblem.CreateInstance(domain, serverProblem);
            //problem.ShowInfo();

            //Server server = new Server(port, listenBacklog, problem);
            //server.Run();
            Console.ReadLine();
        }

        static void ShowDomainInfo(Domain domain)
        {
            Console.WriteLine("Name: {0}", domain.Name);
            Console.WriteLine(Domain.BarLine);

            Console.Write("Types: ");
            for (int i = 0; i < domain.TypeList.Count - 1; i++)
            {
                Console.Write("{0}, ", domain.TypeList[i]);
            }
            Console.WriteLine("{0}", domain.TypeList[domain.TypeList.Count - 1]);
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Predicates:");
            foreach (var pred in domain.PredicateDict.Values)
            {
                Console.WriteLine("  Name: {0}", pred.Name);
                Console.WriteLine("  Variable: {0}", pred.Count);
                for (int i = 0; i < pred.Count; i++)
                {
                    Console.WriteLine("    Index: {0}, Name: {1}, PlanningType: {2}", i, pred.VariableList[i].Item1,
                        pred.VariableList[i].Item2);
                }
                Console.WriteLine();
            }
            Console.WriteLine(Domain.BarLine);

            //Console.WriteLine("Actions:");
            //foreach (var action in domain.ActionDict.Values)
            //{
            //    Console.WriteLine("  Name: {0}", action.Name);
            //    Console.WriteLine("  Variable: {0}", action.Count);
            //    for (int i = 0; i < action.Count; i++)
            //    {
            //        Console.WriteLine("    Index: {0}, Name: {1}, PlanningType: {2}", i, action.VariableList[i].Item1,
            //            action.VariableList[i].Item2);
            //    }

            //    Console.WriteLine("    Abstract Predicates: ");
            //    foreach (var pair in action.AbstractPredicateDict)
            //    {
            //        Console.WriteLine("      Name: {0}, CuddIndex: {1}", pair.Key, pair.Value.CuddIndexList[0]);
            //    }
            //    Console.WriteLine("  Precondition:");
            //    CUDD.Print.PrintMinterm(action.Precondition);

            //    Console.WriteLine("  Effect:");
            //    for (int i = 0; i < action.Effect.Count; i++)
            //    {
            //        Console.WriteLine("      Index:{0}", i);
            //        Console.WriteLine("      Condition:");
            //        CUDD.Print.PrintMinterm(action.Effect[i].Item1);

            //        Console.Write("      Literals: { ");
            //        var literal = action.Effect[i].Item2[0];
            //        if (literal.Item2)
            //        {
            //            Console.Write("{0}", literal.Item1);
            //        }
            //        else
            //        {
            //            Console.Write("not {0}", literal.Item1);
            //        }

            //        for (int j = 1; j < action.Effect[i].Item2.Count; j++)
            //        {
            //            if (literal.Item2)
            //            {
            //                Console.Write(", {0}", literal.Item1);
            //            }
            //            else
            //            {
            //                Console.Write(", not {0}", literal.Item1);
            //            }
            //        }

            //        Console.WriteLine(" }");
            //    }

            //    Console.WriteLine();
            //}
        }

        static void Test2()
        {
            //CUDDNode a, b, c;
            //CUDDVars vars;

            Console.WriteLine("\nTest program for CUDD\n====================");

            // initialise cudd
            CUDD.InitialiseCUDD(256, 256, 262144, 0.1);

            CUDDNode x0 = CUDD.Var(0);
            CUDDNode x1 = CUDD.Var(1);

            CUDDNode xor = CUDD.Function.Xor(x0, x1);
            CUDD.Ref(xor);

            CUDDNode and1 = CUDD.Function.And(x0, CUDD.Function.Not(x1));
            CUDD.Ref(and1);
            CUDDNode and2 = CUDD.Function.And(x0, x1);
            CUDD.Ref(and2);
            CUDDNode sum = CUDD.Function.Or(and1, and2);
            CUDD.Ref(sum);
            CUDD.Deref(and1);
            CUDD.Deref(and2);

            CUDDNode and = CUDD.Function.And(x0, CUDD.Function.Not(x0));
            CUDD.Ref(and);

            CUDDNode or = CUDD.Function.Or(x0, CUDD.Function.Not(x0));
            CUDD.Ref(or);

            //CUDDNode node = CUDD.Function.Equal(and, CUDD.ZERO);
            CUDDNode node = CUDD.Function.Equal(or, CUDD.ZERO);

            Console.WriteLine(node.GetValue());

            //CUDDNode xor2 = CUDD.Function.Or()

            //CUDDNode and1 = CUDD.Function.And(x0, CUDD.Function.Not(x1));
            //CUDD.Ref(and1);

            //CUDDNode and2 = CUDD.Function.And(CUDD.Function.Not(x0), x1);
            //CUDD.Ref(and2);

            //CUDDNode sum = CUDD.Function.Or(and1, and2);
            //CUDD.Ref(sum);

            //CUDD.Deref(and1);
            //CUDD.Deref(and2);

            //CUDDNode carry = CUDD.Function.And(x0, x1);
            //CUDD.Ref(carry);

            //int size = 4;
            //CUDDNode[] restrictBy = new CUDDNode[4];
            //CUDDNode[] testSum = new CUDDNode[4];
            //CUDDNode[] testCarry = new CUDDNode[4];

            //restrictBy[0] = CUDD.Function.And(CUDD.Function.Not(x0), CUDD.Function.Not(x1));
            //restrictBy[1] = CUDD.Function.And(CUDD.Function.Not(x0), x1);
            //restrictBy[2] = CUDD.Function.And(x0, CUDD.Function.Not(x1));
            //restrictBy[3] = CUDD.Function.And(x0, x1);

            //for (int i = 0; i < size; i++)
            //{
            //    CUDD.Ref(restrictBy[i]);

            //    testSum[i] = CUDD.Function.Restrict(sum, restrictBy[i]);
            //    testCarry[i] = CUDD.Function.Restrict(carry, restrictBy[i]);
            //    CUDD.Deref(restrictBy[i]);
            //}

            //CUDD.Print.PrintBDDTree(testSum[0]);

            //for (int i = 0; i < size; i++)
            //{
            //    CUDD.Deref(testSum[i]);
            //    CUDD.Deref(testSum[i]);
            //}
        }
    }
}
