using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.FlightSimulator.SimConnect;
using System.Windows.Threading;
using System.Windows.Forms;

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

    public enum DEFINITION
    {
        Dummy = 0
    };

    public enum REQUEST
    {
        Dummy = 0
    };

    public class SimVar
    {
        public DEFINITION eDef = DEFINITION.Dummy;
        public REQUEST eRequest = REQUEST.Dummy;
        public string sName;
        public double dValue;
        public string sUnits;

        public string controlName;

        public double failureChance;
        public bool willFail;
        public int failureHeight;
        public int failureTime;

        public SimVar(int id, string _name, string _cname, string _unit = "Number")
        {
            eDef = (DEFINITION)id;
            eRequest = (REQUEST)id;
            sName = _name;
            sUnits = _unit;
            controlName = _cname;
        }
    };


    class Simcon : ISimCon
    {
        public const int WM_USER_SIMCONNECT = 0x0402;
        private IntPtr m_hWnd = new IntPtr(0);
        private SimConnect simconnect = null;
        private DispatcherTimer GroundTimer;
        private DispatcherTimer ContinTimer;
        public List<SimVar> lSimVars;
        public Form1 f1;

        public void UpdateData()
        {
            foreach (SimVar s in lSimVars)
            {
                simconnect.RequestDataOnSimObjectType(s.eRequest, s.eDef, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                Console.WriteLine(s.sName + " " + s.dValue + " " + s.controlName + " " + s.failureChance);
            }
        }

        public void SetValue()
        {
            /*Console.WriteLine(sim.altitude * 3);
            if (sim.altitude * 3 >= 3000)
            {
                eng2.turbocharger = 1;
            }

            simconnect.SetDataOnSimObject(DEFINITIONS.Engine2, 0, 0, eng2);*/
        }

        private void OnTickGround(object sender, EventArgs e)
        {
            UpdateData();
            SetValue();
        }

        private void OnTickContin(object sender, EventArgs e)
        {
            //avi.leftFlap = 0.5;
            //simconnect.SetDataOnSimObject(DEFINITIONS.Aviation, 0, 0, avi);
        }

        void InitData()
        {
            createRegister(new SimVar(lSimVars.Count(), "PLANE ALTITUDE", "", "Feet"));
            createRegister(new SimVar(lSimVars.Count(), "SIM ON GROUND", ""));
            createRegister(new SimVar(lSimVars.Count(), "TRAILING EDGE FLAPS LEFT PERCENT", "fLeftFlap"));
            createRegister(new SimVar(lSimVars.Count(), "TRAILING EDGE FLAPS RIGHT PERCENT", "fRightFlap"));
            createRegister(new SimVar(lSimVars.Count(), "ELECTRICAL MASTER BATTERY", ""));
            createRegister(new SimVar(lSimVars.Count(), "FUEL TANK CENTER QUANTITY", ""));
            createRegister(new SimVar(lSimVars.Count(), "FUEL TANK LEFT MAIN QUANTITY", ""));
            createRegister(new SimVar(lSimVars.Count(), "FUEL TANK RIGHT MAIN QUANTITY", ""));

            for (int i = 0; i < 4; i++)
            {
                createRegister(new SimVar(lSimVars.Count(), "ENG ON FIRE: " + (i + 1), ""));
                createRegister(new SimVar(lSimVars.Count(), "RECIP ENG COOLANT RESERVOIR PERCENT:" + (i + 1), ""));
                createRegister(new SimVar(lSimVars.Count(), "RECIP ENG TURBOCHARGER FAILED:" + (i + 1), ""));
            }
        }

        void createRegister(SimVar temp)
        {
            simconnect.AddToDataDefinition(temp.eDef, temp.sName, temp.sUnits, SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simconnect.RegisterDataDefineStruct<double>(temp.eDef);

            if (temp.controlName != "")
                temp.failureChance = (double)((NumericUpDown)(f1.Controls[temp.controlName])).Value;

            lSimVars.Add(temp);
        }


        public Simcon(Form1 form)
        {
            GroundTimer = new DispatcherTimer();
            GroundTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            GroundTimer.Tick += new EventHandler(OnTickGround);

            ContinTimer = new DispatcherTimer();
            ContinTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            ContinTimer.Tick += new EventHandler(OnTickContin);

            m_hWnd = form.Handle;

            lSimVars = new List<SimVar>();

            f1 = form;
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

        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            uint iRequest = data.dwRequestID;
            //uint iObject = data.dwObjectID;

            foreach (SimVar oSimvarRequest in lSimVars)
            {
                if (iRequest == (uint)oSimvarRequest.eRequest)
                {
                    double dValue = (double)data.dwData[0];
                    oSimvarRequest.dValue = dValue;
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

            //m_oTimer.Start();
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

            //lErrorMessages.Add("SimConnect : " + eException.ToString());
        }

        public SimConnect GetSimConnect()
        {
            return simconnect;
        }
    }
}
