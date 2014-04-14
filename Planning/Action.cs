using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class Action : Predicate
    {
        #region Fields

        private List<AbstractPredicate> _preAbstractPredicates;

        private List<AbstractPredicate> _sucAbstractPredicates;

        private PlanningParser.ActionDefineContext _context;

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

        public Action(int initialCuddIndex, PlanningParser.ActionDefineContext context)
        {
            CurrentCuddIndex = initialCuddIndex;
            _preAbstractPredicates = new List<AbstractPredicate>();
            _sucAbstractPredicates = new List<AbstractPredicate>();
            _context = context;
            FromContext();
        }

        #endregion

        #region Methods

        private void FromContext()
        {
            Name = _context.actionSymbol().GetText();
            AddVariablesToContainer();
            GenerateAbstractPredicates();
            GeneratePrecondition();
            GenerateEffect();
        }

        private void AddVariablesToContainer()
        {
            var listVariableContext =  _context.listVariable();
            do
            {
                if (listVariableContext.VAR().Count != 0)
                {
                    string type = listVariableContext.type() == null
                        ? DomainLoader.DefaultType
                        : listVariableContext.type().GetText();
                    foreach (var varNode in listVariableContext.VAR())
                    {
                        AddVariable(varNode.GetText(), type);
                    }
                }
                listVariableContext = listVariableContext.listVariable();
            } while (listVariableContext != null);
        }

        public void GenerateAbstractPredicates()
        {
            PlanningParser.ActionDefBodyContext actionDefBodyContext = _context.actionDefBody();
            if (actionDefBodyContext.PRE() != null)
            {
                if (actionDefBodyContext.emptyOrPreGD() != null)
                {
                    GenerateAbstractPredicates(actionDefBodyContext.emptyOrPreGD().gd(), _preAbstractPredicates);
                }
            }

            if (actionDefBodyContext.EFF() != null)
            {
                if (actionDefBodyContext.emptyOrEffect() != null)
                {
                    foreach (var cEffectContext in actionDefBodyContext.emptyOrEffect().effect().cEffect())
                    {
                        GenerateAbstractPredicates(cEffectContext);
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

        private AbstractPredicate GetAbstractPredicate(PlanningParser.AtomicFormulaTermContext context)
        {
            List<string> variableNameList = new List<string>();
            for (int i = 0; i < context.term().Count; i++)
            {
                variableNameList.Add(context.term()[i].GetText());
            }

            AbstractPredicate abstractPredicate = new AbstractPredicate(variableNameList);
            abstractPredicate.Name = context.predicate().GetText();
            return abstractPredicate;
        }

        private void GenerateAbstractPredicates(PlanningParser.AtomicFormulaTermContext context, List<AbstractPredicate> abstractPredicates)
        {
            var abstractPredicate = GetAbstractPredicate(context);
            AddAbstractPredicate(abstractPredicate, abstractPredicates);
        }

        private void GenerateAbstractPredicates(PlanningParser.LiteralTermContext context, List<AbstractPredicate> abstractPredicates)
        {
            GenerateAbstractPredicates(context.atomicFormulaTerm(), abstractPredicates);
        }

        private void GenerateAbstractPredicates(PlanningParser.GdContext context, List<AbstractPredicate> abstractPredicates)
        {
            if (context.atomicFormulaTerm() != null)
            {
                GenerateAbstractPredicates(context.atomicFormulaTerm(), abstractPredicates);
            }
            else if (context.literalTerm() != null)
            {
                GenerateAbstractPredicates(context.literalTerm(), abstractPredicates);
            }
            else
            {
                for (int i = 0; i < context.gd().Count; i++)
                {
                    GenerateAbstractPredicates(context.gd()[i], abstractPredicates);
                }
            }
        }

        private void GenerateAbstractPredicates(PlanningParser.CEffectContext context)
        {
            if (context.WHEN() != null)
            {
                GenerateAbstractPredicates(context.gd(), _preAbstractPredicates);
                foreach (var literalTermContext in context.condEffect().literalTerm())
                {
                    GenerateAbstractPredicates(literalTermContext, _sucAbstractPredicates);
                }
            }
            else
            {
                GenerateAbstractPredicates(context.literalTerm(), _sucAbstractPredicates);
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

        private void GeneratePrecondition()
        {
            Precondition = CUDD.ONE;

            if (_context.actionDefBody().emptyOrPreGD() != null)
            {
                if (_context.actionDefBody().emptyOrPreGD().gd() != null)
                {
                    Precondition = Visit(_context.actionDefBody().emptyOrPreGD().gd(),true);
                }
            }
        }

        private void GenerateEffect()
        {
            Effect = CUDD.ONE;
            if (_context.actionDefBody().emptyOrEffect() != null)
            {
                if (_context.actionDefBody().emptyOrEffect().effect() != null)
                {
                    Effect = Visit(_context.actionDefBody().emptyOrEffect().effect());
                    //Console.WriteLine("     GenerateEffect EffNode: {0}", _context.actionDefBody().emptyOrEffect().effect().GetText());
                    //Console.WriteLine("     GenerateEffect EffNode minterm:");
                    //CUDD.Print.PrintMinterm(Effect);
                }
            }
        }
        private CUDDNode Visit(PlanningParser.EffectContext context)
        {
            CUDDNode result = Visit(context.cEffect()[0]);
            //Console.WriteLine("     CEffNode: {0}", context.cEffect()[0].GetText());
            //Console.WriteLine("     CEffNode minterm:");
            //CUDD.Print.PrintMinterm(result);

            for (int i = 1; i < context.cEffect().Count; i++)
            {
                CUDDNode gdNode = Visit(context.cEffect()[i]);
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

        private CUDDNode Visit(PlanningParser.CEffectContext context)
        {
            CUDDNode result;

            if (context.WHEN() != null)
            {
                CUDDNode condPreNode = Visit(context.gd(), true);
                CUDDNode condEffNode = Visit(context.condEffect());
                result = CUDD.Function.Implies(condPreNode, condEffNode);
                CUDD.Ref(result);
                CUDD.Deref(condPreNode);
                CUDD.Deref(condEffNode);
            }
            else
            {
                result = Visit(context.literalTerm(), false);
            }
            return result;
        }

        private CUDDNode Visit(PlanningParser.CondEffectContext context)
        {
            CUDDNode result = Visit(context.literalTerm()[0], false);
            //Console.WriteLine("     GdNode: {0}", context.literalTerm()[0].GetText());
            //Console.WriteLine("     GdNode minterm:");
            //CUDD.Print.PrintMinterm(result);

            for (int i = 1; i < context.literalTerm().Count; i++)
            {
                CUDDNode gdNode = Visit(context.literalTerm()[i], false);
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

        private CUDDNode Visit(PlanningParser.AtomicFormulaTermContext context, bool isPrevious)
        {
            //Console.WriteLine("Before Atomic Formula: {0}, count: {1}", context.GetText(), _variablesCount);
            AbstractPredicate abstractPredicate = GetAbstractPredicate(context);
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

        private CUDDNode Visit(PlanningParser.LiteralTermContext context, bool isPrevious)
        {
            //Console.WriteLine("Before Literal: {0}, count: {1}", context.GetText(), _variablesCount);
            CUDDNode subNode = Visit(context.atomicFormulaTerm(), isPrevious);
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

        private CUDDNode Visit(PlanningParser.GdContext context, bool isPrevious)
        {
            //Console.WriteLine("Before Gd: {0}", context.GetText());
            CUDDNode result = null;

            if (context.atomicFormulaTerm() != null)
            {
                result = Visit(context.atomicFormulaTerm(), isPrevious);
            }
            else if (context.literalTerm() != null)
            {
                //Console.WriteLine("Before Literal gd: {0}", context.GetText());
                result = Visit(context.literalTerm(), isPrevious);
                //Console.WriteLine("After Literal gd: {0}", context.GetText());
            }
            else if (context.AND() != null)
            {
                result = Visit(context.gd()[0], isPrevious);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = Visit(context.gd()[i], isPrevious);
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Deref(result);
                    CUDD.Ref(andNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = Visit(context.gd()[0], isPrevious);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = Visit(context.gd()[i], isPrevious);
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    CUDD.Deref(result);
                    CUDD.Ref(orNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = Visit(context.gd()[0], isPrevious);
                result = CUDD.Function.Not(gdNode);
                CUDD.Deref(gdNode);
                CUDD.Ref(result);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = Visit(context.gd()[0], isPrevious);
                CUDDNode gdNode1 = Visit(context.gd()[1], isPrevious);

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
