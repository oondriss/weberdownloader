using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class PLCResultStruct
    {
        public byte UnitTorque;

        public uint ProgNum;

        public short IONIO;

        public byte LastStep;

        public DateTimeStruct Time;

        public uint Cycle;

        public float ScrewDuration;

        public byte ResultStep1;

        public byte ResultParam1;

        public byte ResultStep2;

        public byte ResultParam2;

        public byte ResultStep3;

        public byte ResultParam3;

        public byte Valid;

        public float Res1;

        public float Res2;

        public float Res3;

        public byte[] ScrewID;

        public PLCResultStruct()
        {
            Initialize();
        }

        public void Initialize()
        {
            UnitTorque = 0;
            ProgNum = 0u;
            IONIO = 0;
            LastStep = 0;
            Time = new DateTimeStruct();
            Cycle = 0u;
            ScrewDuration = 0f;
            ResultStep1 = 0;
            ResultParam1 = 0;
            ResultStep2 = 0;
            ResultParam2 = 0;
            ResultStep3 = 0;
            ResultParam3 = 0;
            Valid = 0;
            Res1 = 0f;
            Res2 = 0f;
            Res3 = 0f;
            ScrewID = new byte[32];
        }
    }
}