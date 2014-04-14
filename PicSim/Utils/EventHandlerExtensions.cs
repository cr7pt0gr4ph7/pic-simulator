using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Utils
{
    public static class EventHandlerExtensions
    {
        public static void RaiseIfNotNull<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e)
        {
            if (handler != null) {
                handler(sender, e);
            }
        }
    }
}
