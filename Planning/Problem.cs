﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public abstract class Problem<TD, TA, TAP, TGAP>
        where TD : Domain<TA, TAP>
        where TA : Action<TAP>, new()
        where TAP : AbstractPredicate, new()
        where TGAP : GroundAction<TA, TAP>, new()
    {
        #region Fields

        private Dictionary<string, string> _constantTypeMap;

        private Dictionary<string, List<string>> _typeConstantListMap;

        private Dictionary<string, Ground<Predicate>> _gndPredDict;

        private Dictionary<string, TGAP> _gndActionDict;

        private IReadOnlyDictionary<string, Predicate> _predDict;

        private IReadOnlyDictionary<string, TA> _actionDict;

        private List<string> _agentList;

        private int _currentCuddIndex;

        #endregion

        #region Properties

        public string Name { get; set; }

        public string DomainName { get; set; }

        public string HostId { get; set; }

        public TD Domain { get; set; }

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

        public IReadOnlyDictionary<string, Ground<Predicate>> GroundPredicateDict
        {
            get { return _gndPredDict; }
        }

        public IReadOnlyDictionary<string, TGAP> GroundActionDict
        {
            get { return _gndActionDict; }
        }

        #endregion

        #region Methods

        public void From(TD domain)
        {
            _constantTypeMap = new Dictionary<string, string>();
            _typeConstantListMap = new Dictionary<string, List<string>>();
            TruePredSet = new HashSet<string>();
            _agentList = new List<string>();
            Domain = domain;
            _predDict = domain.PredicateDict;
            _actionDict = domain.ActionDict;
            _gndPredDict = new Dictionary<string, Ground<Predicate>>();
            _gndActionDict = new Dictionary<string, TGAP>();
            _currentCuddIndex = domain.CurrentCuddIndex;
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
            Ground<Predicate> gndPred = new Ground<Predicate>(pred, constantList);
            gndPred.CuddIndex = _currentCuddIndex;
            _currentCuddIndex++;
            _gndPredDict.Add(gndPred.ToString(), gndPred);
        }

        private void AddToGroundActionDict(string actionName, string[] constantList)
        {
            TA action = _actionDict[actionName];
            TGAP gndAction = new TGAP();
            gndAction.From(action, constantList, _gndPredDict);
            //Console.WriteLine("Ground action: {0}", gndAction);
            //CUDD.Print.PrintMinterm(gndAction.Precondition);
            _gndActionDict.Add(gndAction.ToString(), gndAction);
        }

        public abstract void ShowInfo();

        #endregion
    }
}
