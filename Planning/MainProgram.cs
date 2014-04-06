using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanguageRecognition;

namespace Planning
{
    public class MainProgram
    {
        private const int port = 888;
        private const int listenBacklog = 1;
        
        public static void Main()
        {
            //Test1();
            //Test2();
            //Test3();
            Test4();
        }

        static void Test1()
        {
            // Create a TextReader that reads from a file
            TextReader tr = new StreamReader(@"E:\EDGolog\Planning\d1.pddl");

            // create a CharStream that reads from standard input
            AntlrInputStream input = new AntlrInputStream(tr);
            // create a lexer that feeds off of input CharStream

            PlanningLexer lexer = new PlanningLexer(input);
            // create a buffer of tokens pulled from the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // create a parser that feeds off the tokens buffer
            PlanningParser parser = new PlanningParser(tokens);

            IParseTree tree = parser.domain();// begin parsing at init rule

            // Create a generic parse tree walker that can trigger callbacks 
            ParseTreeWalker walker = new ParseTreeWalker();
            // Walk the tree created during the parse, trigger callbacks 
            DomainLoader loader = new DomainLoader();
            walker.Walk(loader, tree);
            loader.ShowInfo();

            //Console.WriteLine(); // print a \n after translation
            //Console.WriteLine(tree.ToStringTree((parser))); // print LISP-style tree
            tr.Close();
        }

        static void Test2()
        {
            // Create a TextReader that reads from a file
            TextReader tr = new StreamReader(@"E:\EDGolog\Planning\p1.pddl");

            // create a CharStream that reads from standard input
            AntlrInputStream input = new AntlrInputStream(tr);
            // create a lexer that feeds off of input CharStream

            PlanningLexer lexer = new PlanningLexer(input);
            // create a buffer of tokens pulled from the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // create a parser that feeds off the tokens buffer
            PlanningParser parser = new PlanningParser(tokens);

            IParseTree tree = parser.problem();// begin parsing at init rule

            // Create a generic parse tree walker that can trigger callbacks 
            ParseTreeWalker walker = new ParseTreeWalker();
            // Walk the tree created during the parse, trigger callbacks 
            ProblemLoader loader = new ProblemLoader();
            walker.Walk(loader, tree);
            loader.ShowInfo();

            //Console.WriteLine(); // print a \n after translation
            //Console.WriteLine(tree.ToStringTree((parser))); // print LISP-style tree
            tr.Close();
        }

        static void Test4()
        {
            // Create a TextReader that reads from a file
            TextReader tr = new StreamReader(@"E:\EDGolog\Planning\d1.pddl");

            // create a CharStream that reads from standard input
            AntlrInputStream input = new AntlrInputStream(tr);
            // create a lexer that feeds off of input CharStream

            PlanningLexer lexer = new PlanningLexer(input);
            // create a buffer of tokens pulled from the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // create a parser that feeds off the tokens buffer
            PlanningParser parser = new PlanningParser(tokens);

            IParseTree tree = parser.domain();// begin parsing at init rule

            // Create a generic parse tree walker that can trigger callbacks 
            ParseTreeWalker walker = new ParseTreeWalker();
            // Walk the tree created during the parse, trigger callbacks 
            DomainLoader domainLoader = new DomainLoader();
            walker.Walk(domainLoader, tree);
            tr.Close();

            // Create a TextReader that reads from a file
            tr = new StreamReader(@"E:\EDGolog\Planning\p1.pddl");

            // create a CharStream that reads from standard input
            input = new AntlrInputStream(tr);
            // create a lexer that feeds off of input CharStream

            lexer = new PlanningLexer(input);
            // create a buffer of tokens pulled from the lexer
            tokens = new CommonTokenStream(lexer);
            // create a parser that feeds off the tokens buffer
            parser = new PlanningParser(tokens);

            tree = parser.problem();// begin parsing at init rule

            // Create a generic parse tree walker that can trigger callbacks 
            walker = new ParseTreeWalker();
            // Walk the tree created during the parse, trigger callbacks 
            ProblemLoader problemLoader = new ProblemLoader();
            walker.Walk(problemLoader, tree);

            Server server = new Server(port, listenBacklog, domainLoader, problemLoader);
            server.ShowInfo();
            server.Listen();
            Console.ReadLine();
        }

        public static void ScanMixedRadix<T>(List<List<T>> collection, Action<T[]> action)
        {
            int count = collection.Count;
            T[] scanArray = new T[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            for (int i = 0; i < count; i++)
            {
                maxIndex[i] = collection[i].Count;
            }

            do
            {
                for (int i = 0; i < count; i++)
                {
                    scanArray[i] = collection[i][index[i]];
                }
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

        private static void Show(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write("{0} ", array[i]);
            }
            Console.WriteLine();
        }

        public static void Test3()
        {
            List<int> array = new List<int> {1, 2, 3, 4};
            List<int> array3 = new List<int> {1, 2};
            List<List<int>> collection = new List<List<int>> {array, array, array3};
            ScanMixedRadix(collection, Show);
        }
    }
}
