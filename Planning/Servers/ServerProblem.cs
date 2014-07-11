using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.ContextExtensions;

namespace Planning.Servers
{
    public class ServerProblem
    {
        #region Fields

        private Dictionary<string, Predicate> _predicateDict;

        private Dictionary<string, Event> _eventDict;

        private Dictionary<string, Action> _actionDict;

        private Dictionary<string, Observation> _obervationDict;
        
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
            Globals.TermInterpreter = new TermInterpreter(serverProblemContext.numericSetting(), domainContext.typeDefine(),
                serverProblemContext.objectDeclaration());
            Console.WriteLine("Finishing term interpreter!");

            HandlePredicateDefine(domainContext.predicateDefine());
            Console.WriteLine("Finishing predicate!");
            HandleInit(serverProblemContext.init());
            Console.WriteLine("Finishing init object base!");
            HandleEventsDefine(domainContext.eventDefine());
            Console.WriteLine("Finishing event define!");
            HandleActionsDefine(domainContext.actionDefine());
            Console.WriteLine("Finishing action define!");
            HandleObservationsDefine(domainContext.observationDefine());
            Console.WriteLine("Finishing observation define!");
        }

        #endregion

        #region Methods

        public static ServerProblem CreateInstance(PlanningParser.DomainContext domainContext, PlanningParser.ServerProblemContext context)
        {
            ServerProblem result = new ServerProblem(domainContext, context);
            return result;
        }

        #region Methods for generating the dictionary of predicates

        private void HandlePredicateDefine(PlanningParser.PredicateDefineContext context)
        {
            _predicateDict = new Dictionary<string, Predicate>();
            foreach (var atomFormSkeleton in context.atomFormSkeleton())
            {
                Build(atomFormSkeleton.listVariable(), atomFormSkeleton, AddToPredicateDict);
            }
        }

        private void Build2<TContext>(PlanningParser.ListVariableContext listVariableContext, MixedRadixEnumerator<PlanningParser.AtomFormSkeletonContext> enumerator)
        {
            IReadOnlyList<IList<string>> collection = listVariableContext.GetCollection();
            MixedRadixEnumerator<PlanningParser.AtomFormSkeletonContext> enumerator = new EventEnumerator(context, collection, _predicateDict, _currentCuddIndex);

            ScanMixedRadix(context, collection, action);
        }

        private void Build<TContext>(PlanningParser.ListVariableContext listVariableContext, TContext context, Action<TContext, string[]> action)
        {
            IReadOnlyList<IList<string>> collection = listVariableContext.GetCollection();
            MixedRadixEnumerator<PlanningParser.AtomFormSkeletonContext> enumerator  = new EventEnumerator(context, collection, _predicateDict, _currentCuddIndex);

            ScanMixedRadix(context, collection, action);
        }

        private void IterativeScanMixedRadix(IMixedRadixEnumerator enumerator)
        {
            int count = enumerator.CollectionCount;
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i => maxIndex[i] = enumerator.Collection[i].Count);
            
            enumerator.Initial(index);

            do
            {
                enumerator.Action();

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

                enumerator.MoveNext(j, index);
                //Parallel.For(j, count, i => scanArray[i] = collection[i][index[i]]);
            } while (true);

        }

        private void ScanMixedRadix<TContext>(TContext context, IReadOnlyList<IList<string>> collection,
            Action<TContext, string[]> action)
        {
            int count = collection.Count;
            string[] scanArray = new string[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i =>
            {
                maxIndex[i] = collection[i].Count;
                scanArray[i] = collection[i][index[i]];
            });

            do
            {
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
                Parallel.For(j, count, i => scanArray[i] = collection[i][index[i]]);
            } while (true);
        }

        private void AddToPredicateDict(PlanningParser.AtomFormSkeletonContext context, string[] constArray)
        {
            Predicate predicate = new Predicate(context, constArray, _currentCuddIndex);
            _currentCuddIndex = predicate.SuccessiveCuddIndex;
            _predicateDict.Add(predicate.FullName, predicate);
            //Console.WriteLine(predicate.FullName);
            //Console.ReadLine();
        }

        #endregion

        #region Methods for generating the dictionary of events/actions/observations

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

        private void HandleObservationsDefine(IReadOnlyList<PlanningParser.ObservationDefineContext> contexts)
        {
            _obervationDict = new Dictionary<string, Observation>();
            foreach (var obsDefineContext in contexts)
            {
                Build(obsDefineContext.listVariable(), obsDefineContext, AddToObservationDict);
            }
        }

        private void Build<TContext>(PlanningParser.ListVariableContext listVariableContext, TContext context, Action<TContext, string[], StringDictionary> action)
        {
            IReadOnlyList<IList<string>> collection = listVariableContext.GetCollection();
            IReadOnlyList<string> varNameList = listVariableContext.GetVariableNameList();
            ScanMixedRadix(context, collection, varNameList, action);
        }

        private void ScanMixedRadix<TContext>(TContext context, IReadOnlyList<IList<string>> collection,
            IReadOnlyList<string> variableNameList, Action<TContext, string[], StringDictionary> action)
        {
            int count = collection.Count;
            StringDictionary assignment = new StringDictionary();
            string[] scanArray = new string[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            for (int i = 0; i < count; i++)
            {
                maxIndex[i] = collection[i].Count;
                string value = collection[i][index[i]];
                scanArray[i] = value;
                assignment.Add(variableNameList[i], value);
            }

            do
            {
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
                for (int i = j; i < count; i++)
                {
                    string value = collection[i][index[i]];
                    scanArray[i] = value;
                    assignment[variableNameList[i]] = value;
                }
            } while (true);
        }

        private void AddToEventDict(PlanningParser.EventDefineContext context, string[] constArray, StringDictionary assignment)
        {
            Event e = new Event(context, _predicateDict, constArray, assignment, _currentCuddIndex);
            _currentCuddIndex = e.CuddIndex + 1;
            //Console.WriteLine(e.FullName);

            if (!e.Precondition.Equals(CUDD.ZERO))
            {
                //Console.WriteLine(e.FullName);
                _eventDict.Add(e.FullName, e);
            }
            else
            {
                CUDD.Deref(e.Precondition);
            }

            //Console.ReadLine();

        }

        private void AddToActionDict(PlanningParser.ActionDefineContext context, string[] constArray, StringDictionary assignment)
        {
            Action action = new Action(context, _eventDict, constArray, assignment);
            _actionDict.Add(action.FullName, action);
        }

        private void AddToObservationDict(PlanningParser.ObservationDefineContext context, string[] constArray, StringDictionary assignment)
        {
            Observation observation = new Observation(context, _predicateDict, _eventDict, constArray, assignment);
            Console.WriteLine(observation.FullName);

            if (!observation.Precondition.Equals(CUDD.ZERO))
            {
                Console.WriteLine(observation.FullName);
                _obervationDict.Add(observation.FullName, observation);
            }
            else
            {
                CUDD.Deref(observation.Precondition);
            }

            //Console.ReadLine();
        }

        #endregion

        private void HandleInit(PlanningParser.InitContext context)
        {
            TruePredSet = new HashSet<string>();
            foreach (var atomForm in context.constTermAtomForm())
            {
                var constTermContexts = atomForm.constTerm();
                int count = constTermContexts.Count;
                string[] termArray = new string[count];
                Parallel.For(0, count, i => termArray[i] = constTermContexts[i].GetText());

                string predicateFullname = ConstContainer.GetFullName(atomForm.predicate().GetText(), termArray);

                TruePredSet.Add(predicateFullname);
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine("Domain name: {0}", DomainName);
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Problem Name: {0}", ProblemName);
            Console.WriteLine(Domain.BarLine);

            Globals.TermInterpreter.ShowInfo();

            Console.WriteLine("Predicates:");
            foreach (var pair in _predicateDict)
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
                Console.WriteLine("    Number of nodes: {0}", CUDD.GetNumNodes(e.Precondition));

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
                foreach (var response in action.ResponseDict.Values)
                {
                    Console.WriteLine("    Name: {0}", response.FullName);
                    for (int i = 0; i < response.EventCollectionList.Count; i++)
                    {
                        Console.WriteLine("    Plausibility degree: {0}", i);
                        Console.Write("    Event list: ");
                        foreach (var e in response.EventCollectionList[i])
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