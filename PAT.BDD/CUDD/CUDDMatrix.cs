namespace PAT.Common.Classes.CUDDLib
{
    public partial class CUDD
    {
        /// <summary>
        /// Matrix in BDD
        /// </summary>
        public class Matrix
        {
            public const int CMU = 1;
            public const int BOULDER = 2;

            /// <summary>
            /// Set the value of i.th element in vector. i is allowed to be negative. -1 is the last element.
            /// [ REFS: 'result', DEREFS: 'dd' ]
            /// </summary>
            public static CUDDNode SetVectorElement(CUDDNode dd, CUDDVars vars, int index, double value)
            {
                return new CUDDNode(PlatformInvoke.DD_SetVectorElement(manager, dd.Ptr, vars.GetArrayPointer(), vars.GetNumVars(), index, value));
            }

            /// <summary>
            /// Sets element in matrix dd
            /// [ REFS: 'result', DEREFS: 'dd' ]
            /// </summary>
            public static CUDDNode SetMatrixElement(CUDDNode dd, CUDDVars rVars, CUDDVars cVars, int rIndex, int cIndex, double value)
            {
                return new CUDDNode(PlatformInvoke.DD_SetMatrixElement(manager, dd.Ptr, rVars.GetArrayPointer(), rVars.GetNumVars(), cVars.GetArrayPointer(), cVars.GetNumVars(), rIndex, cIndex, value));
            }

            /// <summary>
            /// // sets element in 3d matrix dd
            /// [ REFS: 'result', DEREFS: 'dd' ]
            /// </summary>
            public static CUDDNode Set3DMatrixElement(CUDDNode dd, CUDDVars rVars, CUDDVars cVars, CUDDVars lVars, int rIndex, int cIndex, int lIndex, double value)
            {
                return new CUDDNode(PlatformInvoke.DD_Set3DMatrixElement(manager, dd.Ptr, rVars.GetArrayPointer(), rVars.GetNumVars(), cVars.GetArrayPointer(), cVars.GetNumVars(), lVars.GetArrayPointer(), lVars.GetNumVars(), rIndex, cIndex, lIndex, value));
            }

            /// <summary>
            /// Get element in vector dd
            /// [ REFS: 'none', DEREFS: 'none' ]
            /// </summary>
            public static double GetVectorElement(CUDDNode dd, CUDDVars vars, int index)
            {
                return PlatformInvoke.DD_GetVectorElement(manager, dd.Ptr, vars.GetArrayPointer(), vars.GetNumVars(), index);
            }

            /// <summary>
            /// Generates 0-1 ADD for the function x = y
            /// where x, y are num_vars-bit numbers encoded by variables x_vars, y_vars
            /// [ REFS: 'result', DEREFS: 'none' ]
            /// </summary>
            public static CUDDNode Identity(CUDDVars rVars, CUDDVars cVars)
            {
                return new CUDDNode(PlatformInvoke.DD_Identity(manager, rVars.GetArrayPointer(), cVars.GetArrayPointer(), rVars.GetNumVars()));
            }

            /// <summary>
            /// Returns transpose of matrix dd
            /// [ REFS: 'result', DEREFS: 'dd' ]
            /// </summary>
            public static CUDDNode Transpose(CUDDNode dd, CUDDVars rVars, CUDDVars cVars)
            {
                return new CUDDNode(PlatformInvoke.DD_Transpose(manager, dd.Ptr, rVars.GetArrayPointer(), cVars.GetArrayPointer(), rVars.GetNumVars()));
            }

            /// <summary>
            /// Returns matrix multiplication of matrices dd1 and dd2
            ///  [ REFS: 'result', DEREFS: 'dd1, dd2' ]
            /// </summary>
            public static CUDDNode MatrixMultiply(CUDDNode dd1, CUDDNode dd2, CUDDVars vars, int method)
            {
                return new CUDDNode(PlatformInvoke.DD_MatrixMultiply(manager, dd1.Ptr, dd2.Ptr, vars.GetArrayPointer(), vars.GetNumVars(), method));
            }
        }
    }
}
