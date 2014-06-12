//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PAT.Common.Classes.CUDDLib;

//namespace Planning.Clients
//{
//    public class ClientGroundAction : GroundAction<ClientAction>
//    {
//        #region Fields

//        private List<GroundPredicate> _gndPredList;

//        #endregion

//        #region Properties

//        public CUDDNode SuccessorStateAxiom { get; set; }

//        public IReadOnlyList<GroundPredicate> GroundPredicateList
//        {
//            get { return _gndPredList; }
//        }

//        #endregion

//        #region Constructors

//        public ClientGroundAction()
//        {
//            _gndPredList = new List<GroundPredicate>();
//        }

//        #endregion

//        #region Methods

//        private void GenerateGroundSuccessorStateAxiom(Dictionary<string, GroundPredicate> gndPredDict)
//        {
//            CUDDVars oldVars = new CUDDVars();
//            CUDDVars newVars = new CUDDVars();

//            Dictionary<string, string> abstractParmMap = new Dictionary<string, string>();

//            //Console.WriteLine("  Ground action constant list count:{0}", gndAction.ConstantList.Count);

//            for (int i = 0; i < ConstantList.Count; i++)
//            {
//                string abstractParm = Container.VariableList[i].Item1;
//                string gndParm = ConstantList[i];
//                abstractParmMap.Add(abstractParm, gndParm);
//                //Console.WriteLine("    Parameter:{0}, constant:{1}", abstractParm, gndParm);
//            }

//            AddOldNewVars(oldVars, newVars, Container.AbstractPredicateDict, gndPredDict, abstractParmMap);

//            SuccessorStateAxiom = CUDD.Variable.SwapVariables(Container.SuccessorStateAxiom, oldVars, newVars);
//            CUDD.Ref(SuccessorStateAxiom);
//        }

//        private void AddOldNewVars(CUDDVars oldVars, CUDDVars newVars,
//            IReadOnlyDictionary<string, AbstractPredicate> abstractPredDict,
//            Dictionary<string, GroundPredicate> gndPredDict, Dictionary<string, string> abstractParmMap)
//        {
//            foreach (var pair in abstractPredDict)
//            {
//                oldVars.AddVar(CUDD.Var(pair.Value.CuddIndexList[0]));
//                oldVars.AddVar(CUDD.Var(pair.Value.CuddIndexList[1]));
//                List<string> collection = new List<string>();
//                foreach (var parm in pair.Value.ParameterList)
//                {
//                    collection.Add(abstractParmMap[parm]);
//                }

//                string gndPredFullName = VariableContainer.GetFullName(pair.Value.Predicate.Name, collection);
//                GroundPredicate gndPred = gndPredDict[gndPredFullName];
//                _gndPredList.Add(gndPred);
//                newVars.AddVar(CUDD.Var(gndPred.CuddIndexList[0]));
//                newVars.AddVar(CUDD.Var(gndPred.CuddIndexList[1]));
//            }
//        }

//        #endregion

//        public override void From(ClientAction action, IEnumerable<string> constantList, Dictionary<string, GroundPredicate> gndPredDict)
//        {
//            Container = action;
//            SetConstantList(constantList);
//            GenerateGroundPrecondition(gndPredDict);
//            GenerateGroundSuccessorStateAxiom(gndPredDict);
//        }
//    }
//}
