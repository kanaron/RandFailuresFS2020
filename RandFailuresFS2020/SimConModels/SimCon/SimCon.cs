using Microsoft.FlightSimulator.SimConnect;
using Serilog;
using System.Runtime.InteropServices;

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
        Dummy, KEY_TOGGLE_VACUUM_FAILURE, KEY_TOGGLE_ENGINE1_FAILURE, KEY_TOGGLE_ENGINE2_FAILURE, KEY_TOGGLE_ENGINE3_FAILURE, KEY_TOGGLE_ENGINE4_FAILURE,
        KEY_TOGGLE_ELECTRICAL_FAILURE, KEY_TOGGLE_PITOT_BLOCKAGE, KEY_TOGGLE_STATIC_PORT_BLOCKAGE, KEY_TOGGLE_HYDRAULIC_FAILURE,
        KEY_TOGGLE_TOTAL_BRAKE_FAILURE, KEY_TOGGLE_LEFT_BRAKE_FAILURE, KEY_TOGGLE_RIGHT_BRAKE_FAILURE
    };

    public class SimCon
    {
        private static readonly SimCon instance = new();

        public event EventHandler<string>? SimException;

        public bool Connected { get; private set; } = false;

        private string state = "Sim not found";
        public event EventHandler<string>? StateChanged;

        public const int WM_USER_SIMCONNECT = 0x0402;
        public IntPtr MHWnd { get; set; }
        private SimConnect? simconnect = null;

        private SimCon()
        {

        }

        public void SetHandle(IntPtr _ptr)
        {
            MHWnd = _ptr;
        }

        public IntPtr ProcessSimCon(IntPtr _hwnd, int msg, IntPtr _wParam, IntPtr _lParam, ref bool handled)
        {
            handled = false;

            if (msg == 0x0402)
            {
                if (simconnect != null)
                {
                    //try
                    //{
                    simconnect.ReceiveMessage();
                    handled = true;
                    /*}
                    catch
                    { }*/
                }
            }
            return (IntPtr)0;
        }

        //TODO make first variable in json Sim on ground or something non setable
        public void RegisterList(List<SimVarModel> list)
        {
            Log.Logger.Information("RegisterList " + nameof(list));
            foreach (SimVarModel simVarModel in list)
            {
                if (simVarModel.IsEvent == false)
                {
                    simconnect!.AddToDataDefinition(simVarModel.eDef, simVarModel.SimVariable, simVarModel.Unit, SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                    simconnect!.RegisterDataDefineStruct<double>(simVarModel.eDef);
                }
            }
            Log.Logger.Information("List registrated");
        }

        public bool Connect()
        {
            Console.WriteLine("Connect");

            try
            {
                simconnect = new SimConnect("RandFailuresFS2020", MHWnd, WM_USER_SIMCONNECT, null, 0);

                simconnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(SimConnect_OnRecvOpen);
                simconnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(SimConnect_OnRecvQuit);

                simconnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(SimConnect_OnRecvException);

                simconnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);

            }
            catch (COMException ex)
            {
                //simException?.Invoke(this, ex.Message);
                Console.WriteLine("Connection to KH failed: " + ex.Message);
            }

            return Connected;
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnect");
            Log.Logger.Information("Simconnect disconnected");

            if (simconnect != null)
            {
                simconnect.Dispose();
                simconnect = null;
            }

            SimConHelper.GetSimConHelper().SimConnectClosed();

            Connected = false;
        }

        public void ChangeState(string _state)
        {
            Log.Logger.Information("ChangeState " + _state);
            state = _state;
            StateChanged?.Invoke(this, state);
        }

        /// <summary>
        /// Sends request to update every element on list
        /// </summary>
        /// <param name="list"></param>
        public void UpdateData(List<SimVarModel> list)
        {
            foreach (SimVarModel simVarModel in list)
            {
                if (simVarModel.IsEvent == false)
                {
                    try
                    {
                        simconnect!.RequestDataOnSimObjectType(simVarModel.eRequest, simVarModel.eDef, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                    }
                    catch (Exception ex)
                    {
                        Log.Logger.Error("UpdateData exception");
                        Log.Logger.Error(ex, ex.StackTrace);
                    }
                }
            }
        }

        /// <summary>
        /// When data is received it searches through Failable list and updates its value 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            uint iRequest = data.dwRequestID;

            //if (SimVarLists.GetSimVarLists().GetFailableList() != null)
            //foreach (SimVarModel oSimvarRequest in SimVarLists.GetSimVarLists().GetFailableList())
            if (SimVarLists.GetSimVarLists().GetFailuresList() != null)
                foreach (SimVarModel oSimvarRequest in SimVarLists.GetSimVarLists().GetFailuresList())
                {
                    if (iRequest == (uint)oSimvarRequest.eRequest)
                    {
                        double dValue = (double)data.dwData[0];
                        oSimvarRequest.Value = dValue;
                        break;
                    }
                }

            foreach (SimVarModel oSimvarRequest in SimVarLists.GetSimVarLists().GetDataList())
            {
                if (iRequest == (uint)oSimvarRequest.eRequest)
                {
                    double dValue = (double)data.dwData[0];
                    oSimvarRequest.Value = dValue;
                    break;
                }
            }
        }

        public static int GetUserSimConnectWinEvent()
        {
            return WM_USER_SIMCONNECT;
        }

        public void ReceiveSimConnectMessage()
        {
            simconnect?.ReceiveMessage();
        }

        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            Log.Logger.Information("Sim connected");
            Connected = true;
            state = "Sim connected";
            StateChanged?.Invoke(this, state);

            Console.WriteLine("SimConnect_OnRecvOpen");
            Console.WriteLine("Connected to KH");
        }

        private void SimConnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            Log.Logger.Information("Simconnect quit");
            Console.WriteLine("SimConnect_OnRecvQuit");
            Console.WriteLine("KH has exited");

            Disconnect();
        }

        private void SimConnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            SIMCONNECT_EXCEPTION eException = (SIMCONNECT_EXCEPTION)data.dwException;
            Console.WriteLine("SimConnect_OnRecvException: " + eException.ToString());

            SimException?.Invoke(this, eException.ToString());
            Log.Logger.Error("Simconnect exception: " + eException.ToString());
        }

        public static SimCon GetSimCon() => instance;

        public SimConnect GetSimConnect() => simconnect!;
    }
}
