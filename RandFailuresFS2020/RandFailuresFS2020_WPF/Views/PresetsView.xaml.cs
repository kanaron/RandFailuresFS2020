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
    /// Interaction logic for PresetsView.xaml
    /// </summary>
    public partial class PresetsView : UserControl
    {
        public event EventHandler? DeletePreset;
        public event EventHandler<string>? NewPreset;
        public event EventHandler<bool>? SaveVars;

        public PresetsView()
        {
            InitializeComponent();
            DeletePopup.IsOpen = false;
            NewPopup.IsOpen = false;
        }

        private void DeleteYesButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePreset?.Invoke(this, EventArgs.Empty);
            DeletePopup.IsOpen = false;
        }

        private void DeleteNoButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePopup.IsOpen = false;
        }

        private void DeletePresetButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePopup.IsOpen = true;
        }

        private void NewPresetButton_Click(object sender, RoutedEventArgs e)
        {
            NewPopup.IsOpen = true;
            NewPresetText.Text = "";
        }

        private void NewCancelButton_Click(object sender, RoutedEventArgs e)
        {
            NewPopup.IsOpen = false;
        }

        private void NewSaveButton_Click(object sender, RoutedEventArgs e)
        {
            NewPopup.IsOpen = false;
            NewPreset?.Invoke(this, NewPresetText.Text);
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
