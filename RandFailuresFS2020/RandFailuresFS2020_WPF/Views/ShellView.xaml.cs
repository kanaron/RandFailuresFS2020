using System;
using System.Windows;

namespace RandFailuresFS2020_WPF.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public event EventHandler? OverviewClick;
        public event EventHandler? PresetsClick;
        public event EventHandler? FailListClick;
        public event EventHandler? HelpClick;

        public ShellView()
        {
            InitializeComponent();
        }

        private void Overview_Click(object sender, RoutedEventArgs e)
        {
            OverviewClick?.Invoke(this, e);
        }

        private void Presets_Click(object sender, RoutedEventArgs e)
        {
            PresetsClick?.Invoke(this, e);
        }

        private void Fail_list_Click(object sender, RoutedEventArgs e)
        {
            FailListClick?.Invoke(this, e);
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpClick?.Invoke(this, e);
        }
    }
}
