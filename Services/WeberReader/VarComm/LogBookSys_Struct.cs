using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class LogBookSys_Struct
    {
        public uint Position;

        public uint Length;

        public LogMessageStruct[] logMessBuffer;

        public LogBookSys_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Position = 0u;
            Length = 0u;
            logMessBuffer = new LogMessageStruct[2500];
            for (int i = 0; i < 2500; i++)
            {
                logMessBuffer[i] = new LogMessageStruct();
            }
        }
    }
}