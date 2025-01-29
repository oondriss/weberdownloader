using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class EnableParamStruct
    {
        public byte Torque;

        public byte Snug;

        public byte FTorque;

        public byte GradientMin;

        public byte GradientMax;

        public byte Angle;

        public byte Time;

        public byte ADepth;

        public byte ADepthGradMin;

        public byte ADepthGradMax;

        public byte Ana;

        public byte Release;

        public EnableParamStruct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Torque = 0;
            Snug = 0;
            FTorque = 0;
            GradientMin = 0;
            GradientMax = 0;
            Angle = 0;
            Time = 0;
            ADepth = 0;
            ADepthGradMin = 0;
            ADepthGradMax = 0;
            Ana = 0;
            Release = 0;
        }
    }
}