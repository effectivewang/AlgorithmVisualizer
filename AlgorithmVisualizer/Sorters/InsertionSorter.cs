using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmVisualizer.Sorters
{
  class InsertionSorter<T>: ISorter<T> where T : IComparable
  {
    #region ISorter<double> Members

    public void Sort(T[] data, ISwapStrategy<T> strategy)
    {
      for (int i = 0; i < data.Length; i++)
      {
        int index = i;
        for (int j = i-1; j >= 0; j-- ) {

          if (data[index].CompareTo(data[j]) >= 0) // If data[i] is greate then or equals the previous one, then it is in order
          {
            break;
          }

          // Else, swap the data[i] with the previous one,until it is greater than the previous one.
          strategy.Swap(data, index, j);
          index = j;
        }
      }
    }

    #endregion
  }
}
