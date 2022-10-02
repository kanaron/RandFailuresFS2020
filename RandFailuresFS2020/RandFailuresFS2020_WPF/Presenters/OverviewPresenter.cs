using RandFailuresFS2020_WPF.Models;
using RandFailuresFS2020_WPF.Views;
using SimConModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class OverviewPresenter
    {
        public OverviewView overviewView { private set; get; }
        public OverviewModel overviewModel { private set; get; }

        public OverviewPresenter()
        {
            overviewView = new OverviewView();
            overviewView.RestartClicked += OverviewView_RestartClicked;

            overviewModel = new OverviewModel();

            overviewView.DataContext = overviewModel;
        }

        public void Reload()
        {
            overviewModel.Reload();
        }

        private void OverviewView_RestartClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
