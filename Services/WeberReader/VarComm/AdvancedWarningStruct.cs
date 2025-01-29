using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class AdvancedWarningStruct
    {
        public byte[] Name = new byte[32];

        public uint Limit = 9999999u;

        public uint Advance = 0u;

        public DateTimeStruct AdvancedWarningTime = new();

        public uint AdvancedDays = 0u;

        public byte EnableAdvancedWarningTime = 0;
    }
}