using System.Collections.Specialized;
using PicSim.Utils;
using Opcode = System.UInt16;

namespace PicSim.Instructions
{
    /// <summary>
    /// Serves as a base class for bit-oriented file register operations,
    /// as detailed in "Figure 9-1: General format for instrutions", p.51.
    /// </summary>
    public abstract class BitOrientedInstruction : Instruction
    {
        private static readonly BitVector32.Section ms_fileSection = SectionUtils.ForRange(0, 6);
        private static readonly BitVector32.Section ms_bitSection = SectionUtils.ForRange(7, 9);

        protected sealed override void OnExecute(Opcode opcode)
        {
            BitVector32 vector = new BitVector32(opcode);
            Execute((byte)vector[ms_bitSection], (byte)vector[ms_fileSection]);
        }

        /// <summary>
        /// Execute this opcode and its associated side effects.
        /// </summary>
        /// <param name="bit">The 3-bit bit address.</param>
        /// <param name="address">The 7-bit file register address.</param>
        protected abstract void Execute(byte bit, byte address);
    }
}
