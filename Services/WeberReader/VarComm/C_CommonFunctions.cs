using System;

namespace TestApp.Services
{
    public class C_CommonFunctions
    {
        public C_CommonFunctions()
        {
        }

        public string UShortToString(ushort[] letters)
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

        public string ByteToAlphanumericString(byte[] letters)
        {
            char[] array = new char[letters.Length];
            letters.CopyTo(array, 0);
            for (int j = 0; j < letters.Length && letters[j] != 0; j++)
            {
            }
            string text = new string(array);
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != '-' && text[i] != '_' && text[i] != ' ' && (!char.IsLetterOrDigit(text, i) || text[i] == 'Ö' || text[i] == 'ö' || text[i] == 'Ä' || text[i] == 'ä' || text[i] == 'Ü' || text[i] == 'ü' || text[i] == 'ß'))
                {
                    text = text.Replace(text[i], ' ');
                }
            }
            return text;
        }

        public void StringToUShort(ref ushort[] array, string text, int length)
        {
            char[] array2 = new char[length];
            Array.Clear(array2, 0, length);
            for (int i = 0; i < length; i++)
            {
                if (text == null)
                {
                    break;
                }
                if (i >= text.Length)
                {
                    break;
                }
                array2[i] = text[i];
            }
            array2.CopyTo(array, 0);
        }

        public void StringToByte(ref byte[] array, string text, int length)
        {
            Array.Clear(array, 0, length);
            for (int i = 0; i < length - 1; i++)
            {
                if (text == null)
                {
                    break;
                }
                if (i >= text.Length)
                {
                    break;
                }
                char value = text[i];
                array[i] = Convert.ToByte(value);
            }
        }

        public string GetSwitchName(ushort param)
        {
            switch (param)
            {
                case 1:
                    return "Torque" + string.Empty;
                case 3:
                    return "FilteredTorque" + string.Empty;
                case 2:
                    return "RelativeTorque" + string.Empty;
                case 11:
                    return "M360Follow" + string.Empty;
                case 4:
                    return "Gradient" + string.Empty;
                case 5:
                    return "Angle" + string.Empty;
                case 6:
                    return "Time" + string.Empty;
                case 7:
                    return "AnaDepth" + string.Empty;
                case 10:
                    return "DepthGrad" + string.Empty;
                case 8:
                    return "AnaSignal" + string.Empty;
                case 9:
                    return "DigitalSignal" + string.Empty;
                case 50:
                    return "Torque (FromAbove)";
                case 51:
                    return "FilteredTorque (FromAbove)";
                case 52:
                    return "Gradient (FromAbove)";
                case 53:
                    return "AnaDepth (FromAbove)";
                case 55:
                    return "DepthGrad (FromAbove)";
                case 54:
                    return "AnaSignal (FromAbove)";
                case 1000:
                    return "JumpOkTo" + string.Empty;
                case 1001:
                    return "JumpNokTo" + string.Empty;
                case 1002:
                    return "JumpAlwaysTo" + string.Empty;
                case 1010:
                    return "Stop" + string.Empty;
                case 1011:
                    return "StopOk" + string.Empty;
                case 1012:
                    return "StopNok" + string.Empty;
                case 1020:
                    return "ResetAngle" + string.Empty;
                case 1030:
                    return "SetDigOut" + string.Empty;
                case 1040:
                    return "ResetADepth" + string.Empty;
                default:
                    return string.Empty;
            }
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

        public string GetUnlocalizedResName(byte result)
        {
            switch (result)
            {
                case 1:
                    return "Torque";
                case 2:
                    return "MaximumTorque";
                case 3:
                    return "FilteredTorque";
                case 4:
                    return "TorqueGradient";
                case 5:
                    return "Angle";
                case 6:
                    return "Time";
                case 7:
                    return "AnalogDepth";
                case 9:
                    return "DigitalSignal";
                case 12:
                    return "DepthGradient";
                case 10:
                    return "Torque@-360°";
                case 11:
                    return "DelayTorque";
                case 8:
                    return "AnalogSignal";
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

        public void RemoveTrailingBlanks(ref string outString, string inString)
        {
            char[] trimChars = new char[1]
            {
                ' '
            };
            outString = inString.TrimEnd(trimChars);
        }
    }
}