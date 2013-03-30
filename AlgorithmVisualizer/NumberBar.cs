using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace AlgorithmVisualizer
{
    class NumberBar : StackPanel
    {
        public Rectangle Rect { get; private set; }

        Label textLabel;

        public NumberBar(double width, double height, string content)
        {
            this.SizeChanged += new System.Windows.SizeChangedEventHandler(NumberBar_SizeChanged);
            this.Margin = new System.Windows.Thickness(1, 0, 1, 0);

            Rect = new Rectangle();
            Orientation = Orientation.Vertical;
            HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

             textLabel = new Label();
            textLabel.Content = content;
            textLabel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            Rect.Width = width;
            Rect.Height = height;

            Rect.Fill = Brushes.OrangeRed;

            Rect.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            Children.Add(textLabel);
            Children.Add(Rect);
        }

        void NumberBar_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            Rect.Height = e.NewSize.Height - textLabel.ActualHeight;
        }

        public void Mark(bool marked)
        {
            if (marked)
                Rect.Fill = Brushes.Orange;
            else
                Rect.Fill = Brushes.OrangeRed;
        }
    }
}
