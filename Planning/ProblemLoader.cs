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
            _preGndPredDict = new Dictionary<string, Ground<Predicate>>();
            _sucGndPredDict = new Dictionary<string, Ground<Predicate>>();
            _gndActionDict = new Dictionary<string, GroundAction>();
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
                    : VariableContainer.DefaultType;

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
                    Tuple<string, string> variable = pred.VariableList[i];
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
            Ground<Predicate> preGndPred = new Ground<Predicate>(pred, variableList);
            preGndPred.CuddIndex = _currentCuddIndex;
            _currentCuddIndex++;
            _preGndPredDict.Add(preGndPred.ToString(), preGndPred);

            Ground<Predicate> sucGndPred = new Ground<Predicate>(pred, variableList);
            sucGndPred.CuddIndex = _currentCuddIndex;
            _currentCuddIndex++;
            _sucGndPredDict.Add(sucGndPred.ToString(), sucGndPred);
        }

        private void BuildGroundActions()
        {
            foreach (var action in _actionDict.Values)
            {
                List<List<string>> collection = new List<List<string>>();

                for (int i = 0; i < action.Count; i++)
                {
                    Tuple<string, string> variable = action.VariableList[i];
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

            GroundAction gndAction = new GroundAction(action, variableList);
            GenerateGroundPrecondition(gndAction);
            _gndActionDict.Add(gndAction.ToString(), gndAction);
        }

        private void GenerateGroundPrecondition(GroundAction gndAction)
        {
            CUDDVars oldVars = new CUDDVars();
            CUDDVars newVars = new CUDDVars();

            Dictionary<string, string> abstractParmMap = new Dictionary<string, string>();

            Console.WriteLine("  Ground action constant list count:{0}", gndAction.ConstantList.Count);

            for (int i = 0; i < gndAction.ConstantList.Count; i++)
            {
                string abstractParm = gndAction.Container.VariableList[i].Item1;
                string gndParm = gndAction.ConstantList[i];
                abstractParmMap.Add(abstractParm, gndParm);
                Console.WriteLine("    Parameter:{0}, Constant:{1}", abstractParm, gndParm);
            }

            foreach (var preAbstractPred in gndAction.Container.PreviousAbstractPredicates)
            {
                oldVars.AddVar(CUDD.Var(preAbstractPred.CuddIndex));
                List<string> collection = new List<string>();
                foreach (var parm in preAbstractPred.ParameterList)
                {
                    collection.Add(abstractParmMap[parm]);
                }

                Ground<Predicate> gndPred = new Ground<Predicate>(preAbstractPred.Predicate, collection);
                gndPred = _preGndPredDict[gndPred.ToString()];
                newVars.AddVar(CUDD.Var(gndPred.CuddIndex));

                Console.WriteLine("  old cuddIndex:{0}, new cuddIndex:{1}", preAbstractPred.CuddIndex, gndPred.CuddIndex);
            }

            CUDDNode abstractPre = gndAction.Container.Precondition;
            Console.WriteLine("  Abstract precondition:");
            CUDD.Print.PrintMinterm(abstractPre);

            gndAction.Precondition = CUDD.Variable.SwapVariables(abstractPre, oldVars, newVars);
            Console.WriteLine("  Ground precondition:");
            CUDD.Print.PrintMinterm(gndAction.Container.Precondition);

            //CUDDNode abstractEff = gndAction.VariableContainer.Effect;
            //gndAction.VariableContainer.Effect = CUDD.Variable.SwapVariables(abstractEff, oldVars, newVars);
        }

        //private void GenerateGroundEffect(Ground<Action> gndAction)
        //{
        //    CUDDVars oldVars = new CUDDVars();
        //    CUDDVars newVars = new CUDDVars();

        //    Dictionary<string, string> abstractParmMap = new Dictionary<string, string>();

        //    for (int i = 0; i < gndAction.ParamList.Count; i++)
        //    {
        //        string abstractParm = gndAction.VariableContainer.VariableTypeList[i].Item1;
        //        string gndParm = gndAction.ParamList[i];
        //        abstractParmMap.Add(abstractParm, gndParm);
        //    }

        //    foreach (var preAbstractPred in gndAction.VariableContainer.PreviousAbstractPredicates)
        //    {
        //        oldVars.AddVar(CUDD.Var(preAbstractPred.CuddIndex));
        //        List<string> collection = new List<string>();
        //        foreach (var parm in preAbstractPred.VariableNameList)
        //        {
        //            collection.Add(abstractParmMap[parm]);
        //        }

        //        Ground<Predicate> gndPred = new Ground<Predicate>(preAbstractPred, collection);
        //        gndPred = _preGndPredDict[gndPred.ToString()];
        //        newVars.AddVar(CUDD.Var(gndPred.CuddIndex));
        //    }

        //    CUDDNode abstractPre = gndAction.VariableContainer.Precondition;
        //    gndAction.VariableContainer.Precondition = CUDD.Variable.SwapVariables(abstractPre, oldVars, newVars);

        //    //CUDDNode abstractEff = gndAction.VariableContainer.Effect;
        //    //gndAction.VariableContainer.Effect = CUDD.Variable.SwapVariables(abstractEff, oldVars, newVars);
        //}

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

            Console.WriteLine("Ground predicates:");
            foreach (var pair in _preGndPredDict)
            {
                Console.WriteLine("  Name: {0}, Index: {1}", pair.Key, pair.Value.CuddIndex);
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

                //Console.WriteLine("    Previous Abstract Predicates: ");
                //foreach (var abstractPredicate in gndAction.PreviousAbstractPredicates)
                //{
                //    Console.WriteLine("      Name: {0}, CuddIndex: {1}", abstractPredicate, abstractPredicate.CuddIndex);
                //}

                //Console.WriteLine("    Successive Abstract Predicates: ");
                //foreach (var abstractPredicate in gndAction.SuccessiveAbstractPredicates)
                //{
                //    Console.WriteLine("      Name: {0}, CuddIndex: {1}", abstractPredicate, abstractPredicate.CuddIndex);
                //}
                Console.WriteLine("  Precondition:");
                CUDD.Print.PrintMinterm(gndAction.Container.Precondition);

                //Console.WriteLine("  Effect:");
                //CUDD.Print.PrintMinterm(gndAction.Effect);

                Console.WriteLine();
            }


            //foreach (var pair in _actionDict)
            //{
            //    Console.WriteLine("  Name: {0}, Index: {1}", pair.Key, pair.Value.CuddIndex);
            //}
        }

        #endregion
    }
}
