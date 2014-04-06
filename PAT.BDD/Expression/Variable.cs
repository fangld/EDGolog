using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using PAT.Common.Classes.CUDDLib;
using PAT.Common.Classes.SemanticModels.LTS.BDD;

namespace PAT.Common.Classes.Expressions.ExpressionClass
{
    public sealed class Variable : Expression
    {
        //public String VarName;

        public Variable(String name)
        {
            //VarName = nam;
            HasVar = true;
            ExpressionType = ExpressionType.Variable;
            expressionID = name;
        }

        public override String ToString()
        {
            return expressionID;
        }

        
        public override Expression ClearConstant(Dictionary<string, Expression> constMapping)
        {
            if (constMapping.ContainsKey(expressionID))
            {
                return constMapping[expressionID];
            }

            return new Variable(expressionID);
        }

        /// <summary>
        /// Encode as booleane expression, check whether the variable is true
        /// </summary>
        public override ExpressionBDDEncoding TranslateBoolExpToBDD(Model model)
        {
            return new PrimitiveApplication(PrimitiveApplication.EQUAL, new Variable(this.expressionID), new BoolConstant(true)).TranslateBoolExpToBDD(model);
        }

        /// <summary>
        /// Return variable expression
        /// </summary>
        public override ExpressionBDDEncoding TranslateIntExpToBDD(Model model)
        {
            ExpressionBDDEncoding result = new ExpressionBDDEncoding();
            result.GuardDDs.Add(CUDD.Constant(1));

            int variableIndex = model.GetVarIndex(this.expressionID);
            CUDDNode varDD = model.variableEncoding[variableIndex];
            CUDD.Ref(varDD);

            result.ExpressionDDs.Add(varDD);

            return result;
        }
    }
}