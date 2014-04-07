namespace PicSim.Instructions
{
    /// <summary>
    /// "Unconditional branch" instruction.
    /// See "9.1 Instruction Descriptions - GOTO", p.58.
    /// </summary>
    public class GOTO : CallOrGotoInstruction
    {
        protected override void ExecuteCore(ushort address)
        {
            // Set the program counter to the specified address.
            // Note that 'address' is only 11 bits wide, so we have to use the correct PC loading method.
            PC.LoadFrom11Bits(address);
        }
    }
}
