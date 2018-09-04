using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class SaveSys_Struct
		{
			public byte RequestBlock;

			public SaveSys_Struct()
			{
				Initialize();
			}

			public void Initialize()
			{
				RequestBlock = 0;
			}
		}
	}
}