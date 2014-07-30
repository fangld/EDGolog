using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.Collections;
using Planning.ContextExtensions;

namespace Planning
{
    public class Event : ConstContainer
    {
        #region Fields

        private List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> _condEffect;

        private HashSet<Predicate> _affectedPredicateSet;

        private List<Observation> _observationList;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; private set; }

        public int CuddIndex { get; private set; }

        public IReadOnlyList<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> CondEffect
        {
            get { return _condEffect; }
        }

        public HashSet<Predicate> AffectedPredicateSet
        {
            get { return _affectedPredicateSet; }
        }

        public CUDDNode PartialSsa { get; set; }

        public IReadOnlyList<Observation> ObservationList
        {
            get { return _observationList; }
        }

        #endregion

        #region Constructors

        public Event(PlanningParser.EventDefineContext context, CUDDNode precondition,
            IReadOnlyDictionary<string, Predicate> predicateDict, string[] constArray, StringDictionary assignment,
            int initialCuddInex) : base(constArray)
        {
            CuddIndex = initialCuddInex;
            Name = context.eventSymbol().GetText();
            Precondition = precondition;
            _condEffect = context.emptyOrEffect().GetEffect(predicateDict, assignment);

            //if (FullName == "pickSuc(a2)")
            //{
            //    Console.WriteLine("Cond effect:");

            //    foreach (var tuple in _condEffect)
            //    {
            //        foreach (var literal in tuple.Item2)
            //        {
            //            Console.WriteLine(literal.Item1);
            //        }
            //    }
            //}

            GeneratePartialSuccessorStateAxiom();
            _observationList = new List<Observation>();
        }

        #endregion

        #region Methods

        public void AddObservation(Observation observation)
        {
            _observationList.Add(observation);
        }


        private void GeneratePartialSuccessorStateAxiom()
        {
            CUDDNode effectNode = GetEffectNode();
            CUDDNode partialFrameNode = GetPartialFrameNode();
            PartialSsa = CUDD.Function.And(effectNode, partialFrameNode);
        }

        private CUDDNode GetEffectNode()
        {
            _affectedPredicateSet = new HashSet<Predicate>();
            CUDDNode result = CUDD.ONE;
            CUDD.Ref(result);

            foreach (var cEffect in _condEffect)
            {
                var firstLiteral = cEffect.Item2[0];
                _affectedPredicateSet.Add(firstLiteral.Item1);
                CUDDNode predNode = CUDD.Var(firstLiteral.Item1.SuccessiveCuddIndex);
                CUDDNode literalsNode = firstLiteral.Item2 ? predNode : CUDD.Function.Not(predNode);
                
                for (int i = 1; i < cEffect.Item2.Length; i++)
                {
                    var literal = cEffect.Item2[i];
                    _affectedPredicateSet.Add(literal.Item1);
                    predNode = CUDD.Var(literal.Item1.SuccessiveCuddIndex);
                    CUDDNode literalNode = literal.Item2 ? predNode : CUDD.Function.Not(predNode);
                    literalsNode = CUDD.Function.And(literalsNode, literalNode);
                }

                CUDD.Ref(cEffect.Item1);
                CUDDNode cEffectNode = CUDD.Function.Implies(cEffect.Item1, literalsNode);

                result = CUDD.Function.And(result, cEffectNode);
            }

            //if (FullName == "pickSuc(a2)")
            //{
            //    Console.WriteLine("Get effect node");
            //    foreach (var predicate in _affectedPredicateSet)
            //    {
            //        Console.WriteLine(predicate);
            //    }
            //}

            return result;
        }

        private CUDDNode GetPartialFrameNode()
        {
            CUDDNode result = CUDD.ONE;
            CUDD.Ref(result);
            foreach (var predicate in _affectedPredicateSet)
            {
                CUDDNode frameCondition = CUDD.ONE;
                CUDD.Ref(frameCondition);

                foreach (var cEffect in _condEffect)
                {
                    if (cEffect.Item2.Any(literal => literal.Item1.Equals(predicate)))
                    {
                        CUDD.Ref(cEffect.Item1);
                        CUDDNode negCondition = CUDD.Function.Not(cEffect.Item1);
                        CUDDNode intermediateNode = CUDD.Function.And(frameCondition, negCondition);
                        frameCondition = intermediateNode;
                    }
                }

                CUDDNode prePredNode = CUDD.Var(predicate.PreviousCuddIndex);
                CUDDNode sucPredNode = CUDD.Var(predicate.SuccessiveCuddIndex);
                CUDDNode invariantNode = CUDD.Function.Equal(prePredNode, sucPredNode);

                CUDDNode frame = CUDD.Function.Implies(frameCondition, invariantNode);
                CUDDNode tmpNode = CUDD.Function.And(result, frame);
                result = tmpNode;
            }

            return result;
        }

        #endregion
    }
}
