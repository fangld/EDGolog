﻿using System;
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
            HandleEventsDefine(domainContext.eventDefine());
            Console.WriteLine("Finishing event define!");
            HandleActionsDefine(domainContext.actionDefine());
            Console.WriteLine("Finishing action define!");
            HandleObservationsDefine(domainContext.observationDefine());
            Console.WriteLine("Finishing observation define!");
            //HandleInit(serverProblemContext.init());
            //Console.WriteLine("Finishing init object base!");
        }

        #endregion

        #region Methods

        public static ServerProblem CreateInstance(PlanningParser.DomainContext domainContext, PlanningParser.ServerProblemContext context)
        {
            ServerProblem result = new ServerProblem(domainContext, context);
            return result;
        }

        private void HandlePredicateDefine(PlanningParser.PredicateDefineContext context)
        {
            _predicateDict = new Dictionary<string, Predicate>();
            foreach (var atomFormSkeleton in context.atomFormSkeleton())
            {
                IReadOnlyList<IList<string>> collection = atomFormSkeleton.listVariable().GetCollection();
                PredicateEnumerator enumerator = new PredicateEnumerator(atomFormSkeleton, collection, _predicateDict, _currentCuddIndex);
                Algorithms.IterativeScanMixedRadix(enumerator);
                _currentCuddIndex = enumerator.CurrentCuddIndex;
            }
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
                IReadOnlyList<IList<string>> collection = actionDefineContext.listVariable().GetCollection();
                IReadOnlyList<string> variableNameList = actionDefineContext.listVariable().GetVariableNameList();
                ActionEnumerator enumerator = new ActionEnumerator(actionDefineContext, collection, variableNameList,
                    _eventDict, _actionDict);
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
                    variableNameList, _predicateDict, _eventDict, _obervationDict);
                Algorithms.IterativeScanMixedRadix(enumerator);
            }
        }

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

            //Console.WriteLine("Initial state:");
            //foreach (var pred in TruePredSet)
            //{
            //    Console.WriteLine("  {0}", pred);
            //}

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
                Console.WriteLine("    Number of nodes: {0}", CUDD.GetNumNodes(e.ParitalSuccessorStateAxiom));
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
                    for (int i = 0; i < response.EventCollectionList.Count; i++)
                    {
                        Console.WriteLine("      Plausibility degree: {0}", i);
                        Console.Write("      Event list: ");
                        foreach (var e in response.EventCollectionList[i])
                        {
                            Console.Write("{0}  ", e.FullName);
                        }
                        Console.WriteLine();
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
                //Console.WriteLine("  Response:");
                //Console.WriteLine("    Name: {0}", response.FullName);
                for (int i = 0; i < observation.EventCollectionList.Count; i++)
                {
                    Console.WriteLine("      Plausibility degree: {0}", i);
                    Console.Write("      Event list: ");
                    foreach (var e in observation.EventCollectionList[i])
                    {
                        Console.Write("{0}  ", e.FullName);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.WriteLine(Domain.BarLine);
        }

        #endregion
    }
}