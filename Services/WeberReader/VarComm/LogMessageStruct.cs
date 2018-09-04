using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class LogMessageStruct
		{
			public DateTimeStruct Time;

			public uint Code;

			public ushort Type;

			public uint ProgNum;

			public byte Step;

			public float Value1;

			public float Value2;

			public ushort[] userName;

			public uint cycNum;

			public byte UnitIndex;

			public byte res;

			public LogMessageStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				Time = new DateTimeStruct();
				Code = 0u;
				Type = 0;
				ProgNum = 0u;
				Step = 0;
				Value1 = 0f;
				Value2 = 0f;
				userName = new ushort[5];
				cycNum = 0u;
				UnitIndex = 0;
				res = 0;
			}
		}
	}
}