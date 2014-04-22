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
using Agents.HighLevelPrograms;

namespace Agents
{
    class Program
    {
        static void Main(string[] args)
        {
            //string programFileName = @"program.edp";
            //Test2(programFileName);
            Test1();
            Console.ReadLine();
        }

        private static void Test1()
        {
            string domainFileName = "d1.pddl";
            string problomFileName = "a1.pddl";

            string programFileName = "program.edp";
            Client agent = new Client(domainFileName, problomFileName, programFileName);
            agent.Connect();
            agent.ExecuteActions();
            
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
