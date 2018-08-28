namespace TestApp.Services
{
    public class C_CommonFunctions
	{
		public C_CommonFunctions()
		{
		}

		public string ByteToString(byte[] letters)
		{
			char[] array = new char[letters.Length];
			letters.CopyTo(array, 0);
			int i;
			for (i = 0; i < letters.Length && letters[i] != 0; i++)
			{
			}
			string text = new string(array);
			return text.Remove(i, letters.Length - i);
		}

		public string GetResName(byte result)
		{
			switch (result)
			{
				case 1:
					return "Torque";
				case 2:
					return "MaxTorque";
				case 3:
					return "FilteredTorque";
				case 4:
					return "Gradient";
				case 5:
					return "Angle";
				case 6:
					return "Time";
				case 7:
					return "AnaDepth";
				case 9:
					return "DigitalSignal";
				case 12:
					return "DepthGrad";
				case 10:
					return "DelayTorque";
				case 11:
					return "M360Follow";
				case 8:
					return "AnaSignal";
				default:
					return "NotValid";
			}
		}

		public float GetResFactor(byte result)
		{
			switch (result)
			{
				case 1:
					return 1f;
				case 2:
					return 1f;
				case 3:
					return 1f;
				case 4:
					return 1f;
				case 5:
					return 0f;
				case 6:
					return 0f;
				case 7:
					return 0f;
				case 9:
					return 0f;
				case 12:
					return 0f;
				case 10:
					return 1f;
				case 11:
					return 1f;
				case 8:
					return 0f;
				default:
					return 0f;
			}
		}

		public string GetResUnit(byte result, string torqueUnit)
		{
			switch (result)
			{
				case 1:
					return torqueUnit;
				case 2:
					return torqueUnit;
				case 3:
					return torqueUnit;
				case 4:
					return torqueUnit + "/Degree";
				case 5:
					return "Degree";
				case 6:
					return "Second";
				case 7:
					return "Milimeter";
				case 9:
					return "EmptyString";
				case 12:
					return "Milimeter/Second";
				case 10:
					return torqueUnit;
				case 11:
					return torqueUnit;
				case 8:
					return "EmptyString";
				default:
					return "EmptyString";
			}
		}

		public int GetResDigits(byte result)
		{
			switch (result)
			{
				case 1:
					return 2;
				case 2:
					return 2;
				case 3:
					return 2;
				case 4:
					return 4;
				case 5:
					return 1;
				case 6:
					return 2;
				case 7:
					return 1;
				case 9:
					return 0;
				case 12:
					return 4;
				case 10:
					return 2;
				case 11:
					return 2;
				case 8:
					return 2;
				default:
					return 0;
			}
		}
	}
}