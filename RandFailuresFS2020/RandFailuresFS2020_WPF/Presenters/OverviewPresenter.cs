using RandFailuresFS2020_WPF.Models;
using RandFailuresFS2020_WPF.Views;
using SimConModels;
using System;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class OverviewPresenter
    {
        public OverviewView OverviewView { private set; get; }
        public OverviewModel OverviewModel { private set; get; }

        public OverviewPresenter()
        {
            OverviewView = new OverviewView();
            OverviewView.RestartClicked += OverviewView_RestartClicked;
            OverviewView.StartClicked += OverviewView_StartClicked;
            OverviewView.StopClicked += OverviewView_StopClicked;

            OverviewModel = new OverviewModel();

            OverviewView.DataContext = OverviewModel;
        }

        public void Reload()
        {
            OverviewModel.Reload();
        }

        private void OverviewView_StartClicked(object? sender, EventArgs e)
        {
            SimConHelper.GetSimConHelper().ManageFailTimer(true);
        }

        private void OverviewView_StopClicked(object? sender, EventArgs e)
        {
            SimConHelper.GetSimConHelper().ManageFailTimer(false);
        }

        private void OverviewView_RestartClicked(object? sender, EventArgs e)
        {
            SimConHelper.GetSimConHelper().SimConnectClosed();
        }
    }
}
