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

        protected GroundAction(TA action, IEnumerable<string> constantList)
            : base(action, constantList)
        {
        }

        #endregion

        #region Methods

        public GroundAction<TA, TAP> From(TA action, IEnumerable<string> constantList,
            Dictionary<string, Ground<Predicate>> gndPredDict)
        {
            result.GenerateGroundPrecondition(gndPredDict);
            result.GenerateGroundEffect(gndPredDict);
            return result;
        }

        private void GenerateGroundPrecondition(Dictionary<string, Ground<Predicate>> preGndPredDict)
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
                oldVars.AddVar(CUDD.Var(pair.Value.CuddIndex));
                List<string> collection = new List<string>();
                foreach (var parm in pair.Value.ParameterList)
                {
                    collection.Add(abstractParmMap[parm]);
                }

                Ground<Predicate> gndPred = new Ground<Predicate>(pair.Value.Predicate, collection);
                gndPred = preGndPredDict[gndPred.ToString()];
                newVars.AddVar(CUDD.Var(gndPred.CuddIndex));
            }

            CUDDNode abstractPre = Container.Precondition;

            Precondition = CUDD.Variable.SwapVariables(abstractPre, oldVars, newVars);
            //Console.WriteLine("  Ground precondition:");
            //CUDD.Print.PrintMinterm(gndAction.Precondition);

            //CUDDNode abstractEff = gndAction.VariableContainer.Effect;
            //gndAction.VariableContainer.Effect = CUDD.Variable.SwapVariables(abstractEff, oldVars, newVars);
        }

        #endregion
    }
}
