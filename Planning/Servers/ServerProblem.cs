﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Servers
{
    public class ServerProblem
    {
        #region Fields

        //private const string Agent1Id = "a1";
        //private const string Agent2Id = "a2";

        //private ConstTermComputer _computer;

        //private Dictionary<string, PlanningType> _typeDict;

        private Dictionary<string, Predicate> _predDict;

        //private Dictionary<string, string> _constantTypeMap;

        //private Dictionary<string, List<string>> _typeConstantListMap;

        private Dictionary<string, GroundPredicate> _gndPredDict;

        private Dictionary<string, Event> _eventDict;

        private int _currentCuddIndex;

        #endregion

        #region Properties

        public string DomainName { get; set; }

        public string ProblemName { get; set; }

        //protected override int PredicateCuddIndexNumber
        //{
        //    get { return 1; }
        //}

        public HashSet<string> TruePredSet { get; set; }

        public Domain Domain { get; set; }

        #endregion

        #region Constructors

        private ServerProblem(PlanningParser.DomainContext domainContext, PlanningParser.ServerProblemContext serverProblemContext)
            //: base(domain)
        {
            _currentCuddIndex = 0;

            //ConstructComputer(serverProblemContext.constSetting());
            DomainName = domainContext.NAME().GetText();
            ProblemName = serverProblemContext.problemName().GetText();
            //HandleTypeDefine(domainContext.typeDefine());
            Globals.TermHandler = new TermHandler(serverProblemContext.numericSetting(), domainContext.typeDefine(),
                serverProblemContext.objectDeclaration());
            HandlePredDefine(domainContext.predDefine());
            //Globals.TermHandler = new TermHandler(serverProblemContext.numericSetting(),
            //    serverProblemContext.objectDeclaration(), _typeDict);
            //BuildConstantTypeMap(serverProblemContext.objectDeclaration());
            BuildGroundPredicate();
            HandleInit(serverProblemContext.init());
            HandleEventsDefine(domainContext.eventDefine());
            //HandleServerProblem(context);
        }

        #endregion

        #region Methods

        public static ServerProblem CreateInstance(PlanningParser.DomainContext domainContext, PlanningParser.ServerProblemContext context)
        {
            ServerProblem result = new ServerProblem(domainContext, context);
            return result;
        }

        //private void ConstructComputer(PlanningParser.NumericSettingContext context)
        //{
        //    if (context != null)
        //    {
        //        Globals.ConstTermComputer = new ConstTermComputer(context);
        //        //_computer = new ConstTermComputer(context);
        //    }
        //}

        //private void HandleDomain(PlanningParser.DomainContext context)
        //{
        //    DomainName = context.NAME().GetText();
        //    HandleTypeDefine(context.typeDefine());
        //    HandlePredDefine(context.predDefine());
        //    //HandleActionsDefine(context.actionDefine());
        //}

        //private void HandleTypeDefine(PlanningParser.TypeDefineContext context)
        //{
        //    _typeDict = new Dictionary<string, PlanningType>();

        //    if (context != null)
        //    {
        //        _typeDict.Add(PlanningType.ObjectType.Name, PlanningType.ObjectType);
        //        _typeDict.Add(PlanningType.AgentType.Name, PlanningType.AgentType);

        //        foreach (var typeContext in context.typeDeclaration())
        //        {
        //            PlanningType type;
        //            string name = typeContext.NAME().GetText();
        //            if (typeContext.LB() == null)
        //            {
        //                type = new PlanningType { Name = name };
        //            }
        //            else
        //            {
        //                int min = Globals.TermHandler.GetValue(typeContext.constTerm(0));
        //                int max = Globals.TermHandler.GetValue(typeContext.constTerm(1));
        //                type = new PlanningNumericType {Name = name, Min = min, Max = max};
        //            }

        //            _typeDict.Add(name, type);
        //        }
        //    }
        //}

        //private void BuildConstantTypeMap(PlanningParser.ObjectDeclarationContext context)
        //{
        //    _constantTypeMap = new Dictionary<string, string>();
        //    _typeConstantListMap = new Dictionary<string, List<string>>();

        //    _constantTypeMap.Add(Agent1Id, PlanningType.AgentType.Name);
        //    _constantTypeMap.Add(Agent2Id, PlanningType.AgentType.Name);
        //    _typeConstantListMap.Add(PlanningType.AgentType.Name, new List<string> {Agent1Id, Agent2Id});


        //    foreach (var pair in _typeDict)
        //    {
        //        if (pair.Value is PlanningNumericType)
        //        {
        //            PlanningNumericType type = pair.Value as PlanningNumericType;
        //            List<string> constantList = new List<string>(type.Max - type.Min + 1);
        //            for (int i = type.Min; i <= type.Max; i++)
        //            {
        //                constantList.Add(i.ToString());
        //            }
        //            _typeConstantListMap.Add(type.Name, constantList);
        //        }
        //    }

        //    if (context != null)
        //    {
        //        var listNameContext = context.listName();
        //        do
        //        {
        //            string type = listNameContext.type() != null
        //                ? listNameContext.type().GetText()
        //                : PlanningType.ObjectType.Name;

        //            List<string> constantList;

        //            if (_typeConstantListMap.ContainsKey(type))
        //            {
        //                constantList = _typeConstantListMap[type];
        //            }
        //            else
        //            {
        //                constantList = new List<string>(listNameContext.NAME().Count);
        //                _typeConstantListMap.Add(type, constantList);
        //            }

        //            foreach (var nameNode in listNameContext.NAME())
        //            {
        //                _constantTypeMap.Add(nameNode.GetText(), type);
        //                constantList.Add(nameNode.GetText());
        //            }

        //            listNameContext = listNameContext.listName();
        //        } while (listNameContext != null);
        //    }
        //}

        private void HandlePredDefine(PlanningParser.PredDefineContext context)
        {
            _predDict = new Dictionary<string, Predicate>();
            foreach (var atomFormSkeleton in context.atomFormSkeleton())
            {
                Predicate pred = Predicate.FromContext(atomFormSkeleton);
                _predDict.Add(pred.Name, pred);
            }
        }

        private void BuildGroundPredicate()
        {
            _gndPredDict = new Dictionary<string, GroundPredicate>();
            BuildGround(_predDict.Values, AddToGroundPredicateDict);
        }

        private void AddToGroundPredicateDict(string predName, string[] constantList)
        {
            Predicate pred = _predDict[predName];
            GroundPredicate gndPred = new GroundPredicate(pred, constantList);
            gndPred.PreviousCuddIndex = _currentCuddIndex;
            _currentCuddIndex++;
            gndPred.SuccessiveCuddIndex = _currentCuddIndex;
            _currentCuddIndex++;
            _gndPredDict.Add(gndPred.ToString(), gndPred);
        }

        private void BuildGround<T>(IEnumerable<T> containters, Action<string, string[]> action) where T : VariableContainer
        {
            foreach (var container in containters)
            {
                List<List<string>> collection = new List<List<string>>();

                for (int i = 0; i < container.Count; i++)
                {
                    Tuple<string, string> variable = container.VariableList[i];
                    List<string> constList = Globals.TermHandler.GetConstList(variable.Item2);
                        //_typeConstantListMap[variable.Item2];
                    collection.Add(constList);
                }

                ScanMixedRadix(container.Name, collection, action);
            }
        }

        private void ScanMixedRadix(string actionName, IReadOnlyList<List<string>> collection, Action<string, string[]> action)
        {
            int count = collection.Count;
            string[] scanArray = new string[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i => maxIndex[i] = collection[i].Count);

            do
            {
                Parallel.For(0, count, i => scanArray[i] = collection[i][index[i]]);

                action(actionName, scanArray);
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

        //private void HandleServerProblem(PlanningParser.ServerProblemContext context)
        //{
        //    Name = context.problemName().GetText();
        //    DomainName = context.domainName().GetText();
        //    HandleAgentDefine(context.agentDefine());
        //    HandleObjectDeclaration(context.objectDeclaration());
        //    HandleInit(context.init());
        //}

        private void HandleInit(PlanningParser.InitContext context)
        {
            TruePredSet = new HashSet<string>();
            foreach (var atomForm in context.constTermAtomForm())
            {
                var constTermContexts = atomForm.constTerm();
                List<string> termList = new List<string>();
                foreach (var constTermContext in constTermContexts)
                {
                    termList.Add(constTermContext.GetText());
                }
                string gndPredName = VariableContainer.GetFullName(atomForm.pred().GetText(), termList);

                TruePredSet.Add(gndPredName);
            }
        }

        private void HandleEventsDefine(IReadOnlyList<PlanningParser.EventDefineContext> contexts)
        {
            _eventDict = new Dictionary<string, Event>();
            foreach (var eventDefineContext in contexts)
            {
                List<List<string>> collection = new List<List<string>>();

                var listVariableContext = eventDefineContext.listVariable();
                do
                {
                    int count = listVariableContext.VAR().Count;
                    if (count != 0)
                    {
                        string type = listVariableContext.type() == null ? PlanningType.ObjectType.Name : listVariableContext.type().GetText();

                        for (int i = 0; i < count; i++)
                        {
                            List<string> constList = Globals.TermHandler.GetConstList(type);
                            collection.Add(constList);
                        }
                    }
                    listVariableContext = listVariableContext.listVariable();
                } while (listVariableContext != null);

                ScanMixedRadix(collection, eventDefineContext);
            }
        }

        private void ScanMixedRadix(IReadOnlyList<List<string>> collection, PlanningParser.EventDefineContext context)
        {
            int count = collection.Count;
            string[] scanArray = new string[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i => maxIndex[i] = collection[i].Count);

            do
            {
                Parallel.For(0, count, i => scanArray[i] = collection[i][index[i]]);

                Event e = Event.From(context, _gndPredDict, scanArray);
                if (!e.Precondition.Equals(CUDD.ZERO))
                {
                    _eventDict.Add(e.Name, e);
                }

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

        public void ShowInfo()
        {
            Console.WriteLine("Domain name: {0}", DomainName);
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Problem Name: {0}", ProblemName);
            Console.WriteLine(Domain.BarLine);

            Globals.TermHandler.ShowInfo();
            //Console.WriteLine("Types:");
            //foreach (var type in _typeDict)
            //{
            //    Console.WriteLine("  {0}", type.Value);
            //}
            //Console.WriteLine(Domain.BarLine);

            //Console.WriteLine("Predicates:");
            //foreach (var pred in _predDict.Values)
            //{
            //    Console.WriteLine("  Name: {0}", pred.Name);
            //    Console.WriteLine("  Variables: {0}", pred.Count);
            //    for (int i = 0; i < pred.Count; i++)
            //    {
            //        Console.WriteLine("    Index: {0}, Name: {1}, Type: {2}", i, pred.VariableList[i].Item1,
            //            pred.VariableList[i].Item2);
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine(Domain.BarLine);

            //Console.WriteLine("Constants:");
            //foreach (var pair in _constantTypeMap)
            //{
            //    Console.WriteLine("  Name: {0}, Type: {1}", pair.Key, pair.Value);
            //}
            //Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Ground predicates:");
            foreach (var pair in _gndPredDict)
            {
                Console.WriteLine("  Name: {0}, PreCuddIndex: {1}, SucCuddIndex: {2}", pair.Key,
                    pair.Value.PreviousCuddIndex, pair.Value.SuccessiveCuddIndex);
            }
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Initial state:");
            foreach (var pred in TruePredSet)
            {
                Console.WriteLine("  {0}", pred);
            }
            Console.WriteLine(Domain.BarLine);

            List<string> eventNameArray = new List<string>
            {
                //"leftFail(a1)",
                //"rightSucWithNotice(a2,-2)",
                "leftSucWithNotice(a1,0)",
                //"leftSucWithoutNotice(a1)",
                //"leftSucWithoutNotice(a2)",
                //"rightSucWithoutNotice(a1)",
                //"rightSucWithoutNotice(a2)",
                //"dropFail(a1)",
                //"dropSuc(a1)",
                //"pickSuc(a2)",
                //"learn(a1,1,-1)",
                //"learn(a1,1,0)"
            };

            

            Console.WriteLine("Ground events:");

            for (int i = 0; i < eventNameArray.Count; i++)
            {
                string eventName = eventNameArray[i];
                Event e = _eventDict[eventName];
                Console.WriteLine("  Name: {0}", eventNameArray[i]);
                Console.WriteLine("  Precondition:");
                CUDD.Print.PrintMinterm(e.Precondition);

                Console.WriteLine("  CondEffect:");
                //Console.WriteLine("  Count:{0}", e.CondEffect.Count);
                for (int j = 0; j < e.CondEffect.Count; j++)
                {
                    Console.WriteLine("    Index: {0}", j);
                    Console.WriteLine("    Condition:");
                    CUDD.Print.PrintMinterm(e.CondEffect[j].Item1);
                    Console.Write("    Literals: ");

                    foreach (var literal in e.CondEffect[j].Item2)
                    {
                        string format = literal.Item2 ? "{0} " : "!{0} ";
                        Console.Write(format, literal.Item1);
                    }

                    Console.WriteLine();
                }

                Console.WriteLine("  Successor state axiom:");
                CUDD.Print.PrintMinterm(e.Precondition);

            }
        }

        #endregion
    }
}
