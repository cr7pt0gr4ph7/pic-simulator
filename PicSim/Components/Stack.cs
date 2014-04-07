using PCAddress = System.UInt16;

namespace PicSim.Components
{
    /// <summary>
    /// This class represents the program stack of the processor
    /// </summary>
    /// <remarks>
    /// The stack has 8 entries and works as a circular buffer.
    /// Each entry is 13 bits wide (our concrete implementation uses 16 bits).
    /// (See: Manual - Section 4.4 "Memory Organization - Stack", p.23).
    /// </remarks>
    public class Stack
    {
        /// <summary>
        /// The default number of stack entries (Section 4.4 Memory Organization - Stack).
        /// </summary>
        private const int DEFAULT_MAX_STACK = 8;

        /// <summary>
        /// This array stores the entries of the stack.
        /// </summary>
        private readonly PCAddress[] m_stackEntries;

        /// <summary>
        /// The index of the current stack item.
        /// </summary>
        private int m_currentIndex;

        public Stack()
        {
            this.m_stackEntries = new PCAddress[DEFAULT_MAX_STACK];
            this.m_currentIndex = RewrapAddress(-1);
        }

        /// <summary>
        /// Rewrap a computed stack address into the valid stack address space.
        /// </summary>
        /// <param name="address">An address that is possibly outside the valid stack index range.</param>
        /// <returns>An address that is guaranteed to be inside the valid stack index range.</returns>
        private int RewrapAddress(int address)
        {
            // TODO Optimize the performance
            var stackSize  = m_stackEntries.Length;
            while (address < 0) address += stackSize;
            while (address >= stackSize) address -= stackSize;
            return address;
        }

        /// <summary>
        /// Push a program counter address on the stack.
        /// </summary>
        /// <param name="address">The address to be pushed on the stack.</param>
        public void Push(PCAddress address)
        {
            m_currentIndex = RewrapAddress(m_currentIndex + 1);
            m_stackEntries[m_currentIndex] = address;
        }

        /// <summary>
        /// Pop a program address from the stack.
        /// </summary>
        /// <returns>The address popped from the stack.</returns>
        public PCAddress Pop()
        {
            PCAddress result = m_stackEntries[m_currentIndex];
            m_currentIndex = RewrapAddress(m_currentIndex - 1);
            return result;
        }
    }
}
