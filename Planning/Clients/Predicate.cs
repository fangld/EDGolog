using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning.Clients
{
    public class Predicate : VariableContainer
    {
        #region Constructors

        private Predicate() { }

        #endregion

        #region Methods

        public static Predicate FromContext(PlanningParser.AtomicFormulaSkeletonContext context)
        {
            Predicate result = new Predicate();
            result.Name = context.predicate().GetText();
            result.GenerateVariableList(context.listVariable());
            return result;
        }

        #endregion
    }
}