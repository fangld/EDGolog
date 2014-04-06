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
            Server server = new Server(port, listenBacklog);
            server.Listen();
            Console.ReadLine();
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
    }
}
