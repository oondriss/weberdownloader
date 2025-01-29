using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class StorageSystem_Struct
    {
        public byte Signal;

        public StorageSystem_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Signal = 0;
        }
    }
}