using RandFailuresFS2020_WPF.Views;
using SimConModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class ShellPresenter
    {
        private ShellView shellView;

        private OverviewPresenter overviewPresenter;
        private SettingsPresenter settingsPresenter;
        private PresetsPresenter presetsPresenter;
        private FailListPresenter failListPresenter;

        public ShellPresenter()
        {
            shellView = new ShellView();
            shellView.OverviewClick += ShellView_OverviewClick;
            shellView.SettingsClick += ShellView_SettingsClick;
            shellView.PresetsClick += ShellView_PresetsClick;
            shellView.FailListClick += ShellView_FailListClick;
            shellView.HelpClick += ShellView_HelpClick;

            overviewPresenter = new OverviewPresenter();
            settingsPresenter = new SettingsPresenter();
            presetsPresenter = new PresetsPresenter();
            failListPresenter = new FailListPresenter();


            shellView.ActiveItem.Content = overviewPresenter.overviewView;

            shellView.Show();

            SimCon.GetSimCon().SetHandle(new WindowInteropHelper(shellView).Handle);

            HwndSource lHwndSource = HwndSource.FromHwnd(new WindowInteropHelper(shellView).Handle);
            lHwndSource.AddHook(new HwndSourceHook(SimCon.GetSimCon().ProcessSimCon));
        }

        private void ShellView_OverviewClick(object? sender, EventArgs e)
        {
            overviewPresenter.Reload();
            shellView.ActiveItem.Content = overviewPresenter.overviewView;
        }

        private void ShellView_SettingsClick(object? sender, EventArgs e)
        {
            settingsPresenter.settingsModel.Reload();
            shellView.ActiveItem.Content = settingsPresenter.settingsView;
        }

        private void ShellView_PresetsClick(object? sender, EventArgs e)
        {
            presetsPresenter.Reload();
            shellView.ActiveItem.Content = presetsPresenter.presetsView;
        }

        private void ShellView_FailListClick(object? sender, EventArgs e)
        {
            shellView.ActiveItem.Content = failListPresenter.failListView;
        }

        private void ShellView_HelpClick(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
