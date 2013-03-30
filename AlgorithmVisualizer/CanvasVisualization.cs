using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Diagnostics.Contracts;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;

namespace AlgorithmVisualizer
{
    class SorintCanvasVisualization : ISwapStrategy<double>, IVisualizationStrategy<double>
    {
        const int PADDING_TOP = 5;

        // Layout caculataion.
        double[] _data;
        double maxValue;
        double rectWidth;
        Dictionary<int, NumberBar> _element;

        private Canvas _canvas;
        private ISorter<double> _sorter;

        AnimationClock anLock;
        AnimationClock srcLock;

        public SorintCanvasVisualization(Canvas canvas, ISorter<double> sorter)
        {
            Contract.Ensures(canvas != null, "canvas can not be null.");
            Contract.Ensures(sorter != null, "sorter can not be null.");

            _canvas = canvas;
            _sorter = sorter;
            _element = new Dictionary<int, NumberBar>();
        }

        #region IUIVisualization<T> Members

        public void Visualize(double[] array)
        {
            Contract.Ensures(array != null && array.Length > 0, "array can be not null and empty.");

            //  Display the value as rectangle.
            // ================================================================================
            //  _   _  |_| |_|  _ _|_|_ _  |_|                   _ _ _ _ _ _ _|_|_|_|_|
            // |_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|      =>       _ _|_|_|_|_|_|_|_|_|_|_|_|
            // |_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|              |_|_|_|_|_|_|_|_|_|_|_|_|_|  
            // ================================================================================

            _data = array;
            _canvas.Children.Clear();

            // get the max value of array.
            for (int i = 0; i < array.Length; i++)
            {
                if (maxValue.CompareTo(array[i]) < 0)
                    maxValue = array[i];
            }

            rectWidth = _canvas.Width / array.Length; // with is average.
            // draw the ui elements.
            for (int i = 0; i < array.Length; i++)
            {
                double height = _canvas.Height * (array[i] / maxValue) - PADDING_TOP;

                NumberBar bar = new NumberBar(rectWidth, height, array[i].ToString());
                bar.Height = height;

                _canvas.Children.Add(bar);

                Canvas.SetLeft(bar, i * rectWidth);
                Canvas.SetBottom(bar, 0);

                _element.Add(i, bar);
            }
        }

        #endregion


        public void Swap(double[] data, int i, int j)
        {
            ManualResetEventSlim reset = new ManualResetEventSlim(false);
            double timeline = 500;

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                // Change the position
                NumberBar srcEl = _element[i];
                NumberBar targetEl = _element[j];

                double sourceLeft = Canvas.GetLeft(srcEl);
                double targetLeft = Canvas.GetLeft(targetEl);

                double oldSourceLeft = sourceLeft;
                double oldTargetLeft = targetLeft;

                double temp = targetLeft;
                targetLeft = sourceLeft;
                sourceLeft = temp;

                DoubleAnimation srcAnimation = new DoubleAnimation(oldSourceLeft, sourceLeft, new Duration(TimeSpan.FromMilliseconds(timeline)));
                DoubleAnimation targetAnimation = new DoubleAnimation(oldTargetLeft, targetLeft, new Duration(TimeSpan.FromMilliseconds(timeline)));

                anLock = targetAnimation.CreateClock();
                srcLock = srcAnimation.CreateClock();

                targetEl.Mark(true);
                srcEl.Mark(true);

                srcEl.BeginAnimation(Canvas.LeftProperty, srcAnimation);
                targetEl.BeginAnimation(Canvas.LeftProperty, targetAnimation);

                EventHandler lockHander = (s, e) =>
                {
                    if (srcLock.CurrentProgress == 1 && anLock.CurrentProgress == 1
                        && reset.IsSet == false)
                    {
                        targetEl.Mark(false);
                        srcEl.Mark(false);

                        lock (_element)
                        {
                            // swap UI element
                            NumberBar tempBar = _element[i];
                            _element[i] = _element[j];
                            _element[j] = tempBar;

                            Debug.WriteLine(string.Format("Swap: {0}, {1}, Value: {2}, {3}, Data: {4}", i, j, _data[i], _data[j], String.Join(" | ", _data)));

                            // swap data
                            temp = _data[i];
                            _data[i] = _data[j];
                            _data[j] = temp;

                            reset.Set();
                        }
                    }
                };

                srcLock.Completed += lockHander;
                anLock.Completed += lockHander;

            }), DispatcherPriority.Render);

            reset.Wait();
        }

        public void Swap(double[] array, double[] temp, int tempIndex, int arrayIndex)
        {
            ManualResetEventSlim reset = new ManualResetEventSlim(false);
            double timeline = 500;

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                array[arrayIndex] = temp[tempIndex];

                NumberBar oldBar = _element[arrayIndex];
                double oldHeight = oldBar.Height;

                _canvas.Children.Remove(oldBar);

                double newHeight = _canvas.Height * (array[arrayIndex] / maxValue) - PADDING_TOP;
                NumberBar newBar = new NumberBar(rectWidth, newHeight, array[arrayIndex].ToString());
                _element[arrayIndex] = newBar;

                newBar.Mark(true);
                DoubleAnimation heightAnimation = new DoubleAnimation(oldHeight, newHeight, new Duration(TimeSpan.FromMilliseconds(timeline)));
                _canvas.Children.Insert(arrayIndex, newBar);

                Canvas.SetLeft(newBar, arrayIndex * rectWidth);
                Canvas.SetBottom(newBar, 0);


                srcLock = heightAnimation.CreateClock();
                newBar.BeginAnimation(NumberBar.HeightProperty, heightAnimation);

                srcLock.Completed += (s, e) =>
                    {
                        newBar.Mark(false);
                        reset.Set();
                    };

            }), DispatcherPriority.Render);

            reset.Wait();
        }
    }
}
