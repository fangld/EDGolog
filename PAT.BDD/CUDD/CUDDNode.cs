﻿using System;

namespace PAT.Common.Classes.CUDDLib
{
    /// <summary>
    /// Wrapper of a boolean expression
    /// </summary>
    public class CUDDNode
    {
        public IntPtr Ptr;

        public CUDDNode(IntPtr p)
        {
            this.Ptr = p;
        }

        public CUDDNode(CUDDNode dd)
        {
            this.Ptr = dd.Ptr;
        }

        /// <summary>
        /// Check whether the represented boolean expression is actually a constant
        /// </summary>
        /// <returns></returns>
        public bool IsConstant()
        {
            return CUDD.IsConstant(this);
        }

        public int GetIndex()
        {
            return CUDD.GetIndex(this);
        }

        public double GetValue()
        {
            return CUDD.GetValue(this);
        }

        public CUDDNode GetThen()
        {
            return CUDD.GetThen(this);
        }

        public CUDDNode GetElse()
        {
            return CUDD.GetElse(this);
        }

        /// <summary>
        /// Use to check 2 CUDDNode are the same. Often used to check a node is a Zero or One
        /// [ REFS: 'none', DEREFS: 'none']
        /// </summary>
        public override bool Equals(object obj)
        {
            //Console.WriteLine("Hello Equal");
            CUDDNode dd = obj as CUDDNode;
            return (this.Ptr == dd.Ptr);
        }

        public override int GetHashCode()
        {
            return (int)this.Ptr;
        }

        //public static bool operator ==(CUDDNode a, CUDDNode b)
        //{
        //    return a.Equals(b);

        //    bool result;
        //    if (a == null)
        //    {
        //        result = b == null;
        //    }
        //    else if (b == null)
        //    {
        //        result = false;
        //    }
        //    else
        //    {
        //        result = a.Equals(b);
        //    }
        //    return result;
        //}

        //public static bool operator !=(CUDDNode a, CUDDNode b)
        //{
        //    return !(a.Equals(b));
        //}
    }
}