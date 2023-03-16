using RandFailuresFS2020_WPF.Models;
using RandFailuresFS2020_WPF.Views;
using SimConModels;
using System;
using System.Windows.Interop;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class ShellPresenter
    {
        private ShellView ShellView { set; get; }
        public ShellModel ShellModel { private set; get; }

        private readonly OverviewPresenter overviewPresenter;
        private readonly PresetsPresenter presetsPresenter;
        private readonly FailListPresenter failListPresenter;
        private readonly HelpPresenter helpPresenter;
        private SettingsPresenter? settingsPresenter;
        private FailuresPresenter? failuresPresenter;

        public ShellPresenter()
        {
            ShellView = new ShellView();
            ShellView.OverviewClick += ShellView_OverviewClick;
            ShellView.PresetsClick += ShellView_PresetsClick;
            ShellView.FailListClick += ShellView_FailListClick;
            ShellView.HelpClick += ShellView_HelpClick;

            ShellModel = new ShellModel();

            ShellView.DataContext = ShellModel;

            overviewPresenter = new OverviewPresenter();
            presetsPresenter = new PresetsPresenter();
            failListPresenter = new FailListPresenter();
            helpPresenter = new HelpPresenter();

            presetsPresenter.PresetsView.FailuresClicked += PresetsView_FailuresClicked;
            presetsPresenter.SettingsOpen += PresetsPresenter_SettingsOpen;

            ShellView.ActiveItem.Content = overviewPresenter.OverviewView;

            ShellView.Show();

            SimCon.GetSimCon().SetHandle(new WindowInteropHelper(ShellView).Handle);

            HwndSource lHwndSource = HwndSource.FromHwnd(new WindowInteropHelper(ShellView).Handle);
            lHwndSource.AddHook(new HwndSourceHook(SimCon.GetSimCon().ProcessSimCon));
        }

        private void PresetsPresenter_SettingsOpen(object? sender, int e)
        {
            settingsPresenter = null;
            settingsPresenter = new(e);
            ShellView.ActiveItem.Content = settingsPresenter.SettingsView;
            settingsPresenter.CloseSettings += SettingsPresenter_CloseSettings;
        }

        private void SettingsPresenter_CloseSettings(object? sender, EventArgs e)
        {
            settingsPresenter = null;
            presetsPresenter.Reload();
            ShellView.ActiveItem.Content = presetsPresenter.PresetsView;
        }

        private void PresetsView_FailuresClicked(object? sender, string e)
        {
            failuresPresenter = null;
            failuresPresenter = new(e, presetsPresenter.PresetsModel.PresetVarsList, presetsPresenter.PresetsModel.SelectedPreset.PresetID);
            ShellView.ActiveItem.Content = failuresPresenter.failuresView;
            failuresPresenter.CloseFailuresView += FailuresPresenter_CloseFailuresView;
        }

        private void FailuresPresenter_CloseFailuresView(object? sender, EventArgs e)
        {
            failuresPresenter = null;
            presetsPresenter.Reload();
            ShellView.ActiveItem.Content = presetsPresenter.PresetsView;
        }

        private void ShellView_OverviewClick(object? sender, EventArgs e)
        {
            overviewPresenter.Reload();
            ShellView.ActiveItem.Content = overviewPresenter.OverviewView;
        }

        private void ShellView_PresetsClick(object? sender, EventArgs e)
        {
            presetsPresenter.Reload();
            ShellView.ActiveItem.Content = presetsPresenter.PresetsView;
        }

        private void ShellView_FailListClick(object? sender, EventArgs e)
        {
            ShellView.ActiveItem.Content = failListPresenter.FailListView;
        }

        private void ShellView_HelpClick(object? sender, EventArgs e)
        {
            ShellView.ActiveItem.Content = helpPresenter.HelpView;
        }

    }
}
