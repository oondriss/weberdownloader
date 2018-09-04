using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class Status1_Struct
		{
			public uint RequestID;

			public ushort VisuLive;

			public Status1_Struct()
			{
				Initialize();
			}

			public void Initialize()
			{
				RequestID = 0u;
				VisuLive = 0;
			}
		}
	}
}