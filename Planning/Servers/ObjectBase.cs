﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Servers
{
    public class ObjectBase
    {
        #region Fields

        private Dictionary<string, bool> _predBooleanMap;

        private IReadOnlyDictionary<string, Predicate> _predicateDict;

        #endregion

        #region Constructors

        public ObjectBase(ServerProblem problem)
        {
            _predBooleanMap = new Dictionary<string, bool>();
            _predicateDict = problem.PredicateDict;
            foreach (var predicateFullName in _predicateDict.Keys)
            {
                bool value = problem.TruePredSet.Contains(predicateFullName);
                _predBooleanMap.Add(predicateFullName, value);
            }
        }

        #endregion

        #region Events

        public EventHandler<Tuple<IReadOnlyDictionary<string, bool>, Event>> ObjectBaseChanged;

        #endregion

        #region Methods

        private CUDDNode GetKbNode()
        {
            List<CUDDNode> literalNodes = new List<CUDDNode>();

            foreach (var pair in _predBooleanMap)
            {
                string name = pair.Key;
                int index = _predicateDict[name].PreviousCuddIndex;
                CUDDNode node = pair.Value ? CUDD.Var(index) : CUDD.Function.Not(CUDD.Var(index));
                literalNodes.Add(node);
            }

            CUDDNode result = literalNodes[0];
            for (int i = 1; i < literalNodes.Count; i++)
            {
                result = CUDD.Function.And(result, literalNodes[i]);
            }
            return result;
        }

        public Response Update(Action action)
        {
            CUDDNode kbNode = GetKbNode();

            List<Response> responseList = new List<Response>();

            foreach (var pair in action.ResponseDict)
            {
                CUDDNode responsePrecondition = pair.Value.EventCollectionList.GetPrecondition();
                
                CUDD.Ref(kbNode);

                CUDDNode impliesNode = CUDD.Function.Implies(kbNode, responsePrecondition);
                if (impliesNode.Equals(CUDD.ONE))
                {
                    responseList.Add(pair.Value);
                }
            }

            int selectiveRepsonseIndex = Globals.Random.Next(responseList.Count);
            Response result = responseList[selectiveRepsonseIndex];
            IReadOnlyList<Event> eventList = result.EventList;

            foreach (var e in eventList)
            {
                CUDD.Ref(e.Precondition);
                CUDDNode impliesNode = CUDD.Function.Implies(kbNode, e.Precondition);
                if (impliesNode.Equals(CUDD.ONE))
                {
                    var literalList = GenerateLiteralList(kbNode, e);
                    UpdateByLiteralList(literalList);
                    if (ObjectBaseChanged != null)
                    {
                        var tuple = new Tuple<IReadOnlyDictionary<string, bool>, Event>(_predBooleanMap, e);
                        ObjectBaseChanged(this, tuple);
                    }
                    break;
                }
            }
            return result;
        }

        private List<Tuple<Predicate, bool>> GenerateLiteralList(CUDDNode node, Event e)
        {
            List<Tuple<Predicate, bool>> result = new List<Tuple<Predicate, bool>>();
            foreach (var cEffect in e.CondEffect)
            {
                CUDD.Ref(node);
                CUDD.Ref(cEffect.Item1);
                CUDDNode impliesNode = CUDD.Function.Implies(node, cEffect.Item1);
                if (impliesNode.Equals(CUDD.ONE))
                {
                    result.AddRange(cEffect.Item2);
                }
            }
            return result;
        }

        private void UpdateByLiteralList(List<Tuple<Predicate, bool>> literalList)
        {
            foreach (var literal in literalList)
            {
                string predicateFullName = literal.Item1.ToString();
                _predBooleanMap[predicateFullName] = literal.Item2;
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine("Object base:");
            foreach (var pair in _predBooleanMap)
            {
                Console.WriteLine("  Predicate: {0}, Value: {1}", pair.Key, pair.Value);
            }
        }

        #endregion
    }
}
