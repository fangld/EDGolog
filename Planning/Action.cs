using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public abstract class Action : VariableContainer 
    {
        #region Fields

        protected Dictionary<string, AbstractPredicate> _abstractPredDict;

        private List<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>> _effect;

        #endregion

        #region Properties

        protected abstract int PredicateCuddIndexNumber { get; }

        public CUDDNode Precondition { get; set; }


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

        protected Action()
        {
            _abstractPredDict = new Dictionary<string, AbstractPredicate>();
            _effect = new List<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>>();
        }

        #endregion

        #region Methods for creating an instance

        public abstract void From(int initialCuddIndex, PlanningParser.ActionDefineContext context,
            IReadOnlyDictionary<string, Predicate> predDict);

        #endregion

        #region Methods for generating abstract predicates

        protected void GenerateAbstractPredicates(PlanningParser.ActionDefBodyContext context, IReadOnlyDictionary<string, Predicate> predDict)
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

        private void GenerateAbstractPredicates(PlanningParser.AtomicFormulaTermContext context,
            IReadOnlyDictionary<string, Predicate> predDict)
        {
            var abstractPredicate = CreateAbstractPredicate(context, predDict);
            if (!_abstractPredDict.ContainsKey(abstractPredicate.ToString()))
            {
                for (int i = 0; i < PredicateCuddIndexNumber; i++)
                {
                    abstractPredicate.SetCuddIndex(i, CurrentCuddIndex);
                    CurrentCuddIndex++;
                }
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

        protected AbstractPredicate CreateAbstractPredicate(PlanningParser.AtomicFormulaTermContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            List<string> constantList = new List<string>();
            for (int i = 0; i < context.term().Count; i++)
            {
                constantList.Add(context.term()[i].GetText());
            }

            Predicate pred = predDict[context.predicate().GetText()];

            AbstractPredicate abstractPredicate = new AbstractPredicate(PredicateCuddIndexNumber, pred, constantList);

            return abstractPredicate;
        }

        #endregion

        #region Methods for generating precondition

        protected abstract CUDDNode GetCuddNode(PlanningParser.GdContext context);

        protected void GeneratePrecondition(PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            Precondition = CUDD.ONE;

            if (context.actionDefBody().emptyOrPreGD() != null)
            {
                if (context.actionDefBody().emptyOrPreGD().gd() != null)
                {
                    Precondition = GetCuddNode(context.actionDefBody().emptyOrPreGD().gd());
                }
            }
        }

        protected AbstractPredicate GetAbstractPredicate(PlanningParser.AtomicFormulaTermContext context)
        {
            string abstractPredName = GetFullName(context);
            AbstractPredicate result = _abstractPredDict[abstractPredName];
            return result;
        }

        #endregion

        #region Methods for generating effect

        protected void GenerateEffect(PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Predicate> predDict)
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
        
        #endregion
    }
}
