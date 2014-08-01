using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Collections
{
    public abstract class MixedRadixEnumeratroWithIndexArray: IMixedRadixEnumerator
    {
        #region Fields
        
        private IReadOnlyList<IList<string>> _collection;

        protected string[] _scanArray;

        protected int[] _index;

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

        protected MixedRadixEnumeratroWithIndexArray(IReadOnlyList<IList<string>> collection)
        {
            _collection = collection;
            _scanArray = new string[_collection.Count];
            _index = new int[_collection.Count];
        }

        #endregion

        #region Methods

        public virtual void Initial(int[] index)
        {
            Parallel.For(0, CollectionCount, i =>
            {
                _index[i] = index[i];
                _scanArray[i] = _collection[i][index[i]];
            });
        }

        public virtual void MoveNext(int j, int[] index)
        {
            Parallel.For(j, CollectionCount, i =>
            {
                _index[i] = index[i];
                _scanArray[i] = _collection[i][index[i]];
            });
        }

        public abstract void Execute();

        #endregion
    }
}
