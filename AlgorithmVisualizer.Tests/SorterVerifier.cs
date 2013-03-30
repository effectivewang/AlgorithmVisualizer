using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmVisualizer.Tests
{
  class SorterVerifier
  {
    public static bool IsAssending(IComparable[] array) {
      Assert.IsNotNull(array);

      if(array.Length < 2) return true;
      for (int i = 0; i < array.Length - 1; i++) {
        if (array[i].CompareTo(array[i+1]) >= 0) {
          return false;
        }
      }

      return true;
    }

    public static bool IsAssending<T>(T[] array) where T : IComparable
    {
      Assert.IsNotNull(array);

      if (array.Length < 2) return true;
      for (int i = 0; i < array.Length - 1; i++)
      {
        if (array[i].CompareTo(array[i + 1]) >= 0)
        {
          return false;
        }
      }

      return true;
    }
  }
}
