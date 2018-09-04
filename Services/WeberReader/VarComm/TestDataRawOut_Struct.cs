using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class TestDataRawOut_Struct
		{
			public byte Command;

			public ushort DO16;

			public byte DONr;

			public byte DOState;

			public float Uo0;

			public float Uo1;

			public float Uo2;

			public float Uo3;

			public byte ResetEnc0;

			public byte ResetEnc1;

			public byte ResetEnc2;

			public byte ResetEncErr;

			public byte ResetAngle;

			public float STSpeed;

			public byte STEnable;

			public TestDataRawOut_Struct()
			{
				Initialize();
			}

			public void Initialize()
			{
				Command = 0;
				DO16 = 0;
				DONr = 0;
				DOState = 0;
				Uo0 = 0f;
				Uo1 = 0f;
				Uo2 = 0f;
				Uo3 = 0f;
				ResetEnc0 = 0;
				ResetEnc1 = 0;
				ResetEnc2 = 0;
				ResetEncErr = 0;
				ResetAngle = 0;
				STSpeed = 0f;
				STEnable = 0;
			}
		}
	}
}