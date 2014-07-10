using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Tree;
using LanguageRecognition;
//using ObjectWorlds.Network;
using PAT.Common.Classes.CUDDLib;
using Planning;
//using Planning.Servers;
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
                domainFileName = "swDomain.pddl";
                problemFileName = "swServerProblem.pddl";
            }

            Test1(domainFileName, problemFileName);

            //Test2();

            //Test3();
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
            //Console.WriteLine(domainContext.ToStringTree(parser));
            tr.Close();
            //var domain = Domain.CreateInstance(domainContext);
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

            var serverProblemContext = parser.serverProblem();// begin parsing at init rule
            //Console.WriteLine(serverProblemContext.ToStringTree(parser));
            tr.Close();
            //Console.ReadLine();
            ServerProblem problem = ServerProblem.CreateInstance(domainContext, serverProblemContext);
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

            //Console.WriteLine("Predicates:");
            //foreach (var pred in domain.PredicateDict.Values)
            //{
            //    Console.WriteLine("  Name: {0}", pred.Name);
            //    Console.WriteLine("  Variable: {0}", pred.Count);
            //    for (int i = 0; i < pred.Count; i++)
            //    {
            //        Console.WriteLine("    Index: {0}, Name: {1}, PlanningType: {2}", i, pred.VariableList[i].Item1,
            //            pred.VariableList[i].Item2);
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine(Domain.BarLine);

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

            CUDDNode firstOne = CUDD.Constant(1);
            Console.WriteLine("initial one: {0}", CUDD.GetReference(firstOne));

            CUDDNode firstZero = CUDD.Constant(0);
            Console.WriteLine("initial zero: {0}", CUDD.GetReference(firstZero));

            CUDDNode x0 = CUDD.Var(0);
            CUDDNode x1 = CUDD.Var(1);
            Console.WriteLine("var({0}): {1}", 0, CUDD.GetReference(x0));
            Console.WriteLine("var({0}): {1}", 1, CUDD.GetReference(x1));
            CUDDNode andNode = CUDD.Function.And(x0, x1);

            //CUDD.Ref(andNode);
            Console.WriteLine("var({0}): {1}", 0, CUDD.GetReference(x0));
            Console.WriteLine("var({0}): {1}", 1, CUDD.GetReference(x1));
            Console.WriteLine("and node: {0}", CUDD.GetReference(andNode));
            //Console.WriteLine(CUDD.GetReference(andNode));

            //CUDDNode one = CUDD.Constant(1);
            //Console.WriteLine("initial one: {0}", CUDD.GetReference(one));

            //CUDDNode f, var, tmp;
            //f = CUDD.Constant(1);
            //Console.WriteLine("initial f: {0}", CUDD.GetReference(f));
            //CUDD.Ref(f);
            //Console.WriteLine("after ref f: {0}", CUDD.GetReference(f));

            //for (int i = 3; i >= 0; i--)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine(i);
            //    var = CUDD.Var(i);
            //    Console.WriteLine("Before and var({0}): {1}", i, CUDD.GetReference(var));
            //    Console.WriteLine();

            //    tmp = CUDD.Function.And(CUDD.Function.Not(var), f);
            //    Console.WriteLine("After and var({0}): {1}", i, CUDD.GetReference(var));
            //    Console.WriteLine("After and f: {0}", CUDD.GetReference(f));
            //    Console.WriteLine("After and tmp: {0}", CUDD.GetReference(tmp));
            //    Console.WriteLine();

            //    CUDD.Ref(tmp);
            //    Console.WriteLine("After ref tmp: {0}", CUDD.GetReference(tmp));
            //    Console.WriteLine();

            //    CUDD.Deref(f);
            //    Console.WriteLine("After deref f: {0}", CUDD.GetReference(f));
            //    Console.WriteLine();

            //    f = tmp;
            //    Console.WriteLine("After assignment f: {0}", CUDD.GetReference(f));
            //    Console.WriteLine("After assignment tmp: {0}", CUDD.GetReference(tmp));

            //}







            //CUDDNode x0 = CUDD.Var(0);
            ////CUDDNode notx0 = CUDD.Function.Not(CUDD.Var(0));
            ////Console.WriteLine("notx0: {0}", CUDD.GetReference(notx0));
            //CUDDNode x1 = CUDD.Var(1);
            //CUDDNode x2 = CUDD.Var(2);
            //CUDDNode x3 = CUDD.Var(3);


            ////CUDD.Ref(x1);

            //Console.WriteLine("x0: {0}", CUDD.GetReference(x0));
            //Console.WriteLine("x1: {0}", CUDD.GetReference(x1));
            //Console.WriteLine("x2: {0}", CUDD.GetReference(x2));
            //Console.WriteLine("x3: {0}", CUDD.GetReference(x3));
            //Console.WriteLine();


            //CUDDNode andNode1 = CUDD.Function.And(x0, x1);
            //CUDD.Ref(andNode1);
            //Console.WriteLine("x0: {0}", CUDD.GetReference(x0));
            //Console.WriteLine("x1: {0}", CUDD.GetReference(x1));
            //Console.WriteLine("andNode1: {0}", CUDD.GetReference(andNode1));
            //Console.WriteLine();


            //CUDDNode andNode2 = CUDD.Function.And(x2, x3);
            //CUDD.Ref(andNode2);
            ////CUDD.Deref(orNode);
            //Console.WriteLine("x2: {0}", CUDD.GetReference(x2));
            //Console.WriteLine("x3: {0}", CUDD.GetReference(x3));
            //Console.WriteLine("andNode2: {0}", CUDD.GetReference(andNode2));
            //Console.WriteLine();


            //CUDDNode orNode = CUDD.Function.Or(andNode2, andNode1);
            //CUDD.Ref(orNode);
            //CUDD.Deref(andNode1);
            //CUDD.Deref(andNode2);


            //Console.WriteLine("x0: {0}", CUDD.GetReference(x0));
            //Console.WriteLine("x1: {0}", CUDD.GetReference(x1));
            //Console.WriteLine("x2: {0}", CUDD.GetReference(x2));
            //Console.WriteLine("x3: {0}", CUDD.GetReference(x3));
            //Console.WriteLine("andNode1: {0}", CUDD.GetReference(andNode1));
            //Console.WriteLine("andNode2: {0}", CUDD.GetReference(andNode2));
            //Console.WriteLine("orNode: {0}", CUDD.GetReference(orNode));



            //CUDD.Deref(orNode);
            //Console.WriteLine("andNode: {0}", CUDD.GetReference(andNode));
            //Console.WriteLine("orNode: {0}", CUDD.GetReference(orNode));
            //Console.WriteLine("x2: {0}", CUDD.GetReference(x2));

            //CUDD.Deref(orNode);
            //Console.WriteLine("andNode: {0}", CUDD.GetReference(andNode));
            //Console.WriteLine("orNode: {0}", CUDD.GetReference(orNode));
            //Console.WriteLine("x2: {0}", CUDD.GetReference(x2));

            //CUDD.Deref(orNode);
            //Console.WriteLine("andNode: {0}", CUDD.GetReference(andNode));
            //Console.WriteLine("orNode: {0}", CUDD.GetReference(orNode));
            //Console.WriteLine("x2: {0}", CUDD.GetReference(x2));



            //CUDDNode orNode = CUDD.Function.Or(x0, x1);

            //Console.WriteLine("x0: {0}", CUDD.GetReference(x0));
            //Console.WriteLine("x1: {0}", CUDD.GetReference(x1));
            //Console.WriteLine("orNode: {0}", CUDD.GetReference(orNode));

            //CUDDNode impliesNode = CUDD.Function.Implies(x0, x1);

            //Console.WriteLine("x0: {0}", CUDD.GetReference(x0));
            //Console.WriteLine("x1: {0}", CUDD.GetReference(x1));
            //Console.WriteLine("impliesNode: {0}", CUDD.GetReference(impliesNode));

            //CUDDNode f = CUDD.ZERO;
            //CUDDNode var, tmp;
            //CUDD.Ref(f);
            //int count = 1000;

            //CUDD.Var(count);
            //for (int i = count; i >= 0; i--)
            ////for (int i = 0; i <= count; i++)
            //{
            //    var = CUDD.Var(i);
            //    //if (i % 6 == 0)
            //    //{
            //        tmp = CUDD.Function.Or(var, f);
            //    //}
            //    //else
            //    //{
            //    //    tmp = CUDD.Function.And(var, f);
            //    //}
            //    CUDD.Ref(tmp);
            //    CUDD.Deref(f);

            //    f = tmp;
            //    if (i % 1000 == 0)
            //    {
            //        Console.WriteLine("Index: {0}", i);
            //        Console.WriteLine("Memory: {0} Kbytes", CUDD.ReadMemoryInUse() / 1024);
            //        //Console.WriteLine("Number of nodes: {0}", CUDD.GetNumNodes(f));
            //        Console.WriteLine();
            //        CUDD.Debug.DebugCheck();
            //    }

            //}

            //CUDD.Print.PrintMinterm(f);

            //CUDDNode x0 = CUDD.Var(0);
            //CUDDNode x1 = CUDD.Var(1);
            //CUDDNode x2 = CUDD.Var(2);

            //CUDDNode effect = CUDD.Function.Implies(x2, x1);
            //CUDDNode frame = CUDD.Function.Implies(CUDD.Function.Not(x2), CUDD.Function.Equal(x0, x1));
            //CUDDNode firstSsa = CUDD.Function.And(effect, frame);

            //CUDDNode secondSsa = CUDD.Function.Equal(x1, CUDD.Function.Or(x2, x0));

            //CUDD.Print.PrintMinterm(firstSsa);
            //CUDD.Print.PrintMinterm(secondSsa);


            //CUDDNode xor = CUDD.Function.Xor(x0, x1);
            //CUDD.Ref(xor);

            //CUDDNode and1 = CUDD.Function.And(x0, CUDD.Function.Not(x1));
            //CUDD.Ref(and1);
            //CUDDNode and2 = CUDD.Function.And(x0, x1);
            //CUDD.Ref(and2);
            //CUDDNode sum = CUDD.Function.Or(and1, and2);
            //CUDD.Ref(sum);
            //CUDD.Deref(and1);
            //CUDD.Deref(and2);

            //CUDDNode and = CUDD.Function.And(x0, CUDD.Function.Not(x0));
            //CUDD.Ref(and);

            //CUDDNode or = CUDD.Function.Or(x0, CUDD.Function.Not(x0));
            //CUDD.Ref(or);

            ////CUDDNode node = CUDD.Function.Equal(and, CUDD.ZERO);
            //CUDDNode node = CUDD.Function.Equal(or, CUDD.ZERO);

            //Console.WriteLine(node.GetValue());

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

        private static void Test3()
        {
            //List<char> x = new List<char>(new[] {'a', 'b', 'c', 'd'});
            List<char> y = new List<char>(new[] {'1', '2', '3', '4'});
            List<List<char>> collection = new List<List<char>>(new[] {y, y, y, y});
            List<char[]> enumeration1 = new List<char[]>();
            List<char[]> enumeration2 = new List<char[]>();

            ScanMixedRadix(collection, chars =>
            {
                char[] array = new char[chars.Length];
                chars.CopyTo(array, 0);
                enumeration1.Add(array);
            });

            ScanMixedRadix2(collection, chars =>
            {
                char[] array = new char[chars.Length];
                chars.CopyTo(array, 0);
                enumeration2.Add(array);
            });

            bool isEqual = true;
            for (int i = 0; i < enumeration1.Count; i ++)
            {
                for (int j = 0; j < enumeration1[i].Length; j++)
                {
                    if (enumeration1[i][j] != enumeration2[i][j])
                    {
                        isEqual = false;
                        Console.WriteLine("1: {0}, 2: {1}", enumeration1[i], enumeration2[i]);
                        Console.WriteLine("false!");
                        break;
                    }
                }
            }
            Console.WriteLine(isEqual);
        }

        static void ScanMixedRadix<T>(IReadOnlyList<List<T>> collection, Action<T[]> action)
        {
            int count = collection.Count;
            T[] scanArray = new T[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i => maxIndex[i] = collection[i].Count);

            do
            {
                Parallel.For(0, count, i => scanArray[i] = collection[i][index[i]]);

                action(scanArray);

                int j = count - 1;
                while (j != -1)
                {
                    if (index[j] == maxIndex[j] - 1)
                    {
                        index[j] = 0;
                        j--;
                        continue;
                    }
                    break;
                }

                if (j == -1)
                    return;
                index[j]++;
            } while (true);
        }

        static void ScanMixedRadix2<T>(IReadOnlyList<List<T>> collection, Action<T[]> action)
        {
            int count = collection.Count;
            T[] scanArray = new T[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i =>
            {
                maxIndex[i] = collection[i].Count;
                scanArray[i] = collection[i][index[i]];
            });

            do
            {
                action(scanArray);

                int j = count - 1;
                while (j != -1)
                {
                    if (index[j] == maxIndex[j] - 1)
                    {
                        index[j] = 0;
                        j--;
                        continue;
                    }
                    break;
                }

                if (j == -1)
                    return;

                index[j]++;
                Parallel.For(j, count, i => scanArray[i] = collection[i][index[i]]);

            } while (true);
        }
    }
}
