using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class SubNetMaskStruct
    {
        public byte Byte1;

        public byte Byte2;

        public byte Byte3;

        public byte Byte4;

        public SubNetMaskStruct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Byte1 = 0;
            Byte2 = 0;
            Byte3 = 0;
            Byte4 = 0;
        }
    }
}