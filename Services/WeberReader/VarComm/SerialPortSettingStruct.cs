using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class SerialPortSettingStruct
		{
			public uint BaudRate;

			public byte Parity;

			public SerialPortSettingStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				BaudRate = 0u;
				Parity = 0;
			}
		}
	}
}