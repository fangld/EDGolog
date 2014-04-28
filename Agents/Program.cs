using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanguageRecognition;
using Agents.HighLevelPrograms;
using PAT.Common.Classes.CUDDLib;
using Planning.Clients;

namespace Agents
{
    class Program
    {
        static void Main(string[] args)
        {
            //string programFileName = @"program.edp";
            //Test2(programFileName);
            //Test1();
            string domainFileName, problemFileName;
            if (args.Length == 3)
            {
                domainFileName = args[1];
                problemFileName = args[2];
            }
            else
            {
                domainFileName = "d1.pddl";
                problemFileName = "a1.pddl";
            }
            Test3(domainFileName, problemFileName);
            Console.ReadLine();
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

            var domainContext = parser.domain();// begin parsing at init rule
            ClientDomainLoader domainLoader = new ClientDomainLoader();
            domainLoader.HandleDomain(domainContext);
            tr.Close();
            var domain = domainLoader.Domain;
            domain.ShowInfo();

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

            //tree = parser.serverProblem();// begin parsing at init rule

            //// Create a generic parse tree walker that can trigger callbacks 
            //walker = new ParseTreeWalker();
            //// Walk the tree created during the parse, trigger callbacks 
            //ProblemLoader problemLoader = new ProblemLoader(domain);
            //walker.Walk(problemLoader, tree);
            //tr.Close();
            //Problem problem = problemLoader.Problem;
            //problem.ShowInfo();

            //Server server = new Server(port, listenBacklog, problem);
            //server.Run();
            Console.ReadLine();
        }

        private static void Test1()
        {
            string domainFileName = "d1.pddl";
            string problomFileName = "a1.pddl";

            string programFileName = "program.edp";
            //Client agent = new Client(domainFileName, problomFileName, programFileName);
            //agent.Connect();
            //agent.ExecuteActions();
            
        }

        private static void Test2(string programFileName)
        {
            TextReader tr = new StreamReader(programFileName);

            // create a CharStream that reads from standard input
            AntlrInputStream input = new AntlrInputStream(tr);
            // create a lexer that feeds off of input CharStream

            HighLevelProgramLexer lexer = new HighLevelProgramLexer(input);
            // create a buffer of tokens pulled from the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // create a parser that feeds off the tokens buffer
            HighLevelProgramParser parser = new HighLevelProgramParser(tokens);

            HighLevelProgramParser.ProgramContext programContext = parser.program();// begin parsing at init rule
            ProgramInterpretor interpretor = new ProgramInterpretor();
            interpretor.EnterProgram(programContext);
            //Console.WriteLine(programContext.GetText());

            tr.Close();
        }
    }
}
