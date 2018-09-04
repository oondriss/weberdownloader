using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class PlcLogBookSys_Struct
		{
			public uint ID1;

			public uint Position;

			public uint Length;

			public PlcLogMessageStruct[] plcLogMessBuffer;

			public uint ID2;

			public PlcLogBookSys_Struct()
			{
				Initialize();
			}

			public void Initialize()
			{
				ID1 = 0u;
				Position = 0u;
				Length = 0u;
				plcLogMessBuffer = new PlcLogMessageStruct[4000];
				for (int i = 0; i < 4000; i++)
				{
					plcLogMessBuffer[i] = new PlcLogMessageStruct();
				}

				ID2 = 0u;
			}
		}
	}
}