using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Notifications
{
    public interface INotifyRegisterChanged
    {
        event EventHandler<RegisterChangedEventArgs> RegisterChanged;
    }
}
