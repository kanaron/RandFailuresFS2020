using Microsoft.FlightSimulator.SimConnect;
using System;

namespace FailuresCommon
{
    public interface ISimCon
    {
        IntPtr m_hWnd { get; set; }

        string Connect();
        string Disconnect();
        SimConnect GetSimConnect();
        int GetUserSimConnectWinEvent();
        void ReceiveSimConnectMessage();
    }
}