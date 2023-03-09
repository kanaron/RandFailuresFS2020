using RandFailuresFS2020_WPF.Models;
using RandFailuresFS2020_WPF.Views;
using System;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class PresetsPresenter
    {
        public event EventHandler<int>? SettingsOpen;

        public PresetsView PresetsView { private set; get; }

        public PresetsModel PresetsModel { private set; get; }

        public PresetsPresenter()
        {
            PresetsView = new PresetsView();
            PresetsView.DeletePreset += PresetsView_DeletePreset;
            PresetsView.NewPreset += PresetsView_NewPreset;
            PresetsView.SettingsClicked += PresetsView_SettingsClicked;

            PresetsModel = new PresetsModel();

            PresetsView.DataContext = PresetsModel;
        }

        private void PresetsView_SettingsClicked(object? sender, EventArgs e)
        {
            SettingsOpen?.Invoke(this, PresetsModel.SelectedPreset.PresetID);
        }

        private void PresetsView_NewPreset(object? sender, string e)
        {
            PresetsModel.NewPreset(e);
        }

        private void PresetsView_DeletePreset(object? sender, EventArgs e)
        {
            PresetsModel.DeletePreset();
        }

        public void Reload()
        {
            PresetsModel.Reload();
        }
    }
}
