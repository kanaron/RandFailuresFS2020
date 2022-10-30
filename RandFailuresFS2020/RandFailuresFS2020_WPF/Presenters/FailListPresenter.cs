using RandFailuresFS2020_WPF.Models;
using RandFailuresFS2020_WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class FailListPresenter
    {
        public FailListView failListView { private set; get; }
        public FailListModel failListModel { private set; get; }

        public FailListPresenter()
        {
            failListView = new FailListView();
            failListView.ShowFailuresClicked += FailListView_ShowFailuresClicked;

            failListModel = new FailListModel();

            failListView.DataContext = failListModel;
        }

        private void FailListView_ShowFailuresClicked(object? sender, EventArgs e)
        {
            failListModel.ShowFailures();
        }
    }
}
