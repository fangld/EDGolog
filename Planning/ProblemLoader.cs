using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class ProblemLoader : PlanningBaseListener
    {
        #region Fields

        private Dictionary<string, string> _objNameTypeMap;

        private Dictionary<string, List<string>> _objTypeNamesMap;

        private Dictionary<string, Ground<Predicate>> _gndPredDict;

        private Dictionary<string, Ground<Action>> _gndActionDict;

        private IReadOnlyDictionary<string, Predicate> _predDict;

        private IReadOnlyDictionary<string, Action> _actionDict;

        private List<string> _agentList;

        private int _currentCuddIndex;

        #endregion

        #region Properties

        public string Name { get; set; }

        public string DomainName { get; set; }

        public DomainLoader DomainLoader { get; set; }

        public HashSet<string> TruePredSet { get; set; }

        public IReadOnlyList<string> AgentList
        {
            get { return _agentList; }
        }

        public IReadOnlyDictionary<string, string> ObjectNameTypeMapMap
        {
            get { return _objNameTypeMap; }
        }

        #endregion

        #region Constructors

        public ProblemLoader(DomainLoader domainLoader)
        {
            _objNameTypeMap = new Dictionary<string, string>();
            _objTypeNamesMap = new Dictionary<string, List<string>>();
            TruePredSet = new HashSet<string>();
            _agentList = new List<string>();
            DomainLoader = domainLoader;
            _predDict = domainLoader.PredicateDict;
            _actionDict = domainLoader.ActionDict;
            _gndPredDict = new Dictionary<string, Ground<Predicate>>();
            _gndActionDict = new Dictionary<string, Ground<Action>>();
            _currentCuddIndex = domainLoader.CurrentCuddIndex;
        }

        #endregion

        #region Methods

        #endregion

        #region Overriden Methods

        public override void EnterProblem(PlanningParser.ProblemContext context)
        {
            Name = context.problemName().GetText();
            DomainName = context.domainName().GetText();
        }

        public override void EnterAgentDefine(PlanningParser.AgentDefineContext context)
        {
            foreach (var nameNode in context.NAME())
            {
                _agentList.Add(nameNode.GetText());
            }
        }

        public override void EnterObjectDeclaration(PlanningParser.ObjectDeclarationContext context)
        {
            var listNameContext = context.listName();
            BuildObjectNamesTypeMap(listNameContext);
            BuildGroundPredicates();
            BuildGroundActions();
        }

        private void BuildObjectNamesTypeMap(PlanningParser.ListNameContext listNameContext)
        {
            do
            {
                string type = listNameContext.type() != null
                    ? listNameContext.type().GetText()
                    : DomainLoader.DefaultType;

                List<string> objNamesList;

                if (_objTypeNamesMap.ContainsKey(type))
                {
                    objNamesList = _objTypeNamesMap[type];
                }
                else
                {
                    objNamesList = new List<string>(listNameContext.NAME().Count);
                    _objTypeNamesMap.Add(type, objNamesList);
                }

                foreach (var nameNode in listNameContext.NAME())
                {
                    _objNameTypeMap.Add(nameNode.GetText(), type);
                    objNamesList.Add(nameNode.GetText());
                }

                listNameContext = listNameContext.listName();
            } while (listNameContext != null);
        }

        private void BuildGroundPredicates()
        {
            foreach (var pred in _predDict.Values)
            {
                List<List<string>> collection = new List<List<string>>();

                for (int i = 0; i < pred.Count; i++)
                {
                    Tuple<string, string> variable = pred.VariableTypeList[i];
                    List<string> objectList = _objTypeNamesMap[variable.Item2];
                    collection.Add(objectList);
                }

                ScanMixedRadixPredicate(pred.Name, collection);
            }
        }

        private void ScanMixedRadixPredicate(string predName, List<List<string>> collection)
        {
            int count = collection.Count;
            string[] scanArray = new string[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            for (int i = 0; i < count; i++)
            {
                maxIndex[i] = collection[i].Count;
            }

            do
            {
                for (int i = 0; i < count; i++)
                {
                    scanArray[i] = collection[i][index[i]];
                }

                AddToGroundPredicateDict(predName, scanArray);
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

        private void AddToGroundPredicateDict(string predName, string[] variableList)
        {
            Predicate pred = _predDict[predName];
            Ground<Predicate> gndPred = new Ground<Predicate>(pred, variableList);
            gndPred.CuddIndex = _currentCuddIndex;
            _currentCuddIndex++;
            _gndPredDict.Add(gndPred.ToString(), gndPred);
        }

        private void BuildGroundActions()
        {
            foreach (var action in _actionDict.Values)
            {
                List<List<string>> collection = new List<List<string>>();

                for (int i = 0; i < action.Count; i++)
                {
                    Tuple<string, string> variable = action.VariableTypeList[i];
                    List<string> objList = _objTypeNamesMap[variable.Item2];
                    collection.Add(objList);
                }

                ScanMixedRadixAction(action.Name, collection);
            }
        }

        private void ScanMixedRadixAction(string actionName, List<List<string>> collection)
        {
            int count = collection.Count;
            string[] scanArray = new string[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            for (int i = 0; i < count; i++)
            {
                maxIndex[i] = collection[i].Count;
            }

            do
            {
                for (int i = 0; i < count; i++)
                {
                    scanArray[i] = collection[i][index[i]];
                }

                AddToGroundActionDict(actionName, scanArray);
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

        private void AddToGroundActionDict(string actionName, string[] variableList)
        {
            Action action = _actionDict[actionName];

            Ground<Action> gndAction = new Ground<Action>(action, variableList);
            _gndActionDict.Add(gndAction.ToString(), gndAction);
        }

        private void SubstitutionPredication(CUDDNode precondition)
        {
            //precondition.
        }

        public override void EnterInit(PlanningParser.InitContext context)
        {
            foreach (var gdNameContext in context.gdName().gdName())
            {
                var afnContext = gdNameContext.atomicFormulaName();
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}(", afnContext.predicate().GetText());
                var nameNodes = afnContext.NAME();
                for (int i = 0; i < nameNodes.Count - 1; i++)
                {
                    sb.AppendFormat("{0},", nameNodes[i].GetText());
                }
                sb.AppendFormat("{0})", nameNodes[nameNodes.Count - 1].GetText());
                TruePredSet.Add(sb.ToString());
            }
        }

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

            //Console.WriteLine("Grounded predicates:");
            //foreach (var gndPred in _gndPredDict.Values)
            //{
            //    Console.WriteLine("  CuddIndex:{0}, Name:{1}", gndPred.CuddIndex, gndPred);
            //}

            Console.WriteLine("Variables:");
            foreach (var pair in _objNameTypeMap)
            {
                Console.WriteLine("  {0} - {1}", pair.Key, pair.Value);
            }
            Console.WriteLine(barline);

            Console.WriteLine("Initial state:");
            foreach (var pred in TruePredSet)
            {
                Console.WriteLine("  {0}", pred);
            }

            Console.WriteLine(barline);
        }

        #endregion
    }
}
