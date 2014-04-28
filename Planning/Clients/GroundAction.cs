using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Clients
{
    public class GroundAction : Ground<Action>
    {
        #region Fields

        private List<GroundPredicate> _gndPredList;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; set; }

        public CUDDNode SuccessorStateAxiom { get; set; }

        public IReadOnlyList<GroundPredicate> GroundPredicateList
        {
            get { return _gndPredList; }
        }

        #endregion

        #region Constructors

        private GroundAction(Action action, IEnumerable<string> constantList) : base(action, constantList)
        {
            _gndPredList = new List<GroundPredicate>();
        }

        #endregion

        #region Methods

        public static GroundAction CreateInstance(Action action, IEnumerable<string> constantList, Dictionary<string, GroundPredicate> gndPredDict)
        {
            GroundAction result = new GroundAction(action, constantList);
            result.GenerateGroundPrecondition(gndPredDict);
            result.GenerateGroundSuccessorStateAxiom(gndPredDict);
            //result.GenerateGroundEffect(preGndPredDict);
            return result;
        }

        private void GenerateGroundPrecondition(Dictionary<string, GroundPredicate> gndPredDict)
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
                oldVars.AddVar(CUDD.Var(pair.Value.PreviousCuddIndex));
                List<string> collection = new List<string>();
                foreach (var parm in pair.Value.ParameterList)
                {
                    collection.Add(abstractParmMap[parm]);
                }

                GroundPredicate gndPred = new GroundPredicate(pair.Value.Predicate, collection);
                gndPred = gndPredDict[gndPred.ToString()];
                newVars.AddVar(CUDD.Var(gndPred.PreviousCuddIndex));

                //Console.WriteLine("  old cuddIndex:{0}, new cuddIndex:{1}", pair.Value.CuddIndex, gndPred.CuddIndex);
            }

            CUDDNode abstractPre = Container.Precondition;
            //Console.WriteLine("  Abstract precondition:");
            //CUDD.Print.PrintMinterm(abstractPre);

            Precondition = CUDD.Variable.SwapVariables(abstractPre, oldVars, newVars);
            //Console.WriteLine("  Ground precondition:");
            //CUDD.Print.PrintMinterm(gndAction.Precondition);

            //CUDDNode abstractEff = gndAction.VariableContainer.Effect;
            //gndAction.VariableContainer.Effect = CUDD.Variable.SwapVariables(abstractEff, oldVars, newVars);
        }

        private void GenerateGroundSuccessorStateAxiom(Dictionary<string, GroundPredicate> gndPredDict)
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

            AddOldNewVars(oldVars, newVars, Container.AbstractPredicateDict, gndPredDict, abstractParmMap);

            SuccessorStateAxiom = CUDD.Variable.SwapVariables(Container.SuccessorStateAxiom, oldVars, newVars);
            CUDD.Ref(SuccessorStateAxiom);
        }

        private void AddOldNewVars(CUDDVars oldVars, CUDDVars newVars,
            IReadOnlyDictionary<string, AbstractPredicate> abstractPredDict,
            Dictionary<string, GroundPredicate> gndPredDict, Dictionary<string, string> abstractParmMap)
        {
            foreach (var pair in abstractPredDict)
            {
                oldVars.AddVar(CUDD.Var(pair.Value.PreviousCuddIndex));
                oldVars.AddVar(CUDD.Var(pair.Value.SuccessorCuddIndex));
                List<string> collection = new List<string>();
                foreach (var parm in pair.Value.ParameterList)
                {
                    collection.Add(abstractParmMap[parm]);
                }

                string gndPredFullName = VariableContainer.GetFullName(pair.Value.Predicate.Name, collection);
                GroundPredicate gndPred = gndPredDict[gndPredFullName];
                _gndPredList.Add(gndPred);
                newVars.AddVar(CUDD.Var(gndPred.PreviousCuddIndex));
                newVars.AddVar(CUDD.Var(gndPred.SuccessorCuddIndex));
            }
        }

        #endregion
    }
}
