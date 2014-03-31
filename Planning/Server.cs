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
    public class Server
    {
        public static void Main()
        {
            Test1();
            //Test2();
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
            ShowDomainLoader(loader);

            //Console.WriteLine(); // print a \n after translation
            //Console.WriteLine(tree.ToStringTree((parser))); // print LISP-style tree
            tr.Close();
        }

        static void ShowDomainLoader(DomainLoader loader)
        {
            const string barline = "----------------";

            Console.WriteLine("Name: {0}", loader.Name);
            Console.WriteLine(barline);

            Console.WriteLine("Requirment:");
            Console.WriteLine("  strips: {0}", loader.Requirements.Strips);
            Console.WriteLine("  typing: {0}", loader.Requirements.Typing);
            Console.WriteLine(barline);

            Console.Write("Types: ");
            for (int i = 0; i < loader.ListType.Count - 1; i++)
            {
                Console.Write("{0}, ", loader.ListType[i]);
            }
            Console.WriteLine("{0}", loader.ListType[loader.ListType.Count - 1]);
            Console.WriteLine(barline);

            Console.WriteLine("Predicates:");
            foreach (var predDef in loader.PredicateDefinitions)
            {
                Console.WriteLine("  Name: {0}", predDef.Name);
                Console.WriteLine("  Variable: {0}", predDef.VariablesNum);
                for (int i = 0; i < predDef.VariablesNum; i++)
                {
                    Console.WriteLine("    Index: {0}, Type: {1}", i, predDef.ListVariablesType[i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine(barline);

            Console.WriteLine("Actions:");
            foreach (var actDef in loader.ActionDefinitions)
            {
                Console.WriteLine("  Name: {0}", actDef.Name);
                Console.WriteLine("  Variable: {0}", actDef.VariablesNum);
                for (int i = 0; i < actDef.VariablesNum; i++)
                {
                    Console.WriteLine("    Index: {0}, Type: {1}", i, actDef.ListVariablesType[i]);
                }
                Console.WriteLine("  Precondition: {0}", actDef.Precondition);
                Console.WriteLine("  Effect: {0}", actDef.Effect);
                Console.WriteLine();
            }
        }

        static void Test2()
        {
            // Create a TextReader that reads from a file
            TextReader tr = new StreamReader(@"E:\EDGolog\Planning\p2.pddl");

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
            //DomainLoader loader = new DomainLoader();
            //walker.Walk(loader, tree);
            //ShowDomainLoader(loader);

            Console.WriteLine(); // print a \n after translation
            Console.WriteLine(tree.ToStringTree((parser))); // print LISP-style tree
            tr.Close();
        }
    }
}
