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

        public DomainLoader DomainLoader { get; set; }

        public HashSet<string> TruePredSet { get; set; }

        public IReadOnlyList<string> AgentList
        {
            get { return _agentList; }
        }

        public IReadOnlyDictionary<string, string> ConstantTypeMap
        {
            get { return _constantTypeMap; }
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

        public ProblemLoader(DomainLoader domainLoader)
        {
            _constantTypeMap = new Dictionary<string, string>();
            _typeConstantListMap = new Dictionary<string, List<string>>();
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
            BuildConstantTypeMap(listNameContext);
            BuildGround(_predDict.Values, AddToGroundPredicateDict);
            BuildGround(_actionDict.Values, AddToGroundActionDict);
        }

        public override void EnterInit(PlanningParser.InitContext context)
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

        #endregion

        #region Methods
        private void BuildConstantTypeMap(PlanningParser.ListNameContext listNameContext)
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

        private void BuildGround<T>(IEnumerable<T> containters, Action<string, string[]> action) where  T : VariableContainer
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


            //foreach (var pair in _actionDict)
            //{
            //    Console.WriteLine("  Name: {0}, Index: {1}", pair.Key, pair.Value.CuddIndex);
            //}
        }



        #endregion
    }
}