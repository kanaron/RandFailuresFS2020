using RandFailuresFS2020_WPF.Models;
using RandFailuresFS2020_WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class PresetsPresenter
    {
        public PresetsView presetsView { private set; get; }

        public PresetsModel presetsModel { private set; get; }

        public PresetsPresenter()
        {
            presetsView = new PresetsView();
            presetsView.DeletePreset += PresetsView_DeletePreset;
            presetsView.NewPreset += PresetsView_NewPreset;
            presetsView.SaveVars += PresetsView_SaveVars;

            presetsModel = new PresetsModel();

            presetsView.DataContext = presetsModel;
        }

        private void PresetsView_SaveVars(object? sender, bool e)
        {
            if (e == false)
            {
                presetsModel.LoadVarsList();
            }
            else
            {
                presetsModel.SaveVarsInPreset();
            }
        }

        private void PresetsView_NewPreset(object? sender, string e)
        {
            presetsModel.NewPreset(e);
        }

        private void PresetsView_DeletePreset(object? sender, EventArgs e)
        {
            presetsModel.DeletePreset();
        }

        public void Reload()
        {
            presetsModel.Reload();
        }
    }
}
