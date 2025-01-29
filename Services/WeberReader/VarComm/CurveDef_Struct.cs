using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class CurveDef_Struct
    {
        public uint Points;

        public float SampleTime;

        public float SpeedSetScale;

        public float SpeedActScale;

        public byte UnitTorque;

        public CurveDef_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Points = 0u;
            SampleTime = 0f;
            SpeedSetScale = 0f;
            SpeedActScale = 0f;
            UnitTorque = 0;
        }
    }
}