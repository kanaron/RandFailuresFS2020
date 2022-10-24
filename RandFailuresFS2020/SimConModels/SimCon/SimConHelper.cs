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
        private System.Timers.Timer ConnectionTimer;
        private System.Timers.Timer StartTimer;

        private System.Timers.Timer DataUpdateTimer;
        private System.Timers.Timer FailTimer;

        public SimConHelper()
        {
            StartTimers();
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


        private void ConnectionTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (SimCon.GetSimCon().Connect())
            {
                ConnectionTimer.Stop();
                StartTimer.Enabled = true;
                StartTimer.Start();

                DataUpdateTimer.Enabled = true;
                DataUpdateTimer.Start();

                SimVarLists.GetSimVarLists().LoadLists(SQLOptions.LoadOptionValueInt("PresetID"));
            }
        }

        private void StartTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            double longtitude = SimVarLists.GetSimVarLists().GetDataList().Find(x => x.SimVariable == "PLANE LONGITUDE").Value;
            double latitude = SimVarLists.GetSimVarLists().GetDataList().Find(x => x.SimVariable == "PLANE LATITUDE").Value;

            if (Math.Abs(longtitude) + Math.Abs(latitude) > 1) 
            {
                SimCon.GetSimCon().ChangeState("Failures started"); //TODO wire it up
                StartTimer.Stop();
            }
        }

        private void DataUpdateTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            SimCon.GetSimCon().UpdateData(SimVarLists.GetSimVarLists().GetDataList());
        }

        private void FailTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            SimCon.GetSimCon().UpdateData(SimVarLists.GetSimVarLists().GetFailableList());
        }
    }
}
