using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Timers;

namespace SimConModels
{
    enum GROUP
    {
        ID_PRIORITY_STANDARD = 1900000000,
    };

    public enum DEFINITION
    {
        Dummy = 0
    };

    public enum REQUEST
    {
        Dummy = 0
    };

    public enum EVENT
    {
        Dummy/*, KEY_TOGGLE_VACUUM_FAILURE, KEY_TOGGLE_ENGINE1_FAILURE, KEY_TOGGLE_ENGINE2_FAILURE, KEY_TOGGLE_ENGINE3_FAILURE, KEY_TOGGLE_ENGINE4_FAILURE,
        KEY_TOGGLE_ELECTRICAL_FAILURE, KEY_TOGGLE_PITOT_BLOCKAGE, KEY_TOGGLE_STATIC_PORT_BLOCKAGE, KEY_TOGGLE_HYDRAULIC_FAILURE,
        KEY_TOGGLE_TOTAL_BRAKE_FAILURE, KEY_TOGGLE_LEFT_BRAKE_FAILURE, KEY_TOGGLE_RIGHT_BRAKE_FAILURE*/
    };

    public class SimCon
    {
        private static readonly SimCon instance = new SimCon();

        private System.Timers.Timer ConnectionTimer;
        private System.Timers.Timer StartTimer;

        public event EventHandler<string> simException;

        public bool connected { get; private set; } = false;

        private string state = "Sim not found";
        public event EventHandler<string> StateChanged;

        public const int WM_USER_SIMCONNECT = 0x0402;
        public IntPtr m_hWnd { get; set; }
        private SimConnect simconnect = null;

        private SimCon()
        {
            //SimVarLists.GetSimVarLists().ListsLoaded += SimCon_ListsLoaded;

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
        }

        private void StartTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            //TODO wire it up
            state = "Failures started";
            StateChanged?.Invoke(this, state);
            
            StartTimer.Stop();
        }

        private void ConnectionTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Connect();
        }

        public static SimCon GetSimCon()
        {
            return instance;
        }

        private void SimCon_ListsLoaded(object sender, List<SimVarModel> e)
        {
            foreach (SimVarModel simVarModel in e)
            {
                if (simVarModel.IsEvent == false)
                {
                    simconnect.AddToDataDefinition(simVarModel.eDef, simVarModel.SimVariable, simVarModel.Unit, SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                    simconnect.RegisterDataDefineStruct<double>(simVarModel.eDef);
                }
            }
        }

        public bool Connect()
        {
            Console.WriteLine("Connect");

            try
            {
                simconnect = new SimConnect("RandFailuresFS2020", m_hWnd, WM_USER_SIMCONNECT, null, 0);

                simconnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(SimConnect_OnRecvOpen);
                simconnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(SimConnect_OnRecvQuit);

                simconnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(SimConnect_OnRecvException);

                simconnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);

                ConnectionTimer.Stop();
                StartTimer.Enabled = true;
                StartTimer.Start();
                connected = true;
                state = "Sim connected";
                StateChanged?.Invoke(this, state);
            }
            catch (COMException ex)
            {
                //simException?.Invoke(this, ex.Message);
                Console.WriteLine("Connection to KH failed: " + ex.Message);
            }

            connected = true;
            return connected;
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnect");

            if (simconnect != null)
            {
                simconnect.Dispose();
                simconnect = null;
            }

            connected = false;
        }

        public void UpdateData(List<SimVarModel> list)
        {
            foreach (SimVarModel simVarModel in list)
            {
                if (simVarModel.IsEvent == false)
                {
                    simconnect.RequestDataOnSimObjectType(simVarModel.eRequest, simVarModel.eDef, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                }
            }
        }

        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            uint iRequest = data.dwRequestID;

            foreach (SimVarModel oSimvarRequest in SimVarLists.GetSimVarLists().GetFailableList())
            {
                if (iRequest == (uint)oSimvarRequest.eRequest)
                {
                    double dValue = (double)data.dwData[0];
                    oSimvarRequest.Value = dValue;
                }
            }
        }

        public int GetUserSimConnectWinEvent()
        {
            return WM_USER_SIMCONNECT;
        }

        public void ReceiveSimConnectMessage()
        {
            simconnect?.ReceiveMessage();
        }

        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            Console.WriteLine("SimConnect_OnRecvOpen");
            Console.WriteLine("Connected to KH");
        }

        private void SimConnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            Console.WriteLine("SimConnect_OnRecvQuit");
            Console.WriteLine("KH has exited");

            Disconnect();
        }

        private void SimConnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            SIMCONNECT_EXCEPTION eException = (SIMCONNECT_EXCEPTION)data.dwException;
            Console.WriteLine("SimConnect_OnRecvException: " + eException.ToString());

            simException?.Invoke(this, eException.ToString());
        }

        public SimConnect GetSimConnect()
        {
            return simconnect;
        }
    }
}
