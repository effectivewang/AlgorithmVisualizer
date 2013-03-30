using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AlgorithmVisualizer.Sorters
{
    class MergerSorter<T> : ISorter<T> where T : IComparable<T>, IComparable
    {
        private ISwapStrategy<T> _strategy;
        private T[] _copy;

        #region ISorter<double> Members

        public void Sort(T[] data, ISwapStrategy<T> strategy)
        {
            if (data == null)
                throw new ArgumentNullException();

            _strategy = strategy;
            _copy = new T[data.Length];

            SortTopDown(data, 0, data.Length / 2, data.Length - 1);

            //SortBottomUp(data);
        }

        private void SortBottomUp(T[] data)
        {
            for (int dist = 1; dist < data.Length; dist = dist + dist)
            {
                for (int i = 0; i < data.Length - dist; i = i + dist + dist) {
                    int low = i;
                    int mid = i + dist - 1;
                    int hi = Math.Min(i + dist + dist - 1, data.Length - 1);

                    MergeBottomUp(data, low, mid, hi);
                }
            }
        }

        private void MergeBottomUp(T[] data, int low, int mid, int hi)
        {
            int index = low;
            int j = mid + 1;

            while (index <= hi) {
                if (data[index].CompareTo(data[j]) > 0)
                { 
                }

                index++;
            }
        }

        private void SortTopDown(T[] data, int low, int mid, int hi)
        {
            if (hi == low) return;

            SortTopDown(data, low, (mid + low) / 2, mid);
            SortTopDown(data, mid + 1, (mid + hi) / 2, hi);

            MergeTopDown(data, low, mid, hi);
        }

        /// <summary>
        /// Merge from top to down
        /// </summary>
        /// <param name="data"></param>
        /// <param name="low"></param>
        /// <param name="mid"></param>
        /// <param name="hi"></param>
        private void MergeTopDown(T[] data, int low, int mid, int hi)
        {
            int i = 0;
            for (i = low; i <= hi; i++)
            {
                _copy[i] = data[i];
            }

            int j = mid + 1;
            i = low;

            for (int k = low; k <= hi; k++)
            {
                if (j <= hi && _copy[j].CompareTo(_copy[i]) < 0)
                {
                    _strategy.Swap(data, _copy, j++, k);
                    // data[k] = _copy[j++];
                }
                else if (i <= mid)
                {
                    _strategy.Swap(data, _copy, i++, k);
                    // data[k] = _copy[i++];
                }
            }
        }

        #endregion
    }
}
