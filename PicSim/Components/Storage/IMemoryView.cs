using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Storage
{
    public interface IMemoryView
    {
        byte this[byte address] { get; set; }
    }
}
