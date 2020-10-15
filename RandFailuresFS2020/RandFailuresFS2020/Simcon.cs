using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using Microsoft.FlightSimulator.SimConnect;

namespace RandFailuresFS2020
{
    interface ISimCon
    {
        void Connect(IntPtr _hWnd);
        void Disconnect();
        void updateData();
        void setValue();
    }

    class Simcon : ISimCon
    {
        public const int WM_USER_SIMCONNECT = 0x0402;
        private IntPtr m_hWnd = new IntPtr(0);
        private SimConnect simconnect = null;

        public Simcon() { }

        enum DEFINITIONS
        {
            engine
        }

        enum DATA_REQ
        {
            REQ_1
        };

        enum EVENTS
        {
            
        };

        public struct engine
        {
            public double fire;
        };

        public int GetUserSimConnectWinEvent()
        {
            return WM_USER_SIMCONNECT;
        }

        public void ReceiveSimConnectMessage()
        {
            simconnect?.ReceiveMessage();
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnect");

            //m_oTimer.Stop();

            if (simconnect != null)
            {
                /// Dispose serves the same purpose as SimConnect_Close()
                simconnect.Dispose();
                simconnect = null;
            }
        }

        public void Connect(IntPtr _hWnd)
        {
            Console.WriteLine("Connect");

            m_hWnd = _hWnd;

            try
            {
                /// The constructor is similar to SimConnect_Open in the native API
                simconnect = new SimConnect("RandFailuresFS2020", m_hWnd, WM_USER_SIMCONNECT, null, 0);

                /// Listen to connect and quit msgs
                simconnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(SimConnect_OnRecvOpen);
                simconnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(SimConnect_OnRecvQuit);

                /// Listen to exceptions
                simconnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(SimConnect_OnRecvException);

                initData();

                /// Catch a simobject data request
                simconnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);
            }
            catch (COMException ex)
            {
                Console.WriteLine("Connection to KH failed: " + ex.Message);
            }
        }

        void initData()
        {
            simconnect.AddToDataDefinition(DEFINITIONS.engine, "ENG ON FIRE:1", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            simconnect.RegisterDataDefineStruct<engine>(DEFINITIONS.engine);
        }

        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            Console.WriteLine("SimConnect_OnRecvOpen");
            Console.WriteLine("Connected to KH");

            //m_oTimer.Start();
        }

        /// The case where the user closes game
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

            //lErrorMessages.Add("SimConnect : " + eException.ToString());
        }

        public engine eng;

        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            Console.WriteLine("SimConnect_OnRecvSimobjectDataBytype");

            switch ((DATA_REQ)data.dwRequestID)
            {
                case DATA_REQ.REQ_1:
                    {
                        eng = (engine)data.dwData[0];

                        break;
                    }
                /*case DATA_REQ.REQ_2:
                    {
                        lp2 = (leftP)data.dwData[0];
                        break;
                    }
                case DATA_REQ.REQ_3:
                    {
                        gps3 = (gps)data.dwData[0];
                        break;
                    }
                case DATA_REQ.REQ_4:
                    {
                        cdp4 = (cdp)data.dwData[0];
                        break;
                    }
                case DATA_REQ.REQ_5:
                    {
                        ra5 = (radio)data.dwData[0];
                        break;
                    }
                case DATA_REQ.REQ_6:
                    {
                        spi6 = (spi)data.dwData[0];
                        break;
                    }
                case DATA_REQ.REQ_7:
                    {
                        servo7 = (servo)data.dwData[0];
                        break;
                    }*/
                default:
                    //displayText("Unknown request ID: " + data.dwRequestID);
                    break;
            }
        }

        public void updateData()
        {
            simconnect.RequestDataOnSimObjectType(DATA_REQ.REQ_1, DEFINITIONS.engine, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
        }

        public void setValue()
        {
            eng.fire = 1;
            simconnect.SetDataOnSimObject(DEFINITIONS.engine, 0,0, eng);
        }

    }
}
