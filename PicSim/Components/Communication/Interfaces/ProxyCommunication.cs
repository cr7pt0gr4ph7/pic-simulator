using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Communication
{
    public class ProxyCommunication : ICommunication
    {
        private ICommunication m_underlying;

        private ICommunication UnderlyingNotNull
        {
            get { return m_underlying ?? NullCommunication.Instance; }
        }

        public void SetUnderlying(Func<ICommunication> newUnderlying)
        {
            var oldUnderlying = UnderlyingNotNull;
            m_underlying = null;
            oldUnderlying.Dispose();         
            m_underlying = newUnderlying();
        }

        #region Proxy methods

        public uint ReadValue()
        {
            return UnderlyingNotNull.ReadValue();
        }

        public bool WriteValue(uint data)
        {
            return UnderlyingNotNull.WriteValue(data);
        }

        public bool IsConnected
        {
            get { return UnderlyingNotNull.IsConnected; }
        }

        public void Dispose()
        {
            m_underlying.Dispose();
            m_underlying = null;
        }

        #endregion
    }
}
