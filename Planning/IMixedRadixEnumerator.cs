using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public interface IMixedRadixEnumerator
    {
        IReadOnlyList<IList<string>> Collection { get; }

        int CollectionCount { get; }

        void Initial(int[] index);

        void MoveNext(int j, int[] index);

        void Execute();
    }
}
