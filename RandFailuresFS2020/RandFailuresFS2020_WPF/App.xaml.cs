using RandFailuresFS2020_WPF.Presenters;
using SimConModels;
using SimConModels.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RandFailuresFS2020_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new DatabaseHelper();
            new SimConHelper();
            new ShellPresenter();
        }
    }
}
