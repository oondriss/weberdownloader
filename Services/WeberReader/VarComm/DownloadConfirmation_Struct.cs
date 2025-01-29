using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class DownloadConfirmation_Struct
    {
        public uint Result;

        public byte[] Info;

        public DownloadConfirmation_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Result = 0u;
            Info = new byte[32];
        }
    }
}