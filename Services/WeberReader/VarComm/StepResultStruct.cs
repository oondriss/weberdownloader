using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class StepResultStruct
    {
        public byte Step;

        public short IONIO;

        public byte OrgStep;

        public float Torque;

        public float MaxTorque;

        public float FTorque;

        public float DelayTorque;

        public float M360Follow;

        public float Gradient;

        public float Angle;

        public float Time;

        public float ADepth;

        public float ADepthGrad;

        public float Ana;

        public ushort Dig;

        public StepResultStruct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Step = 0;
            IONIO = 0;
            OrgStep = 0;
            Torque = 0f;
            MaxTorque = 0f;
            FTorque = 0f;
            DelayTorque = 0f;
            M360Follow = 0f;
            Gradient = 0f;
            Angle = 0f;
            Time = 0f;
            ADepth = 0f;
            ADepthGrad = 0f;
            Ana = 0f;
            Dig = 0;
        }
    }
}