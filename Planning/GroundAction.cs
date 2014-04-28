using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public abstract class GroundAction<TA, TAP> : Ground<TA> where TA: Action<TAP>, new() where TAP : AbstractPredicate, new()
    {
        #region Properties

        public CUDDNode Precondition { get; set; }

        #endregion

        #region Constructors


        #endregion

        #region Methods

        public abstract void From(TA action, IEnumerable<string> constantList,
            Dictionary<string, Ground<Predicate>> gndPredDict);

        protected abstract int GetPreconditionCuddIndex(TAP abstractPred);

        protected void GenerateGroundPrecondition(Dictionary<string, Ground<Predicate>> gndPredDict)
        {
            CUDDVars oldVars = new CUDDVars();
            CUDDVars newVars = new CUDDVars();

            Dictionary<string, string> abstractParmMap = new Dictionary<string, string>();

            //Console.WriteLine("  Ground action constant list count:{0}", gndAction.ConstantList.Count);

            for (int i = 0; i < ConstantList.Count; i++)
            {
                string abstractParm = Container.VariableList[i].Item1;
                string gndParm = ConstantList[i];
                abstractParmMap.Add(abstractParm, gndParm);
                //Console.WriteLine("    Parameter:{0}, constant:{1}", abstractParm, gndParm);
            }

            foreach (var pair in Container.AbstractPredicateDict)
            {
                int index = GetPreconditionCuddIndex(pair.Value);
                oldVars.AddVar(CUDD.Var(index));
                List<string> collection = new List<string>();
                foreach (var parm in pair.Value.ParameterList)
                {
                    collection.Add(abstractParmMap[parm]);
                }

                Ground<Predicate> gndPred = new Ground<Predicate>(pair.Value.Predicate, collection);
                gndPred = gndPredDict[gndPred.ToString()];
                newVars.AddVar(CUDD.Var(gndPred.CuddIndex));
            }

            CUDDNode abstractPre = Container.Precondition;

            Precondition = CUDD.Variable.SwapVariables(abstractPre, oldVars, newVars);
            //Console.WriteLine("  Ground precondition:");
            //CUDD.Print.PrintMinterm(Precondition);

            //CUDDNode abstractEff = gndAction.VariableContainer.Effect;
            //gndAction.VariableContainer.Effect = CUDD.Variable.SwapVariables(abstractEff, oldVars, newVars);
        }

        #endregion
    }
}
