using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class Result_Struct
    {
        public ProgStruct Prog;

        public byte UnitTorque;

        public short IONIO;

        public byte LastStep;

        public DateTimeStruct Time;

        public uint Cycle;

        public float ScrewDuration;

        public StepResultStruct[] StepResult;

        public byte Valid;

        public sbyte ResultStep1;

        public float Res1;

        public sbyte ResultStep2;

        public float Res2;

        public sbyte ResultStep3;

        public float Res3;

        public byte[] ScrewID;

        public byte[] ExtendedResult;

        public Result_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Prog = new ProgStruct();
            UnitTorque = 0;
            IONIO = 0;
            LastStep = 0;
            Time = new DateTimeStruct();
            Cycle = 0u;
            ScrewDuration = 0f;
            StepResult = new StepResultStruct[250];
            for (int i = 0; i < 250; i++)
            {
                StepResult[i] = new StepResultStruct();
            }

            Valid = 0;
            ResultStep1 = 0;
            Res1 = 0f;
            ResultStep2 = 0;
            Res2 = 0f;
            ResultStep3 = 0;
            Res3 = 0f;
            ScrewID = new byte[32];
            ExtendedResult = new byte[20];
        }
    }
}