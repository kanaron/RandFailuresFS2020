using System;
using System.Windows;
using System.Windows.Controls;

namespace RandFailuresFS2020_WPF.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public event EventHandler? SaveClicked;
        public event EventHandler? CancelClicked;
        public SettingsView()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveClicked?.Invoke(this, EventArgs.Empty);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
