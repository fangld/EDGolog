using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Collections
{
    public abstract class MixedRadixToListWithAssignment<TContext, TOutput> :
        MixedRadixEnumeratorWithAssignment<TContext>
    {
        #region Properties

        public List<TOutput> OutputList;

        #endregion

        #region Constructors

        protected MixedRadixToListWithAssignment(TContext context, IReadOnlyList<IList<string>> collection,
            IReadOnlyList<string> variableNameList) : base(context, collection, variableNameList)
        {
            OutputList = new List<TOutput>();
        }

        protected MixedRadixToListWithAssignment(TContext context, IReadOnlyList<IList<string>> collection,
            IReadOnlyList<string> variableNameList, StringDictionary assignment)
            : base(context, collection, variableNameList, assignment)
        {
            OutputList = new List<TOutput>();
        }

        #endregion
    }
}