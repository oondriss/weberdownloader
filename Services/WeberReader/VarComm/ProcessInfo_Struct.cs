using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class ProcessInfo_Struct
    {
        public ProgInfoStruct[] ProgInfo;

        public ProcessInfo_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            ProgInfo = new ProgInfoStruct[1024];
            for (int i = 0; i < 1024; i++)
            {
                ProgInfo[i] = new ProgInfoStruct();
            }
        }
    }
}