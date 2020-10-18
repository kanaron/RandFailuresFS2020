using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using Microsoft.FlightSimulator.SimConnect;
using System.Windows.Threading;

namespace RandFailuresFS2020
{
    interface ISimCon
    {
        void Connect();
        void Disconnect();
        void UpdateData();
        void SetValue();

        SimConnect GetSimConnect();
    }

    enum DEFINITIONS
    {
        Simulation, Engine, Aviation, Electrical
    }

    enum DATA_REQ
    {
        REQ_1, REQ_2, REQ_3, REQ_4
    };

    enum EVENTS
    {

    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct Simulation
    {
        public double OnGround;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct Engine
    {
        public double fire;
        public double rpm;
        public double OnGround;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct Aviation
    {
        public double leftFlap;
        public double rightFlap;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct Electrical
    {
        public double batteryMaster;
    };


    class Simcon : ISimCon
    {
        public const int WM_USER_SIMCONNECT = 0x0402;
        private IntPtr m_hWnd = new IntPtr(0);
        private SimConnect simconnect = null;
        private DispatcherTimer GroundTimer;
        private DispatcherTimer ContinTimer;
        public Engine eng;
        public Simulation sim;
        public Aviation avi;
        public Electrical ele;


        public void UpdateData()
        {
            simconnect.RequestDataOnSimObjectType(DATA_REQ.REQ_1, DEFINITIONS.Simulation, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
            simconnect.RequestDataOnSimObjectType(DATA_REQ.REQ_2, DEFINITIONS.Engine, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
            simconnect.RequestDataOnSimObjectType(DATA_REQ.REQ_3, DEFINITIONS.Aviation, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
            simconnect.RequestDataOnSimObjectType(DATA_REQ.REQ_4, DEFINITIONS.Electrical, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);

        }

        public void SetValue()
        {
            Console.WriteLine("sim" + sim.OnGround);
            Console.WriteLine((int)sim.OnGround);
            if ((int)sim.OnGround == 0)
            {
                eng.fire = 1;
                simconnect.SetDataOnSimObject(DEFINITIONS.Engine, 0, 0, eng);
            }
        }

        private void OnTickGround(object sender, EventArgs e)
        {
            UpdateData();
            SetValue();
        }

        private void OnTickContin(object sender, EventArgs e)
        {
            avi.leftFlap = 0.5;
            simconnect.SetDataOnSimObject(DEFINITIONS.Aviation, 0, 0, avi);
        }

        void InitData()
        {
            simconnect.AddToDataDefinition(DEFINITIONS.Simulation, "SIM ON GROUND", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            simconnect.AddToDataDefinition(DEFINITIONS.Engine, "ENG ON FIRE:1", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simconnect.AddToDataDefinition(DEFINITIONS.Engine, "GENERAL ENG OIL TEMPERATURE:1", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simconnect.AddToDataDefinition(DEFINITIONS.Engine, "SIM ON GROUND", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            simconnect.AddToDataDefinition(DEFINITIONS.Aviation, "TRAILING EDGE FLAPS LEFT PERCENT", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simconnect.AddToDataDefinition(DEFINITIONS.Aviation, "TRAILING EDGE FLAPS RIGHT PERCENT", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            simconnect.AddToDataDefinition(DEFINITIONS.Electrical, "ELECTRICAL MASTER BATTERY", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            simconnect.RegisterDataDefineStruct<Simulation>(DEFINITIONS.Simulation);
            simconnect.RegisterDataDefineStruct<Engine>(DEFINITIONS.Engine);
            simconnect.RegisterDataDefineStruct<Aviation>(DEFINITIONS.Aviation);
            simconnect.RegisterDataDefineStruct<Electrical>(DEFINITIONS.Electrical);

        }


        public Simcon(IntPtr _hWnd)
        {
            GroundTimer = new DispatcherTimer();
            GroundTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            GroundTimer.Tick += new EventHandler(OnTickGround);

            ContinTimer = new DispatcherTimer();
            ContinTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            ContinTimer.Tick += new EventHandler(OnTickContin);

            m_hWnd = _hWnd;
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

        public void Connect()
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

                InitData();

                /// Catch a simobject data request
                simconnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);

                UpdateData();

                GroundTimer.Start();
                ContinTimer.Start();
            }
            catch (COMException ex)
            {
                Console.WriteLine("Connection to KH failed: " + ex.Message);
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

        public SimConnect GetSimConnect()
        {
            return simconnect;
        }

        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            //Console.WriteLine("SimConnect_OnRecvSimobjectDataBytype");

            switch ((DATA_REQ)data.dwRequestID)
            {
                case DATA_REQ.REQ_1:
                    {
                        sim = (Simulation)data.dwData[0];
                        break;
                    }
                case DATA_REQ.REQ_2:
                    {
                        eng = (Engine)data.dwData[0];
                        break;
                    }
                case DATA_REQ.REQ_3:
                    {
                        avi = (Aviation)data.dwData[0];
                        break;
                    }
                case DATA_REQ.REQ_4:
                    {
                        ele = (Electrical)data.dwData[0];
                        break;
                    }
                /*case DATA_REQ.REQ_5:
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
                    Console.WriteLine("Unknown request ID: " + data.dwRequestID);
                    break;
            }
        }
    }
}
