using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Instructions
{
    public enum AffectedStatus
    {
        /// <summary>
        /// This instruction affects the C, DC and Z status flags.
        /// </summary>
        Arithmetic,

        /// <summary>
        /// This instruction affects the Z status flag only.
        /// </summary>
        Zero,

        /// <summary>
        /// This instruction does not affect the status bits.
        /// </summary>
        None
    }

    public abstract class BinaryByteOrientedInstruction : ByteOrientedInstruction
    {
        protected override void Execute(bool destination, byte address)
        {
            var input = NeedsInputValue ? LoadAddress(address) : (byte)0;
            var result = Compute(input);
            Operations.HandleResult(Processor, result, destination, address);
        }

        protected virtual AffectedStatus AffectedStatus
        {
            get { return AffectedStatus.Zero; }
        }

        protected virtual bool NeedsInputValue
        {
            get { return true; }
        }

        protected abstract byte Compute(byte value);
    }
}
