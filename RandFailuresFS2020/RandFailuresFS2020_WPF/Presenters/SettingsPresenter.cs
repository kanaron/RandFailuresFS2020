using RandFailuresFS2020_WPF.Models;
using RandFailuresFS2020_WPF.Views;
using System;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class SettingsPresenter
    {
        public SettingsView SettingsView { private set; get; }

        public SettingsModel SettingsModel { private set; get; }

        public SettingsPresenter()
        {
            SettingsView = new SettingsView();
            SettingsView.SaveClicked += SettingsView_SaveClicked;
            SettingsView.CancelClicked += SettingsView_CancelClicked;

            SettingsModel = new SettingsModel();

            SettingsView.DataContext = SettingsModel;
        }

        private void SettingsView_CancelClicked(object? sender, EventArgs e)
        {
            SettingsModel.Reload();
        }

        private void SettingsView_SaveClicked(object? sender, EventArgs e)
        {
            SettingsModel.SavePreset();
        }
    }
}
