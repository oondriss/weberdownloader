using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class ErrorSys_Struct
		{
			public uint Num;

			public ushort[] Text;

			public byte SpindleTest;

			public byte Warning;

			public byte Quit;

			public uint PlcError;

			public ErrorSys_Struct()
			{
				Initialize();
			}

			public void Initialize()
			{
				Num = 0u;
				Text = new ushort[1000];
				SpindleTest = 0;
				Warning = 0;
				Quit = 0;
				PlcError = 0u;
			}
		}
	}
}