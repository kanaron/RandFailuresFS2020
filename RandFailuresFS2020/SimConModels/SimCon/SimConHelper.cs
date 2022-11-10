using Serilog;
using System.Timers;

namespace SimConModels
{
    public class SimConHelper
    {
        private static readonly SimConHelper instance = new SimConHelper();

        public event EventHandler<bool> FailuresInProgressEvent;

        //public bool Autostart = false;

        private System.Timers.Timer ConnectionTimer;
        private System.Timers.Timer StartTimer;

        private System.Timers.Timer DataUpdateTimer;
        private System.Timers.Timer FailTimer;

        private SimConHelper()
        {

        }

        public void Initialize()
        {
            Log.Logger.Information("Initializing SimConHelper");
            //Autostart = SQLOptions.LoadOptionValueBool("Autostart");
            StartTimers();
        }

        /*public void ChangeAutostart()
        {
            Autostart = SQLOptions.LoadOptionValueBool("Autostart");

        }*/

        public void StartTimers()
        {
            Log.Logger.Information("Starting timers");
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

        public void SimConnectClosed()
        {
            Log.Logger.Information("SimConnectClosed");
            FailTimer.Stop();
            DataUpdateTimer.Stop();
            StartTimer.Stop();
            ConnectionTimer.Start();

            FailuresInProgressEvent?.Invoke(this, false);

            SimCon.GetSimCon().ChangeState("Sim not found");
        }

        public void ManageFailTimer(bool enable)
        {
            if (enable)
            {
                Log.Logger.Information("Starting failures");
                SimVarLists.GetSimVarLists().RandomizeFailures();
                FailTimer.Enabled = true;
                FailTimer.Start();
                FailuresInProgressEvent?.Invoke(this, true);
                SimCon.GetSimCon().ChangeState("Failures started");
                Log.Logger.Information("Failures started");
            }
            else
            {
                FailuresInProgressEvent?.Invoke(this, false);
                SimCon.GetSimCon().ChangeState("Failures stopped");
                Log.Logger.Information("Failures stopped");
                FailTimer.Stop();
            }
        }

        private void ConnectionTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (SimCon.GetSimCon().Connect())
            {
                ConnectionTimer.Stop();

                //if (Autostart)
                //{
                StartTimer.Enabled = true;
                StartTimer.Start();
                //}

                SimVarLists.GetSimVarLists().LoadDataList();

                DataUpdateTimer.Enabled = true;
                DataUpdateTimer.Start();
            }
        }

        private void StartTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (!isAtMainMenu())
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
            {
                SimVarLists.GetSimVarLists().AddFlyTime();
                if (isAtMainMenu())
                {
                    FailTimer.Stop();
                    StartTimer.Start();
                    SimCon.GetSimCon().ChangeState("Sim connected");
                    FailuresInProgressEvent?.Invoke(this, false);
                    Log.Logger.Information("Back at main menu. Simcon connected");
                }
            }
        }

        private void FailTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            SimCon.GetSimCon().UpdateData(SimVarLists.GetSimVarLists().GetDataList());
            SimCon.GetSimCon().UpdateData(SimVarLists.GetSimVarLists().GetFailuresList());
            SimVarLists.GetSimVarLists().SetFailures();
        }

        private bool isAtMainMenu()
        {
            double longtitude = SimVarLists.GetSimVarLists().GetDataList().Find(x => x.SimVariable == "PLANE LONGITUDE").Value;
            double latitude = SimVarLists.GetSimVarLists().GetDataList().Find(x => x.SimVariable == "PLANE LATITUDE").Value;

            return Math.Abs(longtitude) + Math.Abs(latitude) < 1;
        }

        public static SimConHelper GetSimConHelper() => instance;
    }
}
