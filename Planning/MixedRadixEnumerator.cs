using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public abstract class MixedRadixEnumerator<TContext> : IMixedRadixEnumerator
    {
        #region Fields

        protected TContext _context;
        
        private IReadOnlyList<IList<string>> _collection;

        protected string[] _scanArray;

        #endregion

        #region Properties

        public int CollectionCount
        {
            get { return _collection.Count; }
        }

        public IReadOnlyList<IList<string>> Collection
        {
            get { return _collection; }
        }

        #endregion

        #region Constructors

        protected MixedRadixEnumerator(TContext context, IReadOnlyList<IList<string>> collection)
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

        public abstract void Execute();

        #endregion
    }
}
