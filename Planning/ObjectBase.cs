//using System;
//using System.CodeDom.Compiler;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PAT.Common.Classes.CUDDLib;

//namespace Planning
//{
//    public class ObjectBase
//    {
//        #region Fields

//        private ServerProblem _problem;

//        private Dictionary<string, bool> _predBooleanMap;

//        #endregion

//        #region Constructors

//        public ObjectBase(Problem problem)
//        {
//            _problem = problem;
//            _predBooleanMap = new Dictionary<string, bool>();
//            foreach (var gndPred in _problem.GroundPredicateDict.Keys)
//            {
//                bool value = _problem.TruePredSet.Contains(gndPred);
//                _predBooleanMap.Add(gndPred, value);
//            }
//        }

//        #endregion

//        #region Methods

//        public CUDDNode GetKnowledgeBase()
//        {
//            List<CUDDNode> literalNodes = new List<CUDDNode>();

//            foreach (var gndPred in _predBooleanMap)
//            {
//                string name = gndPred.Key;
//                int index = _problem.GroundPredicateDict[name].CuddIndex;
//                CUDDNode node;

//                if (gndPred.Value)
//                {
//                    node = CUDD.Var(index);
//                }
//                else
//                {
//                    node = CUDD.Function.Not(CUDD.Var(index));
//                }
//                literalNodes.Add(node);
//            }

//            CUDDNode result = literalNodes[0];
//            for (int i = 1; i < literalNodes.Count; i++)
//            {
//                CUDDNode literalNode = literalNodes[i];
//                CUDDNode andNode = CUDD.Function.And(result, literalNode);
//                CUDD.Ref(andNode);
//                CUDD.Deref(result);
//                CUDD.Deref(literalNode);
//                result = andNode;
//            }
//            return result;
//        }

//        public void Update(GroundAction gndAction)
//        {
//            CUDDNode kbNode = GetKnowledgeBase();
//            CUDDNode preconditionNode = CUDD.Function.Implies(kbNode, gndAction.Precondition);

//            CUDD.Print.PrintMinterm(kbNode);

//            if (preconditionNode.GetValue() > 0.5)
//            {
//                var gndLiteralList = GenerateLiteralList(kbNode, gndAction);
//                UpdateByLiteralList(gndLiteralList);
//                CUDD.Deref(kbNode);
//            }
//            else
//            {
//                Console.WriteLine("    Action {0} is not executable now!", gndAction);
//            }
//        }

//        private List<Tuple<Ground<Predicate>, bool>> GenerateLiteralList(CUDDNode node, GroundAction gndAction)
//        {
//            List<Tuple<Ground<Predicate>, bool>> result = new List<Tuple<Ground<Predicate>, bool>>();
//            foreach (var cEffect in gndAction.Effect)
//            {
//                CUDDNode impliesNode = CUDD.Function.Implies(node, cEffect.Item1);
//                if (impliesNode.GetValue() > 0.5)
//                {
//                    result.AddRange(cEffect.Item2);
//                }
//            }
//            return result;
//        }

//        private void UpdateByLiteralList(List<Tuple<Ground<Predicate>, bool>> gndLiteralList)
//        {
//            foreach (var literal in gndLiteralList)
//            {
//                string gndPredName = literal.Item1.ToString();
//                _predBooleanMap[gndPredName] = literal.Item2;
//            }
//        }

//        public void ShowInfo()
//        {
//            Console.WriteLine("Knowledge base:");
//            foreach (var pair in _predBooleanMap)
//            {
//                Console.WriteLine("  Predicate: {0}, Value: {1}", pair.Key, pair.Value);
//            }
//        }

//        #endregion
//    }
//}
