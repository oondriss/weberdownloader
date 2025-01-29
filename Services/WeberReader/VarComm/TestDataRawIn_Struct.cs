using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class TestDataRawIn_Struct
    {
        public uint ISRCount;

        public ushort DI;

        public ushort DO;

        public byte HexSwitch;

        public float Ui0;

        public float Ui1;

        public float Ui2;

        public float Ui3;

        public byte UErr;

        public short Enc0;

        public short Enc1;

        public short Enc3;

        public byte EncErr;

        public ushort GRADCount;

        public float Nact;

        public float Torque1;

        public float Angle1;

        public float Torque2;

        public float Angle2;

        public float ADepth;

        public float ExtAna;

        public byte BatteryOk;

        public TestDataRawIn_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            ISRCount = 0u;
            DI = 0;
            DO = 0;
            HexSwitch = 0;
            Ui0 = 0f;
            Ui1 = 0f;
            Ui2 = 0f;
            Ui3 = 0f;
            UErr = 0;
            Enc0 = 0;
            Enc1 = 0;
            Enc3 = 0;
            EncErr = 0;
            GRADCount = 0;
            Nact = 0f;
            Torque1 = 0f;
            Angle1 = 0f;
            Torque2 = 0f;
            Angle2 = 0f;
            ADepth = 0f;
            ExtAna = 0f;
            BatteryOk = 0;
        }
    }
}