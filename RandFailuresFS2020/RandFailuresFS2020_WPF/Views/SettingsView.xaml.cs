using System;
using System.Text.RegularExpressions;
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
        public event EventHandler? PopupButtonClicked;
        private static readonly Regex _regex = new("[0-9]+");
        private string? oldValue;

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

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            oldValue = (e.OriginalSource as TextBox)!.Text;
            e.Handled = !_regex.IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int result = int.Parse((e.OriginalSource as TextBox)!.Text);
                if ((e.OriginalSource as TextBox)!.Tag.ToString() == "1")
                {
                    if (result > 1000)
                        (e.OriginalSource as TextBox)!.Text = "1000";
                    else if (result < 0)
                        (e.OriginalSource as TextBox)!.Text = "0";
                }
                else if ((e.OriginalSource as TextBox)!.Tag.ToString() == "2")
                {
                    if (result > 100000)
                        (e.OriginalSource as TextBox)!.Text = "100000";
                    else if (result < 0)
                        (e.OriginalSource as TextBox)!.Text = "0";
                }
                else if ((e.OriginalSource as TextBox)!.Tag.ToString() == "3")
                {
                    if (result > 43200)
                        (e.OriginalSource as TextBox)!.Text = "43200";
                    else if (result < 0)
                        (e.OriginalSource as TextBox)!.Text = "0";
                }
                else if ((e.OriginalSource as TextBox)!.Tag.ToString() == "4")
                {
                    if (result > 99)
                        (e.OriginalSource as TextBox)!.Text = "99";
                    else if (result < 1)
                        (e.OriginalSource as TextBox)!.Text = "1";
                }
            }
            catch
            {
                if ((e.OriginalSource as TextBox)!.Text != "-")
                    (e.OriginalSource as TextBox)!.Text = oldValue;
            }
            finally
            {
                oldValue = null;
            }
        }

        private void SettingsPopupButton_Click(object sender, RoutedEventArgs e)
        {
            PopupButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
