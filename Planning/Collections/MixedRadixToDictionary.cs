using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Collections
{
    public abstract class MixedRadixToDictionary<TContext, TOutput> : MixedRadixEnumerator<TContext>
    {
        #region Properties

        public Dictionary<string, TOutput> OutputDictionary;

        #endregion

        #region Constructors

        protected MixedRadixToDictionary(TContext context, IReadOnlyList<IList<string>> collection)
            : base(context, collection)
        {
            OutputDictionary = new Dictionary<string, TOutput>();
        }

        #endregion
    }
}
