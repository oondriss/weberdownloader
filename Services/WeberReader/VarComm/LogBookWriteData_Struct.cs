using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class LogBookWriteData_Struct
    {
        public LogMessageWriteBufferStruct[] LogData;

        public LogBookWriteData_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            LogData = new LogMessageWriteBufferStruct[2500];
            for (int i = 0; i < 2500; i++)
            {
                LogData[i] = new LogMessageWriteBufferStruct();
            }
        }
    }
}