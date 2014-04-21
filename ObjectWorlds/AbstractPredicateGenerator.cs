using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class AbstractPredicateGenerator
    {
        #region Fields

        private List<AbstractPredicate> _preAbstractPredicates;

        private List<AbstractPredicate> _sucAbstractPredicates;

        #endregion

        #region Properties

        public int CurrentIndex { get; set; }

        #endregion

        #region Constructors

        public AbstractPredicateGenerator(int initialIndex)
        {
            CurrentIndex = initialIndex;
            _preAbstractPredicates = new List<AbstractPredicate>();
            _sucAbstractPredicates = new List<AbstractPredicate>();
        }

        #endregion

        #region Overriden Methods

        private void AddAbstractPredicate(AbstractPredicate abstractPredicate)
        {
            if (!_preAbstractPredicates.Contains(abstractPredicate))
            {
                abstractPredicate.CuddIndex = CurrentIndex;
                CurrentIndex++;
                _preAbstractPredicates.Add(abstractPredicate);
            }
        }

        public void Visit(PlanningParser.ActionDefBodyContext context)
        {
            if (context.PRE() != null)
            {
                if (context.emptyOrPreGD() != null)
                {
                    VisitPre(context.emptyOrPreGD().gd());
                }
            }

            if (context.EFF() != null)
            {
                if (context.emptyOrEffect() != null)
                {
                    foreach (var cEffectContext in context.emptyOrEffect().effect().cEffect())
                    {
                        VisitEffectPre(cEffectContext);
                    }
                    foreach (var cEffectContext in context.emptyOrEffect().effect().cEffect())
                    {
                        VisitEffectSuc(cEffectContext);
                    }
                }
            }
        }

        public void VisitPre(PlanningParser.AtomicFormulaTermContext context)
        {
            List<string> variableNameList = new List<string>();
            for (int i = 0; i < context.term().Count; i++)
            {
                variableNameList.Add(context.term()[i].GetText());
            }

            AbstractPredicate abstractPredicate = new AbstractPredicate(variableNameList);
            AddAbstractPredicate(abstractPredicate);
        }

        public void VisitPre(PlanningParser.LiteralTermContext context)
        {
            VisitPre(context.atomicFormulaTerm());
        }

        public void VisitPre(PlanningParser.GdContext context)
        {
            if (context.atomicFormulaTerm() != null)
            {
                VisitPre(context.atomicFormulaTerm());
            }
            else if (context.literalTerm() != null)
            {
                VisitPre(context.literalTerm());
            }
            else
            {
                for (int i = 0; i < context.gd().Count; i++)
                {
                    VisitPre(context.gd()[i]);
                }
            }
        }

        private void VisitEffectPre(PlanningParser.CEffectContext context)
        {
            VisitPre(context.);
        }

        private void VisitEffectSuc(PlanningParser.CEffectContext context)
        {
            
        }

        #endregion
    }
}