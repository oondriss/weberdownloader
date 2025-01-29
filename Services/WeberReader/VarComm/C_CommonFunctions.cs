using System;

namespace TestApp.Services.WeberReader.VarComm;

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
        string text = new(array);
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
        string text = new(array);
        return text.Remove(i, letters.Length - i);
    }

    public string ByteToAlphanumericString(byte[] letters)
    {
        char[] array = new char[letters.Length];
        letters.CopyTo(array, 0);
        for (int j = 0; j < letters.Length && letters[j] != 0; j++)
        {
        }
        string text = new(array);
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
        return param switch
        {
            1 => "Torque" + string.Empty,
            3 => "FilteredTorque" + string.Empty,
            2 => "RelativeTorque" + string.Empty,
            11 => "M360Follow" + string.Empty,
            4 => "Gradient" + string.Empty,
            5 => "Angle" + string.Empty,
            6 => "Time" + string.Empty,
            7 => "AnaDepth" + string.Empty,
            10 => "DepthGrad" + string.Empty,
            8 => "AnaSignal" + string.Empty,
            9 => "DigitalSignal" + string.Empty,
            50 => "Torque (FromAbove)",
            51 => "FilteredTorque (FromAbove)",
            52 => "Gradient (FromAbove)",
            53 => "AnaDepth (FromAbove)",
            55 => "DepthGrad (FromAbove)",
            54 => "AnaSignal (FromAbove)",
            1000 => "JumpOkTo" + string.Empty,
            1001 => "JumpNokTo" + string.Empty,
            1002 => "JumpAlwaysTo" + string.Empty,
            1010 => "Stop" + string.Empty,
            1011 => "StopOk" + string.Empty,
            1012 => "StopNok" + string.Empty,
            1020 => "ResetAngle" + string.Empty,
            1030 => "SetDigOut" + string.Empty,
            1040 => "ResetADepth" + string.Empty,
            _ => string.Empty,
        };
    }



    public string GetResName(byte result)
    {
        return result switch
        {
            1 => "Torque",
            2 => "MaxTorque",
            3 => "FilteredTorque",
            4 => "Gradient",
            5 => "Angle",
            6 => "Time",
            7 => "AnaDepth",
            9 => "DigitalSignal",
            12 => "DepthGrad",
            10 => "DelayTorque",
            11 => "M360Follow",
            8 => "AnaSignal",
            _ => "NotValid",
        };
    }

    public string GetUnlocalizedResName(byte result)
    {
        return result switch
        {
            1 => "Torque",
            2 => "MaximumTorque",
            3 => "FilteredTorque",
            4 => "TorqueGradient",
            5 => "Angle",
            6 => "Time",
            7 => "AnalogDepth",
            9 => "DigitalSignal",
            12 => "DepthGradient",
            10 => "Torque@-360°",
            11 => "DelayTorque",
            8 => "AnalogSignal",
            _ => "NotValid",
        };
    }

    public float GetResFactor(byte result)
    {
        return result switch
        {
            1 => 1f,
            2 => 1f,
            3 => 1f,
            4 => 1f,
            5 => 0f,
            6 => 0f,
            7 => 0f,
            9 => 0f,
            12 => 0f,
            10 => 1f,
            11 => 1f,
            8 => 0f,
            _ => 0f,
        };
    }

    public string GetResUnit(byte result, string torqueUnit)
    {
        return result switch
        {
            1 => torqueUnit,
            2 => torqueUnit,
            3 => torqueUnit,
            4 => torqueUnit + "/Degree",
            5 => "Degree",
            6 => "Second",
            7 => "Milimeter",
            9 => "EmptyString",
            12 => "Milimeter/Second",
            10 => torqueUnit,
            11 => torqueUnit,
            8 => "EmptyString",
            _ => "EmptyString",
        };
    }

    public int GetResDigits(byte result)
    {
        return result switch
        {
            1 => 2,
            2 => 2,
            3 => 2,
            4 => 4,
            5 => 1,
            6 => 2,
            7 => 1,
            9 => 0,
            12 => 4,
            10 => 2,
            11 => 2,
            8 => 2,
            _ => 0,
        };
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