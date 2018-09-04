using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class AdvancedWarningStruct
		{
			public byte[] Name;

			public uint Limit;

			public uint Advance;

			public DateTimeStruct AdvancedWarningTime;

			public uint AdvancedDays;

			public byte EnableAdvancedWarningTime;

			public AdvancedWarningStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				Name = new byte[32];
				Limit = 9999999u;
				Advance = 0u;
				AdvancedWarningTime = new DateTimeStruct();
				AdvancedDays = 0u;
				EnableAdvancedWarningTime = 0;
			}
		}
	}
}