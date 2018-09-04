using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class CurveData_Struct
		{
			public CurveDataStruct[] Point;

			public CurveData_Struct()
			{
				Initialize();
			}

			public void Initialize()
			{
				Point = new CurveDataStruct[20000];
				for (int i = 0; i < 20000; i++)
				{
					Point[i] = new CurveDataStruct();
				}
			}
		}
	}
}