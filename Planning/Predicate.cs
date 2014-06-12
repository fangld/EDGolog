using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class Predicate : VariableContainer
    {
        #region Constructors

        private Predicate() { }

        #endregion

        #region Methods

        public static Predicate FromContext(PlanningParser.AtomFormSkeletonContext context)
        {
            Predicate result = new Predicate();
            result.Name = context.pred().GetText();
            result.GenerateVariableList(context.listVariable());
            return result;
        }

        #endregion
    }
}