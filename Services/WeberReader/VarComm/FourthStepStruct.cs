using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class FourthStepStruct
		{
			public FourStepAtomStruct[] FourStepAtoms;

			public FourthStepStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				FourStepAtoms = new FourStepAtomStruct[7];
				for (int i = 0; i < 7; i++)
				{
					FourStepAtoms[i] = new FourStepAtomStruct();
				}
			}
		}
	}
}