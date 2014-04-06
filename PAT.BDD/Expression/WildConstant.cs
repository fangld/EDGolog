using System;
using PAT.Common.Classes.SemanticModels.LTS.BDD;
using PAT.Common.Classes.CUDDLib;

namespace PAT.Common.Classes.Expressions.ExpressionClass
{
    public sealed class WildConstant : ExpressionValue
    {
        public override String ToString()
        {
            return "*";            
        }

        /// <summary>
        /// Only 1 expression of constant, guard ONE
        /// </summary>
        public override ExpressionBDDEncoding TranslateBoolExpToBDD(Model model)
        {
            ExpressionBDDEncoding result = new ExpressionBDDEncoding();
            result.GuardDDs.Add(CUDD.Constant(1));
            return result;
        }
    }
}