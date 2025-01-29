using System;

namespace TestApp.Services;

public partial class VarComm
{
    [Serializable]
    public class SysConst_Struct
    {
        public DateTimeStruct ActualTime;

        public DateTimeStruct SettingTime;

        public ushort[] SystemID;

        public ushort[] IdentServerName;

        public IPAddressStruct IPAddress;

        public uint DHCP;

        public SubNetMaskStruct SubNetMask;

        public IPAddressStruct DefaultGateway;

        public AdvancedWarningStruct[] AdvancedWarnings;

        public byte UnitTorque;

        public SerialPortSettingStruct Com1;

        public PassCodeStruct[] PassCodes;

        public UserLevelStruct UserLevels;

        public ushort[] AreaCode;

        public SysConst_Struct()
        {
            Initialize();
        }

        public void Initialize()
        {
            ActualTime = new DateTimeStruct();
            SettingTime = new DateTimeStruct();
            SystemID = new ushort[20];
            IdentServerName = new ushort[32];
            IPAddress = new IPAddressStruct();
            DHCP = 0u;
            SubNetMask = new SubNetMaskStruct();
            DefaultGateway = new IPAddressStruct();
            AdvancedWarnings = new AdvancedWarningStruct[5];
            for (int i = 0; i < 5; i++)
            {
                AdvancedWarnings[i] = new AdvancedWarningStruct();
            }

            UnitTorque = 0;
            Com1 = new SerialPortSettingStruct();
            PassCodes = new PassCodeStruct[20];
            for (int j = 0; j < 20; j++)
            {
                PassCodes[j] = new PassCodeStruct();
            }

            UserLevels = new UserLevelStruct();
            AreaCode = new ushort[16];
        }
    }
}