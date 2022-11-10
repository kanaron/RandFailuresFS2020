using System;
using System.Windows;
using System.Windows.Controls;

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
