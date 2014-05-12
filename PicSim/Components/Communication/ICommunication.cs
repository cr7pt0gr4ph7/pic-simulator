using System;
namespace PicSim.Components.Communication
{
    public interface ICommunication : IDisposable
    {
        uint ReadValue();
        bool WriteValue(uint _data);

        bool IsConnected { get; }
    }
}
