using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class LogBookWriteControl_Struct
    {
        public byte Command;

        public LogBookWriteControl_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Command = 0;
        }
    }
}