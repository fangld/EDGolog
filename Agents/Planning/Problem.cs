using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Agents.Planning
{
    public class Problem
    {
        #region Fields

        private Dictionary<string, string> _constantTypeMap;

        private Dictionary<string, List<string>> _typeConstantListMap;

        private Dictionary<string, GroundPredicate> _gndPredDict;

        private Dictionary<string, GroundAction> _gndActionDict;

        private IReadOnlyDictionary<string, Predicate> _predDict;

        private IReadOnlyDictionary<string, Action> _actionDict;

        private List<string> _agentList;

        private int _currentCuddIndex;

        #endregion

        #region Properties

        public string Name { get; set; }

        public string DomainName { get; set; }

        public string AgentId { get; set; }

        public Domain Domain { get; set; }

        public HashSet<string> TruePredSet { get; set; }

        public IReadOnlyList<string> AgentList
        {
            get { return _agentList; }
        }

        public IReadOnlyDictionary<string, string> ConstantTypeMap
        {
            get { return _constantTypeMap; }
        }

        public IReadOnlyDictionary<string, List<string>> TypeConstantListMap
        {
            get { return _typeConstantListMap; }
        }

        public IReadOnlyDictionary<string, GroundPredicate> GroundPredicateDict
        {
            get { return _gndPredDict; }
        }

        public IReadOnlyDictionary<string, GroundAction> GroundActionDict
        {
            get { return _gndActionDict; }
        }

        public CUDDNode Knowledge { get; set; }

        public CUDDNode Belief { get; set; }


        #endregion

        #region Constructors

        public Problem(Domain domain)
        {
            _constantTypeMap = new Dictionary<string, string>();
            _typeConstantListMap = new Dictionary<string, List<string>>();
            TruePredSet = new HashSet<string>();
            _agentList = new List<string>();
            Domain = domain;
            _predDict = domain.PredicateDict;
            _actionDict = domain.ActionDict;
            _gndPredDict = new Dictionary<string, GroundPredicate>();
            _gndActionDict = new Dictionary<string, GroundAction>();
            _currentCuddIndex = domain.CurrentCuddIndex;
        }

        #endregion

        #region Methods

        public void ShowInfo()
        {
            const string barline = "----------------";

            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine(barline);

            Console.WriteLine("Domain name: {0}", DomainName);
            Console.WriteLine(barline);

            Console.WriteLine("Agents:");
            foreach (var agent in _agentList)
            {
                Console.WriteLine("  {0}", agent);
            }
            Console.WriteLine(barline);

            Console.WriteLine("Variables:");
            foreach (var pair in _constantTypeMap)
            {
                Console.WriteLine("  {0} - {1}", pair.Key, pair.Value);
            }
            Console.WriteLine(barline);

            Console.WriteLine("Ground predicates:");
            Console.WriteLine("  Previous:");
            foreach (var pair in _gndPredDict)
            {
                Console.WriteLine("    Name: {0}, Previous index: {1}, Successsor index:{2}", pair.Key,
                    pair.Value.PreviousCuddIndex, pair.Value.SuccessorCuddIndex);
            }
            Console.WriteLine(barline);

            Console.WriteLine("Initial state:");
            foreach (var pred in TruePredSet)
            {
                Console.WriteLine("  {0}", pred);
            }
            Console.WriteLine(barline);

            Console.WriteLine("Ground actions:");
            foreach (var gndAction in _gndActionDict.Values)
            {
                Console.WriteLine("  Name: {0}", gndAction.Container.Name);
                Console.WriteLine("  Variable: {0}", gndAction.Container.Count);
                for (int i = 0; i < gndAction.ConstantList.Count; i++)
                {
                    Console.WriteLine("    Index: {0}, Name: {1}", i, gndAction.ConstantList[i]);
                }
                Console.WriteLine("  Precondition:");
                CUDD.Print.PrintMinterm(gndAction.Precondition);

                Console.WriteLine("  Successor state axiom:");
                CUDD.Print.PrintMinterm(gndAction.SuccessorStateAxiom);

                //Console.WriteLine("  Effect:");
                //for (int i = 0; i < gndAction.Effect.Count; i++)
                //{
                //    Console.WriteLine("      Index:{0}", i);
                //    Console.WriteLine("      Condition:");
                //    CUDD.Print.PrintMinterm(gndAction.Effect[i].Item1);

                //    Console.Write("      Literals: { ");
                //    var literal = gndAction.Effect[i].Item2[0];
                //    if (literal.Item2)
                //    {
                //        Console.Write("{0}", literal.Item1);
                //    }
                //    else
                //    {
                //        Console.Write("not {0}", literal.Item1);
                //    }

                //    for (int j = 1; j < gndAction.Effect[i].Item2.Count; j++)
                //    {
                //        if (literal.Item2)
                //        {
                //            Console.Write(", {0}", literal.Item1);
                //        }
                //        else
                //        {
                //            Console.Write(", not {0}", literal.Item1);
                //        }
                //    }

                //    Console.WriteLine(" }");
                //}
            }
        }

        internal void AddAgent(string name)
        {
            _agentList.Add(name);
        }

        internal void BuildConstantTypeMap(PlanningParser.ListNameContext listNameContext)
        {
            do
            {
                string type = listNameContext.type() != null
                    ? listNameContext.type().GetText()
                    : VariableContainer.DefaultType;

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

        internal void BuildGroundPredicate()
        {
            BuildGround(_predDict.Values, AddToGroundPredicateDict);            
        }

        internal void BuildGroundAction()
        {
            BuildGround(_actionDict.Values, AddToGroundActionDict);
        }

        internal void BuildTruePredicateSet(PlanningParser.InitContext context)
        {
            foreach (var atomicFormula in context.atomicFormulaName())
            {
                var nameNodes = atomicFormula.NAME();
                List<string> termList = new List<string>();
                foreach (var nameNode in nameNodes)
                {
                    termList.Add(nameNode.GetText());
                }
                string gndPredName = VariableContainer.GetFullName(atomicFormula.predicate().GetText(), termList);

                TruePredSet.Add(gndPredName);
            }
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

        private void AddToGroundPredicateDict(string predName, string[] constantList)
        {
            Predicate pred = _predDict[predName];
            GroundPredicate gndPred = new GroundPredicate(pred, constantList);
            gndPred.PreviousCuddIndex = _currentCuddIndex;
            _currentCuddIndex++;
            gndPred.SuccessorCuddIndex = _currentCuddIndex;
            _currentCuddIndex++;
            _gndPredDict.Add(gndPred.ToString(), gndPred);
        }

        private void AddToGroundActionDict(string actionName, string[] constantList)
        {
            Action action = _actionDict[actionName];

            GroundAction gndAction = GroundAction.CreateInstance(action, constantList, _gndPredDict);
            _gndActionDict.Add(gndAction.ToString(), gndAction);
        }

        private GroundPredicate GetGroundPredicate(PlanningParser.AtomicFormulaNameContext context)
        {
            string gndPredName = VariableContainer.GetFullName(context);
            GroundPredicate result = _gndPredDict[gndPredName];
            return result;
        }

        #endregion

        #region Methods for generating knowledge and belief

        public void GenerateKnowledge(PlanningParser.GdNameContext context)
        {
            Knowledge = GetCuddNode(context);
        }

        public void GenerateBelief(PlanningParser.GdNameContext context)
        {
            Belief = GetCuddNode(context);
        }

        private CUDDNode GetCuddNode(PlanningParser.AtomicFormulaNameContext context)
        {
            GroundPredicate gndPred = GetGroundPredicate(context);

            int index = gndPred.PreviousCuddIndex;

            CUDDNode result = CUDD.Var(index);
            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.LiteralNameContext context)
        {
            CUDDNode subNode = GetCuddNode(context.atomicFormulaName());
            CUDDNode result;

            if (context.NOT() != null)
            {
                result = CUDD.Function.Not(subNode);
                CUDD.Ref(result);
            }
            else
            {
                result = subNode;
            }

            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.GdNameContext context)
        {
            CUDDNode result = null;

            if (context.atomicFormulaName() != null)
            {
                result = GetCuddNode(context.atomicFormulaName());
            }
            else if (context.literalName() != null)
            {
                result = GetCuddNode(context.literalName());
            }
            else if (context.AND() != null)
            {
                result = GetCuddNode(context.gdName()[0]);
                for (int i = 1; i < context.gdName().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gdName()[i]);
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Ref(andNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = GetCuddNode(context.gdName()[0]);
                for (int i = 1; i < context.gdName().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gdName()[i]);
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    CUDD.Ref(orNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = GetCuddNode(context.gdName()[0]);
                result = CUDD.Function.Not(gdNode);
                CUDD.Ref(result);
                CUDD.Deref(gdNode);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = GetCuddNode(context.gdName()[0]);
                CUDDNode gdNode1 = GetCuddNode(context.gdName()[1]);

                result = CUDD.Function.Implies(gdNode0, gdNode1);
                CUDD.Ref(result);
                CUDD.Deref(gdNode0);
                CUDD.Deref(gdNode1);
            }

            return result;
        }

        #endregion
    }
}
