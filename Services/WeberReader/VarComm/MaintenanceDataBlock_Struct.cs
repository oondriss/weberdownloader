using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class MaintenanceDataBlock_Struct
		{
			public uint BlockNum;

			public uint LastBlock;

			public uint NextBlock;

			public MaintenanceBufferStruct[] MaintenanceData;

			public MaintenanceDataBlock_Struct()
			{
				Initialize();
			}

			public void Initialize()
			{
				BlockNum = 0u;
				LastBlock = 0u;
				NextBlock = 255u;
				MaintenanceData = new MaintenanceBufferStruct[32];
				for (int i = 0; i < 32; i++)
				{
					MaintenanceData[i] = new MaintenanceBufferStruct();
				}
			}
		}
	}
}