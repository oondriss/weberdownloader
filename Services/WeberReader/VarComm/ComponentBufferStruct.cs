using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class ComponentBufferStruct
		{
			public ushort[] userName;

			public DateTimeStruct Time;

			public ushort[] ComponentOrPartText;

			public ushort[] ReasonText;

			public ushort[] SerialNumber;

			public byte NewEntry;

			public uint PieceCount;

			public uint Cycle;

			public ComponentBufferStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				userName = new ushort[5];
				Time = new DateTimeStruct();
				ComponentOrPartText = new ushort[100];
				ReasonText = new ushort[100];
				SerialNumber = new ushort[31];
				NewEntry = 0;
				PieceCount = 0u;
				Cycle = 0u;
			}
		}
	}
}