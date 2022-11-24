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
            OverviewView.StartStopClicked += OverviewView_StartStopClicked;

            OverviewModel = new OverviewModel();

            OverviewView.DataContext = OverviewModel;
        }

        private void OverviewView_StartStopClicked(object? sender, EventArgs e)
        {
            OverviewModel.StartStopClicked();
        }

        public void Reload()
        {
            OverviewModel.Reload();
        }

        private void OverviewView_RestartClicked(object? sender, EventArgs e)
        {
            SimConHelper.GetSimConHelper().SimConnectClosed();
        }
    }
}
