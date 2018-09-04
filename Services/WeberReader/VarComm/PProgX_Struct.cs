using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class PProgX_Struct
		{
			public float PProgVersion;

			public ProgStruct[] Num;

			public PProgX_Struct()
			{
				Initialize();
			}

			public void Initialize()
			{
				PProgVersion = 22.12f;
				Num = new ProgStruct[32];
				for (int i = 0; i < 32; i++)
				{
					Num[i] = new ProgStruct();
				}
			}
		}
	}
}