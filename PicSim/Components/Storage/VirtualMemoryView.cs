using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Components.Registers;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections;
using PicSim.Components.Notifications;

namespace PicSim.Components.Storage
{
    /// <summary>
    /// A memory view that maps multiple registers and device outputs
    /// to absolute memory addresses.
    /// </summary>
    public class VirtualMemoryView : IMemoryView, IEnumerable<byte>, INotifyCollectionChanged, INotifyMemoryChanged
    {
        private readonly IRegister[] m_registers;

        public VirtualMemoryView(IRegister[] registers)
        {
            Ensure.ArgumentNotNull(registers, "registers");
            m_registers = registers;

            RegisterChangeNotifications();
        }

        public byte this[byte address]
        {
            get { return m_registers[address].Value; }
            set { m_registers[address].Value = value; }
        }

        public int MemorySize
        {
            get { return m_registers.Length; }
        }

        #region Enumerable support

        public IEnumerator<byte> GetEnumerator()
        {
            return new VirtualMemoryEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class VirtualMemoryEnumerator : IEnumerator<byte>
        {
            private readonly VirtualMemoryView m_outer;
            private int m_address = -1;

            public byte Current
            {
                get
                {
                    if (m_address == -1) {
                        throw new InvalidOperationException("Enumeration not started yet.");
                    }
                    if (m_address >= m_outer.MemorySize) {
                        throw new InvalidOperationException("Enumeration already completed.");
                    }
                    return m_outer[(byte)m_address];
                }
            }

            public void Dispose()
            {
                // Not operation required
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (m_address >= m_outer.MemorySize) {
                    throw new InvalidOperationException("Enumeration already completed.");
                }
                m_address++;
                return (m_address < m_outer.MemorySize);
            }

            public void Reset()
            {
                m_address = 0;
            }
        }

        #endregion

        #region Change notifications

        private void RegisterChangeNotifications()
        {
            int len = MemorySize;
            for (int i = 0; i < len; i++) {
                var register = m_registers[i];
                var inrc = register as INotifyRegisterChanged;

                // NOTE: A change to one register might produce changes
                //       at multiple memory locations due to mirrored registers.

                // TODO Use weak event listeners
                if (inrc != null) {
                    ushort memAddr = (ushort)i;
                    inrc.RegisterChanged += (sender, e) => {
                        OnMemoryChanged(new MemoryChangedEventArgs(memAddr, e.NewValue));
                    };
                }
            }
        }

        private void OnMemoryChanged(MemoryChangedEventArgs e)
        {
            var handler = MemoryChanged;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<MemoryChangedEventArgs> MemoryChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var handler = CollectionChanged;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion
    }
}
