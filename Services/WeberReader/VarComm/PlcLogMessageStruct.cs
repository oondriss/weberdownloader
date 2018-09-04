using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class PlcLogMessageStruct
		{
			public DateTimeStruct Time;

			public uint Code;

			public ushort Type;

			public uint cycNum;

			public byte UnitIndex;

			public byte ResByte;

			public uint ResUint1;

			public PlcLogMessageStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				Time = new DateTimeStruct();
				Code = 0u;
				Type = 0;
				cycNum = 0u;
				UnitIndex = 0;
				ResByte = 0;
				ResUint1 = 0u;
			}
		}
	}
}