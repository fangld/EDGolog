using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Clients
{
    public class Action : VariableContainer
    {
        #region Fields

        private Dictionary<string, AbstractPredicate> _abstractPredDict;

        private List<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>> _effect;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; set; }

        public CUDDNode SuccessorStateAxiom { get; set; }

        public IReadOnlyList<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>> Effect
        {
            get { return _effect; }
        }

        public int CurrentCuddIndex { get; set; }

        public IReadOnlyDictionary<string, AbstractPredicate> AbstractPredicateDict
        {
            get { return _abstractPredDict; }
        }

        #endregion

        #region Constructors

        private Action(int initialCuddIndex)
        {
            CurrentCuddIndex = initialCuddIndex;
            _abstractPredDict = new Dictionary<string, AbstractPredicate>();
            _effect = new List<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>>();
        }

        #endregion

        #region Methods for creating an instance

        public static Action FromContext(int initialCuddIndex, PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            Action result = new Action(initialCuddIndex);
            result.Name = context.actionSymbol().GetText();
            result.GenerateVariableList(context.listVariable());
            result.GenerateAbstractPredicates(context.actionDefBody(), predDict);
            result.GeneratePrecondition(context);
            result.GenerateEffect(context);
            result.GenerateSuccessorStateAxiom();
            return result;
        }

        #endregion

        #region Methods for generating abstract predicates

        private void GenerateAbstractPredicates(PlanningParser.ActionDefBodyContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            if (context.PRE() != null)
            {
                if (context.emptyOrPreGD() != null)
                {
                    GenerateAbstractPredicates(context.emptyOrPreGD().gd(), predDict);
                }
            }

            if (context.EFF() != null)
            {
                if (context.emptyOrEffect() != null)
                {
                    foreach (var cEffectContext in context.emptyOrEffect().effect().cEffect())
                    {
                        GenerateAbstractPredicates(cEffectContext, predDict);
                    }
                }
            }
        }

        private void GenerateAbstractPredicates(PlanningParser.AtomicFormulaTermContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            var abstractPredicate = CreateAbstractPredicate(context, predDict);
            if (!_abstractPredDict.ContainsKey(abstractPredicate.ToString()))
            {
                abstractPredicate.PreviousCuddIndex = CurrentCuddIndex;
                CurrentCuddIndex++;
                abstractPredicate.SuccessorCuddIndex = CurrentCuddIndex;
                CurrentCuddIndex++;
                _abstractPredDict.Add(abstractPredicate.ToString(), abstractPredicate);
            }
        }

        private void GenerateAbstractPredicates(PlanningParser.LiteralTermContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            GenerateAbstractPredicates(context.atomicFormulaTerm(), predDict);
        }

        private void GenerateAbstractPredicates(PlanningParser.GdContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            if (context.atomicFormulaTerm() != null)
            {
                GenerateAbstractPredicates(context.atomicFormulaTerm(), predDict);
            }
            else if (context.literalTerm() != null)
            {
                GenerateAbstractPredicates(context.literalTerm(), predDict);
            }
            else
            {
                for (int i = 0; i < context.gd().Count; i++)
                {
                    GenerateAbstractPredicates(context.gd()[i], predDict);
                }
            }
        }

        private void GenerateAbstractPredicates(PlanningParser.CEffectContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            if (context.WHEN() != null)
            {
                GenerateAbstractPredicates(context.gd(), predDict);
                foreach (var literalTermContext in context.condEffect().literalTerm())
                {
                    GenerateAbstractPredicates(literalTermContext, predDict);
                }
            }
            else
            {
                GenerateAbstractPredicates(context.literalTerm(), predDict);
            }
        }

        private AbstractPredicate CreateAbstractPredicate(PlanningParser.AtomicFormulaTermContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            List<string> parameterList = new List<string>();
            for (int i = 0; i < context.term().Count; i++)
            {
                parameterList.Add(context.term()[i].GetText());
            }

            AbstractPredicate abstractPredicate = new AbstractPredicate(parameterList);
            abstractPredicate.Predicate = predDict[context.predicate().GetText()];
            return abstractPredicate;
        }

        #endregion

        #region Methods for generating precondition

        private AbstractPredicate GetAbstractPredicate(PlanningParser.AtomicFormulaTermContext context)
        {
            string abstractPredName = GetFullName(context);
            AbstractPredicate result = _abstractPredDict[abstractPredName];
            return result;
        }

        private void GeneratePrecondition(PlanningParser.ActionDefineContext context)
        {
            Precondition = CUDD.ONE;

            if (context.actionDefBody().emptyOrPreGD() != null)
            {
                if (context.actionDefBody().emptyOrPreGD().gd() != null)
                {
                    Precondition = GetCuddNode(context.actionDefBody().emptyOrPreGD().gd(), true);
                }
            }
        }

        private CUDDNode GetCuddNode(PlanningParser.AtomicFormulaTermContext context, bool isPrevious)
        {
            AbstractPredicate abstractPredicate = GetAbstractPredicate(context);

            int index = isPrevious ? abstractPredicate.PreviousCuddIndex : abstractPredicate.SuccessorCuddIndex;

            CUDDNode result = CUDD.Var(index);
            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.LiteralTermContext context, bool isPrevious)
        {
            CUDDNode subNode = GetCuddNode(context.atomicFormulaTerm(), isPrevious);
            CUDDNode result;

            if (context.NOT() != null)
            {
                result = CUDD.Function.Not(subNode);
                CUDD.Ref(result);
            }
            else
            {
                result = subNode;
            }

            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.GdContext context, bool isPrevious)
        {
            CUDDNode result = null;

            if (context.atomicFormulaTerm() != null)
            {
                result = GetCuddNode(context.atomicFormulaTerm(), isPrevious);
            }
            else if (context.literalTerm() != null)
            {
                result = GetCuddNode(context.literalTerm(), isPrevious);
            }
            else if (context.AND() != null)
            {
                result = GetCuddNode(context.gd()[0], isPrevious);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gd()[i], isPrevious);
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Ref(andNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = GetCuddNode(context.gd()[0], isPrevious);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gd()[i], isPrevious);
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    CUDD.Ref(orNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = GetCuddNode(context.gd()[0], isPrevious);
                result = CUDD.Function.Not(gdNode);
                CUDD.Ref(result);
                CUDD.Deref(gdNode);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = GetCuddNode(context.gd()[0], isPrevious);
                CUDDNode gdNode1 = GetCuddNode(context.gd()[1], isPrevious);

                result = CUDD.Function.Implies(gdNode0, gdNode1);
                CUDD.Ref(result);
                CUDD.Deref(gdNode0);
                CUDD.Deref(gdNode1);
            }

            return result;
        }

        #endregion

        #region Methods for generating effect

        private void GenerateEffect(PlanningParser.ActionDefineContext context)
        {
            PlanningParser.EmptyOrEffectContext emptyOrEffectContext = context.actionDefBody().emptyOrEffect();
            if (emptyOrEffectContext != null)
            {
                PlanningParser.EffectContext effectContext = emptyOrEffectContext.effect();
                if (effectContext != null)
                {
                    foreach (var cEffectContext in effectContext.cEffect())
                    {
                        var condEffect = GetCondEffect(cEffectContext);
                        _effect.Add(condEffect);
                    }
                }
            }
        }

        private Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>> GetCondEffect(PlanningParser.CEffectContext context)
        {
            CUDDNode condition;
            var abstractLiterals = new List<Tuple<AbstractPredicate, bool>>();
            if (context.literalTerm() != null)
            {
                condition = CUDD.ONE;
                var literal = GetAbstractLiteral(context.literalTerm());
                abstractLiterals.Add(literal);
            }
            else
            {
                condition = GetCuddNode(context.gd(), true);
                foreach (var literalTermNode in context.condEffect().literalTerm())
                {
                    var literal = GetAbstractLiteral(literalTermNode);
                    abstractLiterals.Add(literal);
                }
            }

            var result = new Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>(condition, abstractLiterals);
            return result;
        }

        private Tuple<AbstractPredicate, bool> GetAbstractLiteral(PlanningParser.LiteralTermContext context)
        {
            AbstractPredicate abstractPredicate = GetAbstractPredicate(context.atomicFormulaTerm());
            bool isPositive = context.NOT() == null;
            return new Tuple<AbstractPredicate, bool>(abstractPredicate, isPositive);
        }

        #endregion

        #region Methods for generating successor state axiom

        private void GenerateSuccessorStateAxiom()
        {
            CUDDNode effectNode = CUDD.ONE;
            foreach (var cEffect in _effect)
            {
                CUDDNode intermediateNode = effectNode;
                CUDDNode cEffectNode = GetEffectNode(cEffect);
                Console.WriteLine("Action:{0}    cEffect:", Name);
                CUDD.Print.PrintMinterm(cEffectNode);
                effectNode = CUDD.Function.And(intermediateNode, cEffectNode);
                CUDD.Ref(effectNode);
                CUDD.Deref(intermediateNode);
                CUDD.Deref(cEffectNode);
            }

            CUDDNode frame = GetFrameNode();

            //Console.WriteLine(Name);
            //Console.WriteLine("       Effect:");
            //CUDD.Print.PrintMinterm(effectNode);

            //Console.WriteLine("       Frame:");
            //CUDD.Print.PrintMinterm(frame);

            SuccessorStateAxiom = CUDD.Function.And(effectNode, frame);
            //Console.WriteLine("       Successor state axiom:");
            //CUDD.Print.PrintMinterm(SuccessorStateAxiom);

            CUDD.Ref(SuccessorStateAxiom);
            CUDD.Deref(effectNode);
            CUDD.Deref(frame);
        }

        private CUDDNode GetFrameNode()
        {
            CUDDNode result = CUDD.ONE;
            //Console.WriteLine("    Previous abstract predicate count:{0}", _preAbstractPredDict.Count);
            foreach (var abstractPredPair in _abstractPredDict)
            {
                CUDDNode frameCondition = CUDD.ONE;
                foreach (var cEffect in _effect)
                {
                    //Console.Write("    Literals:");
                    //for (int i = 0; i < cEffect.Item2.Count; i++)
                    //{
                    //    Console.Write("{0}, ", cEffect.Item2[i].Item1);
                    //}
                    //Console.WriteLine();
                    //Console.WriteLine("    Abstract predicate:{0}", abstractPredicate.Key);
                    //Console.WriteLine(cEffect.Item2.Exists(literal => literal.Item1.Equals(abstractPredicate.Value)));

                    if (cEffect.Item2.Exists(literal => literal.Item1.Equals(abstractPredPair.Value)))
                    {
                        CUDDNode intermediate = frameCondition;
                        CUDDNode negCondition = CUDD.Function.Not(cEffect.Item1);
                        CUDD.Ref(negCondition);
                        frameCondition = CUDD.Function.And(intermediate, negCondition);
                        CUDD.Ref(frameCondition);
                        CUDD.Deref(intermediate);
                        CUDD.Deref(negCondition);
                    }
                }

                CUDDNode preAbstractPredNode = CUDD.Var(abstractPredPair.Value.PreviousCuddIndex);
                //AbstractPredicate sucAbstractPredicate = _sucAbstractPredDict[abstractPredicate.Key];
                CUDDNode sucAbstractPredNode = CUDD.Var(abstractPredPair.Value.SuccessorCuddIndex);

                CUDDNode invariant = CUDD.Function.Equal(preAbstractPredNode, sucAbstractPredNode);

                //Console.WriteLine("       Frame condition:");
                //CUDD.Print.PrintMinterm(frameCondition);
                //Console.WriteLine("       Invariant:");
                //CUDD.Print.PrintMinterm(invariant);
                CUDD.Ref(invariant);
                CUDDNode frame = CUDD.Function.Implies(frameCondition, invariant);
                CUDD.Ref(frame);
                CUDD.Deref(frameCondition);
                CUDD.Deref(invariant);

                CUDDNode conjunct = result;
                result = CUDD.Function.And(conjunct, frame);
                CUDD.Ref(result);
                CUDD.Deref(conjunct);
            }

            return result;
        }

        private CUDDNode GetEffectNode(Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>> cEffect)
        {
            CUDDNode effect = CUDD.ONE;

            foreach (var literal in cEffect.Item2)
            {
                CUDDNode intermediate = effect;
                CUDDNode abstractPred = CUDD.Var(literal.Item1.SuccessorCuddIndex);
                CUDDNode literalNode = literal.Item2 ? abstractPred : CUDD.Function.Not(abstractPred);
                effect = CUDD.Function.And(intermediate, literalNode);
                CUDD.Ref(effect);
                CUDD.Deref(intermediate);
            }

            //Console.WriteLine("    Condition:");
            //CUDD.Print.PrintMinterm(cEffect.Item1);
            //Console.WriteLine("    Effect:");
            //CUDD.Print.PrintMinterm(effect);

            CUDDNode result = CUDD.Function.Implies(cEffect.Item1, effect);
            CUDD.Ref(result);
            CUDD.Deref(effect);

            return result;
        }

        #endregion
    }
}
