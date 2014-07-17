using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Collections
{
    public abstract class MixedRadixToList<TContext, TOutput> : MixedRadixEnumerator<TContext>
    {
        #region Properties

        public List<TOutput> OutputList;

        #endregion

        #region Constructors

        protected MixedRadixToList(TContext context, IReadOnlyList<IList<string>> collection)
            : base(context, collection)
        {
            OutputList = new List<TOutput>();
        }

        #endregion
    }
}
