using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class ProgStruct
		{
			public byte byteReserve_1;

			public ProgInfoStruct Info;

			public float M1FilterTime;

			public ushort GradientLength;

			public sbyte GradientFilter;

			public float ADepthFilterTime;

			public ushort ADepthGradientLength;

			public float MaxTime;

			public float PressureHolder;

			public byte EndSetDigOut1;

			public byte EndValueDigOut1;

			public byte EndSetDigOut2;

			public byte EndValueDigOut2;

			public byte EndSetSync1;

			public byte EndValueSync1;

			public byte EndSetSync2;

			public byte EndValueSync2;

			public StepStruct[] Step;

			public byte UseLocalJawSettings;

			public byte JawLocalWrittenOnce;

			public float JawOpenDistance;

			public float JawOpenDepthGradMax;

			public float JawOpenDepthGradMin;

			public FourProgStruct FourProg;

			public ProgStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				byteReserve_1 = 0;
				Info = new ProgInfoStruct();
				M1FilterTime = 0f;
				GradientLength = 1;
				GradientFilter = 0;
				ADepthFilterTime = 0f;
				ADepthGradientLength = 1;
				MaxTime = 0f;
				PressureHolder = 0f;
				EndSetDigOut1 = 0;
				EndValueDigOut1 = 0;
				EndSetDigOut2 = 0;
				EndValueDigOut2 = 0;
				EndSetSync1 = 0;
				EndValueSync1 = 0;
				EndSetSync2 = 0;
				EndValueSync2 = 0;
				Step = new StepStruct[25];
				for (int i = 0; i < 25; i++)
				{
					Step[i] = new StepStruct();
				}

				UseLocalJawSettings = 0;
				JawLocalWrittenOnce = 0;
				JawOpenDistance = 99.9f;
				JawOpenDepthGradMax = 100f;
				JawOpenDepthGradMin = 25f;
				FourProg = new FourProgStruct();
			}
		}
	}
}