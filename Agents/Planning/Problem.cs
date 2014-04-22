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

        private Dictionary<string, Ground<Predicate>> _preGndPredDict;

        private Dictionary<string, Ground<Predicate>> _sucGndPredDict;

        private Dictionary<string, GroundAction> _gndActionDict;

        private IReadOnlyDictionary<string, Predicate> _predDict;

        private IReadOnlyDictionary<string, Action> _actionDict;

        private List<string> _agentList;

        private int _currentCuddIndex;

        #endregion

        #region Properties

        public string Name { get; set; }

        public string DomainName { get; set; }

        public string HostId { get; set; }

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

        public IReadOnlyDictionary<string, Ground<Predicate>> PreviousGroundPredicateDict
        {
            get { return _preGndPredDict; }
        }

        public IReadOnlyDictionary<string, GroundAction> GroundActionDict
        {
            get { return _gndActionDict; }
        }

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
            _preGndPredDict = new Dictionary<string, Ground<Predicate>>();
            _sucGndPredDict = new Dictionary<string, Ground<Predicate>>();
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
            foreach (var pair in _preGndPredDict)
            {
                Console.WriteLine("    Name: {0}, Index: {1}", pair.Key, pair.Value.CuddIndex);
            }
            Console.WriteLine("  Successive:");
            foreach (var pair in _sucGndPredDict)
            {
                Console.WriteLine("    Name: {0}, Index: {1}", pair.Key, pair.Value.CuddIndex);
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

                Console.WriteLine("  Effect:");
                for (int i = 0; i < gndAction.Effect.Count; i++)
                {
                    Console.WriteLine("      Index:{0}", i);
                    Console.WriteLine("      Condition:");
                    CUDD.Print.PrintMinterm(gndAction.Effect[i].Item1);

                    Console.Write("      Literals: { ");
                    var literal = gndAction.Effect[i].Item2[0];
                    if (literal.Item2)
                    {
                        Console.Write("{0}", literal.Item1);
                    }
                    else
                    {
                        Console.Write("not {0}", literal.Item1);
                    }

                    for (int j = 1; j < gndAction.Effect[i].Item2.Count; j++)
                    {
                        if (literal.Item2)
                        {
                            Console.Write(", {0}", literal.Item1);
                        }
                        else
                        {
                            Console.Write(", not {0}", literal.Item1);
                        }
                    }

                    Console.WriteLine(" }");
                }
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
            foreach (var gdNameContext in context.gdName().gdName())
            {
                var afnContext = gdNameContext.atomicFormulaName();
                var nameNodes = afnContext.NAME();
                List<string> termList = new List<string>();
                foreach (var nameNode in nameNodes)
                {
                    termList.Add(nameNode.GetText());
                }
                string gndPredName = VariableContainer.GetFullName(afnContext.predicate().GetText(), termList);

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
            Ground<Predicate> preGndPred = new Ground<Predicate>(pred, constantList);
            preGndPred.CuddIndex = _currentCuddIndex;
            _currentCuddIndex++;
            _preGndPredDict.Add(preGndPred.ToString(), preGndPred);

            Ground<Predicate> sucGndPred = new Ground<Predicate>(pred, constantList);
            sucGndPred.CuddIndex = _currentCuddIndex;
            _currentCuddIndex++;
            _sucGndPredDict.Add(sucGndPred.ToString(), sucGndPred);
        }

        private void AddToGroundActionDict(string actionName, string[] constantList)
        {
            Action action = _actionDict[actionName];

            GroundAction gndAction = GroundAction.CreateInstance(action, constantList, _preGndPredDict);
            _gndActionDict.Add(gndAction.ToString(), gndAction);
        }

        #endregion
    }
}
