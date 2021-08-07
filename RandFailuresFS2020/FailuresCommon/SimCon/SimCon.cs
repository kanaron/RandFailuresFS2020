using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace FailuresCommon
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
        public event EventHandler<string> simException;

        public bool connected { get; private set; } = false;

        public const int WM_USER_SIMCONNECT = 0x0402;
        public IntPtr m_hWnd { get; set; }
        private SimConnect simconnect = null;

        public List<SimVarModel> listSimVar { get; set; } = new List<SimVarModel>();

        public SimCon(IntPtr handler)
        {
            m_hWnd = handler;
        }

        public bool Connect()
        {
            Console.WriteLine("Connect");

            try
            {
                /// The constructor is similar to SimConnect_Open in the native API
                simconnect = new SimConnect("RandFailuresFS2020", m_hWnd, WM_USER_SIMCONNECT, null, 0);

                /// Listen to connect and quit msgs
                simconnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(SimConnect_OnRecvOpen);
                simconnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(SimConnect_OnRecvQuit);

                /// Listen to exceptions
                simconnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(SimConnect_OnRecvException);

                /// Catch a simobject data request
                simconnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);

            }
            catch (COMException ex)
            {
                simException?.Invoke(this, ex.Message);
                Console.WriteLine("Connection to KH failed: " + ex.Message);
                throw;
            }

            simException?.Invoke(this, "SimConnect connected");
            connected = true;
            return connected;
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnect");

            if (simconnect != null)
            {
                /// Dispose serves the same purpose as SimConnect_Close()
                simconnect.Dispose();
                simconnect = null;
            }

            simException?.Invoke(this, "SimConnect disconnected");
            connected = false;
        }

        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            uint iRequest = data.dwRequestID;
            //uint iObject = data.dwObjectID;

            foreach (SimVarModel oSimvarRequest in listSimVar)
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
