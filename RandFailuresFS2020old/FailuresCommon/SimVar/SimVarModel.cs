using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FailuresCommon
{
    public class SimVarModel
    {
        /// <summary>
        /// Definition for SimConnect
        /// </summary>
        public DEFINITION eDef = DEFINITION.Dummy;

        /// <summary>
        /// Request for SimConnect
        /// </summary>
        public REQUEST eRequest = REQUEST.Dummy;

        /// <summary>
        /// Event for SimConnect
        /// </summary>
        public EVENT eEvent = EVENT.Dummy;

        /// <summary>
        /// ID of SimVar
        /// </summary>
        public int SimVarID { get; set; }

        /// <summary>
        /// name of SimVar
        /// </summary>
        public string SimVarName { get; set; }

        /// <summary>
        /// represents sim variable string known by SimConnect
        /// </summary>
        public string SimVariable { get; set; }

        /// <summary>
        /// unit of sim variable in case if nessecary
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// domain of simVar (engine, avionics, fuel...)
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// current value of simVar
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// value of failure (leak rate, possition of stuck gear...)
        /// </summary>
        public double FailureValue { get; set; }

        /// <summary>
        /// value of SimVar before failure starts (usable for continous failures)
        /// </summary>
        public double BeforeFailureValue { get; set; }


        /// <summary>
        /// if simVar is SimConnect event
        /// </summary>
        public bool IsEvent { get; set; }

        /// <summary>
        /// if simVar is for failures or to read flight parameters
        /// </summary>
        public bool IsFailable { get; set; }


        /// <summary>
        /// if failure type is leak
        /// </summary>
        public bool IsLeak { get; set; }

        /// <summary>
        /// if failure type is stuck
        /// </summary>
        public bool IsStuck { get; set; }

        /// <summary>
        /// if failure type is once complete failure
        /// </summary>
        public bool IsCompleteFail { get; set; }

        /// <summary>
        /// if failure type is countinous changing failure (short circuit)
        /// </summary>
        public bool IsContinousFail { get; set; }


        /// <summary>
        /// if failure already triggered
        /// </summary>
        public bool Failed { get; set; }

        /// <summary>
        /// if failure started (usable for stuck and continous type of failures)
        /// </summary>
        public bool Started { get; set; }

        /// <summary>
        /// random object for turning on and off continous event failures
        /// </summary>
        public Random Random { get; set; }


        /// <summary>
        /// if failure enabled in current preset
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// for Twitch. Price of failure
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// for Twitch. Ammount of commands placed into failure
        /// </summary>
        public int Commands { get; set; }

        /// <summary>
        /// for normal version. Failure percent
        /// </summary>
        public int FailPercent { get; set; }

        public void FillEnums()
        {
            eDef = (DEFINITION)SimVarID;
            eRequest = (REQUEST)SimVarID;
        }

        public void AddCommands(int Com)
        {
            if (Commands >= 0)
            {
                Commands += Com;
            }
        }
    }
}
