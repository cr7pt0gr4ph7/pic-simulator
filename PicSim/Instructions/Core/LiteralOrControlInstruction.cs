using System.Collections.Specialized;
using PicSim.Utils;
using Opcode = System.UInt16;


namespace PicSim.Instructions
{
    /// <summary>
    /// Serves as a base class for literal and control operations (with the exception of <c>CALL</c> and <c>GOTO</c>),
    /// as detailed in "Figure 9-1: General format for instrutions", p.51.
    /// </summary>
    /// <seealso cref="CallOrGotoInstruction"/>
    public abstract class LiteralOrControlInstruction : Instruction
    {
        private static readonly BitVector32.Section ms_literalSection = SectionUtils.ForRange(0, 7);

        protected sealed override void OnExecute(Opcode opcode)
        {
            BitVector32 vector = new BitVector32(opcode);
            Execute((byte)vector[ms_literalSection]);
        }

        /// <summary>
        /// Execute this opcode and its associated side effects.
        /// </summary>
        /// <param name="address">The 8-bit immediate value.</param>
        protected abstract void Execute(byte immediateValue);
    }
}
