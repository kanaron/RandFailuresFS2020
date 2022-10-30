using SimConModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RandFailuresFS2020_WPF.Views
{
    /// <summary>
    /// Interaction logic for OverviewView.xaml
    /// </summary>
    public partial class OverviewView : UserControl
    {
        public event EventHandler RestartClicked;
        public event EventHandler StartClicked;
        public event EventHandler StopClicked;

        public OverviewView()
        {
            InitializeComponent();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            RestartClicked?.Invoke(this, e);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            StartClicked?.Invoke(this, e);
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            StopClicked?.Invoke(this, e);
        }
    }
}
