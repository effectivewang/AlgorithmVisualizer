using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmVisualizer.Sorters
{
  class SelectionSorter<T> : ISorter<T> where T : IComparable<T>, IComparable
  {
    #region ISorter<double> Members

    public void Sort(T[] data, ISwapStrategy<T> strategy)
    {
      if (data == null)
        throw new ArgumentNullException();

      for (int i = 0; i < data.Length; i++)
      {
        int min = i;
        for (int j = i; j < data.Length; j++)
        {
          if (((IComparable<T>)data[j]).CompareTo((data[min])) < 0)
          {
            min = j;
          }
        }

        strategy.Swap(data, i, min);
      }
    }

    #endregion
  }
}
