using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class StatControl_Struct
    {
        public byte Command;

        public StatControl_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Command = 0;
        }
    }
}