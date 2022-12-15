using System;
using System.Windows;
using System.Windows.Controls;

namespace RandFailuresFS2020_WPF.Views
{
    /// <summary>
    /// Interaction logic for FailuresView.xaml
    /// </summary>
    public partial class FailuresView : UserControl
    {
        public event EventHandler<bool>? SaveVars;

        public FailuresView()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveVars?.Invoke(this, true);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SaveVars?.Invoke(this, false);
        }
    }
}
