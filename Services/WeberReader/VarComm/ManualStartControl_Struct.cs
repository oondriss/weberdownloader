using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class ManualStartControl_Struct
    {
        public byte Command;

        public uint ProgNum;

        public ManualStartControl_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Command = 0;
            ProgNum = 0u;
        }
    }
}