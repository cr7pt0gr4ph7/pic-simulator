﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Communication
{
    public interface ICommunication
    {
        bool Open();
        bool Close();
        bool Reset();

        uint ReadValue();
        bool WriteValue(uint _data);

    }
}
