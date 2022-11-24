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
        public event EventHandler? RestartClicked;
        public event EventHandler? StartStopClicked;

        public OverviewView()
        {
            InitializeComponent();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            RestartClicked?.Invoke(this, e);
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            StartStopClicked?.Invoke(this, e);
        }
    }
}
