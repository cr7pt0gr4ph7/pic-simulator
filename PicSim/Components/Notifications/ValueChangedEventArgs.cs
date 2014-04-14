using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Notifications
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        private readonly T _value;

        public ValueChangedEventArgs(T value)
        {
            _value = value;
        }

        public T Value
        {
            get { return _value; }
        }
    }
}
