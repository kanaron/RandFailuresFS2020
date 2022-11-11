using System;
using System.Windows;
using System.Windows.Controls;

namespace RandFailuresFS2020_WPF.Views
{
    /// <summary>
    /// Interaction logic for FailListView.xaml
    /// </summary>
    public partial class FailListView : UserControl
    {
        public event EventHandler? ShowFailuresClicked;

        public FailListView()
        {
            InitializeComponent();
        }

        private void ShowFailures_Click(object sender, RoutedEventArgs e)
        {
            ShowFailuresClicked?.Invoke(this, e);
        }
    }
}
