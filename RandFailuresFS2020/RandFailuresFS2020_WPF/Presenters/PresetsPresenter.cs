using RandFailuresFS2020_WPF.Models;
using RandFailuresFS2020_WPF.Views;
using System;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class PresetsPresenter
    {
        public PresetsView PresetsView { private set; get; }

        public PresetsModel PresetsModel { private set; get; }

        public PresetsPresenter()
        {
            PresetsView = new PresetsView();
            PresetsView.DeletePreset += PresetsView_DeletePreset;
            PresetsView.NewPreset += PresetsView_NewPreset;

            PresetsModel = new PresetsModel();

            PresetsView.DataContext = PresetsModel;
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
