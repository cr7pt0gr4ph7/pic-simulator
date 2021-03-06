﻿using PicSim.Components.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Registers
{
    public interface IRegister : INotifyRegisterChanged
    {
        byte Value { get; set; }
    }
}
