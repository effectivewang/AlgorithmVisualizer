using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading;

namespace AlgorithmVisualizer
{
    interface ISorter<T> where T : IComparable
    {
        void Sort(T[] data, ISwapStrategy<T> strategy);
    }

    interface ISwapStrategy<T> where T : IComparable
    {
        void Swap(T[] array, int sourceIndex, int targetIndex);
        void Swap(T[] array, T[] temp, int tempIndex, int arrayIndex);
    }

    interface IVisualizationStrategy<T> where T : IComparable
    {
      /// <summary>
      /// Visualize the value to target UIElement
      /// </summary>
      /// <param name="obj">value</param>
      /// <returns></returns>
      void Visualize(T[] array);
    }

    interface ICancelable
    {
        void Cancel();
        bool IsRunning { get; }
    }
}
