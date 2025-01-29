using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class UserRelatedData_Struct
    {
        public byte UserLevel;

        public byte[] UserName;

        public byte Reserve1;

        public byte Reserve2;

        public byte Reserve3;

        public byte Reserve4;

        public byte Reserve5;

        public byte ReserveSignals;

        public byte[] IpAddress;

        public UserRelatedData_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            UserLevel = 0;
            UserName = new byte[5];
            Reserve1 = 0;
            Reserve2 = 0;
            Reserve3 = 0;
            Reserve4 = 0;
            Reserve5 = 0;
            ReserveSignals = 0;
            IpAddress = new byte[10];
        }
    }
}