using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class StepStruct
		{
			public ushort Type;

			public ushort Switch;

			public byte IsResult1;

			public byte IsResult2;

			public byte IsResult3;

			public EnableParamStruct Enable;

			public float NA;

			public float TM;

			public float MP;

			public float Mmin;

			public float Mmax;

			public float MS;

			public float MRP;

			public byte MRStep;

			public byte MRType;

			public float MDelayTime;

			public float MFP;

			public float MFmin;

			public float MFmax;

			public float MGP;

			public float MGmin;

			public float MGmax;

			public float WP;

			public float Wmin;

			public float Wmax;

			public float WN;

			public float TP;

			public float Tmin;

			public float Tmax;

			public float TN;

			public float LP;

			public float Lmin;

			public float Lmax;

			public float LGP;

			public float LGmin;

			public float LGmax;

			public float AnaP;

			public float AnaMin;

			public float AnaMax;

			public sbyte DigP;

			public sbyte DigMin;

			public sbyte DigMax;

			public byte JumpTo;

			public sbyte ModDigOut;

			public float PressureSpindle;

			public byte CountPassMax;

			public byte UserRights;

			public StepStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				Type = 0;
				Switch = 0;
				IsResult1 = 0;
				IsResult2 = 0;
				IsResult3 = 0;
				Enable = new EnableParamStruct();
				NA = 0f;
				TM = 0f;
				MP = 0f;
				Mmin = 0f;
				Mmax = 0f;
				MS = 0f;
				MRP = 0f;
				MRStep = 0;
				MRType = 0;
				MDelayTime = 0f;
				MFP = 0f;
				MFmin = 0f;
				MFmax = 0f;
				MGP = 0f;
				MGmin = 0f;
				MGmax = 0f;
				WP = 0f;
				Wmin = 0f;
				Wmax = 0f;
				WN = 0f;
				TP = 0f;
				Tmin = 0f;
				Tmax = 0f;
				TN = 0f;
				LP = 0f;
				Lmin = 0f;
				Lmax = 0f;
				LGP = 0f;
				LGmin = 0f;
				LGmax = 0f;
				AnaP = 0f;
				AnaMin = 0f;
				AnaMax = 0f;
				DigP = 0;
				DigMin = 0;
				DigMax = 0;
				JumpTo = 0;
				ModDigOut = 0;
				PressureSpindle = 0f;
				CountPassMax = 0;
				UserRights = 0;
			}
		}
	}
}