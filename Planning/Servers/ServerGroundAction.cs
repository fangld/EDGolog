//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PAT.Common.Classes.CUDDLib;

//namespace Planning.Servers
//{
//    public class ServerGroundAction : GroundAction<ServerAction>
//    {
//        #region Fields

//        private List<Tuple<CUDDNode, List<Tuple<Ground<Predicate>, bool>>>> _effect;

//        #endregion

//        #region Properties

//        public IReadOnlyList<Tuple<CUDDNode, List<Tuple<Ground<Predicate>, bool>>>> CondEffect
//        {
//            get { return _effect; }
//        }

//        #endregion

//        #region Constructors

//        public ServerGroundAction()
//        {
//            _effect = new List<Tuple<CUDDNode, List<Tuple<Ground<Predicate>, bool>>>>();
//        }

//        #endregion

//        #region Methods

//        public override void From(ServerAction action, IEnumerable<string> constantList, Dictionary<string, GroundPredicate> gndPredDict)
//        {
//            Container = action;
//            SetConstantList(constantList);
//            GenerateGroundPrecondition(gndPredDict);
//            GenerateGroundEffect(gndPredDict);
//        }

//        private void GenerateGroundEffect(Dictionary<string, GroundPredicate> preGndPredDict)
//        {
//            CUDDVars oldVars = new CUDDVars();
//            CUDDVars newVars = new CUDDVars();

//            Dictionary<string, string> abstractParmMap = new Dictionary<string, string>();

//            for (int i = 0; i < ConstantList.Count; i++)
//            {
//                string abstractParm = Container.VariableList[i].Item1;
//                string gndParm = ConstantList[i];
//                abstractParmMap.Add(abstractParm, gndParm);
//            }

//            foreach (var pair in Container.AbstractPredicateDict)
//            {
//                oldVars.AddVar(CUDD.Var(pair.Value.CuddIndexList[0]));
//                List<string> collection = new List<string>();
//                foreach (var parm in pair.Value.ParameterList)
//                {
//                    collection.Add(abstractParmMap[parm]);
//                }

//                string gndPredFullName = VariableContainer.GetFullName(pair.Value.Predicate.Name, collection);
//                GroundPredicate gndPred = preGndPredDict[gndPredFullName];
//                newVars.AddVar(CUDD.Var(gndPred.CuddIndexList[0]));
//            }

//            foreach (var cEffect in Container.CondEffect)
//            {
//                CUDDNode abstractCondition = cEffect.Item1;
//                CUDDNode gndCondition = CUDD.Variable.SwapVariables(abstractCondition, oldVars, newVars);
//                CUDD.Ref(gndCondition);

//                var gndLiteralList = new List<Tuple<Ground<Predicate>, bool>>();
//                var abstractLiteralList = cEffect.Item2;
//                foreach (var abstractLiteral in abstractLiteralList)
//                {
//                    List<string> collection = new List<string>();
//                    foreach (var parm in abstractLiteral.Item1.ParameterList)
//                    {
//                        collection.Add(abstractParmMap[parm]);
//                    }

//                    string gndPredFullName = VariableContainer.GetFullName(abstractLiteral.Item1.Predicate.Name,
//                        collection);
//                    Ground<Predicate> gndPred = preGndPredDict[gndPredFullName];
//                    var gndLiteral = new Tuple<Ground<Predicate>, bool>(gndPred, abstractLiteral.Item2);
//                    gndLiteralList.Add(gndLiteral);
//                }

//                var gndCEffect = new Tuple<CUDDNode, List<Tuple<Ground<Predicate>, bool>>>(gndCondition, gndLiteralList);
//                _effect.Add(gndCEffect);
//            }
//        }

//        #endregion
//    }
//}
