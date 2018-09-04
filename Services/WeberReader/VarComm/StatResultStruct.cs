using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class StatResultStruct
		{
			public uint ProgNum;

			public byte[] ProgName;

			public short IONIO;

			public byte LastStep;

			public DateTimeStruct Time;

			public uint Cycle;

			public float ScrewDuration;

			public byte Valid;

			public sbyte ResultStep1;

			public byte ResultParam1;

			public float Res1;

			public sbyte ResultStep2;

			public byte ResultParam2;

			public float Res2;

			public sbyte ResultStep3;

			public byte ResultParam3;

			public float Res3;

			public byte[] ScrewID;

			public byte res;

			public StatResultStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				ProgNum = 0u;
				ProgName = new byte[32];
				IONIO = 0;
				LastStep = 0;
				Time = new DateTimeStruct();
				Cycle = 0u;
				ScrewDuration = 0f;
				Valid = 0;
				ResultStep1 = 0;
				ResultParam1 = 0;
				Res1 = 0f;
				ResultStep2 = 0;
				ResultParam2 = 0;
				Res2 = 0f;
				ResultStep3 = 0;
				ResultParam3 = 0;
				Res3 = 0f;
				ScrewID = new byte[32];
				res = 0;
			}
		}
	}
}