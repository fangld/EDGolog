using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Antlr4.Runtime;
using LanguageRecognition;
using ObjectWorlds.Network;
using PAT.Common.Classes.CUDDLib;
using Planning.Servers;

namespace SquirrelsWorlds
{
    public partial class SquirrelsWorldForm : Form
    {
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
            problem.ShowInfo();

            //Server server = new Server(port, listenBacklog, problem);
            //server.Run();
            Console.ReadLine();
        }

        public SquirrelsWorldForm()
        {
            InitializeComponent();
        }
    }
}
