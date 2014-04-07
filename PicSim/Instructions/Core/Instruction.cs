using PicSim.Components;
using PicSim.Execution;
using PicSim.Utils;
using Opcode = System.UInt16;

namespace PicSim.Instructions
{
    /// <summary>
    /// The base class for all instructions.
    /// </summary>
    /// <remarks>
    /// (See: Section 9.0 "Instruction Set Summary" for a list of instructions)
    /// </remarks>
    public abstract class Instruction
    {
        public Processor Processor { get; set; }

        /// <summary>
        /// Execute this opcode and its associated side effects.
        /// </summary>
        /// <param name="opcode">The complete opcode from which the parameters have to be extracted.</param>
        public void Execute(Opcode opcode)
        {
            OnExecute(opcode);
        }

        protected abstract void OnExecute(Opcode opcode);

        #region Helpers

        protected ProgramCounter PC
        {
            get { return Processor.ProgramCounter; }
        }

        protected Stack Stack
        {
            get { return Processor.Stack; }
        }

        protected byte W
        {
            get { return Processor.W; }
            set { Processor.W = value; }
        }

        private void WriteAddress(byte address, byte value)
        {
            Processor.Memory[address] = value;
        }

        protected byte LoadAddress(byte address)
        {
            return Processor.Memory[address];
        }

        protected void CheckZero(byte value)
        {
            Processor.CheckZero(value);
        }

        protected void SetStatus(OperationResult result)
        {
            Processor.SetStatus(result);
        }

        protected void Tick(int instructionCycles)
        {
            Processor.Clock.Tick(instructionCycles);
        }

        protected bool GetBit(byte bit, byte address)
        {
            return LoadAddress(address).GetBit(bit);
        }

        #endregion
    }
}