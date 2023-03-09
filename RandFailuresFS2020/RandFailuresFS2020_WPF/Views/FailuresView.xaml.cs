using System;
using System.Text.RegularExpressions;
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
        public event EventHandler? ApplyClicked;

        private static readonly Regex _regex = new("[^0-9]+");

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

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = int.TryParse((e.OriginalSource as TextBox)!.Text, out int result);
            if (result > 1000)
            {
                (e.OriginalSource as TextBox)!.Text = "1000";
            }
        }

        private void ApplyAllPercentage_Click(object sender, RoutedEventArgs e)
        {
            ApplyClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
