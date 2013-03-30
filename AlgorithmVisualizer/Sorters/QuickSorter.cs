using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmVisualizer.Sorters
{
  class QuickSorter<T> : ISorter<T> where T : IComparable<T>, IComparable
  {
    #region ISorter<T> Members

    public void Sort(T[] data, ISwapStrategy<T> strategy)
    {
      if (data == null)
        throw new ArgumentNullException();

      for (int i = 0; i < data.Length; i++)
      {
        for (int j = i; j < data.Length; j++)
        {
          if (((IComparable<T>)data[i]).CompareTo(data[j]) > 0)
          {
            strategy.Swap(data, i, j);
          }
        }
      }
    }

    #endregion
  }
}
