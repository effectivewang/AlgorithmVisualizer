using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmVisualizer.Sorters
{
    sealed class SorterFactory
    {
        public static ISorter<T> CreateSorter<TSorter,T>() 
            where T: IComparable
            where TSorter : ISorter<T>
        {
           ISorter<T> sorter = Activator.CreateInstance<TSorter>() ;
           AsyncSorter<T> asyncSorter = new AsyncSorter<T>(sorter);

           return asyncSorter;
        }
    }
}
