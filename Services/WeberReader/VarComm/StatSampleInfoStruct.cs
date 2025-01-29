using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class StatSampleInfoStruct
    {
        public ushort Length;

        public ushort Position;

        public StatSampleInfoStruct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Length = 0;
            Position = 0;
        }
    }
}