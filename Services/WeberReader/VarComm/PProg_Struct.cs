using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class PProg_Struct
		{
			public float PProgVersion;

			public ProgStruct[] Num;

			public PProg_Struct()
			{
				Initialize();
			}

			public void Initialize()
			{
				PProgVersion = 22.15f;
				Num = new ProgStruct[1024];
				for (int i = 0; i < 1024; i++)
				{
					Num[i] = new ProgStruct();
				}
			}
		}
	}
}