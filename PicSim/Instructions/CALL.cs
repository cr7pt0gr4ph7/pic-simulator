namespace PicSim.Instructions
{
    /// <summary>
    /// "Call Subroutine" instruction.
    /// See "9.1 Instruction Descriptions - CALL", p.55.
    /// </summary>
    public class CALL : CallOrGotoInstruction
    {
        protected override void ExecuteCore(ushort address)
        {
            // Push the address of the next instruction onto the call stack
            Stack.Push(PC.NextValue);

            // Set the program counter to the specified subroutine address.
            // Note that 'address' is only 11 bits wide, so we have to use the correct PC loading method.
            PC.LoadFrom11Bits(address);
        }
    }
}
