using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SimConModels
{
    public class SimConHelper
    {
        private static readonly SimConHelper instance = new SimConHelper();

        public bool Autostart = false;

        private System.Timers.Timer ConnectionTimer;
        private System.Timers.Timer StartTimer;

        private System.Timers.Timer DataUpdateTimer;
        private System.Timers.Timer FailTimer;

        private SimConHelper()
        {

        }

        public void Initialize()
        {
            Autostart = SQLOptions.LoadOptionValueBool("Autostart");
            StartTimers();
        }

        public void ChangeAutostart()
        {
            Autostart = SQLOptions.LoadOptionValueBool("Autostart");

        }

        public void StartTimers()
        {
            ConnectionTimer = new System.Timers.Timer();
            ConnectionTimer.Interval = 1000;
            ConnectionTimer.Elapsed += ConnectionTimer_Elapsed;
            ConnectionTimer.AutoReset = true;
            ConnectionTimer.Enabled = true;
            ConnectionTimer.Start();

            StartTimer = new System.Timers.Timer();
            StartTimer.Interval = 1000;
            StartTimer.Elapsed += StartTimer_Elapsed;
            StartTimer.AutoReset = true;
            StartTimer.Enabled = false;

            DataUpdateTimer = new System.Timers.Timer();
            DataUpdateTimer.Interval = 1000;
            DataUpdateTimer.Elapsed += DataUpdateTimer_Elapsed;
            DataUpdateTimer.AutoReset = true;
            DataUpdateTimer.Enabled = false;

            FailTimer = new System.Timers.Timer();
            FailTimer.Interval = 1;
            FailTimer.Elapsed += FailTimer_Elapsed;
            FailTimer.AutoReset = true;
            FailTimer.Enabled = false;
        }

        public void ManageFailTimer(bool enable)
        {
            if (enable)
            {
                SimVarLists.GetSimVarLists().RandomizeFailures();
                FailTimer.Enabled = true;
                FailTimer.Start();
                SimCon.GetSimCon().ChangeState("Failures started");
            }
            else
            {
                FailTimer.Stop();
            }
        }

        private void ConnectionTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (SimCon.GetSimCon().Connect())
            {
                ConnectionTimer.Stop();

                if (Autostart)
                {
                    StartTimer.Enabled = true;
                    StartTimer.Start();
                }

                SimVarLists.GetSimVarLists().LoadDataList();

                DataUpdateTimer.Enabled = true;
                DataUpdateTimer.Start();
            }
        }

        private void StartTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            double longtitude = SimVarLists.GetSimVarLists().GetDataList().Find(x => x.SimVariable == "PLANE LONGITUDE").Value;
            double latitude = SimVarLists.GetSimVarLists().GetDataList().Find(x => x.SimVariable == "PLANE LATITUDE").Value;

            if (Math.Abs(longtitude) + Math.Abs(latitude) > 1)
            {
                StartTimer.Stop();

                ManageFailTimer(true);
            }
        }

        private void DataUpdateTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (!FailTimer.Enabled)
            {
                SimCon.GetSimCon().UpdateData(SimVarLists.GetSimVarLists().GetDataList());
            }
            else
                SimVarLists.GetSimVarLists().AddFlyTime();
        }

        private void FailTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            SimCon.GetSimCon().UpdateData(SimVarLists.GetSimVarLists().GetDataList());
            SimCon.GetSimCon().UpdateData(SimVarLists.GetSimVarLists().GetFailuresList());
            SimVarLists.GetSimVarLists().SetFailures();
        }

        public static SimConHelper GetSimVarLists() => instance;
    }
}
