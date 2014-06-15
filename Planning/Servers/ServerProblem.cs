using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Servers
{
    public class ServerProblem// : Problem<ServerAction, ServerGroundAction>
    {
        #region Fields

        private const string Agent1Id = "a1";
        private const string Agent2Id = "a2";


        private ConstTermComputer _computer;

        private Dictionary<string, PlanningType> _typeDict;

        private Dictionary<string, Predicate> _predDict;

        private Dictionary<string, string> _constantTypeMap;

        private Dictionary<string, List<string>> _typeConstantListMap;

        private Dictionary<string, GroundPredicate> _gndPredDict;

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

            ConstructComputer(serverProblemContext.constSetting());
            DomainName = domainContext.NAME().GetText();
            HandleTypeDefine(domainContext.typeDefine());
            HandlePredDefine(domainContext.predDefine());
            BuildConstantTypeMap(serverProblemContext.objectDeclaration());
            BuildGroundPredicate();
            BuildTruePredicateSet(serverProblemContext.init());
            //HandleServerProblem(context);
        }

        #endregion

        #region Methods

        public static ServerProblem CreateInstance(PlanningParser.DomainContext domainContext, PlanningParser.ServerProblemContext context)
        {
            ServerProblem result = new ServerProblem(domainContext, context);
            return result;
        }

        private void ConstructComputer(PlanningParser.ConstSettingContext context)
        {
            if (context != null)
            {
                _computer = new ConstTermComputer(context);
            }
        }

        private void HandleDomain(PlanningParser.DomainContext context)
        {
            DomainName = context.NAME().GetText();
            HandleTypeDefine(context.typeDefine());
            HandlePredDefine(context.predDefine());
            //HandleActionsDefine(context.actionDefine());
        }

        private void HandleTypeDefine(PlanningParser.TypeDefineContext context)
        {
            _typeDict = new Dictionary<string, PlanningType>();

            if (context != null)
            {
                _typeDict.Add(PlanningType.ObjectType.Name, PlanningType.ObjectType);
                _typeDict.Add(PlanningType.AgentType.Name, PlanningType.AgentType);

                foreach (var typeContext in context.typeDeclaration())
                {
                    PlanningType type;
                    string name = typeContext.NAME().GetText();
                    if (typeContext.LB() == null)
                    {
                        type = new PlanningType { Name = name };
                    }
                    else
                    {
                        int min = _computer.GetValue(typeContext.constTerm(0));
                        int max = _computer.GetValue(typeContext.constTerm(1));
                        type = new PlanningNumericType {Name = name, Min = min, Max = max};
                    }

                    _typeDict.Add(name, type);
                }
            }
        }

        private void BuildConstantTypeMap(PlanningParser.ObjectDeclarationContext context)
        {
            _constantTypeMap = new Dictionary<string, string>();
            _typeConstantListMap = new Dictionary<string, List<string>>();

            _constantTypeMap.Add(Agent1Id, PlanningType.AgentType.Name);
            _constantTypeMap.Add(Agent2Id, PlanningType.AgentType.Name);
            _typeConstantListMap.Add(PlanningType.AgentType.Name, new List<string> {Agent1Id, Agent2Id});


            foreach (var pair in _typeDict)
            {
                if (pair.Value is PlanningNumericType)
                {
                    PlanningNumericType type = pair.Value as PlanningNumericType;
                    List<string> constantList = new List<string>(type.Max - type.Min + 1);
                    for (int i = type.Min; i <= type.Max; i++)
                    {
                        constantList.Add(i.ToString());
                    }
                    _typeConstantListMap.Add(type.Name, constantList);
                }
            }

            if (context != null)
            {
                var listNameContext = context.listName();
                do
                {
                    string type = listNameContext.type() != null
                        ? listNameContext.type().GetText()
                        : PlanningType.ObjectType.Name;

                    List<string> constantList;

                    if (_typeConstantListMap.ContainsKey(type))
                    {
                        constantList = _typeConstantListMap[type];
                    }
                    else
                    {
                        constantList = new List<string>(listNameContext.NAME().Count);
                        _typeConstantListMap.Add(type, constantList);
                    }

                    foreach (var nameNode in listNameContext.NAME())
                    {
                        _constantTypeMap.Add(nameNode.GetText(), type);
                        constantList.Add(nameNode.GetText());
                    }

                    listNameContext = listNameContext.listName();
                } while (listNameContext != null);
            }
        }

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

            //for (int i = 0; i < gndPred.CuddIndexList.Count; i++)
            //{
            //    gndPred.SetCuddIndex(i, _currentCuddIndex);
            //    _currentCuddIndex++;
            //}
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
                    List<string> constantList = _typeConstantListMap[variable.Item2];
                    collection.Add(constantList);
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

        private void BuildTruePredicateSet(PlanningParser.InitContext context)
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

        //private void HandleInit(PlanningParser.InitContext context)
        //{
        //    BuildTruePredicateSet(context);
        //}

        public void ShowInfo()
        {
            Console.WriteLine("Domain name: {0}", DomainName);
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Problem Name: {0}", ProblemName);
            Console.WriteLine(Domain.BarLine);

            if (_computer != null)
            {
                _computer.ShowInfo();
            }
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Types:");
            foreach (var type in _typeDict)
            {
                Console.WriteLine("  {0}", type.Value);
            }
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Predicates:");
            foreach (var pred in _predDict.Values)
            {
                Console.WriteLine("  Name: {0}", pred.Name);
                Console.WriteLine("  Variables: {0}", pred.Count);
                for (int i = 0; i < pred.Count; i++)
                {
                    Console.WriteLine("    Index: {0}, Name: {1}, Type: {2}", i, pred.VariableList[i].Item1,
                        pred.VariableList[i].Item2);
                }
                Console.WriteLine();
            }
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Constants:");
            foreach (var pair in _constantTypeMap)
            {
                Console.WriteLine("  Name: {0}, Type: {1}", pair.Key, pair.Value);
            }
            Console.WriteLine(Domain.BarLine);

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

            //Console.WriteLine("Ground actions:");
            //foreach (var gndAction in GroundActionDict.Values)
            //{
            //    Console.WriteLine("  Name: {0}", gndAction);
            //    Console.WriteLine("  Precondition:");
            //    CUDD.Print.PrintMinterm(gndAction.Precondition);

            //    Console.WriteLine("  Effect:");
            //    for (int i = 0; i < gndAction.Effect.Count; i++)
            //    {
            //        Console.WriteLine("    Index: {0}", i);
            //        Console.WriteLine("    Condition:");
            //        CUDD.Print.PrintMinterm(gndAction.Effect[i].Item1);

            //        Console.Write("    Literals: { ");
            //        var literal = gndAction.Effect[i].Item2[0];
            //        if (literal.Item2)
            //        {
            //            Console.Write("{0}", literal.Item1);
            //        }
            //        else
            //        {
            //            Console.Write("not {0}", literal.Item1);
            //        }

            //        for (int j = 1; j < gndAction.Effect[i].Item2.Count; j++)
            //        {
            //            if (literal.Item2)
            //            {
            //                Console.Write(", {0}", literal.Item1);
            //            }
            //            else
            //            {
            //                Console.Write(", not {0}", literal.Item1);
            //            }
            //        }

            //        Console.WriteLine(" }");
            //    }
            //    Console.WriteLine();
            //}
        }

        #endregion
    }
}
