using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class ComponentBufferStruct
    {
        public ushort[] userName = new ushort[5];

        public DateTimeStruct Time = new();

        public ushort[] ComponentOrPartText = new ushort[100];

        public ushort[] ReasonText = new ushort[100];

        public ushort[] SerialNumber = new ushort[31];

        public byte NewEntry = 0;

        public uint PieceCount = 0u;

        public uint Cycle = 0u;
    }
}