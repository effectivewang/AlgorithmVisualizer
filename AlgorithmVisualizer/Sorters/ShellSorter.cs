using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmVisualizer.Sorters
{
  class ShellSorter<T>: ISorter<T> where T : IComparable<T>, IComparable
  {
    #region ISorter<double> Members

    public void Sort(T[] data, ISwapStrategy<T> strategy)
    {
      if (data == null)
        throw new ArgumentNullException();

      int maxDist = 1;
      while (maxDist < (data.Length / 3)) maxDist = 3 * maxDist + 1;

      for (int d = maxDist; d > 0; d = d / 3)
      {
        for (int i = d; i < data.Length; i = i + d) {
          int index = i;
          for (int j = i - d; j >= 0; j = j - d) {
            if (data[index].CompareTo(data[j]) >= 0) {
              break;
            }

            strategy.Swap(data, index, j);
            index = j;
          }
        }
      }
    }
    
    #endregion
  }
}
