using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.UI.Models
{
    public class FileModel
    {
        private readonly int[] m_pc2codeLine;
        private readonly int[] m_codeLine2pc;

        private readonly string[] m_lines;
        private readonly ushort[] m_opcodes;

        private FileModel(int[] pc2codeLine, int[] codeLine2pc, string[] lines, ushort[] opcodes)
        {
            m_pc2codeLine = pc2codeLine;
            m_codeLine2pc = codeLine2pc;
            m_lines = lines;
            m_opcodes = opcodes;
        }

        #region Properties

        public string[] Lines
        {
            get { return m_lines; }
        }

        public ushort[] Opcodes
        {
            get { return m_opcodes; }
        }

        #endregion

        #region Line number <-> PC address mapping

        /// <summary>
        /// Returns the line number that corresponds to the specified PC address,
        /// or <c>null</c> if no matching line can be found.
        /// </summary>
        /// <param name="pcAddress"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="pcAddress"/> is negative.</exception>
        public int? GetLineForPCAddress(int pcAddress)
        {
            Ensure.ArgumentNotNegative(pcAddress, "pcAddress");

            if (!IsValidPCAddress(pcAddress)) return null;
            return m_pc2codeLine[pcAddress];
        }

        private bool IsValidPCAddress(int pcAddress)
        {
            if (pcAddress < 0) return false;
            if (pcAddress >= m_pc2codeLine.Length) return false;
            return true;
        }

        /// <summary>
        /// Returns the program counter address that corresponds to the specified PC address,
        /// or <c>null</c> if no matching line can be found.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="line"/> is not a valid line number (either negative or too large).</exception>
        public int? GetPCAddressForLine(int line)
        {
            if (!IsValidLineNumber(line)) {
                throw new ArgumentOutOfRangeException("line", line, "Not a valid line number.");
            }
            // NOTE: -1 is used to signal a line that does not contain an opcode
            int result = m_pc2codeLine[line];
            if (result < 0) return null;
            return result;
        }

        private bool IsValidLineNumber(int line)
        {
            if (line < 0) return false;
            if (line >= m_codeLine2pc.Length) return false;
            return true;
        }

        #endregion

        #region File loading

        public static FileModel FromFile(string filePath)
        {
            Ensure.ArgumentNotNullOrEmpty(filePath, "filePath");

            return FileModel.FromLines(File.ReadAllLines(filePath));
        }

        public static FileModel FromLines(string[] lines)
        {
            var parsed = lines.Select(ParseLine).ToArray();

            // TODO Invalid PC addresses are not detected, and lead to invalid behavior
            var pc2codeLine = parsed
                .Select((x, i) => new { Data = x, Index = i })
                .Where(x => x.Data.PCAddress.HasValue)
                .Select(x => x.Index)
                .ToArray();

            var codeLine2PC = parsed.Select(x => x.PCAddress ?? -1).ToArray();

            var opcodes = parsed.Where(x => x.Opcode.HasValue).Select(x => (ushort)x.Opcode).ToArray();

            return new FileModel(pc2codeLine, codeLine2PC, lines, opcodes);
        }

        #endregion

        #region File parsing

        private static LineInfo ParseLine(string line)
        {
            Ensure.ArgumentNotNull(line, "line");

            // If the line is smaller than 9 characters, then it cannot possibly contain a complete opcode
            if (line.Length < 9) return new LineInfo(null, null, line);

            // Format of a line in an .LST file:
            // 0000 2808           00028  $_COLD   goto      $_COLDX
            // +--+ +--+
            // [PC] [OP]           >Line  >Original code
            var pcAddrPart = ParseHexIfNotEmpty(line.Substring(0, 4));
            var opCodePart = ParseHexIfNotEmpty(line.Substring(5, 4));
            var sourceCode = line;

            return new LineInfo(pcAddrPart, opCodePart, sourceCode);
        }

        private static int? ParseHexIfNotEmpty(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            return Int32.Parse(str, NumberStyles.HexNumber);
        }

        private struct LineInfo
        {
            private readonly int? m_pcAddress;
            private readonly int? m_opcode;
            private readonly string m_sourceCode;

            public LineInfo(int? pcAddress, int? opcode, string sourceCode)
            {
                Ensure.ArgumentNotNull(sourceCode, "sourceCode");
                Debug.Assert(pcAddress.HasValue == opcode.HasValue);

                m_pcAddress = pcAddress;
                m_opcode = opcode;
                m_sourceCode = sourceCode;
            }

            public int? PCAddress
            {
                get { return m_pcAddress; }
            }

            public int? Opcode
            {
                get { return m_opcode; }
            }

            public string SourceCode
            {
                get { return m_sourceCode; }
            }
        }

        #endregion
    }
}
