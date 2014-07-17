using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Collections
{
    public abstract class MixedRadixToDictionaryWithAssignment<TContext, TOutput> :
        MixedRadixEnumeratorWithAssignment<TContext>
    {
        #region Properties

        public Dictionary<string, TOutput> OutputDictionary;

        #endregion

        #region Constructors

        protected MixedRadixToDictionaryWithAssignment(TContext context, IReadOnlyList<IList<string>> collection,
            IReadOnlyList<string> variableNameList) : base(context, collection, variableNameList)
        {
            OutputDictionary = new Dictionary<string, TOutput>();
        }

        protected MixedRadixToDictionaryWithAssignment(TContext context, IReadOnlyList<IList<string>> collection,
            IReadOnlyList<string> variableNameList, StringDictionary assignment)
            : base(context, collection, variableNameList, assignment)
        {
            OutputDictionary = new Dictionary<string, TOutput>();
        }

        #endregion
    }
}