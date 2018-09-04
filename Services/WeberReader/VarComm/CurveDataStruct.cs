using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class CurveDataStruct
		{
			public short Nset;

			public short Nact;

			public float Torque;

			public float FiltTorque;

			public float Angle;

			public byte DDepth;

			public float ADepth;

			public float ADepthGrad;

			public float AExt;

			public float Gradient;

			public byte CurrentStep;

			public CurveDataStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				Nset = 0;
				Nact = 0;
				Torque = 0f;
				FiltTorque = 0f;
				Angle = 0f;
				DDepth = 0;
				ADepth = 0f;
				ADepthGrad = 0f;
				AExt = 0f;
				Gradient = 0f;
				CurrentStep = 0;
			}
		}
	}
}