using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AlgorithmVisualizer.Sorters
{
    class AsyncSorter<T> : ISorter<T>, ICancelable where T : IComparable
    {
        private ISorter<T> _sorterInternal;
        private Thread _sortingThread;

        public AsyncSorter(ISorter<T> _sorter)
        {
            _sorterInternal = _sorter;
        }

        #region ISorter<T> Members

        public void Sort(T[] data, ISwapStrategy<T> strategy)
        {
            _sortingThread = new Thread(delegate()
            {
                _sorterInternal.Sort(data, strategy);
            });
            _sortingThread.Start();
        }

        #endregion

        public void Cancel()
        {
            if (_sortingThread != null && _sortingThread.IsAlive)
                _sortingThread.Abort();
        }

        public bool IsRunning
        {
            get
            {
                if (_sortingThread != null && _sortingThread.IsAlive)
                    return true;

                return false;
            }
        }
    }
}
