using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class LastNIOResults_Struct
		{
			public uint ID1;

			public StatResultStruct[] Num;

			public uint ID2;

			public LastNIOResults_Struct()
			{
				Initialize();
			}

			public void Initialize()
			{
				ID1 = 0u;
				Num = new StatResultStruct[100];
				for (int i = 0; i < 100; i++)
				{
					Num[i] = new StatResultStruct();
				}

				ID2 = 0u;
			}
		}
	}
}