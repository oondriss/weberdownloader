using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class PLCOutputStruct
		{
			public byte SystemOK;

			public byte ReadyToStart;

			public byte ProcessRunning;

			public byte IO;

			public byte NIO;

			public byte Sync1;

			public byte Sync2;

			public byte PowerEnabled;

			public byte TM1;

			public byte TM2;

			public byte ExtDigIn;

			public byte StorageSignals;

			public float AnaDepthMM;

			public float AnaDepthVolt;

			public float ExtAna;

			public byte LivingSignResponse;

			public byte LivingMonitor;

			public byte UserLevel;

			public byte[] UserName;

			public byte Reserve1;

			public byte Reserve2;

			public byte Reserve3;

			public byte DriveUnitInvers;

			public byte LivingSignEnabled;

			public byte ReserveSignals;

			public byte[] IpAddress;

			public byte MaintenanceCounterReached;

			public byte AdvancedCounterReached;

			public float PressureSpindle;

			public float PressureHolder;

			public float PressureScaleSpindle;

			public float PressureScaleHolder;

			public float Res1;

			public PLCOutputStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				SystemOK = 0;
				ReadyToStart = 0;
				ProcessRunning = 0;
				IO = 0;
				NIO = 0;
				Sync1 = 0;
				Sync2 = 0;
				PowerEnabled = 0;
				TM1 = 0;
				TM2 = 0;
				ExtDigIn = 0;
				StorageSignals = 0;
				AnaDepthMM = 0f;
				AnaDepthVolt = 0f;
				ExtAna = 0f;
				LivingSignResponse = 0;
				LivingMonitor = 0;
				UserLevel = 0;
				UserName = new byte[5];
				Reserve1 = 0;
				Reserve2 = 0;
				Reserve3 = 0;
				DriveUnitInvers = 0;
				LivingSignEnabled = 0;
				ReserveSignals = 0;
				IpAddress = new byte[10];
				MaintenanceCounterReached = 0;
				AdvancedCounterReached = 0;
				PressureSpindle = 0f;
				PressureHolder = 0f;
				PressureScaleSpindle = 0f;
				PressureScaleHolder = 0f;
				Res1 = 0f;
			}
		}
	}
}