using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class ProgInfoStruct
    {
        public uint ProgNum;

        public ushort[] Name;

        public byte Steps;

        public byte ResultParam1;

        public byte ResultParam2;

        public byte ResultParam3;

        public ProgInfoStruct()
        {
            Initialize();
        }

        public void Initialize()
        {
            ProgNum = 0u;
            Name = new ushort[32];
            Steps = 0;
            ResultParam1 = 1;
            ResultParam2 = 1;
            ResultParam3 = 1;
        }
    }
}