using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmVisualizer.Sorters;
using System.Collections.Concurrent;

namespace AlgorithmVisualizer.Tests
{
  [TestClass]
  public class SorterTest
  {
    internal ISorter<double> Sorter { get; private set; }
    public TestContext TestContext { get; set; }

    public SorterTest()
    {
      Sorter = new MergerSorter<double>();
    }

    internal SorterTest(ISorter<double> sorter)
    {
      Sorter = sorter;
    }

    [TestMethod]
    public void when_noorder_array_then_return_ordered_array()
    {
      const int COUNT = 100;

      for (int i = 1; i < 3; i++)
      {
        string msg = string.Format("When array count is {0}, sorter did not sort correctly.", COUNT * i);

        double[] data = DataRepository.Instance.GetRandDouble(COUNT * i);
        Sorter.Sort(data, new SimpleSwapStrategy<double>());

        Assert.IsTrue(SorterVerifier.IsAssending(data), msg);
      }
    }

  }
}
