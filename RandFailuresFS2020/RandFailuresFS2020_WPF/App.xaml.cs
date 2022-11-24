using Microsoft.Extensions.Configuration;
using RandFailuresFS2020_WPF.Presenters;
using Serilog;
using SimConModels;
using SimConModels.DatabaseHelper;
using System;
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
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var builder = new ConfigurationBuilder();

            Log.Logger = new LoggerConfiguration().WriteTo.File("logs/log" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".txt")
                .ReadFrom.Configuration(builder.Build()).Enrich.FromLogContext().CreateLogger();

            Log.Logger.Information("Application started");

            Log.Logger.Information("Creating Database helper");
            _ = new DatabaseHelper();

            SimConHelper.GetSimConHelper().Initialize();

            Log.Logger.Information("Starting Shell view");
            _ = new ShellPresenter();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Logger.Error("Unhandled exception");
            Log.Logger.Error(e.ToString()!);
        }
    }
}
