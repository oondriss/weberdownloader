using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class PassCodeStruct
    {
        public ushort[] Name;

        public uint Code;

        public byte Level;

        public PassCodeStruct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Name = new ushort[5];
            Code = 0u;
            Level = 0;
        }
    }
}