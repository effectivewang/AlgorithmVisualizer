using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AlgorithmVisualizer
{
    /// <summary>
    /// Interaction logic for LabWindow.xaml
    /// </summary>
    public partial class LabWindow : Window
    {
        public LabWindow()
        {
            InitializeComponent();

            ParameterPass pass = new ParameterPass();
            OutputLabel.Content = pass.TestArrayPass();
        }
    }

    class ParameterPass
    {
        private ParameterPass _instance;
        private double[] _data;
        
        public bool TestReferencePass() {
            _instance = new ParameterPass();
            return ReferencePass(_instance);
        }

        public bool ReferencePass(ParameterPass param) {
            return ReferenceEquals(_instance, param);
        }

        public bool TestArrayPass() {
            _data = new double[5] { 1, 2, 3, 4, 5 };

            return ArrayPass(_data);
        }

        private bool ArrayPass(double[] param) {
            return ReferenceEquals(_data, param);
        }
    }
}
