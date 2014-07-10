﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.ContextExtensions;

namespace Planning
{
    public class Event : ConstContainer
    {
        #region Fields

        private List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>> _condEffect;

        private HashSet<Predicate> _affectedPredicateSet;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; set; }

        public int CuddIndex { get; set; }

        public IReadOnlyList<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>> CondEffect
        {
            get { return _condEffect; }
        }

        public HashSet<Predicate> AffectedPredicateSet
        {
            get { return _affectedPredicateSet; }
        }

        public CUDDNode ParitalSuccessorStateAxiom { get; set; }

        #endregion

        #region Constructors

        public Event(PlanningParser.EventDefineContext context, IReadOnlyDictionary<string, Predicate> predicateDict,string[] constArray, Dictionary<string, string> assignment, int initialCuddInex) : base(constArray)
        {
            CuddIndex = initialCuddInex;
            Name = context.eventSymbol().GetText();
            Console.WriteLine(FullName);
            Precondition = context.emptyOrPreGD().ToPrecondition(predicateDict, assignment);
            Console.WriteLine("Finishing event define precondition");
            Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(Precondition));
            if (context.emptyOrPreGD() != null)
            {
                Console.WriteLine("  Context: {0}", context.emptyOrPreGD().GetText());
            }
            //string filename = string.Format("{0}.dot", FullName);
            //CUDD.Print.PrintBDDTree(Precondition, filename);
            //CUDD.Print.PrintMinterm(Precondition);
            //Console.ReadLine();

            GenerateEffect(context.emptyOrEffect(), predicateDict, assignment);
            Console.WriteLine("Finishing event effect");

            GeneratePartialSuccessorStateAxiom(predicateDict);
            Console.WriteLine("Finishing event parital successor state axiom");
            Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(ParitalSuccessorStateAxiom));
        }

        #endregion

        #region Methods

        #region Methods for generating precondition

        private void GeneratePrecondition(PlanningParser.EmptyOrPreGDContext context, IReadOnlyDictionary<string, Predicate> predicateDict, Dictionary<string, string> assignment)
        {
            Precondition = CUDD.ONE;
            CUDD.Ref(Precondition);

            if (context != null)
            {
                PlanningParser.GdContext gdContext = context.gd();
                if (gdContext != null)
                {
                    CUDD.Deref(Precondition);
                    Precondition = gdContext.GetCuddNode(predicateDict, assignment);
                }
            }
        }

        #endregion

        #region Methods for generating effect

        private void GenerateEffect(PlanningParser.EmptyOrEffectContext context,
            IReadOnlyDictionary<string, Predicate> predicateDict, Dictionary<string, string> assignment)
        {
            _condEffect = new List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>>();
            if (context != null)
            {
                PlanningParser.EffectContext effectContext = context.effect();
                if (effectContext != null)
                {
                    foreach (var cEffectContext in effectContext.cEffect())
                    {
                        CUDDNode initialCuddNode = CUDD.ONE;
                        CUDD.Ref(initialCuddNode);
                        var condEffect = GetCondEffectList(cEffectContext, initialCuddNode, predicateDict, assignment);
                        _condEffect.AddRange(condEffect);
                    }
                }
            }
        }

        private List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>> GetCondEffectList(
            PlanningParser.EffectContext context, CUDDNode currentConditionNode,
            IReadOnlyDictionary<string, Predicate> predicateDict, Dictionary<string, string> assignment)
        {
            var result = new List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>>();

            foreach (var cEffectContext in context.cEffect())
            {
                var condEffect = GetCondEffectList(cEffectContext, currentConditionNode, predicateDict, assignment);
                result.AddRange(condEffect);
            }

            return result;
        }

        private List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>> GetCondEffectList(
            PlanningParser.CEffectContext context, CUDDNode currentConditionNode,
            IReadOnlyDictionary<string, Predicate> predicateDict, Dictionary<string, string> assignment)
        {
            List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>> result;
            if (context.FORALL() != null)
            {
                var listVariableContext = context.listVariable();
                var varNameList = listVariableContext.GetVariableNameList();
                var collection = listVariableContext.GetCollection();
                result = ScanMixedRadix(varNameList, collection, assignment, currentConditionNode, context.effect(),
                    predicateDict);
            }
            else if (context.WHEN() != null)
            {
                result = new List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>>();
                CUDD.Ref(currentConditionNode);
                CUDDNode gdNode = context.gd().GetCuddNode(predicateDict, assignment);
                CUDDNode conditionNode = CUDD.Function.And(currentConditionNode, gdNode);
                if (!conditionNode.Equals(CUDD.ZERO))
                {
                    var literalList = GetLiteralList(context.condEffect(), predicateDict, assignment);
                    var condEffect = new Tuple<CUDDNode, List<Tuple<Predicate, bool>>>(conditionNode, literalList);
                    result.Add(condEffect);
                }
                else
                {
                    CUDD.Deref(conditionNode);
                }
            }
            else
            {
                result = new List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>>();
                var literalList = GetLiteralList(context.condEffect(), predicateDict, assignment);
                var condEffect = new Tuple<CUDDNode, List<Tuple<Predicate, bool>>>(currentConditionNode, literalList);
                result.Add(condEffect);
            }
            return result;
        }

        private List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>> ScanMixedRadix(
            IReadOnlyList<string> variableNameList, IReadOnlyList<List<string>> collection,
            Dictionary<string, string> assignment, CUDDNode currentConditionNode, PlanningParser.EffectContext context,
            IReadOnlyDictionary<string, Predicate> predicateDict)
        {
            var result = new List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>>();
            int count = collection.Count;
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i => maxIndex[i] = collection[i].Count);

            do
            {
                for (int i = 0; i < count; i++)
                {
                    string value = collection[i][index[i]];
                    string variableName = variableNameList[i];
                    if (assignment.ContainsKey(variableName))
                    {
                        assignment[variableName] = value;
                    }
                    else
                    {
                        assignment.Add(variableName, value);
                    }
                }

                var condEffectList = GetCondEffectList(context, currentConditionNode, predicateDict, assignment);
                result.AddRange(condEffectList);

                int j = count - 1;
                while (j != -1)
                {
                    if (index[j] == maxIndex[j] - 1)
                    {
                        index[j] = 0;
                        j--;
                        continue;
                    }
                    break;
                }
                if (j == -1)
                    return result;
                index[j]++;
            } while (true);
        }

        private List<Tuple<Predicate, bool>> GetLiteralList(PlanningParser.CondEffectContext context,
            IReadOnlyDictionary<string, Predicate> predicateDict, Dictionary<string, string> assignment)
        {
            List<Tuple<Predicate, bool>> result = new List<Tuple<Predicate, bool>>(context.termLiteral().Count);
            //return context.termLiteral().Select(termLiteralContext => GetLiteral(termLiteralContext, predicateDict, assignment));
            foreach (var termLiteralContext in context.termLiteral())
            {
                var literal = GetLiteral(termLiteralContext, predicateDict, assignment);
                result.Add(literal);
            }
            return result;
        }

        private Tuple<Predicate, bool> GetLiteral(PlanningParser.TermLiteralContext context,
            IReadOnlyDictionary<string, Predicate> predicateDict, Dictionary<string, string> assignment)
        {
            string predicateFullName = GetFullName(context.termAtomForm(), assignment);
            Predicate predicate = predicateDict[predicateFullName];
            bool isPositive = context.NOT() == null;
            return new Tuple<Predicate, bool>(predicate, isPositive);
        }

        #endregion

        #region Methods for generating partial successor state axiom

        private void GeneratePartialSuccessorStateAxiom(IReadOnlyDictionary<string, Predicate> predDict)
        {
            CUDDNode effectNode = GetEffectNode();
            CUDDNode partialFrameNode = GetPartialFrameNode();

            ParitalSuccessorStateAxiom = CUDD.Function.And(effectNode, partialFrameNode);
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
                
                for (int i = 1; i < cEffect.Item2.Count; i++)
                {
                    var literal = cEffect.Item2[i];
                    CUDDNode intermediateNode = literalsNode;
                    predicate = CUDD.Var(literal.Item1.SuccessiveCuddIndex);
                    CUDDNode literalNode = literal.Item2 ? predicate : CUDD.Function.Not(predicate);
                    literalsNode = CUDD.Function.And(intermediateNode, literalNode);
                }

                CUDD.Ref(cEffect.Item1);
                CUDDNode cEffectNode = CUDD.Function.Implies(cEffect.Item1, literalsNode);

                CUDDNode tmpNode = result;
                result = CUDD.Function.And(tmpNode, cEffectNode);
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
                    if (cEffect.Item2.Exists(literal => literal.Item1.Equals(predicate)))
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

        #endregion
    }
}
