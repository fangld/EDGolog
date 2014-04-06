using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using PAT.Common.Classes.SemanticModels.LTS.BDD;
using PAT.Common.Classes.CUDDLib;

namespace PAT.Common.Classes.Expressions.ExpressionClass
{
    public sealed class Sequence : Expression
    {
        public Expression FirstPart, SecondPart;
        public Sequence(Expression f, Expression s)
        {
            FirstPart = f;
            SecondPart = s;
            HasVar = f.HasVar || s.HasVar;
            ExpressionType = ExpressionType.Sequence;
            expressionID = FirstPart.ExpressionID + ";" + SecondPart.ExpressionID;

        }

        public override String ToString()
        {
            return FirstPart + " " + SecondPart;
        }

       public override Expression ClearConstant(Dictionary<string, Expression> constMapping)
        {
            return new Sequence(FirstPart.ClearConstant(constMapping), SecondPart.ClearConstant(constMapping));
        }

        /// <summary>
        /// A sequence, containing some statement, must be encoded in TranslateStatementToBDD mode
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override ExpressionBDDEncoding TranslateBoolExpToBDD(Model model)
        {
            ExpressionBDDEncoding result = new ExpressionBDDEncoding();
            result.GuardDDs.Add(CUDD.Constant(1));

            result = TranslateStatementToBDD(result, model);
                        
            return result;
        }

        /// <summary>
        /// [ REFS: 'result', DEREFS: 'resultBefore' ]
        /// </summary>
        public override ExpressionBDDEncoding TranslateStatementToBDD(ExpressionBDDEncoding resultBefore, Model model)
        {
            resultBefore = this.FirstPart.TranslateStatementToBDD(resultBefore, model);
            resultBefore = this.SecondPart.TranslateStatementToBDD(resultBefore, model);

            return resultBefore;
        }
    }
}