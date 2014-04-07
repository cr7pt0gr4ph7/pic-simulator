using System.Collections.Specialized;
using PicSim.Utils;
using Opcode = System.UInt16;

namespace PicSim.Instructions
{
    /// <summary>
    /// Serves as a base class for byte-oriented file register operations,
    /// as detailed in "Figure 9-1: General format for instrutions", p.51.
    /// </summary>
    public abstract class ByteOrientedInstruction : Instruction
    {
        private static readonly BitVector32.Section ms_fileSection = SectionUtils.ForRange(0, 6);
        private static readonly int ms_destinationBit = (1 << 7);

        protected sealed override void OnExecute(Opcode opcode)
        {
            BitVector32 vector = new BitVector32(opcode);
            Execute(vector[ms_destinationBit], (byte)vector[ms_fileSection]);
        }

        /// <summary>
        /// Execute this opcode and its associated side effects.
        /// </summary>
        /// <param name="bit">The destination bit.</param>
        /// <param name="address">The 7-bit file register address.</param>
        protected abstract void Execute(bool destination, byte address);

        /// <summary>
        /// Write <paramref name="value"/> either to the register addressed by <paramref name="address"/>,
        /// or to the W register, depending on <paramref name="destination"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="destination"></param>
        /// <param name="address"></param>
        protected void WriteDestination(byte value, bool destination, byte address)
        {
            if (destination) {
                // If 'd' is 1 the result is stored back in register 'f':
                Processor.Memory[address] = value;
            } else {
                // If 'd' is 0 the result is stored back in the W register:
                Processor.W = value;
            }
        }

        protected void SetStatusAndWrite(OperationResult result, bool destination, byte address)
        {
            SetStatus(result);
            WriteDestination(result.Value, destination, address);
        }
    }
}
