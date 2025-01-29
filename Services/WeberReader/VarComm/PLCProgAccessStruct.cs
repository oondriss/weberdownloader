using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class PLCProgAccessStruct
    {
        public float SpindleTorque;

        public float DriveUnitRpm;

        public byte Signal;

        public byte Length0;

        public byte Length1;

        public byte Length2;

        public uint Address0;

        public uint Address1;

        public uint Address2;

        public byte[] Data0;

        public byte[] Data1;

        public byte[] Data2;

        public PLCProgAccessStruct()
        {
            Initialize();
        }

        public void Initialize()
        {
            SpindleTorque = 0f;
            DriveUnitRpm = 0f;
            Signal = 0;
            Length0 = 0;
            Length1 = 0;
            Length2 = 0;
            Address0 = 0u;
            Address1 = 0u;
            Address2 = 0u;
            Data0 = new byte[4];
            Data1 = new byte[4];
            Data2 = new byte[4];
        }
    }
}