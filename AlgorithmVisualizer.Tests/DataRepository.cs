using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmVisualizer.Tests
{
  public class DataRepository
  {
    public static DataRepository Instance = new DataRepository();

    public DataRepository()
    {
    }

    public double[] GetRandDouble(int count)
    {
      Assert.IsTrue(count > 0);
      double[] list = new double[count];

      Random rand = new Random((int)DateTime.Now.Ticks);
      for (int i = 0; i < count; i++)
      {
        list[i] = rand.NextDouble();
      }

      return list;
    }
  }
}
