using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
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
        List<SimVar> getFailList();
    }

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
        Dummy = 0
    };

    public enum WHEN_FAIL
    {
        ALT, TIME, TAXI, INSTANT
    };
    public enum POSSIBLE_FAIL_TYPE
    {
        NO, CONTINOUS, COMPLETE, BOTH, STUCK, LEAK
    };

    public class SimVar
    {
        public DEFINITION eDef = DEFINITION.Dummy;
        public REQUEST eRequest = REQUEST.Dummy;
        public string sName;
        public double dValue;
        public string sUnits;

        public string controlName;

        public int failureChance;
        public POSSIBLE_FAIL_TYPE possibleType;
        public double failureValue;
        public WHEN_FAIL whenFail;
        public int failureHeight;
        public int failureTime;
        public bool done = false;
        public bool failStart = false;

        public SimVar(int id, string _name, string _cname, POSSIBLE_FAIL_TYPE ptype, string _unit = "Number")
        {
            //MessageBox.Show(id.ToString());
            eDef = (DEFINITION)id;
            eRequest = (REQUEST)id;
            sName = _name;
            sUnits = _unit;
            controlName = _cname;
            possibleType = ptype;
        }

        public void setFail()
        {
            switch (possibleType)
            {
                case POSSIBLE_FAIL_TYPE.STUCK:
                    {
                        if (!failStart)
                        {
                            failureValue = dValue;
                            failStart = true;
                        }
                        dValue = failureValue;
                        break;
                    }
                case POSSIBLE_FAIL_TYPE.LEAK:
                    {
                        dValue -= failureValue;
                        break;
                    }
                case POSSIBLE_FAIL_TYPE.CONTINOUS:
                    {
                        dValue = failureValue;
                        break;
                    }
                case POSSIBLE_FAIL_TYPE.COMPLETE:
                    {
                        dValue = failureValue;
                        done = true;
                        break;
                    }
            }
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
        public List<SimVar> lWillFailSV;
        public Form1 f1;
        public IEnumerable<Control> controls;

        public uint flyTime = 0;

        public void UpdateData()
        {
            foreach (SimVar s in lSimVars)
            {
                simconnect.RequestDataOnSimObjectType(s.eRequest, s.eDef, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                //Console.WriteLine(s.sName + " " + s.dValue + " " + s.controlName + " " + s.failureChance);
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

            if (lSimVars[2].dValue == 0)
                flyTime++;
        }

        private void OnTickContin(object sender, EventArgs e)
        {
            foreach (SimVar s in lWillFailSV)
            {
                if (!s.done)
                {
                    switch (s.whenFail)
                    {
                        case WHEN_FAIL.INSTANT:
                            {
                                s.setFail();
                                break;
                            }
                        case WHEN_FAIL.TAXI:
                            {
                                if (lSimVars[1].dValue >= 50)
                                    s.setFail();
                                break;
                            }
                        case WHEN_FAIL.ALT:
                            {
                                if (lSimVars[0].dValue >= s.failureHeight)
                                    s.setFail();
                                break;
                            }
                        case WHEN_FAIL.TIME:
                            {
                                if (flyTime >= s.failureTime)
                                    s.setFail();
                                break;
                            }
                    }
                }

                simconnect.SetDataOnSimObject(s.eDef, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_DATA_SET_FLAG.DEFAULT, s.dValue);
            }
        }

        void InitData()
        {
            createRegister(new SimVar(lSimVars.Count(), "PLANE ALTITUDE", "", POSSIBLE_FAIL_TYPE.NO, "Feet"));
            createRegister(new SimVar(lSimVars.Count(), "GROUND VELOCITY", "", POSSIBLE_FAIL_TYPE.NO, "Knots"));
            createRegister(new SimVar(lSimVars.Count(), "SIM ON GROUND", "", POSSIBLE_FAIL_TYPE.NO));

            createRegister(new SimVar(lSimVars.Count(), "TRAILING EDGE FLAPS LEFT PERCENT", "fLeftFlap", POSSIBLE_FAIL_TYPE.STUCK));
            createRegister(new SimVar(lSimVars.Count(), "TRAILING EDGE FLAPS RIGHT PERCENT", "fRightFlap", POSSIBLE_FAIL_TYPE.STUCK));

            createRegister(new SimVar(lSimVars.Count(), "FUEL TANK CENTER QUANTITY", "fFuelCenter", POSSIBLE_FAIL_TYPE.LEAK));
            createRegister(new SimVar(lSimVars.Count(), "FUEL TANK LEFT MAIN QUANTITY", "fFuelLeft", POSSIBLE_FAIL_TYPE.LEAK));
            createRegister(new SimVar(lSimVars.Count(), "FUEL TANK RIGHT MAIN QUANTITY", "fFuelRight", POSSIBLE_FAIL_TYPE.LEAK));

            for (int i = 0; i < 4; i++)
            {
                createRegister(new SimVar(lSimVars.Count(), "ENG ON FIRE:" + (i + 1), "fE" + (i + 1) + "Fire", POSSIBLE_FAIL_TYPE.COMPLETE));
                createRegister(new SimVar(lSimVars.Count(), "RECIP ENG COOLANT RESERVOIR PERCENT:" + (i + 1), "fE" + (i + 1) + "Leak", POSSIBLE_FAIL_TYPE.LEAK));
                createRegister(new SimVar(lSimVars.Count(), "RECIP ENG TURBOCHARGER FAILED:" + (i + 1), "fE" + (i + 1) + "Turbo", POSSIBLE_FAIL_TYPE.COMPLETE));
            }
        }

        void createRegister(SimVar temp)
        {
            simconnect.AddToDataDefinition(temp.eDef, temp.sName, temp.sUnits, SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simconnect.RegisterDataDefineStruct<double>(temp.eDef);

            if (temp.controlName != "")
            {
                try
                {
                    foreach (Control c in controls)
                    {
                        if (c is NumericUpDown)
                        {
                            if (c.Name == temp.controlName)
                            {
                                temp.failureChance = (int)(((NumericUpDown)c).Value);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            lSimVars.Add(temp);
        }

        void prepareFailures()
        {
            Random rnd = new Random();
            foreach (SimVar s in lSimVars)
            {
                if (s.possibleType != POSSIBLE_FAIL_TYPE.NO && rnd.Next(0, 100) < s.failureChance)
                {
                    switch (s.possibleType)
                    {
                        case POSSIBLE_FAIL_TYPE.COMPLETE:
                            {
                                s.failureValue = 1;
                                break;
                            }
                        case POSSIBLE_FAIL_TYPE.LEAK:
                            {
                                s.failureValue = 0.0001 + (rnd.Next(0, 11) / 100000);
                                break;
                            }
                        case POSSIBLE_FAIL_TYPE.STUCK:
                            {
                                s.failureValue = s.dValue;
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Unexpected failure type");
                                break;
                            }
                    }

                    s.whenFail = (WHEN_FAIL)rnd.Next(0, 4);

                    switch (s.whenFail)
                    {
                        case WHEN_FAIL.ALT:
                            {
                                s.failureHeight = (int)(lSimVars[0].dValue + 2000 + (rnd.Next(30) * 500));
                                break;
                            }
                        case WHEN_FAIL.TIME:
                            {
                                s.failureTime = 30 + rnd.Next(3600);
                                break;
                            }
                    }

                    lWillFailSV.Add(s);
                }
            }
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
            lWillFailSV = new List<SimVar>();

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

                controls = GetAll(f1, typeof(NumericUpDown));

                InitData();

                /// Catch a simobject data request
                simconnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);

                UpdateData();

                prepareFailures();

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

        public List<SimVar> getFailList()
        {
            return lWillFailSV;
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }
    }
}
