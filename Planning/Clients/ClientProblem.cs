using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.Collections;
using Planning.ContextExtensions;

namespace Planning.Clients
{
    public class ClientProblem
    {
        #region Fields

        private Dictionary<string, Predicate> _predicateDict;

        private Dictionary<string, Event> _eventDict;

        private Dictionary<string, Action> _actionDict;

        private Dictionary<string, Observation> _obervationDict;

        private Dictionary<string, Agent> _agentDict;

        private List<string> _agentList;

        private int _currentCuddIndex;

        #endregion

        #region Properties

        public string DomainName { get; set; }

        public string ProblemName { get; set; }

        public string AgentId { get; set; }

        public CUDDNode InitKnowledge { get; set; }

        public CUDDNode InitBelief { get; set; }

        public IReadOnlyDictionary<string, Predicate> PredicateDict
        {
            get { return _predicateDict; }
        }

        #endregion

        #region Constructors

        private ClientProblem(PlanningParser.DomainContext domainContext, PlanningParser.ClientProblemContext problemContext)
        {
            _currentCuddIndex = 0;

            DomainName = domainContext.NAME().GetText();
            ProblemName = problemContext.problemName().GetText();
            Console.WriteLine("Finishing setting name!");

            Globals.TermInterpreter = new TermInterpreter(problemContext.numericSetting(), domainContext.typeDefine(),
                problemContext.objectDeclaration());
            Console.WriteLine("Finishing genertating term interpreter!");

            GenerateAgentDict();
            Console.WriteLine("Finishing genertating agent!");

            HandlePredicateDefine(domainContext.predicateDefine());
            Console.WriteLine("Finishing handling predicate!");
            //Console.ReadLine();

            HandleEventsDefine(domainContext.eventDefine());
            Console.WriteLine("Finishing handling event define!");
            //Console.ReadLine();

            HandleActionsDefine(domainContext.actionDefine());
            Console.WriteLine("Finishing handling action define!");
            //Console.ReadLine();

            HandleObservationsDefine(domainContext.observationDefine());
            Console.WriteLine("Finishing handling observation define!");
            //Console.ReadLine();

            HandleInitKnowledge(problemContext.initKnowledge());
            Console.WriteLine("Finishing handling init knowledge!");

            HandleInitBelief(problemContext.initBelief());
            Console.WriteLine("Finishing handling init knowledge!");

            GenerateAgentList();
            Console.WriteLine("Finishing generating agent list!");
        }

        #endregion

        #region Methods

        public static ClientProblem CreateInstance(PlanningParser.DomainContext domainContext, PlanningParser.ClientProblemContext context)
        {
            ClientProblem result = new ClientProblem(domainContext, context);
            return result;
        }

        private void GenerateAgentDict()
        {
            _agentDict = new Dictionary<string, Agent>();
            Agent a1 = new Agent("a1");
            Agent a2 = new Agent("a2");
            _agentDict.Add(a1.Name, a1);
            _agentDict.Add(a2.Name, a2);
        }

        private void HandlePredicateDefine(PlanningParser.PredicateDefineContext context)
        {
            _predicateDict = new Dictionary<string, Predicate>();
            foreach (var atomFormSkeleton in context.atomFormSkeleton())
            {
                PredicateEnumerator enumerator = new PredicateEnumerator(atomFormSkeleton, _predicateDict, _currentCuddIndex);
                Algorithms.IterativeScanMixedRadix(enumerator);
                _currentCuddIndex = enumerator.CurrentCuddIndex;
            }

            foreach (var predicate in _predicateDict.Values)
            {
                Console.WriteLine("name: {0}, Previous index: {1}, successive index: {2}", predicate.FullName, predicate.PreviousCuddIndex, predicate.SuccessiveCuddIndex);
            }
            //Console.ReadLine();
        }

        private void HandleEventsDefine(IReadOnlyList<PlanningParser.EventDefineContext> contexts)
        {
            _eventDict = new Dictionary<string, Event>();
            foreach (var eventDefineContext in contexts)
            {
                IReadOnlyList<IList<string>> collection = eventDefineContext.listVariable().GetCollection();
                IReadOnlyList<string> variableNameList = eventDefineContext.listVariable().GetVariableNameList();
                EventEnumerator enumerator = new EventEnumerator(eventDefineContext, collection, variableNameList,
                    _predicateDict, _eventDict, _currentCuddIndex);
                Algorithms.IterativeScanMixedRadix(enumerator);
                _currentCuddIndex = enumerator.CurrentCuddIndex;
            }
        }

        private void HandleActionsDefine(IReadOnlyList<PlanningParser.ActionDefineContext> contexts)
        {
            _actionDict = new Dictionary<string, Action>();
            foreach (var actionDefineContext in contexts)
            {
                ActionEnumerator enumerator = new ActionEnumerator(actionDefineContext, _eventDict, _agentDict, _actionDict);
                Algorithms.IterativeScanMixedRadix(enumerator);
            }
        }

        private void HandleObservationsDefine(IReadOnlyList<PlanningParser.ObservationDefineContext> contexts)
        {
            _obervationDict = new Dictionary<string, Observation>();
            foreach (var obsDefineContext in contexts)
            {
                IReadOnlyList<IList<string>> collection = obsDefineContext.listVariable().GetCollection();
                IReadOnlyList<string> variableNameList = obsDefineContext.listVariable().GetVariableNameList();
                ObservationEnumerator enumerator = new ObservationEnumerator(obsDefineContext, collection,
                    variableNameList, _predicateDict, _eventDict, _agentDict, _obervationDict);
                Algorithms.IterativeScanMixedRadix(enumerator);
            }
        }

        private void HandleInitKnowledge(PlanningParser.InitKnowledgeContext context)
        {
            if (context != null)
            {
                StringDictionary assignment = new StringDictionary();
                InitKnowledge = context.gd().GetCuddNode(_predicateDict, assignment);
            }
            else
            {
                InitKnowledge = CUDD.Constant(1);
            }
        }

        private void HandleInitBelief(PlanningParser.InitBeliefContext context)
        {
            if (context != null)
            {
                StringDictionary assignment = new StringDictionary();
                InitBelief = context.gd().GetCuddNode(_predicateDict, assignment);
            }
            else
            {
                InitBelief = InitKnowledge;
            }
        }

        private void GenerateAgentList()
        {
            _agentList = new List<string> { "a1", "a2" };
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

            List<string> eventNameArray = new List<string>
            {
                "leftFail(a1)",
                "rightSucWithNotice(a2,-2)",
                "leftSucWithNotice(a1,0)",
                "leftSucWithoutNotice(a1)",
                "leftSucWithoutNotice(a2)",
                "rightSucWithoutNotice(a1)",
                "rightSucWithoutNotice(a2)",
                "dropFail(a1)",
                "dropSuc(a1)",
                "pickSuc(a2)",
                "learn(a1,1,-1)",
                "learn(a1,1,0)"
            };

            Console.WriteLine("Events:");

            for (int i = 0; i < eventNameArray.Count; i++)
            {
                string eventName = eventNameArray[i];
                Event e = _eventDict[eventName];
                Console.WriteLine("  Name: {0}", eventNameArray[i]);
                Console.WriteLine("  Cudd index: {0}", e.CuddIndex);

                Console.WriteLine("  Precondition:");
                Console.WriteLine("    Number of nodes: {0}", CUDD.GetNumNodes(e.Precondition));

                //Console.WriteLine("  CondEffect:");
                ////Console.WriteLine("  Count:{0}", e.CondEffect.Count);
                //for (int j = 0; j < e.CondEffect.Count; j++)
                //{
                //    Console.WriteLine("    Index: {0}", j);
                //    //Console.WriteLine("    Condition:");
                //    //CUDD.Print.PrintMinterm(e.CondEffect[j].Item1);
                //    Console.Write("    Literals: ");

                //    foreach (var literal in e.CondEffect[j].Item2)
                //    {
                //        string format = literal.Item2 ? "{0} " : "!{0} ";
                //        Console.Write(format, literal.Item1);
                //    }

                //    Console.WriteLine();
                //}

                Console.WriteLine("  Partial successor state axiom:");
                Console.WriteLine("    Number of nodes: {0}", CUDD.GetNumNodes(e.PartialSsa));
                Console.WriteLine();
            }
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Actions:");

            foreach (var action in _actionDict.Values)
            {
                Console.WriteLine("  Name: {0}", action.FullName);
                Console.WriteLine("  Response:");
                foreach (var response in action.ResponseDict.Values)
                {
                    Console.WriteLine("    Name: {0}", response.FullName);
                    Console.Write("      Believe Event list: ");
                    foreach (var e in response.EventModel.BelieveEventList)
                    {
                        Console.Write("{0}  ", e.FullName);
                    }
                    Console.WriteLine();
                    Console.Write("      Know Event list: ");
                    foreach (var e in response.EventModel.KnowEventList)
                    {
                        Console.Write("{0}  ", e.FullName);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Observations:");

            foreach (var observation in _obervationDict.Values)
            {
                Console.WriteLine("  Name: {0}", observation.FullName);
                Console.Write("      Believe Event list: ");
                foreach (var e in observation.EventModel.BelieveEventList)
                {
                    Console.Write("{0}  ", e.FullName);
                }
                Console.WriteLine();
                Console.Write("      Know Event list: ");
                foreach (var e in observation.EventModel.KnowEventList)
                {
                    Console.Write("{0}  ", e.FullName);
                }
                Console.WriteLine();
            }
            Console.WriteLine(Domain.BarLine);
        }

        #endregion
    }
}
