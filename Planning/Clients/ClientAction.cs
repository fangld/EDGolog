//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LanguageRecognition;
//using PAT.Common.Classes.CUDDLib;

//namespace Planning.Clients
//{
//    public class ClientAction : Action
//    {
//        #region Properties

//        protected override int PredicateCuddIndexNumber
//        {
//            get { return 2; }
//        }

//        public CUDDNode SuccessorStateAxiom { get; set; }

//        #endregion

//        #region Methods for creating an instance

//        public override void From(int initialCuddIndex, PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Predicate> predDict)
//        {
//            CurrentCuddIndex = initialCuddIndex;
//            Name = context.actionSymbol().GetText();
//            GenerateVariableList(context.listVariable());
//            GenerateAbstractPredicates(context.actionDefBody(), predDict);
//            ToPrecondition(context, predDict);
//            GenerateEffect(context, predDict);
//            GenerateSuccessorStateAxiom();
//        }

//        #endregion

//        #region Methods for getting cudd nodes

//        protected override CUDDNode GetCuddNode(PlanningParser.GdContext context)
//        {
//            return GetCuddNode(context, true);
//        }

//        private CUDDNode GetCuddNode(PlanningParser.AtomicFormulaTermContext context, bool isPrevious)
//        {
//            AbstractPredicate abstractPredicate = GetAbstractPredicate(context);

//            int index = isPrevious ? abstractPredicate.CuddIndexList[0] : abstractPredicate.CuddIndexList[1];

//            CUDDNode result = CUDD.Var(index);
//            return result;
//        }

//        private CUDDNode GetCuddNode(PlanningParser.LiteralTermContext context, bool isPrevious)
//        {
//            CUDDNode subNode = GetCuddNode(context.atomicFormulaTerm(), isPrevious);
//            CUDDNode result;

//            if (context.NOT() != null)
//            {
//                result = CUDD.Function.Not(subNode);
//                CUDD.Ref(result);
//            }
//            else
//            {
//                result = subNode;
//            }

//            return result;
//        }

//        private CUDDNode GetCuddNode(PlanningParser.GdContext context, bool isPrevious)
//        {
//            CUDDNode result = null;

//            if (context.atomicFormulaTerm() != null)
//            {
//                result = GetCuddNode(context.atomicFormulaTerm(), isPrevious);
//            }
//            else if (context.literalTerm() != null)
//            {
//                result = GetCuddNode(context.literalTerm(), isPrevious);
//            }
//            else if (context.AND() != null)
//            {
//                result = GetCuddNode(context.gd()[0], isPrevious);
//                for (int i = 1; i < context.gd().Count; i++)
//                {
//                    CUDDNode gdNode = GetCuddNode(context.gd()[i], isPrevious);
//                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
//                    CUDD.Ref(andNode);
//                    CUDD.Deref(result);
//                    CUDD.Deref(gdNode);
//                    result = andNode;
//                }
//            }
//            else if (context.OR() != null)
//            {
//                result = GetCuddNode(context.gd()[0], isPrevious);
//                for (int i = 1; i < context.gd().Count; i++)
//                {
//                    CUDDNode gdNode = GetCuddNode(context.gd()[i], isPrevious);
//                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
//                    CUDD.Ref(orNode);
//                    CUDD.Deref(result);
//                    CUDD.Deref(gdNode);
//                    result = orNode;
//                }
//            }
//            else if (context.NOT() != null)
//            {
//                CUDDNode gdNode = GetCuddNode(context.gd()[0], isPrevious);
//                result = CUDD.Function.Not(gdNode);
//                CUDD.Ref(result);
//                CUDD.Deref(gdNode);
//            }
//            else if (context.IMPLY() != null)
//            {
//                CUDDNode gdNode0 = GetCuddNode(context.gd()[0], isPrevious);
//                CUDDNode gdNode1 = GetCuddNode(context.gd()[1], isPrevious);

//                result = CUDD.Function.Implies(gdNode0, gdNode1);
//                CUDD.Ref(result);
//                CUDD.Deref(gdNode0);
//                CUDD.Deref(gdNode1);
//            }

//            return result;
//        }

//        #endregion

//        #region Methods for generating successor state axiom
//        private void GenerateSuccessorStateAxiom()
//        {
//            CUDDNode effectNode = CUDD.ONE;
//            foreach (var cEffect in CondEffect)
//            {
//                CUDDNode intermediateNode = effectNode;
//                CUDDNode cEffectNode = GetEffectNode(cEffect);
//                Console.WriteLine("Action:{0}    cEffect:", Name);
//                CUDD.Print.PrintMinterm(cEffectNode);
//                effectNode = CUDD.Function.And(intermediateNode, cEffectNode);
//                CUDD.Ref(effectNode);
//                CUDD.Deref(intermediateNode);
//                CUDD.Deref(cEffectNode);
//            }

//            CUDDNode frame = GetFrameNode();

//            //Console.WriteLine(Name);
//            //Console.WriteLine("       CondEffect:");
//            //CUDD.Print.PrintMinterm(effectNode);

//            //Console.WriteLine("       Frame:");
//            //CUDD.Print.PrintMinterm(frame);

//            SuccessorStateAxiom = CUDD.Function.And(effectNode, frame);
//            //Console.WriteLine("       Successor state axiom:");
//            //CUDD.Print.PrintMinterm(SuccessorStateAxiom);

//            CUDD.Ref(SuccessorStateAxiom);
//            CUDD.Deref(effectNode);
//            CUDD.Deref(frame);
//        }

//        private CUDDNode GetFrameNode()
//        {
//            CUDDNode result = CUDD.ONE;
//            //Console.WriteLine("    Previous abstract predicate count:{0}", _preAbstractPredDict.Count);
//            foreach (var abstractPredPair in _abstractPredDict)
//            {
//                CUDDNode frameCondition = CUDD.ONE;
//                foreach (var cEffect in CondEffect)
//                {
//                    //Console.Write("    Literals:");
//                    //for (int i = 0; i < cEffect.Item2.Count; i++)
//                    //{
//                    //    Console.Write("{0}, ", cEffect.Item2[i].Item1);
//                    //}
//                    //Console.WriteLine();
//                    //Console.WriteLine("    Abstract predicate:{0}", abstractPredicate.Key);
//                    //Console.WriteLine(cEffect.Item2.Exists(literal => literal.Item1.Equals(abstractPredicate.Value)));

//                    if (cEffect.Item2.Exists(literal => literal.Item1.Equals(abstractPredPair.Value)))
//                    {
//                        CUDDNode intermediate = frameCondition;
//                        CUDDNode negCondition = CUDD.Function.Not(cEffect.Item1);
//                        CUDD.Ref(negCondition);
//                        frameCondition = CUDD.Function.And(intermediate, negCondition);
//                        CUDD.Ref(frameCondition);
//                        CUDD.Deref(intermediate);
//                        CUDD.Deref(negCondition);
//                    }
//                }

//                CUDDNode preAbstractPredNode = CUDD.Var(abstractPredPair.Value.CuddIndexList[0]);
//                CUDDNode sucAbstractPredNode = CUDD.Var(abstractPredPair.Value.CuddIndexList[1]);

//                CUDDNode invariant = CUDD.Function.Equal(preAbstractPredNode, sucAbstractPredNode);

//                //Console.WriteLine("       Frame condition:");
//                //CUDD.Print.PrintMinterm(frameCondition);
//                //Console.WriteLine("       Invariant:");
//                //CUDD.Print.PrintMinterm(invariant);
//                CUDD.Ref(invariant);
//                CUDDNode frame = CUDD.Function.Implies(frameCondition, invariant);
//                CUDD.Ref(frame);
//                CUDD.Deref(frameCondition);
//                CUDD.Deref(invariant);

//                CUDDNode conjunct = result;
//                result = CUDD.Function.And(conjunct, frame);
//                CUDD.Ref(result);
//                CUDD.Deref(conjunct);
//            }

//            return result;
//        }

//        private CUDDNode GetEffectNode(Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>> cEffect)
//        {
//            CUDDNode effect = CUDD.ONE;

//            foreach (var literal in cEffect.Item2)
//            {
//                CUDDNode intermediate = effect;
//                CUDDNode abstractPred = CUDD.Var(literal.Item1.CuddIndexList[1]);
//                CUDDNode literalNode = literal.Item2 ? abstractPred : CUDD.Function.Not(abstractPred);
//                effect = CUDD.Function.And(intermediate, literalNode);
//                CUDD.Ref(effect);
//                CUDD.Deref(intermediate);
//            }

//            //Console.WriteLine("    Condition:");
//            //CUDD.Print.PrintMinterm(cEffect.Item1);
//            //Console.WriteLine("    CondEffect:");
//            //CUDD.Print.PrintMinterm(effect);

//            CUDDNode result = CUDD.Function.Implies(cEffect.Item1, effect);
//            CUDD.Ref(result);
//            CUDD.Deref(effect);

//            return result;
//        }

//        #endregion
//    }
//}
