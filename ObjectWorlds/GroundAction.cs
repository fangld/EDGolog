using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace ObjectWorlds
{
    public class GroundAction : Ground<Action>
    {
        #region Fields

        private List<Tuple<CUDDNode, List<Tuple<Ground<Predicate>, bool>>>> _effect;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; set; }

        public IReadOnlyList<Tuple<CUDDNode, List<Tuple<Ground<Predicate>, bool>>>> Effect
        {
            get { return _effect; }
        }

        #endregion

        #region Constructors

        private GroundAction(Action action, IEnumerable<string> constantList) : base(action, constantList)
        {
            _effect = new List<Tuple<CUDDNode, List<Tuple<Ground<Predicate>, bool>>>>(Container.Effect.Count);
        }

        #endregion

        #region Methods

        public static GroundAction CreateInstance(Action action, IEnumerable<string> constantList, Dictionary<string, Ground<Predicate>> preGndPredDict)
        {
            GroundAction result = new GroundAction(action, constantList);
            result.GenerateGroundPrecondition(preGndPredDict);
            result.GenerateGroundEffect(preGndPredDict);
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

            foreach (var pair in Container.PreviousAbstractPredicateDict)
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

        private void GenerateGroundEffect(Dictionary<string, Ground<Predicate>> preGndPredDict)
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

            foreach (var pair in Container.PreviousAbstractPredicateDict)
            {
                oldVars.AddVar(CUDD.Var(pair.Value.CuddIndex));
                List<string> collection = new List<string>();
                foreach (var parm in pair.Value.ParameterList)
                {
                    collection.Add(abstractParmMap[parm]);
                }

                string gndPredFullName = VariableContainer.GetFullName(pair.Value.Predicate.Name, collection);
                Ground<Predicate> gndPred = preGndPredDict[gndPredFullName];
                newVars.AddVar(CUDD.Var(gndPred.CuddIndex));

                //Console.WriteLine("  old cuddIndex:{0}, new cuddIndex:{1}", pair.Value.CuddIndex, gndPred.CuddIndex);
            }

            foreach (var cEffect in Container.Effect)
            {
                CUDDNode abstractCondition = cEffect.Item1;
                CUDDNode gndCondition = CUDD.Variable.SwapVariables(abstractCondition, oldVars, newVars);

                var gndLiteralList = new List<Tuple<Ground<Predicate>, bool>>();
                var abstractLiteralList = cEffect.Item2;
                foreach (var abstractLiteral in abstractLiteralList)
                {
                    List<string> collection = new List<string>();
                    foreach (var parm in abstractLiteral.Item1.ParameterList)
                    {
                        collection.Add(abstractParmMap[parm]);
                    }

                    string gndPredFullName = VariableContainer.GetFullName(abstractLiteral.Item1.Predicate.Name,
                        collection);
                    Ground<Predicate> gndPred = preGndPredDict[gndPredFullName];
                    var gndLiteral = new Tuple<Ground<Predicate>, bool>(gndPred, abstractLiteral.Item2);
                    gndLiteralList.Add(gndLiteral);
                }

                var gndCEffect = new Tuple<CUDDNode, List<Tuple<Ground<Predicate>, bool>>>(gndCondition, gndLiteralList);
                _effect.Add(gndCEffect);
            }
        }

        #endregion
    }
}
