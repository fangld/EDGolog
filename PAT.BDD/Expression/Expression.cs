using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using PAT.Common.Classes.CUDDLib;
using PAT.Common.Classes.SemanticModels.LTS.BDD;

namespace PAT.Common.Classes.Expressions.ExpressionClass
{
    /// <summary>
    /// ExpressionBDDEncoding to suport array expression.
    /// For example: a[i] then the GuardDDs will contains condition of value of i, i = 0, i = 1,...,
    /// ExpressionDDs will contain the corresponding expression of a[i], a[0], a[1], a[2]...
    /// After finishing encoding a boolean expresion which does not consist of any ADD, then only need to use GuarDDS
    /// Then just get the GuardDDs[0]
    /// </summary>
    public class ExpressionBDDEncoding
    {
        /// <summary>
        /// BDD
        /// When ExpressionBDDEncoding is a result of an complete expression, GuardDDs only contains 1 elements
        /// </summary>
        public List<CUDDNode> GuardDDs;

        /// <summary>
        /// ADD
        /// When ExpressionBDDEncoding is a result of an incomplete expression, ExpressionDDs is not empty
        /// </summary>
        public List<CUDDNode> ExpressionDDs;

       
        public ExpressionBDDEncoding()
        {
            this.GuardDDs = new List<CUDDNode>();
            this.ExpressionDDs = new List<CUDDNode>();
        }

        /// <summary>
        /// Some expression we only care with guard, not expression  like boolean expression.
        /// In such cases, expression is empty. So size should get from guards
        /// </summary>
        public int Count()
        {
            return this.GuardDDs.Count;
        }

        public void DeRef()
        {
            CUDD.Deref(this.GuardDDs);
            CUDD.Deref(this.ExpressionDDs);
        }

        public void Ref()
        {
            CUDD.Ref(this.GuardDDs);
            CUDD.Ref(this.ExpressionDDs);
        }

        /// <summary>
        /// [ REFS: 'none', DEREFS: 'dd if failed to add' ]
        /// </summary>
        public void AddNodeToGuard(CUDDNode dd)
        {
            if (!dd.Equals(CUDD.ZERO))
            {
                this.GuardDDs.Add(dd);
            }
            else
            {
                CUDD.Deref(dd);
            }
        }

        /// <summary>
        /// [ REFS: 'none', DEREFS: 'dd if failed to add' ]
        /// </summary>
        public void AddNodeToGuard(List<CUDDNode> dds)
        {
            foreach (CUDDNode dd in dds)
            {
                this.AddNodeToGuard(dd);
            }
        }
    }

    public enum ExpressionType : byte
    {
        Constant,
        Variable,
        Record,
        PrimitiveApplication,
        Assignment,
        PropertyAssignment,
        If,
        Sequence,
        While,
        Let,
        StaticMethodCall,
        ClassMethodCall,
        NewObjectCreation,
        ClassMethodCallInstance,
        ClassProperty,
        ClassPropertyAssignment
        //Fun,
        //RecFun,
        //Application

    }

    [Serializable]
    public abstract class Expression
    {
        public ExpressionType ExpressionType;
        internal string expressionID;
        public virtual string ExpressionID
        {
            get
            {
                return expressionID;
            }
        }

        public virtual List<string> GetVars()
        {
            return new List<string>(0);
        }

        public bool HasVar;

        public virtual Expression ClearConstant(Dictionary<string, Expression> constMapping)
        {
            return this;
        }

        /// <summary>
        /// Encode a boolean expression
        /// [ REFS: 'result', DEREFS: '' ]
        /// </summary>
        public virtual ExpressionBDDEncoding TranslateBoolExpToBDD(Model model)
        {
            return new ExpressionBDDEncoding();
        }

        /// <summary>
        /// Encode an arithmetic expression
        /// [ REFS: 'result', DEREFS: '' ]
        /// </summary>
        public virtual ExpressionBDDEncoding TranslateIntExpToBDD(Model model)
        {
            return new ExpressionBDDEncoding();
        }

        /// <summary>
        /// For Update, If While, Sequence. Based on the current variable values in resultBefore, return the variable values after the statement is executed
        /// [ REFS: 'result', DEREFS: 'resultBefore' ]
        /// </summary>
        public virtual ExpressionBDDEncoding TranslateStatementToBDD(ExpressionBDDEncoding resultBefore, Model model)
        {
            return new ExpressionBDDEncoding();
        }

        /// <summary>
        /// Return guard1 and guard2
        /// </summary>
        /// <param name="guard1"></param>
        /// <param name="guard2"></param>
        /// <returns></returns>
        public static Expression CombineGuard(Expression guard1, Expression guard2)
        {
            if (guard1 != null && guard2 != null)
            {
                return new PrimitiveApplication(PrimitiveApplication.AND, guard1, guard2);
            }
            else if (guard1 == null)
            {
                return guard2;
            }
            else
            {
                return guard1;
            }
        }

        /// <summary>
        /// Return block1;block2
        /// </summary>
        /// <param name="block1"></param>
        /// <param name="block2"></param>
        /// <returns></returns>
        public static Expression CombineProgramBlock(Expression block1, Expression block2)
        {
            if (block1 != null && block2 != null)
            {
                return new Sequence(block1, block2);
            }
            else if (block1 == null)
            {
                return block2;
            }
            else
            {
                return block1;
            }
        }
    }

    [Serializable]
    public abstract class ExpressionValue : Expression
    {
       
    }
}
