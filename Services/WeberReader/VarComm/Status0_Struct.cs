using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class Status0_Struct
		{
			public byte AutoMode;

			public byte PowerEnabled;

			public byte ParameterMode;

			public byte TestMode;

			public byte StorageSystemMode;

			public uint OwnerID1;

			public uint OwnerID2;

			public uint OwnerID3;

			public ushort[] Version;

			public uint TestError;

			public ushort[] TestErrorText;

			public Status0_Struct()
			{
				Initialize();
			}

			public void Initialize()
			{
				AutoMode = 0;
				PowerEnabled = 0;
				ParameterMode = 0;
				TestMode = 0;
				StorageSystemMode = 0;
				OwnerID1 = 0u;
				OwnerID2 = 0u;
				OwnerID3 = 0u;
				Version = new ushort[10];
				TestError = 0u;
				TestErrorText = new ushort[1000];
			}
		}
	}
}