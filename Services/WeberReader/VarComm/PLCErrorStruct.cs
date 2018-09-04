using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class PLCErrorStruct
		{
			public uint Num;

			public byte Warning;

			public PLCErrorStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				Num = 0u;
				Warning = 0;
			}
		}
	}
}