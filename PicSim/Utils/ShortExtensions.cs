namespace PicSim.Utils
{
    /// <summary>
    /// Provides extension methods for working with <see cref="System.UInt16"/> (aka <c>ushort</c>).
    /// </summary>
    public static class ShortExtensions
    {
        /// <summary>
        /// Returns the lower byte of an unsigned short value.
        /// </summary>
        /// <param name="value">A <see cref="System.UInt16"/> value.</param>
        /// <returns>The lower byte of <paramref name="value"/>.</returns>
        public static byte GetLowerByte(this ushort value)
        {
            return (byte)(value & 0xFF);
        }

        /// <summary>
        /// Returns the higher byte of an unsigned short value.
        /// </summary>
        /// <param name="value">A <see cref="System.UInt16"/> value.</param>
        /// <returns>The higher byte of <paramref name="value"/>.</returns>
        public static byte GetHigherByte(this ushort value)
        {
            return (byte)((value >> 8) & 0xFF);
        }
    }
}