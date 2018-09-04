using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class PLCExternalAnalogSignalStruct
		{
			public byte SetSignal;

			public float Pressure1;

			public float Pressure2;

			public PLCExternalAnalogSignalStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				SetSignal = 0;
				Pressure1 = 0f;
				Pressure2 = 0f;
			}
		}
	}
}