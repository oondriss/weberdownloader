using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class FourStepAtomStruct
    {
        public byte StepIndex;

        public byte TypeOfData;

        public float Value;

        public float MinValue;

        public float MaxValue;

        public FourStepAtomStruct()
        {
            Initialize();
        }

        public void Initialize()
        {
            StepIndex = 0;
            TypeOfData = 0;
            Value = 0f;
            MinValue = 0f;
            MaxValue = 0f;
        }
    }
}