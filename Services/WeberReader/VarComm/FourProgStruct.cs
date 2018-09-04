using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class FourProgStruct
		{
			public FourthStepStruct[] FourthStep;

			public FourProgStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				FourthStep = new FourthStepStruct[4];
				for (int i = 0; i < 4; i++)
				{
					FourthStep[i] = new FourthStepStruct();
				}
			}
		}
	}
}