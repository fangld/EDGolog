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

        private List<AbstractPredicate> _preAbstractPredicates;

        private List<AbstractPredicate> _sucAbstractPredicates;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; set; }

        public CUDDNode Effect { get; set; }

        public int CurrentCuddIndex { get; set; }

        public IReadOnlyList<AbstractPredicate> PreviousAbstractPredicates
        {
            get { return _preAbstractPredicates; }
        }

        public IReadOnlyList<AbstractPredicate> SuccessiveAbstractPredicates
        {
            get { return _sucAbstractPredicates; }
        }

        #endregion

        #region Constructors

        private Action(int initialCuddIndex)
        {
            CurrentCuddIndex = initialCuddIndex;
            _preAbstractPredicates = new List<AbstractPredicate>();
            _sucAbstractPredicates = new List<AbstractPredicate>();
        }

        //public Action(int initialCuddIndex, PlanningParser.ActionDefineContext context)
        //{
        //    CurrentCuddIndex = initialCuddIndex;
        //    _preAbstractPredicates = new List<AbstractPredicate>();
        //    _sucAbstractPredicates = new List<AbstractPredicate>();
        //    _context = context;
        //    FromContext();
        //}

        #endregion

        #region Methods

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

        private void GenerateAbstractPredicates(PlanningParser.ActionDefBodyContext context, Dictionary<string, Predicate> predDict)
        {
            //PlanningParser.ActionDefBodyContext actionDefBodyContext = context;
            if (context.PRE() != null)
            {
                if (context.emptyOrPreGD() != null)
                {
                    GenerateAbstractPredicates(context.emptyOrPreGD().gd(), _preAbstractPredicates, predDict);
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

            //Console.WriteLine("Previous Abstract Predicates: ");
            //foreach (var abstractPredicate in _preAbstractPredicates)
            //{
            //    Console.WriteLine("  Predicate: {0}, Index: {1}", abstractPredicate, abstractPredicate.CuddIndex);
            //}

            //Console.WriteLine("Successive Abstract Predicates: ");
            //foreach (var abstractPredicate in _sucAbstractPredicates)
            //{
            //    Console.WriteLine("  Predicate: {0}, Index: {1}", abstractPredicate, abstractPredicate.CuddIndex);
            //}
        }

        private AbstractPredicate GetAbstractPredicate(PlanningParser.AtomicFormulaTermContext context, Dictionary<string, Predicate> predDict)
        {
            List<string> variableNameList = new List<string>();
            for (int i = 0; i < context.term().Count; i++)
            {
                variableNameList.Add(context.term()[i].GetText());
            }

            AbstractPredicate abstractPredicate = new AbstractPredicate(variableNameList);
            abstractPredicate.Predicate = predDict[context.predicate().GetText()];
            return abstractPredicate;
        }

        private void GenerateAbstractPredicates(PlanningParser.AtomicFormulaTermContext context, List<AbstractPredicate> abstractPredicates, Dictionary<string, Predicate> predDict)
        {
            var abstractPredicate = GetAbstractPredicate(context, predDict);
            AddAbstractPredicate(abstractPredicate, abstractPredicates);
        }

        private void GenerateAbstractPredicates(PlanningParser.LiteralTermContext context, List<AbstractPredicate> abstractPredicates, Dictionary<string, Predicate> predDict)
        {
            GenerateAbstractPredicates(context.atomicFormulaTerm(), abstractPredicates, predDict);
        }

        private void GenerateAbstractPredicates(PlanningParser.GdContext context, List<AbstractPredicate> abstractPredicates, Dictionary<string, Predicate> predDict)
        {
            if (context.atomicFormulaTerm() != null)
            {
                GenerateAbstractPredicates(context.atomicFormulaTerm(), abstractPredicates, predDict);
            }
            else if (context.literalTerm() != null)
            {
                GenerateAbstractPredicates(context.literalTerm(), abstractPredicates, predDict);
            }
            else
            {
                for (int i = 0; i < context.gd().Count; i++)
                {
                    GenerateAbstractPredicates(context.gd()[i], abstractPredicates, predDict);
                }
            }
        }

        private void GenerateAbstractPredicates(PlanningParser.CEffectContext context, Dictionary<string, Predicate> predDict)
        {
            if (context.WHEN() != null)
            {
                GenerateAbstractPredicates(context.gd(), _preAbstractPredicates, predDict);
                foreach (var literalTermContext in context.condEffect().literalTerm())
                {
                    GenerateAbstractPredicates(literalTermContext, _sucAbstractPredicates, predDict);
                }
            }
            else
            {
                GenerateAbstractPredicates(context.literalTerm(), _sucAbstractPredicates, predDict);
            }
        }

        private void AddAbstractPredicate(AbstractPredicate abstractPredicate, List<AbstractPredicate> abstractPredicates)
        {
            if (!abstractPredicates.Contains(abstractPredicate))
            {
                abstractPredicate.CuddIndex = CurrentCuddIndex;
                CurrentCuddIndex++;
                abstractPredicates.Add(abstractPredicate);
            }
        }

        private void GeneratePrecondition(PlanningParser.ActionDefineContext context, Dictionary<string, Predicate> predDict)
        {
            Precondition = CUDD.ONE;

            if (context.actionDefBody().emptyOrPreGD() != null)
            {
                if (context.actionDefBody().emptyOrPreGD().gd() != null)
                {
                    Precondition = Visit(true, context.actionDefBody().emptyOrPreGD().gd(), predDict);
                }
            }
        }

        private void GenerateEffect(PlanningParser.ActionDefineContext context, Dictionary<string, Predicate> predDict)
        {
            Effect = CUDD.ONE;
            if (context.actionDefBody().emptyOrEffect() != null)
            {
                if (context.actionDefBody().emptyOrEffect().effect() != null)
                {
                    Effect = Visit(context.actionDefBody().emptyOrEffect().effect(), predDict);
                    //Console.WriteLine("     GenerateEffect EffNode: {0}", _context.actionDefBody().emptyOrEffect().effect().GetText());
                    //Console.WriteLine("     GenerateEffect EffNode minterm:");
                    //CUDD.Print.PrintMinterm(Effect);
                }
            }
        }
        private CUDDNode Visit(PlanningParser.EffectContext context, Dictionary<string, Predicate> predDict)
        {
            CUDDNode result = Visit(context.cEffect()[0], predDict);
            //Console.WriteLine("     CEffNode: {0}", context.cEffect()[0].GetText());
            //Console.WriteLine("     CEffNode minterm:");
            //CUDD.Print.PrintMinterm(result);

            for (int i = 1; i < context.cEffect().Count; i++)
            {
                CUDDNode gdNode = Visit(context.cEffect()[i], predDict);
                //Console.WriteLine("     CEffNode: {0}", context.cEffect()[i].GetText());
                //Console.WriteLine("     CEffNode minterm:");
                //CUDD.Print.PrintMinterm(gdNode);

                CUDDNode andNode = CUDD.Function.And(result, gdNode);
                CUDD.Deref(result);
                CUDD.Ref(andNode);
                result = andNode;
            }

            //Console.WriteLine("     EffNode: {0}", context.GetText());
            //Console.WriteLine("     EffNode minterm:");
            //CUDD.Print.PrintMinterm(result);
            return result;
        }

        private CUDDNode Visit(PlanningParser.CEffectContext context, Dictionary<string, Predicate> predDict)
        {
            CUDDNode result;

            if (context.WHEN() != null)
            {
                CUDDNode condPreNode = Visit(true, context.gd(), predDict);
                CUDDNode condEffNode = Visit(context.condEffect(), predDict);
                result = CUDD.Function.Implies(condPreNode, condEffNode);
                CUDD.Ref(result);
                CUDD.Deref(condPreNode);
                CUDD.Deref(condEffNode);
            }
            else
            {
                result = Visit(false, context.literalTerm(), predDict);
            }
            return result;
        }

        private CUDDNode Visit(PlanningParser.CondEffectContext context, Dictionary<string, Predicate> predDict)
        {
            CUDDNode result = Visit(false, context.literalTerm()[0], predDict);
            //Console.WriteLine("     GdNode: {0}", context.literalTerm()[0].GetText());
            //Console.WriteLine("     GdNode minterm:");
            //CUDD.Print.PrintMinterm(result);

            for (int i = 1; i < context.literalTerm().Count; i++)
            {
                CUDDNode gdNode = Visit(false, context.literalTerm()[i], predDict);
                //Console.WriteLine("     GdNode: {0}", context.literalTerm()[i].GetText());
                //Console.WriteLine("     GdNode minterm:");
                //CUDD.Print.PrintMinterm(gdNode);
                CUDDNode andNode = CUDD.Function.And(result, gdNode);
                CUDD.Deref(result);
                CUDD.Ref(andNode);
                result = andNode;
            }

            //Console.WriteLine("  CondEffect minterm:");
            //CUDD.Print.PrintMinterm(result);
            return result;
        }

        private CUDDNode Visit(bool isPrevious, PlanningParser.AtomicFormulaTermContext context, Dictionary<string, Predicate> predDict)
        {
            //Console.WriteLine("Before Atomic Formula: {0}, count: {1}", context.GetText(), _variablesCount);
            AbstractPredicate abstractPredicate = GetAbstractPredicate(context, predDict);
            AbstractPredicate existingAbstractPredicate = isPrevious
                ? _preAbstractPredicates.Find(ap => ap.Equals(abstractPredicate))
                : _sucAbstractPredicates.Find(ap => ap.Equals(abstractPredicate));

            //Console.WriteLine("Test abstract predicate: {0}", abstractPredicate);
            //Console.WriteLine("Existing abstract predicate: {0}", existingAbstractPredicate == null);

            int index = existingAbstractPredicate.CuddIndex;

            //Console.WriteLine("After Atomic Formula: {0}, count: {1}", context.GetText(), _variablesCount);
            CUDDNode result = CUDD.Var(index);
            return result;
        }

        private CUDDNode Visit(bool isPrevious, PlanningParser.LiteralTermContext context, Dictionary<string, Predicate> predDict)
        {
            //Console.WriteLine("Before Literal: {0}, count: {1}", context.GetText(), _variablesCount);
            CUDDNode subNode = Visit(isPrevious, context.atomicFormulaTerm(), predDict);
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

        private CUDDNode Visit(bool isPrevious, PlanningParser.GdContext context, Dictionary<string, Predicate> predDict)
        {
            //Console.WriteLine("Before Gd: {0}", context.GetText());
            CUDDNode result = null;

            if (context.atomicFormulaTerm() != null)
            {
                result = Visit(isPrevious, context.atomicFormulaTerm(), predDict);
            }
            else if (context.literalTerm() != null)
            {
                //Console.WriteLine("Before Literal gd: {0}", context.GetText());
                result = Visit(isPrevious, context.literalTerm(), predDict);
                //Console.WriteLine("After Literal gd: {0}", context.GetText());
            }
            else if (context.AND() != null)
            {
                result = Visit(isPrevious, context.gd()[0], predDict);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = Visit(isPrevious, context.gd()[i], predDict);
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Deref(result);
                    CUDD.Ref(andNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = Visit(isPrevious, context.gd()[0], predDict);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = Visit(isPrevious, context.gd()[i], predDict);
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    CUDD.Deref(result);
                    CUDD.Ref(orNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = Visit(isPrevious, context.gd()[0], predDict);
                result = CUDD.Function.Not(gdNode);
                CUDD.Deref(gdNode);
                CUDD.Ref(result);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = Visit(isPrevious, context.gd()[0], predDict);
                CUDDNode gdNode1 = Visit(isPrevious, context.gd()[1], predDict);

                result = CUDD.Function.Implies(gdNode0, gdNode1);
                CUDD.Deref(gdNode0);
                CUDD.Deref(gdNode1);
                CUDD.Ref(result);
            }

            //Console.WriteLine("After Gd: {0}", context.GetText());
            return result;
        }

        #endregion
    }
}
