using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning;
using Planning.Clients;
using Planning.HighLevelProgramExecution;
using Action = Planning.Action;
using Program = Planning.HighLevelProgramExecution.Program;

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
            //Console.WriteLine("Belief");
            //CUDD.Print.PrintMinterm(_mentalAttitude.Belief);
            //Console.WriteLine("Knowledge");
            //CUDD.Print.PrintMinterm(_mentalAttitude.Belief);
            _actionDict = _problem.ActionDict;
            _observationDict = _problem.ObservationDict;
        }

        #endregion

        #region Methods

        private void Initial(string domainFileName, string problemFileName, string programFileName)
        {
            CUDD.InitialiseCUDD(3072, 256, 262144, 0.1);

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
            Interpret(_programContext);
        }

        private void Interpret(PlanningParser.ProgramContext context)
        {
            var program = Planning.HighLevelProgramExecution.Program.CreateInstance(context);
            Interpret(program);

        }

        private void Interpret(Planning.HighLevelProgramExecution.Program program)
        {
            if (!Final(program))
            {
                do
                {
                    Console.WriteLine(program.GetType());
                    string message = ReceiveMessage();
                    Console.WriteLine("Receive message: {0}", message);
                    if (message == "observation")
                    {
                        string observationName = ReceiveMessage();
                        Console.WriteLine("Receive observation: {0}", observationName);
                        Observation observation = _observationDict[observationName];
                        _mentalAttitude.Update(observation);
                    }
                    else
                    {
                        Console.WriteLine(program);
                        program = Trans(program);
                        if (!Final(program))
                        {
                            SendMessage("remain");
                        }
                        else
                        {
                            SendMessage("quit");
                            break;
                        }
                    }
                } while (true);
            }
        }

        private bool Final(Planning.HighLevelProgramExecution.Program program)
        {
            bool result;
            if (program is EmptyProgram)
            {
                result = true;
            }
            
            else if (program is SequenceStructure)
            {
                SequenceStructure seq = ((SequenceStructure)program);
                result = true;
                foreach (var subProgram in seq.SubPrograms)
                {
                    if (!Final(subProgram))
                    {
                        result = false;
                        break;
                    }
                }
            }
            
            else if (program is ConditionalStructure)
            {
                ConditionalStructure cond = ((ConditionalStructure)program);
                return _mentalAttitude.Implies(cond.Condition) ? Final(cond.SubProgram1) : Final(cond.SubProgram2);
            }

            else if (program is LoopStructure)
            {
                LoopStructure loop = ((LoopStructure) program);
                result = true;
                if (_mentalAttitude.Implies(loop.Condition))
                {
                    result = Final(loop.SubProgram);
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
        
        private Planning.HighLevelProgramExecution.Program Trans(Planning.HighLevelProgramExecution.Program program)
        {
            //Console.WriteLine();
            //Console.WriteLine(program);
            Planning.HighLevelProgramExecution.Program remainingProgram;
            
            if (program is Planning.HighLevelProgramExecution.Action)
            {
                remainingProgram = Planning.HighLevelProgramExecution.Program.EmptyProgram;

                Planning.HighLevelProgramExecution.Action programAction =
                    (Planning.HighLevelProgramExecution.Action) program;
                Action action = _actionDict[programAction.FullName];
                SendMessage(action.FullName);
                string responseName = ReceiveMessage();
                Console.WriteLine("Receive response: {0}", responseName);
                Response response = action.ResponseDict[responseName];
                _mentalAttitude.Update(response);
            }
            else if (program is SequenceStructure)
            {
                SequenceStructure seq = ((SequenceStructure) program);
                int i;
                for (i = 0; i < seq.SubProgramLength - 1; i++)
                {
                    if (!Final(seq.SubPrograms[i]))
                    {
                        break;
                    }
                }

                Planning.HighLevelProgramExecution.Program[] newRemainingProgramArray =
                    new Planning.HighLevelProgramExecution.Program[seq.SubProgramLength - i];
                newRemainingProgramArray[0] = Trans(seq.SubPrograms[i]);
                for (int j = 1; j < newRemainingProgramArray.Length; j++)
                {
                    //Console.WriteLine("newIndex:{0}, oldIndex:{1}", j, j + i - 1);
                    newRemainingProgramArray[j] = seq.SubPrograms[j + i];
                }

                remainingProgram = new SequenceStructure(newRemainingProgramArray);
            }
            else if (program is ConditionalStructure)
            {
                ConditionalStructure cond = (ConditionalStructure) program;
                remainingProgram = _mentalAttitude.Implies(cond.Condition)
                    ? Trans(cond.SubProgram1)
                    : Trans(cond.SubProgram2);
            }
            else if (program is LoopStructure)
            {
                LoopStructure loop = (LoopStructure) program;
                remainingProgram = _mentalAttitude.Implies(loop.Condition)
                    ? new SequenceStructure(new[] {Trans(loop.SubProgram), program})
                    : null;
            }
            else
            {
                throw new PlanningException("Trans an empty program!");
            }

            return remainingProgram;
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
