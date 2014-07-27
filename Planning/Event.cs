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
            GeneratePartialSuccessorStateAxiom();
            _observationList = new List<Observation>();
        }

        #endregion

        #region Methods

        public void AddObservation(Observation observation)
        {
            _observationList.Add(observation);
        }

        //private void GenerateEffect(PlanningParser.EmptyOrEffectContext context,
        //    IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
        //{
        //    _condEffect = new List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>>();
        //    if (context != null)
        //    {
        //        PlanningParser.EffectContext effectContext = context.effect();
        //        if (effectContext != null)
        //        {
        //            foreach (var cEffectContext in effectContext.cEffect())
        //            {
        //                CUDDNode initialCuddNode = CUDD.ONE;
        //                CUDD.Ref(initialCuddNode);
        //                var condEffect = GetCondEffectList(cEffectContext, initialCuddNode, predicateDict, assignment);
        //                _condEffect.AddRange(condEffect);
        //            }
        //        }
        //    }
        //}

        //public static List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> GetCondEffectList(PlanningParser.EffectContext context,
        //    CUDDNode currentConditionNode, IReadOnlyDictionary<string, Predicate> predicateDict,
        //    StringDictionary assignment)
        //{
        //    var result = new List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>>();

        //    foreach (var cEffectContext in context.cEffect())
        //    {
        //        var condEffect = GetCondEffectList(cEffectContext, currentConditionNode, predicateDict, assignment);
        //        result.AddRange(condEffect);
        //    }

        //    return result;
        //}

        //private static List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> GetCondEffectList(
        //    PlanningParser.CEffectContext context, CUDDNode currentConditionNode,
        //    IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
        //{
        //    List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> result;
        //    if (context.FORALL() != null)
        //    {
        //        CEffectEnumerator enumerator = new CEffectEnumerator(context, currentConditionNode, predicateDict,
        //            assignment);
        //        Algorithms.IterativeScanMixedRadix(enumerator);
        //        result = enumerator.CondEffectList;
        //    }
        //    else if (context.WHEN() != null)
        //    {
        //        result = new List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>>();
        //        CUDD.Ref(currentConditionNode);
        //        CUDDNode gdNode = context.gd().GetCuddNode(predicateDict, assignment);
        //        CUDDNode conditionNode = CUDD.Function.And(currentConditionNode, gdNode);
        //        if (!conditionNode.Equals(CUDD.ZERO))
        //        {
        //            var literaCollection = GetLiteralArray(context.condEffect(), predicateDict, assignment);
        //            var condEffect = new Tuple<CUDDNode, Tuple<Predicate, bool>[]>(conditionNode, literaCollection);
        //            result.Add(condEffect);
        //        }
        //        else
        //        {
        //            CUDD.Deref(conditionNode);
        //        }
        //    }
        //    else
        //    {
        //        result = new List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>>();
        //        var literals = GetLiteralArray(context.condEffect(), predicateDict, assignment);
        //        var condEffect = new Tuple<CUDDNode, Tuple<Predicate, bool>[]>(currentConditionNode, literals);
        //        result.Add(condEffect);
        //    }
        //    return result;
        //}

        //private static Tuple<Predicate, bool>[] GetLiteralArray(PlanningParser.CondEffectContext context,
        //    IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
        //{
        //    int count = context.termLiteral().Count;
        //    Tuple<Predicate, bool>[] result = new Tuple<Predicate, bool>[count];
        //    Parallel.For(0, count, i => result[i] = GetLiteral(context.termLiteral()[i], predicateDict, assignment));
        //    return result;
        //}

        //private static Tuple<Predicate, bool> GetLiteral(PlanningParser.TermLiteralContext context,
        //    IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
        //{
        //    string fullName = GetFullName(context.termAtomForm(), assignment);
        //    Predicate predicate = predicateDict[fullName];
        //    bool isPositive = context.NOT() == null;
        //    return new Tuple<Predicate, bool>(predicate, isPositive);
        //}


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

                CUDDNode predicate = CUDD.Var(firstLiteral.Item1.SuccessiveCuddIndex);
                CUDDNode literalsNode = firstLiteral.Item2 ? predicate : CUDD.Function.Not(predicate);
                
                for (int i = 1; i < cEffect.Item2.Length; i++)
                {
                    var literal = cEffect.Item2[i];
                    predicate = CUDD.Var(literal.Item1.SuccessiveCuddIndex);
                    CUDDNode literalNode = literal.Item2 ? predicate : CUDD.Function.Not(predicate);
                    literalsNode = CUDD.Function.And(literalsNode, literalNode);
                }

                CUDD.Ref(cEffect.Item1);
                CUDDNode cEffectNode = CUDD.Function.Implies(cEffect.Item1, literalsNode);

                result = CUDD.Function.And(result, cEffectNode);
            }

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
