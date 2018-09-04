using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class SpConst_Struct
		{
			public float TorqueSensorScale;

			public float TorqueSensorTolerance;

			public float AngleSensorScale;

			public byte TorqueSensorInvers;

			public byte AngleSensorInvers;

			public byte RedundantSensorActive;

			public float TorqueRedundantTime;

			public float TorqueRedundantTolerance;

			public short AngleRedundantTolerance;

			public float DriveUnitRpm;

			public byte DriveUnitInvers;

			public float DepthSensorScale;

			public float DepthSensorOffset;

			public float DepthSensorOffsetMin;

			public float DepthSensorOffsetMax;

			public float DepthSensorOffsetPreset;

			public byte DepthSensorInvers;

			public float AnaSigScale;

			public float AnaSigOffset;

			public float SpindleTorque;

			public float SpindleGearFactor;

			public float ReleaseSpeed;

			public float FrictionTorque;

			public float FrictionSpeed;

			public byte FrictionTestStartup;

			public byte FrictionTestEMG;

			public float PressureScaleSpindle;

			public float ReserveFloat_1;

			public float PressureScaleHolder;

			public float JawOpenDistance;

			public float JawOpenDepthGradMax;

			public float JawOpenDepthGradMin;

			public byte ReserveByte_1;

			public byte UsbKeyActivated;

			public SpConst_Struct()
			{
				Initialize();
			}

			public void Initialize()
			{
				TorqueSensorScale = 999f;
				TorqueSensorTolerance = 1f;
				AngleSensorScale = 0.25f;
				TorqueSensorInvers = 0;
				AngleSensorInvers = 0;
				RedundantSensorActive = 0;
				TorqueRedundantTime = 0.25f;
				TorqueRedundantTolerance = 3f;
				AngleRedundantTolerance = 10;
				DriveUnitRpm = 1000f;
				DriveUnitInvers = 0;
				DepthSensorScale = 50f;
				DepthSensorOffset = 0f;
				DepthSensorOffsetMin = 0f;
				DepthSensorOffsetMax = 10f;
				DepthSensorOffsetPreset = 0f;
				DepthSensorInvers = 0;
				AnaSigScale = 1f;
				AnaSigOffset = 0f;
				SpindleTorque = 0.1f;
				SpindleGearFactor = 1f;
				ReleaseSpeed = 100f;
				FrictionTorque = 5f;
				FrictionSpeed = 5f;
				FrictionTestStartup = 1;
				FrictionTestEMG = 1;
				PressureScaleSpindle = 1f;
				ReserveFloat_1 = 0f;
				PressureScaleHolder = 1f;
				JawOpenDistance = 3f;
				JawOpenDepthGradMax = 100f;
				JawOpenDepthGradMin = 20f;
				ReserveByte_1 = 0;
				UsbKeyActivated = 0;
			}
		}
	}
}