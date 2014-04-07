using System.Collections.Specialized;
using Opcode = System.UInt16;
using PCAddress = System.UInt16;

namespace PicSim.Instructions
{
    /// <summary>
    /// Serves as a base class for the <c>CALL</c> and <c>GOTO</c> instructions,
    /// as detailed in "Figure 9-1: General format for instrutions", p.51.
    /// </summary>
    /// <seealso cref="LiteralOrControlInstruction"/>
    public abstract class CallOrGotoInstruction : Instruction
    {
        private static readonly BitVector32.Section ms_addressSection = BitVector32.CreateSection((2 << 11) - 1);

        protected sealed override void OnExecute(Opcode opcode)
        {
            BitVector32 vector = new BitVector32(opcode);
            ExecuteCore((ushort)vector[ms_addressSection]);
        }

        /// <summary>
        /// Execute this opcode and its associated side effects.
        /// </summary>
        /// <param name="address">The 11-bit immediate value (a program address).</param>
        protected abstract void ExecuteCore(ushort address);
    }
}
