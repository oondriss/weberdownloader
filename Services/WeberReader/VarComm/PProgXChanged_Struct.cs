using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class PProgXChanged_Struct
    {
        public byte[] Changed;

        public PProgXChanged_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Changed = new byte[1024];
        }
    }
}