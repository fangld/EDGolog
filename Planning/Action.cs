using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class Action : VariableContainer
    {
        #region Fields

        private Dictionary<string, AbstractPredicate> _preAbstractPredDict;

        private Dictionary<string, AbstractPredicate> _sucAbstractPredDict;

        private List<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>> _effect;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; set; }

        public IReadOnlyList<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>> Effect
        {
            get { return _effect; }
        }

        public int CurrentCuddIndex { get; set; }

        public IReadOnlyDictionary<string, AbstractPredicate> PreviousAbstractPredicateDict
        {
            get { return _preAbstractPredDict; }
        }

        public IReadOnlyDictionary<string, AbstractPredicate> SuccessiveAbstractPredicateDict
        {
            get { return _sucAbstractPredDict; }
        }

        #endregion

        #region Constructors

        private Action(int initialCuddIndex)
        {
            CurrentCuddIndex = initialCuddIndex;
            _preAbstractPredDict = new Dictionary<string, AbstractPredicate>();
            _sucAbstractPredDict = new Dictionary<string, AbstractPredicate>();
            _effect = new List<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>>();
        }

        #endregion

        #region Methods for creating an instance

        public static Action FromContext(int initialCuddIndex, PlanningParser.ActionDefineContext context, Dictionary<string, Predicate> predDict)
        {
            Action result = new Action(initialCuddIndex);
            result.Name = context.actionSymbol().GetText();
            result.GenerateVariableList(context.listVariable());
            result.GenerateAbstractPredicates(context.actionDefBody(), predDict);
            result.GeneratePrecondition(context, predDict);
            result.GenerateEffect(context, predDict);
            return result;
        }

        #endregion

        #region Methods for generating abstract predicates

        private void GenerateAbstractPredicates(PlanningParser.ActionDefBodyContext context, Dictionary<string, Predicate> predDict)
        {
            if (context.PRE() != null)
            {
                if (context.emptyOrPreGD() != null)
                {
                    GenerateAbstractPredicates(context.emptyOrPreGD().gd(), _preAbstractPredDict, predDict);
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

        private void GenerateAbstractPredicates(PlanningParser.AtomicFormulaTermContext context, Dictionary<string, AbstractPredicate> abstractPredDict, Dictionary<string, Predicate> predDict)
        {
            var abstractPredicate = CreateAbstractPredicate(context, predDict);
            if (!abstractPredDict.ContainsKey(abstractPredicate.ToString()))
            {
                abstractPredicate.CuddIndex = CurrentCuddIndex;
                CurrentCuddIndex++;
                abstractPredDict.Add(abstractPredicate.ToString(), abstractPredicate);
            }
        }

        private void GenerateAbstractPredicates(PlanningParser.LiteralTermContext context, Dictionary<string, AbstractPredicate> abstractPredDict, Dictionary<string, Predicate> predDict)
        {
            GenerateAbstractPredicates(context.atomicFormulaTerm(), abstractPredDict, predDict);
        }

        private void GenerateAbstractPredicates(PlanningParser.GdContext context, Dictionary<string, AbstractPredicate> abstractPredDict, Dictionary<string, Predicate> predDict)
        {
            if (context.atomicFormulaTerm() != null)
            {
                GenerateAbstractPredicates(context.atomicFormulaTerm(), abstractPredDict, predDict);
            }
            else if (context.literalTerm() != null)
            {
                GenerateAbstractPredicates(context.literalTerm(), abstractPredDict, predDict);
            }
            else
            {
                for (int i = 0; i < context.gd().Count; i++)
                {
                    GenerateAbstractPredicates(context.gd()[i], abstractPredDict, predDict);
                }
            }
        }

        private void GenerateAbstractPredicates(PlanningParser.CEffectContext context, Dictionary<string, Predicate> predDict)
        {
            if (context.WHEN() != null)
            {
                GenerateAbstractPredicates(context.gd(), _preAbstractPredDict, predDict);
                foreach (var literalTermContext in context.condEffect().literalTerm())
                {
                    GenerateAbstractPredicates(literalTermContext, _sucAbstractPredDict, predDict);
                }
            }
            else
            {
                GenerateAbstractPredicates(context.literalTerm(), _sucAbstractPredDict, predDict);
            }
        }
        
        private AbstractPredicate CreateAbstractPredicate(PlanningParser.AtomicFormulaTermContext context, Dictionary<string, Predicate> predDict)
        {
            List<string> constantList = new List<string>();
            for (int i = 0; i < context.term().Count; i++)
            {
                constantList.Add(context.term()[i].GetText());
            }

            AbstractPredicate abstractPredicate = new AbstractPredicate(constantList);
            abstractPredicate.Predicate = predDict[context.predicate().GetText()];
            return abstractPredicate;
        }

        #endregion

        #region Methods for generating precondition

        private AbstractPredicate GetAbstractPredicate(PlanningParser.AtomicFormulaTermContext context)
        {
            string abstractPredName = GetFullName(context);
            //foreach (var pair in _preAbstractPredDict)
            //{
            //    Console.WriteLine("     Key:{0}", pair.Key);
            //}

            //Console.WriteLine("    Key:{0}", abstractPredName);
            AbstractPredicate result = _preAbstractPredDict[abstractPredName];
            return result;
        }

        private AbstractPredicate GetAbstractPredicate(PlanningParser.LiteralTermContext context)
        {
            return GetAbstractPredicate(context.atomicFormulaTerm());
        }

        private void GeneratePrecondition(PlanningParser.ActionDefineContext context, Dictionary<string, Predicate> predDict)
        {
            Precondition = CUDD.ONE;

            if (context.actionDefBody().emptyOrPreGD() != null)
            {
                if (context.actionDefBody().emptyOrPreGD().gd() != null)
                {
                    Precondition = Visit(context.actionDefBody().emptyOrPreGD().gd(), predDict, true);
                }
            }
        }

        #endregion

        #region Methods for generating effect

        private void GenerateEffect(PlanningParser.ActionDefineContext context, Dictionary<string, Predicate> predDict)
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
                condition = GetCuddNode(context.gd());
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

        private CUDDNode GetCuddNode(PlanningParser.AtomicFormulaTermContext context)
        {
            AbstractPredicate abstractPredicate = GetAbstractPredicate(context);
            int index = abstractPredicate.CuddIndex;
            CUDDNode result = CUDD.Var(index);
            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.LiteralTermContext context)
        {
            CUDDNode subNode = GetCuddNode(context.atomicFormulaTerm());
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

        private CUDDNode GetCuddNode(PlanningParser.GdContext context)
        {
            CUDDNode result = null;

            if (context.atomicFormulaTerm() != null)
            {
                result = GetCuddNode(context.atomicFormulaTerm());
            }
            else if (context.literalTerm() != null)
            {
                result = GetCuddNode(context.literalTerm());
            }
            else if (context.AND() != null)
            {
                result = GetCuddNode(context.gd()[0]);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gd()[i]);
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Ref(andNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = GetCuddNode(context.gd()[0]);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gd()[i]);
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    CUDD.Ref(orNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = GetCuddNode(context.gd()[0]);
                result = CUDD.Function.Not(gdNode);
                CUDD.Ref(result);
                CUDD.Deref(gdNode);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = GetCuddNode(context.gd()[0]);
                CUDDNode gdNode1 = GetCuddNode(context.gd()[1]);

                result = CUDD.Function.Implies(gdNode0, gdNode1);
                CUDD.Ref(result);
                CUDD.Deref(gdNode0);
                CUDD.Deref(gdNode1);
            }

            return result;
        }


        //private CUDDNode Visit(PlanningParser.EffectContext context, Dictionary<string, Predicate> predDict)
        //{
        //    CUDDNode result = Visit(context.cEffect()[0], predDict);
        //    //Console.WriteLine("     CEffNode: {0}", context.cEffect()[0].GetText());
        //    //Console.WriteLine("     CEffNode minterm:");
        //    //CUDD.Print.PrintMinterm(result);

        //    for (int i = 1; i < context.cEffect().Count; i++)
        //    {
        //        CUDDNode gdNode = Visit(context.cEffect()[i], predDict);
        //        //Console.WriteLine("     CEffNode: {0}", context.cEffect()[i].GetText());
        //        //Console.WriteLine("     CEffNode minterm:");
        //        //CUDD.Print.PrintMinterm(gdNode);

        //        CUDDNode andNode = CUDD.Function.And(result, gdNode);
        //        CUDD.Deref(result);
        //        CUDD.Ref(andNode);
        //        result = andNode;
        //    }

        //    //Console.WriteLine("     EffNode: {0}", context.GetText());
        //    //Console.WriteLine("     EffNode minterm:");
        //    //CUDD.Print.PrintMinterm(result);
        //    return result;
        //}

        //private CUDDNode Visit(PlanningParser.CEffectContext context, Dictionary<string, Predicate> predDict)
        //{
        //    CUDDNode result;

        //    if (context.WHEN() != null)
        //    {
        //        CUDDNode condPreNode = Visit(context.gd(), predDict, true);
        //        CUDDNode condEffNode = Visit(context.condEffect(), predDict);
        //        result = CUDD.Function.Implies(condPreNode, condEffNode);
        //        CUDD.Ref(result);
        //        CUDD.Deref(condPreNode);
        //        CUDD.Deref(condEffNode);
        //    }
        //    else
        //    {
        //        result = Visit(context.literalTerm(), predDict, false);
        //    }
        //    return result;
        //}

        //private CUDDNode Visit(PlanningParser.CondEffectContext context, Dictionary<string, Predicate> predDict)
        //{
        //    CUDDNode result = Visit(context.literalTerm()[0], predDict, false);
        //    //Console.WriteLine("     GdNode: {0}", context.literalTerm()[0].GetText());
        //    //Console.WriteLine("     GdNode minterm:");
        //    //CUDD.Print.PrintMinterm(result);

        //    for (int i = 1; i < context.literalTerm().Count; i++)
        //    {
        //        CUDDNode gdNode = Visit(context.literalTerm()[i], predDict, false);
        //        //Console.WriteLine("     GdNode: {0}", context.literalTerm()[i].GetText());
        //        //Console.WriteLine("     GdNode minterm:");
        //        //CUDD.Print.PrintMinterm(gdNode);
        //        CUDDNode andNode = CUDD.Function.And(result, gdNode);
        //        CUDD.Ref(andNode);
        //        CUDD.Deref(result);
        //        CUDD.Deref(gdNode);
        //        result = andNode;
        //    }

        //    //Console.WriteLine("  CondEffect minterm:");
        //    //CUDD.Print.PrintMinterm(result);
        //    return result;
        //}

        private CUDDNode Visit(PlanningParser.AtomicFormulaTermContext context, Dictionary<string, Predicate> predDict, bool isPrevious)
        {
            //Console.WriteLine("Before Atomic Formula: {0}, count: {1}", context.GetText(), _variablesCount);
            AbstractPredicate abstractPredicate = GetAbstractPredicate(context);
            //AbstractPredicate existingAbstractPredicate = isPrevious
            //    ? _preAbstractPredicates.Find(ap => ap.Equals(abstractPredicate))
            //    : _sucAbstractPredicates.Find(ap => ap.Equals(abstractPredicate));

            //Console.WriteLine("Test abstract predicate: {0}", abstractPredicate);
            //Console.WriteLine("Existing abstract predicate: {0}", existingAbstractPredicate == null);

            int index = abstractPredicate.CuddIndex;

            //Console.WriteLine("After Atomic Formula: {0}, count: {1}", context.GetText(), _variablesCount);
            CUDDNode result = CUDD.Var(index);
            return result;
        }

        private CUDDNode Visit(PlanningParser.LiteralTermContext context, Dictionary<string, Predicate> predDict, bool isPrevious)
        {
            //Console.WriteLine("Before Literal: {0}, count: {1}", context.GetText(), _variablesCount);
            CUDDNode subNode = Visit(context.atomicFormulaTerm(), predDict, isPrevious);
            CUDDNode result;

            if (context.NOT() != null)
            {
                //Console.WriteLine("Before Not literal: {0}, count: {1}", context.GetText(), _variablesCount);
                result = CUDD.Function.Not(subNode);
                CUDD.Ref(result);
                //Console.WriteLine("After Not literal: {0}, count: {1}", context.GetText(), _variablesCount);
            }
            else
            {
                result = subNode;
            }

            //Console.WriteLine("After Literal: {0}, count: {1}", context.GetText(), _variablesCount);
            return result;
        }

        private CUDDNode Visit(PlanningParser.GdContext context, Dictionary<string, Predicate> predDict, bool isPrevious)
        {
            //Console.WriteLine("Before Gd: {0}", context.GetText());
            CUDDNode result = null;

            if (context.atomicFormulaTerm() != null)
            {
                result = Visit(context.atomicFormulaTerm(), predDict, isPrevious);
            }
            else if (context.literalTerm() != null)
            {
                //Console.WriteLine("Before Literal gd: {0}", context.GetText());
                result = Visit(context.literalTerm(), predDict, isPrevious);
                //Console.WriteLine("After Literal gd: {0}", context.GetText());
            }
            else if (context.AND() != null)
            {
                result = Visit(context.gd()[0], predDict, isPrevious);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = Visit(context.gd()[i], predDict, isPrevious);
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Ref(andNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = Visit(context.gd()[0], predDict, isPrevious);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = Visit(context.gd()[i], predDict, isPrevious);
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    CUDD.Ref(orNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = Visit(context.gd()[0], predDict, isPrevious);
                result = CUDD.Function.Not(gdNode);
                CUDD.Ref(result);
                CUDD.Deref(gdNode);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = Visit(context.gd()[0], predDict, isPrevious);
                CUDDNode gdNode1 = Visit(context.gd()[1], predDict, isPrevious);

                result = CUDD.Function.Implies(gdNode0, gdNode1);
                CUDD.Ref(result);
                CUDD.Deref(gdNode0);
                CUDD.Deref(gdNode1);
            }

            //Console.WriteLine("After Gd: {0}", context.GetText());
            return result;
        }

        #endregion
    }
}
