using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Collections
{
    public abstract class MixedRadixEnumeratorWithAssignment<TContext> : MixedRadixEnumerator<TContext>
    {
        #region Fields

        protected StringDictionary _assignment;

        private IReadOnlyList<string> _variableNameList;

        #endregion;

        #region Constructors

        protected MixedRadixEnumeratorWithAssignment(TContext context, IReadOnlyList<IList<string>> collection, IReadOnlyList<string> variableNameList)
            : base(context, collection)
        {
            _assignment = new StringDictionary();
            _context = context;
            _variableNameList = variableNameList;
        }

        protected MixedRadixEnumeratorWithAssignment(TContext context, IReadOnlyList<IList<string>> collection, IReadOnlyList<string> variableNameList, StringDictionary assignment)
            : base(context, collection)
        {
            _assignment = assignment;
            _context = context;
            _variableNameList = variableNameList;
        }

        #endregion

        #region Methods

        public override void Initial(int[] index)
        {
            base.Initial(index);
            for (int i = 0; i < CollectionCount; i++)
            {
                string value = Collection[i][index[i]];
                _assignment[_variableNameList[i]] = value;
            }
        }

        public override void MoveNext(int j, int[] index)
        {
            base.MoveNext(j, index);
            for (int i = j; i < CollectionCount; i++)
            {
                _assignment[_variableNameList[i]] = _scanArray[i];

            }
        }

        #endregion
    }
}
