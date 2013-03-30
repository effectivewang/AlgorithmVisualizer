using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace AlgorithmVisualizer
{
  class SimpleSwapStrategy<T> : ISwapStrategy<T> where T : IComparable, IComparable<T>
  {
    #region ISwapStrategy<T> Members
    
    public void Swap(T[] data, int sourceIndex, int targetIndex)
    {
        Contract.Ensures(data == null || sourceIndex < 0 || sourceIndex > data.Length - 1 || targetIndex < 0 || targetIndex > data.Length - 1,
            "Data is null, or sourceIndex or targetIndex is out of range.");

      T temp = data[sourceIndex];
      data[sourceIndex] = data[targetIndex];
      data[targetIndex] = temp;
    }

    #endregion


    public void Swap(T[] array, T[] temp, int tempIndex, int arrayIndex)
    {
        Contract.Ensures(array != null && temp != null);
        Contract.Ensures(tempIndex >= 0 && tempIndex < temp.Length);
        Contract.Ensures(arrayIndex >= 0 && arrayIndex < array.Length);

        array[arrayIndex] = temp[tempIndex];
    }
  }
}
