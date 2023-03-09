using System;
using System.Windows;
using System.Windows.Controls;

namespace RandFailuresFS2020_WPF.Views
{
    /// <summary>
    /// Interaction logic for PresetsView.xaml
    /// </summary>
    public partial class PresetsView : UserControl
    {
        public event EventHandler? DeletePreset;
        public event EventHandler<string>? FailuresClicked;
        public event EventHandler<string>? NewPreset;
        public event EventHandler? SettingsClicked;

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

        private void Flight_controls_Click(object sender, RoutedEventArgs e)
        {
            FailuresClicked?.Invoke(this, "Flight controls");
        }

        private void Gear_Click(object sender, RoutedEventArgs e)
        {
            FailuresClicked?.Invoke(this, "Gear");
        }

        private void Fuel_Click(object sender, RoutedEventArgs e)
        {
            FailuresClicked?.Invoke(this, "Fuel");
        }

        private void Panel_Click(object sender, RoutedEventArgs e)
        {
            FailuresClicked?.Invoke(this, "Panel");
        }
        private void Engine_Click(object sender, RoutedEventArgs e)
        {
            FailuresClicked?.Invoke(this, "Engine");
        }

        private void Brake_Click(object sender, RoutedEventArgs e)
        {
            FailuresClicked?.Invoke(this, "Brake");
        }

        private void Other_Click(object sender, RoutedEventArgs e)
        {
            FailuresClicked?.Invoke(this, "Other");
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
