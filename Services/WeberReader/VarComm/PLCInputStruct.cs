using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class PLCInputStruct
    {
        public byte Automatic;

        public byte Start;

        public byte Quit;

        public byte Sync1;

        public byte Sync2;

        public byte KalDisable;

        public byte TeachAnalogDepth;

        public uint ProgNum;

        public byte[] ScrewID;

        public byte LivingSignRequest;

        public byte Reserve1;

        public byte Reserve2;

        public byte Reserve3;

        public byte AnalogsignalCurve;

        public byte LivingSignEnabled;

        public byte ReserveSignals;

        public byte[] ReserveStr;

        public byte UsingProgName;

        public byte Reserve5;

        public byte[] ProgName;

        public byte[] ExtendedResult;

        public uint PlcError;

        public uint CounterNIO;

        public uint CounterIOTotal;

        public uint CounterNIOTotal;

        public float PressureSpindle;

        public float PressureHolder;

        public float Res1;

        public PLCInputStruct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Automatic = 0;
            Start = 0;
            Quit = 0;
            Sync1 = 0;
            Sync2 = 0;
            KalDisable = 0;
            TeachAnalogDepth = 0;
            ProgNum = 0u;
            ScrewID = new byte[32];
            LivingSignRequest = 0;
            Reserve1 = 0;
            Reserve2 = 0;
            Reserve3 = 0;
            AnalogsignalCurve = 0;
            LivingSignEnabled = 0;
            ReserveSignals = 0;
            ReserveStr = new byte[10];
            UsingProgName = 0;
            Reserve5 = 0;
            ProgName = new byte[32];
            ExtendedResult = new byte[20];
            PlcError = 0u;
            CounterNIO = 0u;
            CounterIOTotal = 0u;
            CounterNIOTotal = 0u;
            PressureSpindle = 0f;
            PressureHolder = 0f;
            Res1 = 0f;
        }
    }
}