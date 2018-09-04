using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class MaintenanceBufferStruct
		{
			public ushort[] userName;

			public DateTimeStruct Time;

			public DateTimeStruct ScheduledTime;

			public ushort[] MaintenanceText;

			public byte Reminder;

			public byte ReserveByte1;

			public byte ReserveByte2;

			public uint Index;

			public byte BlockNum;

			public byte NewEntry;

			public uint Cycle;

			public uint NextCycle;

			public MaintenanceBufferStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				userName = new ushort[5];
				Time = new DateTimeStruct();
				ScheduledTime = new DateTimeStruct();
				MaintenanceText = new ushort[100];
				Reminder = 1;
				ReserveByte1 = 0;
				ReserveByte2 = 0;
				Index = 0u;
				BlockNum = 0;
				NewEntry = 0;
				Cycle = 0u;
				NextCycle = 0u;
			}
		}
	}
}