//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PAT.Common.Classes.CUDDLib;

//namespace Planning
//{
//    public abstract class GroundAction<TA> : Ground<TA> 
//        where TA: Action, new() 
//    {
//        #region Properties

//        public CUDDNode Precondition { get; set; }

//        #endregion

//        #region Constructors


//        #endregion

//        #region Methods

//        public abstract void From(TA action, IEnumerable<string> constantList,
//            Dictionary<string, GroundPredicate> gndPredDict);

//        //protected abstract int GetPreconditionCuddIndex(AbstractPredicate abstractPred);

//        //protected abstract int GetPreconditionCuddIndex(GroundPredicate gndPred);

//        //protected abstract void GenerateGroundPrecondition(Dictionary<string, TGP> gndPredDict);

//        protected void GenerateGroundPrecondition(Dictionary<string, GroundPredicate> gndPredDict)
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
//                int abstractIndex = pair.Value.CuddIndexList[0];
//                oldVars.AddVar(CUDD.Var(abstractIndex));
//                List<string> collection = new List<string>();
//                foreach (var parm in pair.Value.ParameterList)
//                {
//                    collection.Add(abstractParmMap[parm]);
//                }

//                string gndPredName = VariableContainer.GetFullName(pair.Value.Predicate.Name, collection);

//                GroundPredicate gndPred = gndPredDict[gndPredName];
//                int gndindex = gndPred.CuddIndexList[0];
//                newVars.AddVar(CUDD.Var(gndindex));
//            }

//            CUDDNode abstractPre = Container.Precondition;

//            Precondition = CUDD.Variable.SwapVariables(abstractPre, oldVars, newVars);
//        }

//        #endregion
//    }
//}
