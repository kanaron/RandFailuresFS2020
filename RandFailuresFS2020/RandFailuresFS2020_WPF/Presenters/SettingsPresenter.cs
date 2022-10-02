using RandFailuresFS2020_WPF.Models;
using RandFailuresFS2020_WPF.Views;
using SimConModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class SettingsPresenter
    {
        public SettingsView settingsView { private set; get; }

        public SettingsModel settingsModel { private set; get; }

        public SettingsPresenter()
        {
            settingsView = new SettingsView();
            settingsView.SaveClicked += SettingsView_SaveClicked;

            settingsModel = new SettingsModel();

            settingsView.DataContext = settingsModel;
        }

        private void SettingsView_SaveClicked(object? sender, EventArgs e)
        {
            settingsModel.SavePreset();
        }
    }
}
