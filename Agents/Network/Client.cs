using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//using Agents.HighLevelPrograms;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Agents.Network
{
    public class Client
    {
        #region Fields

        private Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private string _host;

        private int _port;

        private string _programFileName;

        private string _domainFileName;

        private string _problemFileName;

        private string _agentId;

        //private Problem _problem;

        private HighLevelProgramParser.ProgramContext _programContext;

        //private ProgramInterpretor _interpretor;

        //private MentalAttitude _mentalAttitude;

        #endregion

        #region Constructors

        public Client()
        {
            _host = "127.0.0.1";
            _port = 888;
            _serverSocket.Connect(_host, _port);

            Console.WriteLine("Please enter the agent id");
            _agentId = Console.ReadLine();
            SendMessage(_agentId);
            string action;
            string rcvMsg;

            if (_agentId == "a1")
            {
                do
                {
                    action = Console.ReadLine();
                    if (action != "quit")
                    {
                        SendMessage(action);
                        rcvMsg = ReceiveMessage();
                        Console.WriteLine("Receive response: {0}", rcvMsg);
                        rcvMsg = ReceiveMessage();
                        Console.WriteLine("Receive observation: {0}", rcvMsg);
                    }
                    else
                    {
                        break;
                    }
                } while (true);
            }
            else
            {
                do
                {
                    rcvMsg = ReceiveMessage();
                    Console.WriteLine("Receive observation: {0}", rcvMsg);

                    action = Console.ReadLine();
                    if (action != "quit")
                    {
                        SendMessage(action);
                        rcvMsg = ReceiveMessage();
                        Console.WriteLine("Receive response: {0}", rcvMsg);
                    }
                    else
                    {
                        break;
                    }
                } while (true);
            }
        }

        public Client(string domainFileName, string problemFileName, string programFileName)
        {
            _host = "127.0.0.1";
            _port = 888;
            //_interpretor = new ProgramInterpretor();

            //Initial(domainFileName, problemFileName, programFileName);
            //_mentalAttitude = new MentalAttitude(_problem);
        }

        #endregion

        #region Methods

        //private void Initial(string domainFileName, string problemFileName, string programFileName)
        //{
        //    CUDD.InitialiseCUDD(256, 256, 262144, 0.1);

        //    // Create a TextReader that reads from a file
        //    TextReader tr = new StreamReader(domainFileName);

        //    // create a CharStream that reads from standard input
        //    AntlrInputStream input = new AntlrInputStream(tr);
        //    // create a lexer that feeds off of input CharStream

        //    ITokenSource lexer = new PlanningLexer(input);
        //    // create a buffer of tokens pulled from the lexer
        //    CommonTokenStream tokens = new CommonTokenStream(lexer);
        //    // create a parser that feeds off the tokens buffer
        //    PlanningParser parser = new PlanningParser(tokens);

        //    IParseTree tree = parser.domain();// begin parsing at init rule

        //    // Create a generic parse tree walker that can trigger callbacks 
        //    ParseTreeWalker walker = new ParseTreeWalker();
        //    // Walk the tree created during the parse, trigger callbacks 
        //    DomainLoader domainLoader = new DomainLoader();
        //    walker.Walk(domainLoader, tree);
        //    tr.Close();
        //    Domain domain = domainLoader.Domain;
        //    domain.ShowInfo();

        //    // Create a TextReader that reads from a file
        //    tr = new StreamReader(problemFileName);

        //    // create a CharStream that reads from standard input
        //    input = new AntlrInputStream(tr);
        //    // create a lexer that feeds off of input CharStream

        //    lexer = new PlanningLexer(input);
        //    // create a buffer of tokens pulled from the lexer
        //    tokens = new CommonTokenStream(lexer);
        //    // create a parser that feeds off the tokens buffer
        //    parser = new PlanningParser(tokens);

        //    tree = parser.clientProblem();// begin parsing at init rule

        //    // Create a generic parse tree walker that can trigger callbacks 
        //    walker = new ParseTreeWalker();
        //    // Walk the tree created during the parse, trigger callbacks 
        //    ProblemLoader problemLoader = new ProblemLoader(domain);
        //    walker.Walk(problemLoader, tree);
        //    tr.Close();
        //    _problem = problemLoader.Problem;
        //    _problem.ShowInfo();

        //    // Create a TextReader that reads from a file
        //    tr = new StreamReader(programFileName);

        //    // create a CharStream that reads from standard input
        //    input = new AntlrInputStream(tr);
        //    // create a lexer that feeds off of input CharStream

        //    lexer = new HighLevelProgramLexer(input);
        //    // create a buffer of tokens pulled from the lexer
        //    tokens = new CommonTokenStream(lexer);
        //    // create a parser that feeds off the tokens buffer
        //    var programParser = new HighLevelProgramParser(tokens);

        //    _programContext = programParser.program();// begin parsing at program rule
        //    //_interpretor.EnterProgram(_programContext);
        //    Console.WriteLine("------------------");
        //}

        public void Connect()
        {
            _serverSocket.Connect(_host, _port);
            //SendMessage(_problem.AgentId);
            //_mentalAttitude.ShowInfo();
        }

        public void ExecuteActions()
        {
            //Execute(_programContext);
        }

        public void ExecutionAction(string name, params string[] parmList)
        {
            StringBuilder sb = new StringBuilder();
            if (parmList.Length != 0)
            {
                sb.AppendFormat("{0}(", name);
                for (int i = 0; i < parmList.Length - 1; i++)
                {
                    sb.AppendFormat("{0},", parmList[i]);
                }
                sb.AppendFormat("{0})", parmList[parmList.Length - 1]);
            }
            else
            {
                sb.AppendFormat("{0}()", name);
            }
            SendMessage(sb.ToString());
        }

        //private void Execute(HighLevelProgramParser.ProgramContext context)
        //{
        //    if (context.action() != null)
        //    {
        //        Execute(context.action());
        //    }
        //    else if (context.SEMICOLON() != null)
        //    {
        //        Execute(context.program()[0]);
        //        Execute(context.program()[1]);
        //    }
        //    else if (context.IF() != null)
        //    {
        //        if (_mentalAttitude.Implies(context.subjectFormula().objectFormula()))
        //        {
        //            Execute(context.program()[0]);
        //        }
        //        else if (context.ELSE() == null)
        //        {
        //            Execute(context.program()[1]);
        //        }
        //    }
        //    else if (context.WHILE() != null)
        //    {
        //        if (_mentalAttitude.Implies(context.subjectFormula().objectFormula()))
        //        {
        //            Execute(context.program()[0]);
        //        }
        //    }
        //}

        //private void Execute(HighLevelProgramParser.ActionContext context)
        //{
        //    string gndActionName = context.GetText();
        //    Console.WriteLine("Action: {0}", gndActionName);
        //    Console.WriteLine("Press enter to execute this action and show the infomation of the next action.");
        //    Console.ReadLine();
        //    GroundAction gndAction = _problem.GroundActionDict[gndActionName];
        //    SendMessage(context.GetText());
        //    _mentalAttitude.Update(gndAction);
        //    _mentalAttitude.ShowInfo();
        //}

        private void SendMessage(string message)
        {
            byte[] lengthBuffer = new byte[4];
            byte[] contentBuffer = StringToBytes(message);
            SetBytesLength(lengthBuffer, contentBuffer.Length);
            _serverSocket.Send(lengthBuffer);
            _serverSocket.Send(contentBuffer);

            Console.WriteLine("Send {0}", message);
        }

        private string ReceiveMessage()
        {
            byte[] lengthBuffer = new byte[4];
            int offset = 0;
            int remaining = 4;

            do
            {
                int count = _serverSocket.Receive(lengthBuffer, offset, remaining, SocketFlags.None);
                offset += count;
                remaining -= count;
            } while (offset < 4);

            int contentBufferCount = GetLength(lengthBuffer);
            byte[] contentBuffer = new byte[contentBufferCount];
            offset = 0;
            remaining = contentBufferCount;

            do
            {
                int count = _serverSocket.Receive(contentBuffer, offset, remaining, SocketFlags.None);
                offset += count;
                remaining -= count;
            } while (offset < contentBufferCount);

            string result = BytesToString(contentBuffer);
            return result;
        }

        private byte[] StringToBytes(string message)
        {
            int length = message.Length;
            byte[] result = new byte[length];
            Parallel.For(0, length, i => result[i] = (byte)message[i]);
            return result;
        }

        private string BytesToString(byte[] bytes)
        {
            return BytesToString(bytes, 0, bytes.Length);
        }

        private string BytesToString(byte[] bytes, int offset, int count)
        {
            char[] chars = new char[count];
            Parallel.For(offset, count, i => chars[i - offset] = (char)bytes[i]);
            return new string(chars);
        }

        private void SetBytesLength(byte[] bytes, int length)
        {
            bytes[0] = (byte)(length >> 24);
            bytes[1] = (byte)(length >> 16);
            bytes[2] = (byte)(length >> 8);
            bytes[3] = (byte)(length);
        }

        private int GetLength(byte[] bytes)
        {
            int result = 0;
            result |= (bytes[0] << 24);
            result |= (bytes[1] << 16);
            result |= (bytes[2] << 8);
            result |= (bytes[3]);
            return result;
        }

        #endregion
    }
}
