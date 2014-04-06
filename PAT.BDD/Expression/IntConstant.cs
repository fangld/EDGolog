using System;
using System.Reflection.Emit;
using PAT.Common.Classes.SemanticModels.LTS.BDD;
using PAT.Common.Classes.CUDDLib;

namespace PAT.Common.Classes.Expressions.ExpressionClass
{
    public sealed class IntConstant : ExpressionValue
    {
        public int Value;
        public string Const;

        public IntConstant(int v)
        {
            Value = v;
            expressionID = Value.ToString();
        }

        public IntConstant(int v, string constName)
        {
            Value = v;
            Const = constName;
            expressionID = Value.ToString();
        }

        public override String ToString()
        {
            if (Const == null)
            {
                return Value.ToString();
            }
            return Const;            
        }

       
        /// <summary>
        /// Return the encoding of the integer value
        /// </summary>
        public override ExpressionBDDEncoding TranslateIntExpToBDD(Model model)
        {
            ExpressionBDDEncoding result = new ExpressionBDDEncoding();
            result.GuardDDs.Add(CUDD.Constant(1));
            result.ExpressionDDs.Add(CUDD.Constant(Value));
            return result;
        }
    }
}