using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Collections
{
    public static class Algorithms
    {
        public static void IterativeScanMixedRadix(IMixedRadixEnumerator enumerator)
        {
            int count = enumerator.CollectionCount;
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i => maxIndex[i] = enumerator.Collection[i].Count);

            enumerator.Initial(index);

            do
            {
                enumerator.Execute();

                int j = count - 1;
                while (j != -1)
                {
                    if (index[j] == maxIndex[j] - 1)
                    {
                        index[j] = 0;
                        j--;
                        continue;
                    }
                    break;
                }

                if (j == -1)
                    return;

                index[j]++;

                enumerator.MoveNext(j, index);
            } while (true);

        }
    }
}
