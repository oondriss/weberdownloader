using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class LogMessageWriteBufferStruct
    {
        public uint Code;

        public ushort Type;

        public uint ProgNum;

        public byte Step;

        public float Value1;

        public float Value2;

        public ushort[] userName;

        public byte UnitIndex;

        public byte res;

        public LogMessageWriteBufferStruct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Code = 0u;
            Type = 0;
            ProgNum = 0u;
            Step = 0;
            Value1 = 0f;
            Value2 = 0f;
            userName = new ushort[5];
            UnitIndex = 0;
            res = 0;
        }
    }
}