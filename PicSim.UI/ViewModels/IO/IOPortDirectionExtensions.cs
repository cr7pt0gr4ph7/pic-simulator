using System;

namespace PicSim.UI.ViewModels
{
    public static class IOPortDirectionExtensions
    {
        public static IOPortDirection ToIOPortDirection(this bool value)
        {
            return value ? IOPortDirection.Input : IOPortDirection.Output;
        }

        public static bool ToBool(this IOPortDirection value)
        {
            switch (value)
            {
                case IOPortDirection.Input: return true;
                case IOPortDirection.Output: return false;
                default: throw new ArgumentException("Invalid IOPortDirection value: " + value.ToString(), "value");
            }
        }
    }
}
