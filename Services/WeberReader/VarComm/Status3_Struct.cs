using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class Status3_Struct
    {
        public uint RequestID;

        public ushort VisuLive;

        public Status3_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            RequestID = 0u;
            VisuLive = 0;
        }
    }
}