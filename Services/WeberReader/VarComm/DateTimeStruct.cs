using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class DateTimeStruct
    {
        public ushort Year;

        public byte Month;

        public byte Day;

        public byte Hour;

        public byte Minute;

        public byte Second;

        public DateTimeStruct()
        {
            Initialize();
        }

        public void Initialize()
        {
            Year = 0;
            Month = 0;
            Day = 0;
            Hour = 0;
            Minute = 0;
            Second = 0;
        }
    }
}