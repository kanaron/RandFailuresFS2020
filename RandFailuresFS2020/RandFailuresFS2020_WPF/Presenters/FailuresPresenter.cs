using RandFailuresFS2020_WPF.Models;
using RandFailuresFS2020_WPF.Views;
using SimConModels;
using System;
using System.Collections.Generic;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class FailuresPresenter
    {
        public event EventHandler? CloseFailuresView;

        public FailuresView failuresView { private set; get; }

        public FailuresModel failuresModel { private set; get; }

        public FailuresPresenter(string _domain, List<SimVarModel> _simVarList, int _presetID)
        {
            failuresView = new();
            failuresView.SaveVars += FailuresView_SaveVars;
            failuresView.ApplyClicked += FailuresView_ApplyClicked;

            failuresModel = new(_domain, _simVarList, _presetID);
            failuresView.DataContext = failuresModel;
        }

        private void FailuresView_ApplyClicked(object? sender, EventArgs e)
        {
            failuresModel.SettAllPercentage();
        }

        private void FailuresView_SaveVars(object? sender, bool e)
        {
            if (e == true)
                failuresModel.SaveVarsInPreset();

            CloseFailuresView?.Invoke(this, EventArgs.Empty);
        }
    }
}
