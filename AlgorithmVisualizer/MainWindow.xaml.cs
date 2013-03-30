using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using AlgorithmVisualizer.Sorters;

namespace AlgorithmVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private class SorterComboItem<T> : ComboBoxItem where T : IComparable<T>, IComparable
        {
            public ISorter<T> Sorter { get; set; }

            public SorterComboItem(string content, ISorter<T> sorter)
            {
                Content = content;
                Sorter = sorter;
            }
        }

        private ISorter<double> _sorter;
        private IVisualizationStrategy<double> _visualizer;

        private double[] data = null;

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            this.SizeChanged += new SizeChangedEventHandler(MainWindow_Loaded);

            FillAlgorithms();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded == false) return;

            GenerateData();
        }

        private void FillAlgorithms()
        {

            AlgorithmComboBox.Items.Add(new SorterComboItem<double>
            (
              "Bubble Sort",
              SorterFactory.CreateSorter<BubbleSorter<double>, double>()
            ));
            AlgorithmComboBox.Items.Add(new SorterComboItem<double>
            (
              "Selection Sort",
              SorterFactory.CreateSorter<SelectionSorter<double>, double>()
            ));
            AlgorithmComboBox.Items.Add(new SorterComboItem<double>
            (
              "Insertion Sort",
              SorterFactory.CreateSorter<InsertionSorter<double>, double>()
            ));
            AlgorithmComboBox.Items.Add(new SorterComboItem<double>
            (
              "Shell Sort",
              SorterFactory.CreateSorter<ShellSorter<double>, double>()
            ));
            AlgorithmComboBox.Items.Add(new SorterComboItem<double>
            (
              "Merge Sort",
              SorterFactory.CreateSorter<MergerSorter<double>, double>()
            ));
            AlgorithmComboBox.Items.Add(new SorterComboItem<double>
            (
              "Quick Sort",
              SorterFactory.CreateSorter<QuickSorter<double>, double>()
            ));

            AlgorithmComboBox.SelectedIndex = 0;
        }

        private void AlgorithmComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_sorter != null)
            {
                ICancelable cancelable = _sorter as ICancelable;
                if (cancelable.IsRunning)
                    SortButton.IsEnabled = false;
            }

            SorterComboItem<double> item = AlgorithmComboBox.SelectedItem as SorterComboItem<double>;
            _sorter = item.Sorter;
        }

        private void GenerateData()
        {
            data = GetData();

            ICancelable cancelable = _sorter as ICancelable;
            if (cancelable != null)
                cancelable.Cancel();

            // Set the acutal size to canvas, it will be used for children layout.
            RowDefinition row = grdContainer.RowDefinitions[Grid.GetRow(SortingCanvas)];
            SortingCanvas.Width = grdContainer.ActualWidth;
            SortingCanvas.Height = row.ActualHeight;

            _visualizer = new SorintCanvasVisualization(SortingCanvas, _sorter);
            _visualizer.Visualize(data);

            DisplayData(data);

            SortButton.IsEnabled = true;
        }


        private double[] GetData()
        {
            const int SIZE = 50;

            double[] data = new double[SIZE];
            Random rand = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < SIZE; i++)
            {
                data[i] = rand.Next(10, 100);
            }

            return data;
        }

        private void DisplayData(double[] data)
        {
            StringBuilder sbOutput = new StringBuilder();
            sbOutput.Append("Data: ");

            for (int i = 0; i < data.Length; i++)
            {
                if (i == data.Length - 1)
                {
                    sbOutput.Append(data[i]);
                }
                else
                {
                    sbOutput.AppendFormat("{0},", data[i]);
                }
            }

            //DataTextBlock.Text = sbOutput.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _sorter.Sort(data, _visualizer as ISwapStrategy<double>);
        }

        private void RandButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateData();
        }

    }
}