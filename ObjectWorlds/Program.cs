using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanguageRecognition;
using ObjectWorlds.Network;
using PAT.Common.Classes.CUDDLib;
using Planning;
using Planning.Servers;

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
                domainFileName = "d1.pddl";
                problemFileName = "p1.pddl";
            }


            Test1(domainFileName, problemFileName);
            //Test2();
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

            var doaminContext = parser.domain();// begin parsing at init rule
            ServerDomainLoader domainLoader = new ServerDomainLoader();
            domainLoader.HandleDomain(doaminContext);
            tr.Close();
            var domain = domainLoader.Domain;
            domain.ShowInfo();

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

            var serverProblem = parser.serverProblem();// begin parsing at init rule

            // Walk the tree created during the parse, trigger callbacks 
            ServerProblemLoader problemLoader = new ServerProblemLoader(domain);
            problemLoader.HandleServerProblem(serverProblem);
            tr.Close();
            ServerProblem problem = problemLoader.Problem;
            problem.ShowInfo();

            Server server = new Server(port, listenBacklog, problem);
            server.Run();
            Console.ReadLine();
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
