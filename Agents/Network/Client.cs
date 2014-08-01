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
using Planning;
using Planning.Clients;
using Action = Planning.Action;

namespace Agents.Network
{
    public class Client
    {
        #region Fields

        private Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private string _host;

        private int _port;

        private string _agentId;

        private ClientProblem _problem;

        private PlanningParser.ProgramContext _programContext;

        private IReadOnlyDictionary<string, Action> _actionDict;

        private IReadOnlyDictionary<string, Observation> _observationDict;

        private MentalAttitude _mentalAttitude;

        #endregion

        #region Constructors
        
        public Client(string domainFileName, string problemFileName, string programFileName)
        {
            _host = "127.0.0.1";
            _port = 888;

            Initial(domainFileName, problemFileName, programFileName);
            _agentId = _problem.AgentId;
            Console.WriteLine(_agentId);
            _mentalAttitude = new MentalAttitude(_problem);
            _actionDict = _problem.ActionDict;
            _observationDict = _problem.ObservationDict;
        }

        #endregion

        #region Methods

        private void Initial(string domainFileName, string problemFileName, string programFileName)
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
            tr.Close();

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

            var clientProblemContext = parser.clientProblem();// begin parsing at client problem rule
            //Console.WriteLine(serverProblemContext.ToStringTree(parser));
            tr.Close();
            //Console.ReadLine();
            _problem = ClientProblem.CreateInstance(domainContext, clientProblemContext);
            //_problem.ShowInfo();

            // Create a TextReader that reads from a file
            tr = new StreamReader(programFileName);

            // create a CharStream that reads from standard input
            input = new AntlrInputStream(tr);
            // create a lexer that feeds off of input CharStream

            lexer = new PlanningLexer(input);
            // create a buffer of tokens pulled from the lexer
            tokens = new CommonTokenStream(lexer);
            // create a parser that feeds off the tokens buffer
            parser = new PlanningParser(tokens);

            _programContext = parser.program(); // begin parsing at program rule
            //Console.WriteLine(serverProblemContext.ToStringTree(parser));
            tr.Close();
        }

        public void Connect()
        {
            _serverSocket.Connect(_host, _port);
        }

        public void Handshake()
        {
            SendMessage(_agentId);
        }

        public void ExecuteProgram()
        {
            if (_agentId != "a1")
            {
                Console.WriteLine("Agent id: {0}", _agentId);
                string observationName = ReceiveMessage();
                Console.WriteLine("Receive observation: {0}", observationName);
                Observation observation = _observationDict[observationName];
                _mentalAttitude.Update(observation);
            }
            Interpret(_programContext);
        }

        private void Interpret(PlanningParser.ProgramContext context)
        {
            Console.WriteLine(context.GetText());

            if (context.SEQ() != null)
            {
                Console.WriteLine("Enter seq");
                foreach (var programContext in context.program())
                {
                    Interpret(programContext);
                }
            }
            else if (context.IF() != null)
            {
                Console.WriteLine("Enter if and condition is {0}", _mentalAttitude.Implies(context.subjectGd()));

                if (_mentalAttitude.Implies(context.subjectGd()))
                {
                    Interpret(context.program()[0]);
                }
                else if (context.program().Count == 2)
                {
                    Interpret(context.program()[1]);
                }
            }
            else if (context.WHILE() != null)
            {
                Console.WriteLine("Enter while and condition is {0}", _mentalAttitude.Implies(context.subjectGd()));

                while (_mentalAttitude.Implies(context.subjectGd()))
                {
                    Interpret(context.program(0));
                }
            }
            else
            {
                Console.WriteLine("Enter primitive action");
                ExecuteAction(context);
            }
        }

        private void ExecuteAction(PlanningParser.ProgramContext context)
        {
            Console.WriteLine("Action context: {0}", context.GetText());
            string actionName;
            string responseName;
            string observationName;
            actionName = ConstContainer.GetFullName(context.actionSymbol(), context.term());
            SendMessage(actionName);
            Action action = _actionDict[actionName];

            responseName = ReceiveMessage();
            Console.WriteLine("Receive response: {0}", responseName);
            Response response = action.ResponseDict[responseName];
            _mentalAttitude.Update(response);
            //Console.WriteLine("Knowledge:");
            //CUDD.Print.PrintMinterm(_mentalAttitude.Knowledge);
            //Console.WriteLine("Belief:");
            //CUDD.Print.PrintMinterm(_mentalAttitude.Belief);

            observationName = ReceiveMessage();
            Console.WriteLine("Receive observation: {0}", observationName);
            Observation observation = _observationDict[observationName];
            _mentalAttitude.Update(observation);
            //Console.WriteLine("Knowledge:");
            //CUDD.Print.PrintMinterm(_mentalAttitude.Knowledge);
            //Console.WriteLine("Belief:");
            //CUDD.Print.PrintMinterm(_mentalAttitude.Belief);

            Console.ReadLine();
        }

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
