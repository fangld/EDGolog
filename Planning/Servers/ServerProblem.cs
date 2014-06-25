using System;
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

        private Dictionary<string, Predicate> _predDict;

        private Dictionary<string, Event> _eventDict;

        private Dictionary<string, Action> _actionDict;


        private int _currentCuddIndex;

        #endregion

        #region Properties

        public string DomainName { get; set; }

        public string ProblemName { get; set; }

        public HashSet<string> TruePredSet { get; set; }

        public Domain Domain { get; set; }

        #endregion

        #region Constructors

        private ServerProblem(PlanningParser.DomainContext domainContext, PlanningParser.ServerProblemContext serverProblemContext)
        {
            _currentCuddIndex = 0;

            DomainName = domainContext.NAME().GetText();
            ProblemName = serverProblemContext.problemName().GetText();
            Console.WriteLine("Finishing name!");
            //HandleTypeDefine(domainContext.typeDefine());
            Globals.TermHandler = new TermHandler(serverProblemContext.numericSetting(), domainContext.typeDefine(),
                serverProblemContext.objectDeclaration());
            Console.WriteLine("Finishing term handler!");
            HandlePredDefine(domainContext.predDefine());
            Console.WriteLine("Finishing predicate!");
            //Globals.TermHandler = new TermHandler(serverProblemContext.numericSetting(),
            //    serverProblemContext.objectDeclaration(), _typeDict);
            //BuildConstantTypeMap(serverProblemContext.objectDeclaration());
            //BuildGroundPredicate();
            HandleInit(serverProblemContext.init());
            Console.WriteLine("Finishing init object base!");
            HandleEventsDefine(domainContext.eventDefine());
            Console.WriteLine("Finishing init event define!");
            HandleActionsDefine(domainContext.actionDefine());
            Console.WriteLine("Finishing init action define!");
            //HandleServerProblem(context);
        }

        #endregion

        #region Methods

        public static ServerProblem CreateInstance(PlanningParser.DomainContext domainContext, PlanningParser.ServerProblemContext context)
        {
            ServerProblem result = new ServerProblem(domainContext, context);
            return result;
        }

        private void HandlePredDefine(PlanningParser.PredDefineContext context)
        {
            _predDict = new Dictionary<string, Predicate>();
            foreach (var atomFormSkeleton in context.atomFormSkeleton())
            {
                Build(atomFormSkeleton.listVariable(), atomFormSkeleton, AddToPredDict);
            }
        }

        private void HandleEventsDefine(IReadOnlyList<PlanningParser.EventDefineContext> contexts)
        {
            _eventDict = new Dictionary<string, Event>();
            foreach (var eventDefineContext in contexts)
            {
                Build(eventDefineContext.listVariable(), eventDefineContext, AddToEventDict);
            }
        }

        private void HandleActionsDefine(IReadOnlyList<PlanningParser.ActionDefineContext> contexts)
        {
            _actionDict = new Dictionary<string, Action>();
            foreach (var actionDefineContext in contexts)
            {
                Build(actionDefineContext.listVariable(), actionDefineContext, AddToActionDict);
            }
        }

        private void Build<TContext>(PlanningParser.ListVariableContext context, TContext baseContext, Action<TContext, string[]> action)
        {
            IReadOnlyList<List<string>> collection = context.GetCollection();
            ScanMixedRadix(collection, baseContext, action);
        }

        private void ScanMixedRadix<TContext>(IReadOnlyList<List<string>> collection, TContext context, Action<TContext, string[]> action)
        {
            int count = collection.Count;
            string[] scanArray = new string[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i => maxIndex[i] = collection[i].Count);

            do
            {
                Parallel.For(0, count, i => scanArray[i] = collection[i][index[i]]);

                action(context, scanArray);

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

        private void AddToPredDict(PlanningParser.AtomFormSkeletonContext context, string[] constArray)
        {
            Predicate pred = new Predicate(context, constArray, ref _currentCuddIndex);
            _predDict.Add(pred.FullName, pred);
        }

        private void Build<TContext>(PlanningParser.ListVariableContext context, TContext baseContext, Action<TContext, string[], Dictionary<string, string>> action)
        {
            IReadOnlyList<List<string>> collection = context.GetCollection();
            IReadOnlyList<string> varNameList = context.GetVarNameList();
            ScanMixedRadix(varNameList, collection, baseContext, action);
        }

        private void ScanMixedRadix<TContext>(IReadOnlyList<string> varNameList, IReadOnlyList<List<string>> collection, TContext context, Action<TContext, string[], Dictionary<string, string>> action)
        {
            int count = collection.Count;
            Dictionary<string, string> assignment = new Dictionary<string, string>();
            string[] scanArray = new string[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i => maxIndex[i] = collection[i].Count);

            do
            {
                for (int i = 0; i < count; i ++)
                {
                    scanArray[i] = collection[i][index[i]];
                    string varName = varNameList[i];
                    if (assignment.ContainsKey(varName))
                    {
                        assignment[varName] = scanArray[i];
                    }
                    else
                    {
                        assignment.Add(varName, scanArray[i]);
                    }
                }

                action(context, scanArray, assignment);

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

        private void AddToEventDict(PlanningParser.EventDefineContext context, string[] constArray, Dictionary<string, string> assignment)
        {
            Event e = new Event(context, _predDict, constArray, assignment, ref _currentCuddIndex);
            if (!e.Precondition.Equals(CUDD.ZERO))
            {
                _eventDict.Add(e.FullName, e);
            }
        }

        private void AddToActionDict(PlanningParser.ActionDefineContext context, string[] constArray, Dictionary<string, string> assignment)
        {
            Action action = new Action(context, _eventDict, constArray, assignment);
            _actionDict.Add(action.FullName, action);
        }

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

        public void ShowInfo()
        {
            Console.WriteLine("Domain name: {0}", DomainName);
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Problem Name: {0}", ProblemName);
            Console.WriteLine(Domain.BarLine);

            Globals.TermHandler.ShowInfo();

            Console.WriteLine("Predicates:");
            foreach (var pair in _predDict)
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
                //"leftSucWithNotice(a1,0)",
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

            Console.WriteLine("Events:");

            for (int i = 0; i < eventNameArray.Count; i++)
            {
                string eventName = eventNameArray[i];
                Event e = _eventDict[eventName];
                Console.WriteLine("  Name: {0}", eventNameArray[i]);
                Console.WriteLine("  Cudd index: {0}", e.CuddIndex);

                Console.WriteLine("  Precondition:");
                CUDD.Print.PrintMinterm(e.Precondition);

                //Console.WriteLine("  CondEffect:");
                ////Console.WriteLine("  Count:{0}", e.CondEffect.Count);
                //for (int j = 0; j < e.CondEffect.Count; j++)
                //{
                //    Console.WriteLine("    Index: {0}", j);
                //    Console.WriteLine("    Condition:");
                //    CUDD.Print.PrintMinterm(e.CondEffect[j].Item1);
                //    Console.Write("    Literals: ");

                //    foreach (var literal in e.CondEffect[j].Item2)
                //    {
                //        string format = literal.Item2 ? "{0} " : "!{0} ";
                //        Console.Write(format, literal.Item1);
                //    }

                //    Console.WriteLine();
                //}

                //Console.WriteLine("  Successor state axiom:");
                //CUDD.Print.PrintMinterm(e.SuccessorStateAxiom);
            }

            Console.WriteLine("Actions:");

            foreach (var action in _actionDict.Values)
            {
                Console.WriteLine("  Name: {0}", action.FullName);
                Console.WriteLine("  Response:");
                foreach (var response in action.RespDict.Values)
                {
                    Console.WriteLine("    Name: {0}", response.FullName);
                    for (int i = 0; i < response.EventList.Count; i++)
                    {
                        Console.WriteLine("    Plausibility degree: {0}", i);
                        Console.Write("    Event list: ");
                        foreach (var e in response.EventList[i])
                        {
                            Console.Write("{0}  ", e.FullName);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();

                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

            }
        }

        #endregion
    }
}