using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public abstract class MixedRadixEnumeratorWithAssignment<TContext> : MixedRadixEnumerator<TContext>
    {
        //private StringDictionary _assignment;

        private IReadOnlyList<string> _variableNameList;

        #region Constructors

        protected MixedRadixEnumeratorWithAssignment(TContext context, IReadOnlyList<IList<string>> collection, I)
        {
            _context = context;
            _collection = collection;
            _scanArray = new string[CollectionCount];
        }

        #endregion

        #region Methods

        public virtual void Initial(int[] index)
        {
            Parallel.For(0, CollectionCount, i => _scanArray[i] = _collection[i][index[i]]);
        }

        public virtual void MoveNext(int j, int[] index)
        {
            Parallel.For(j, CollectionCount, i => _scanArray[i] = _collection[i][index[i]]);
        }

        public abstract void Action();

        #endregion
    }
}
