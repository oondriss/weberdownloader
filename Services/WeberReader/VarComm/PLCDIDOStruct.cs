using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class PLCDIDOStruct
		{
			public byte DI0_8;

			public byte DO2_8;

			public PLCDIDOStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				DI0_8 = 0;
				DO2_8 = 0;
			}
		}
	}
}