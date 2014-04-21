using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agents.Network;
using LanguageRecognition;

namespace Agents
{
    class Program
    {
        static void Main(string[] args)
        {
            string programFileName = @"program.edp";
            Test2(programFileName);
        }

        private static void Test1()
        {
            Client agent = new Client();
            agent.Connect();
            do
            {
                string action = Console.ReadLine();
                if (action[0] == 'd')
                {
                    string bombName = string.Format("bomb{0}", action[1]);
                    string toiletName = string.Format("toilet{0}", action[2]);

                    agent.ExecutionAction("dunk", bombName, toiletName);
                }
                else if (action[0] == 'f')
                {
                    string toiletName = string.Format("toilet{0}", action[1]);
                    agent.ExecutionAction("flush", toiletName);
                }
            } while (true);
        }

        private static void Test2(string programFileName)
        {
            TextReader tr = new StreamReader(programFileName);

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
        }
    }
}
