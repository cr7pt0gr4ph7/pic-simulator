using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Communication.Interfaces
{
    /// <summary>
    /// An implementation of <see cref="ICommunication"/> that is never connected.
    /// </summary>
    public class NullCommunication : ICommunication
    {
        private static readonly NullCommunication ms_instance = new NullCommunication();

        public static NullCommunication Instance
        {
            get { return ms_instance; }
        }

        public void Dispose()
        {
        }

        public uint ReadValue()
        {
            throw new InvalidOperationException("Not valid for NullCommunication");
        }

        public bool WriteValue(uint _data)
        {
            throw new InvalidOperationException("Not valid for NullCommunication");
        }

        public bool IsConnected
        {
            get { return false; }
        }
    }
}
