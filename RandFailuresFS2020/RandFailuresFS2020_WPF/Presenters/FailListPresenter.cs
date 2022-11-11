using RandFailuresFS2020_WPF.Models;
using RandFailuresFS2020_WPF.Views;
using System;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class FailListPresenter
    {
        public FailListView FailListView { private set; get; }
        public FailListModel FailListModel { private set; get; }

        public FailListPresenter()
        {
            FailListView = new FailListView();
            FailListView.ShowFailuresClicked += FailListView_ShowFailuresClicked;

            FailListModel = new FailListModel();

            FailListView.DataContext = FailListModel;
        }

        private void FailListView_ShowFailuresClicked(object? sender, EventArgs e)
        {
            FailListModel.ShowFailures();
        }
    }
}
