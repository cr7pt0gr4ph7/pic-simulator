using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Utils;

namespace PicSim.Components.Registers
{
    public class IntconRegister : MemoryCell
    {
        private bool GetBit(byte bitNo)
        {
            return RegisterExtensions.GetBit(this, bitNo);
        }

        private void SetBit(byte bitNo, bool value)
        {
            if (value) RegisterExtensions.SetBit(this, bitNo);
            else RegisterExtensions.ClearBit(this, bitNo);
        }

        public bool RBIF
        {
            get { return GetBit(0); }
            set { SetBit(0, value); }
        }

        public bool INTF
        {
            get { return GetBit(1); }
            set { SetBit(1, value); }
        }

        public bool T0IF
        {
            get { return GetBit(2); }
            set { SetBit(2, value); }
        }

        public bool RBIE
        {
            get { return GetBit(3); }
            set { SetBit(3, value); }
        }

        public bool INTE
        {
            get { return GetBit(4); }
            set { SetBit(4, value); }
        }

        public bool T0IE
        {
            get { return GetBit(5); }
            set { SetBit(5, value); }
        }

        public bool EEIE
        {
            get { return GetBit(6); }
            set { SetBit(6, value); }
        }

        public bool GIE
        {
            get { return GetBit(7); }
            set { SetBit(7, value); }
        }
    }
}
