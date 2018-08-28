using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace TestApp.Services
{
    public class VarComm
	{
        #region Structs, Classes declarations
        private struct VARCOMMCommand
        {
            public uint ID1;

            public sbyte Direction;

            public ushort Block;

            public byte[] Event;

            public uint ID2;
        }

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

        [Serializable]
        public class AdvancedWarningStruct
        {
            public byte[] Name;

            public uint Limit;

            public uint Advance;

            public DateTimeStruct AdvancedWarningTime;

            public uint AdvancedDays;

            public byte EnableAdvancedWarningTime;

            public AdvancedWarningStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Name = new byte[32];
                Limit = 9999999u;
                Advance = 0u;
                AdvancedWarningTime = new DateTimeStruct();
                AdvancedDays = 0u;
                EnableAdvancedWarningTime = 0;
            }
        }

        [Serializable]
        public class PlcLogMessageStruct
        {
            public DateTimeStruct Time;

            public uint Code;

            public ushort Type;

            public uint cycNum;

            public byte UnitIndex;

            public byte ResByte;

            public uint ResUint1;

            public PlcLogMessageStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Time = new DateTimeStruct();
                Code = 0u;
                Type = 0;
                cycNum = 0u;
                UnitIndex = 0;
                ResByte = 0;
                ResUint1 = 0u;
            }
        }

        [Serializable]
        public class LogMessageStruct
        {
            public DateTimeStruct Time;

            public uint Code;

            public ushort Type;

            public uint ProgNum;

            public byte Step;

            public float Value1;

            public float Value2;

            public ushort[] userName;

            public uint cycNum;

            public byte UnitIndex;

            public byte res;

            public LogMessageStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Time = new DateTimeStruct();
                Code = 0u;
                Type = 0;
                ProgNum = 0u;
                Step = 0;
                Value1 = 0f;
                Value2 = 0f;
                userName = new ushort[5];
                cycNum = 0u;
                UnitIndex = 0;
                res = 0;
            }
        }

        [Serializable]
        public class LogMessageWriteBufferStruct
        {
            public uint Code;

            public ushort Type;

            public uint ProgNum;

            public byte Step;

            public float Value1;

            public float Value2;

            public ushort[] userName;

            public byte UnitIndex;

            public byte res;

            public LogMessageWriteBufferStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Code = 0u;
                Type = 0;
                ProgNum = 0u;
                Step = 0;
                Value1 = 0f;
                Value2 = 0f;
                userName = new ushort[5];
                UnitIndex = 0;
                res = 0;
            }
        }

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

        [Serializable]
        public class EnableParamStruct
        {
            public byte Torque;

            public byte Snug;

            public byte FTorque;

            public byte GradientMin;

            public byte GradientMax;

            public byte Angle;

            public byte Time;

            public byte ADepth;

            public byte ADepthGradMin;

            public byte ADepthGradMax;

            public byte Ana;

            public byte Release;

            public EnableParamStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Torque = 0;
                Snug = 0;
                FTorque = 0;
                GradientMin = 0;
                GradientMax = 0;
                Angle = 0;
                Time = 0;
                ADepth = 0;
                ADepthGradMin = 0;
                ADepthGradMax = 0;
                Ana = 0;
                Release = 0;
            }
        }

        [Serializable]
        public class FourStepAtomStruct
        {
            public byte StepIndex;

            public byte TypeOfData;

            public float Value;

            public float MinValue;

            public float MaxValue;

            public FourStepAtomStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                StepIndex = 0;
                TypeOfData = 0;
                Value = 0f;
                MinValue = 0f;
                MaxValue = 0f;
            }
        }

        [Serializable]
        public class FourthStepStruct
        {
            public FourStepAtomStruct[] FourStepAtoms;

            public FourthStepStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                FourStepAtoms = new FourStepAtomStruct[7];
                for (int i = 0; i < 7; i++)
                {
                    FourStepAtoms[i] = new FourStepAtomStruct();
                }
            }
        }

        [Serializable]
        public class FourProgStruct
        {
            public FourthStepStruct[] FourthStep;

            public FourProgStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                FourthStep = new FourthStepStruct[4];
                for (int i = 0; i < 4; i++)
                {
                    FourthStep[i] = new FourthStepStruct();
                }
            }
        }

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

        [Serializable]
        public class ProgInfoStruct
        {
            public uint ProgNum;

            public ushort[] Name;

            public byte Steps;

            public byte ResultParam1;

            public byte ResultParam2;

            public byte ResultParam3;

            public ProgInfoStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                ProgNum = 0u;
                Name = new ushort[32];
                Steps = 0;
                ResultParam1 = 1;
                ResultParam2 = 1;
                ResultParam3 = 1;
            }
        }

        [Serializable]
        public class ProgStruct
        {
            public byte byteReserve_1;

            public ProgInfoStruct Info;

            public float M1FilterTime;

            public ushort GradientLength;

            public sbyte GradientFilter;

            public float ADepthFilterTime;

            public ushort ADepthGradientLength;

            public float MaxTime;

            public float PressureHolder;

            public byte EndSetDigOut1;

            public byte EndValueDigOut1;

            public byte EndSetDigOut2;

            public byte EndValueDigOut2;

            public byte EndSetSync1;

            public byte EndValueSync1;

            public byte EndSetSync2;

            public byte EndValueSync2;

            public StepStruct[] Step;

            public byte UseLocalJawSettings;

            public byte JawLocalWrittenOnce;

            public float JawOpenDistance;

            public float JawOpenDepthGradMax;

            public float JawOpenDepthGradMin;

            public FourProgStruct FourProg;

            public ProgStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                byteReserve_1 = 0;
                Info = new ProgInfoStruct();
                M1FilterTime = 0f;
                GradientLength = 1;
                GradientFilter = 0;
                ADepthFilterTime = 0f;
                ADepthGradientLength = 1;
                MaxTime = 0f;
                PressureHolder = 0f;
                EndSetDigOut1 = 0;
                EndValueDigOut1 = 0;
                EndSetDigOut2 = 0;
                EndValueDigOut2 = 0;
                EndSetSync1 = 0;
                EndValueSync1 = 0;
                EndSetSync2 = 0;
                EndValueSync2 = 0;
                Step = new StepStruct[25];
                for (int i = 0; i < 25; i++)
                {
                    Step[i] = new StepStruct();
                }

                UseLocalJawSettings = 0;
                JawLocalWrittenOnce = 0;
                JawOpenDistance = 99.9f;
                JawOpenDepthGradMax = 100f;
                JawOpenDepthGradMin = 25f;
                FourProg = new FourProgStruct();
            }
        }

        [Serializable]
        public class StepResultStruct
        {
            public byte Step;

            public short IONIO;

            public byte OrgStep;

            public float Torque;

            public float MaxTorque;

            public float FTorque;

            public float DelayTorque;

            public float M360Follow;

            public float Gradient;

            public float Angle;

            public float Time;

            public float ADepth;

            public float ADepthGrad;

            public float Ana;

            public ushort Dig;

            public StepResultStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Step = 0;
                IONIO = 0;
                OrgStep = 0;
                Torque = 0f;
                MaxTorque = 0f;
                FTorque = 0f;
                DelayTorque = 0f;
                M360Follow = 0f;
                Gradient = 0f;
                Angle = 0f;
                Time = 0f;
                ADepth = 0f;
                ADepthGrad = 0f;
                Ana = 0f;
                Dig = 0;
            }
        }

        [Serializable]
        public class SerialPortSettingStruct
        {
            public uint BaudRate;

            public byte Parity;

            public SerialPortSettingStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                BaudRate = 0u;
                Parity = 0;
            }
        }

        [Serializable]
        public class IPAddressStruct
        {
            public byte Byte1;

            public byte Byte2;

            public byte Byte3;

            public byte Byte4;

            public IPAddressStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Byte1 = 0;
                Byte2 = 0;
                Byte3 = 0;
                Byte4 = 0;
            }
        }

        [Serializable]
        public class SubNetMaskStruct
        {
            public byte Byte1;

            public byte Byte2;

            public byte Byte3;

            public byte Byte4;

            public SubNetMaskStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Byte1 = 0;
                Byte2 = 0;
                Byte3 = 0;
                Byte4 = 0;
            }
        }

        [Serializable]
        public class PassCodeStruct
        {
            public ushort[] Name;

            public uint Code;

            public byte Level;

            public PassCodeStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Name = new ushort[5];
                Code = 0u;
                Level = 0;
            }
        }

        [Serializable]
        public class UserLevelStruct
        {
            public byte UserLevel_BackupForm;

            public byte UserLevel_CheckParamForm;

            public byte UserLevel_CycleCounterForm;

            public byte UserLevel_EditSteps;

            public byte UserLevel_PrgOptParameterForm;

            public byte UserLevel_ProgramOverviewForm;

            public byte UserLevel_SpindleConstantsForm;

            public byte UserLevel_StatisticsLastResForm;

            public byte UserLevel_StepOverviewForm;

            public byte UserLevel_SystemConstantsForm;

            public byte UserLevel_TestIOForm;

            public byte UserLevel_TestMotorSensorForm;

            public byte UserLevel_VisualisationParamForm;

            public byte UserLevel_FourStepEditForm;

            public byte UserLevel_Maintenance;

            public byte UserLevel_HandStartForm;

            public byte UserLevel_BrowserForm;

            public byte UserLevel_Reserve1;

            public byte UserLevel_Reserve2;

            public byte UserLevel_Reserve3;

            public byte UserLevel_Reserve4;

            public byte UserLevel_Reserve5;

            public UserLevelStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                UserLevel_BackupForm = 3;
                UserLevel_CheckParamForm = 2;
                UserLevel_CycleCounterForm = 3;
                UserLevel_EditSteps = 2;
                UserLevel_PrgOptParameterForm = 3;
                UserLevel_ProgramOverviewForm = 2;
                UserLevel_SpindleConstantsForm = 3;
                UserLevel_StatisticsLastResForm = 1;
                UserLevel_StepOverviewForm = 2;
                UserLevel_SystemConstantsForm = 3;
                UserLevel_TestIOForm = 4;
                UserLevel_TestMotorSensorForm = 2;
                UserLevel_VisualisationParamForm = 3;
                UserLevel_FourStepEditForm = 3;
                UserLevel_Maintenance = 3;
                UserLevel_HandStartForm = 3;
                UserLevel_BrowserForm = 1;
                UserLevel_Reserve1 = 0;
                UserLevel_Reserve2 = 0;
                UserLevel_Reserve3 = 0;
                UserLevel_Reserve4 = 0;
                UserLevel_Reserve5 = 0;
            }
        }

        [Serializable]
        public class CurveDataStruct
        {
            public short Nset;

            public short Nact;

            public float Torque;

            public float FiltTorque;

            public float Angle;

            public byte DDepth;

            public float ADepth;

            public float ADepthGrad;

            public float AExt;

            public float Gradient;

            public byte CurrentStep;

            public CurveDataStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Nset = 0;
                Nact = 0;
                Torque = 0f;
                FiltTorque = 0f;
                Angle = 0f;
                DDepth = 0;
                ADepth = 0f;
                ADepthGrad = 0f;
                AExt = 0f;
                Gradient = 0f;
                CurrentStep = 0;
            }
        }

        [Serializable]
        public class StatSampleInfoStruct
        {
            public ushort Length;

            public ushort Position;

            public StatSampleInfoStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Length = 0;
                Position = 0;
            }
        }

        [Serializable]
        public class StatResultStruct
        {
            public uint ProgNum;

            public byte[] ProgName;

            public short IONIO;

            public byte LastStep;

            public DateTimeStruct Time;

            public uint Cycle;

            public float ScrewDuration;

            public byte Valid;

            public sbyte ResultStep1;

            public byte ResultParam1;

            public float Res1;

            public sbyte ResultStep2;

            public byte ResultParam2;

            public float Res2;

            public sbyte ResultStep3;

            public byte ResultParam3;

            public float Res3;

            public byte[] ScrewID;

            public byte res;

            public StatResultStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                ProgNum = 0u;
                ProgName = new byte[32];
                IONIO = 0;
                LastStep = 0;
                Time = new DateTimeStruct();
                Cycle = 0u;
                ScrewDuration = 0f;
                Valid = 0;
                ResultStep1 = 0;
                ResultParam1 = 0;
                Res1 = 0f;
                ResultStep2 = 0;
                ResultParam2 = 0;
                Res2 = 0f;
                ResultStep3 = 0;
                ResultParam3 = 0;
                Res3 = 0f;
                ScrewID = new byte[32];
                res = 0;
            }
        }

        [Serializable]
        public class PLCInputStruct
        {
            public byte Automatic;

            public byte Start;

            public byte Quit;

            public byte Sync1;

            public byte Sync2;

            public byte KalDisable;

            public byte TeachAnalogDepth;

            public uint ProgNum;

            public byte[] ScrewID;

            public byte LivingSignRequest;

            public byte Reserve1;

            public byte Reserve2;

            public byte Reserve3;

            public byte AnalogsignalCurve;

            public byte LivingSignEnabled;

            public byte ReserveSignals;

            public byte[] ReserveStr;

            public byte UsingProgName;

            public byte Reserve5;

            public byte[] ProgName;

            public byte[] ExtendedResult;

            public uint PlcError;

            public uint CounterNIO;

            public uint CounterIOTotal;

            public uint CounterNIOTotal;

            public float PressureSpindle;

            public float PressureHolder;

            public float Res1;

            public PLCInputStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Automatic = 0;
                Start = 0;
                Quit = 0;
                Sync1 = 0;
                Sync2 = 0;
                KalDisable = 0;
                TeachAnalogDepth = 0;
                ProgNum = 0u;
                ScrewID = new byte[32];
                LivingSignRequest = 0;
                Reserve1 = 0;
                Reserve2 = 0;
                Reserve3 = 0;
                AnalogsignalCurve = 0;
                LivingSignEnabled = 0;
                ReserveSignals = 0;
                ReserveStr = new byte[10];
                UsingProgName = 0;
                Reserve5 = 0;
                ProgName = new byte[32];
                ExtendedResult = new byte[20];
                PlcError = 0u;
                CounterNIO = 0u;
                CounterIOTotal = 0u;
                CounterNIOTotal = 0u;
                PressureSpindle = 0f;
                PressureHolder = 0f;
                Res1 = 0f;
            }
        }

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

        [Serializable]
        public class PLCResultStruct
        {
            public byte UnitTorque;

            public uint ProgNum;

            public short IONIO;

            public byte LastStep;

            public DateTimeStruct Time;

            public uint Cycle;

            public float ScrewDuration;

            public byte ResultStep1;

            public byte ResultParam1;

            public byte ResultStep2;

            public byte ResultParam2;

            public byte ResultStep3;

            public byte ResultParam3;

            public byte Valid;

            public float Res1;

            public float Res2;

            public float Res3;

            public byte[] ScrewID;

            public PLCResultStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                UnitTorque = 0;
                ProgNum = 0u;
                IONIO = 0;
                LastStep = 0;
                Time = new DateTimeStruct();
                Cycle = 0u;
                ScrewDuration = 0f;
                ResultStep1 = 0;
                ResultParam1 = 0;
                ResultStep2 = 0;
                ResultParam2 = 0;
                ResultStep3 = 0;
                ResultParam3 = 0;
                Valid = 0;
                Res1 = 0f;
                Res2 = 0f;
                Res3 = 0f;
                ScrewID = new byte[32];
            }
        }

        [Serializable]
        public class PLCErrorStruct
        {
            public uint Num;

            public byte Warning;

            public PLCErrorStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Num = 0u;
                Warning = 0;
            }
        }

        [Serializable]
        public class PLCDIDOStruct
        {
            public byte DI0_8;

            public byte DO2_8;

            public PLCDIDOStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                DI0_8 = 0;
                DO2_8 = 0;
            }
        }

        [Serializable]
        public class PLCProgAccessStruct
        {
            public float SpindleTorque;

            public float DriveUnitRpm;

            public byte Signal;

            public byte Length0;

            public byte Length1;

            public byte Length2;

            public uint Address0;

            public uint Address1;

            public uint Address2;

            public byte[] Data0;

            public byte[] Data1;

            public byte[] Data2;

            public PLCProgAccessStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                SpindleTorque = 0f;
                DriveUnitRpm = 0f;
                Signal = 0;
                Length0 = 0;
                Length1 = 0;
                Length2 = 0;
                Address0 = 0u;
                Address1 = 0u;
                Address2 = 0u;
                Data0 = new byte[4];
                Data1 = new byte[4];
                Data2 = new byte[4];
            }
        }

        [Serializable]
        public class PLCExternalAnalogSignalStruct
        {
            public byte SetSignal;

            public float Pressure1;

            public float Pressure2;

            public PLCExternalAnalogSignalStruct()
            {
                Initialize();
            }

            public void Initialize()
            {
                SetSignal = 0;
                Pressure1 = 0f;
                Pressure2 = 0f;
            }
        }

        [Serializable]
        public class Status0_Struct
        {
            public byte AutoMode;

            public byte PowerEnabled;

            public byte ParameterMode;

            public byte TestMode;

            public byte StorageSystemMode;

            public uint OwnerID1;

            public uint OwnerID2;

            public uint OwnerID3;

            public ushort[] Version;

            public uint TestError;

            public ushort[] TestErrorText;

            public Status0_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                AutoMode = 0;
                PowerEnabled = 0;
                ParameterMode = 0;
                TestMode = 0;
                StorageSystemMode = 0;
                OwnerID1 = 0u;
                OwnerID2 = 0u;
                OwnerID3 = 0u;
                Version = new ushort[10];
                TestError = 0u;
                TestErrorText = new ushort[1000];
            }
        }

        [Serializable]
        public class Status1_Struct
        {
            public uint RequestID;

            public ushort VisuLive;

            public Status1_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                RequestID = 0u;
                VisuLive = 0;
            }
        }

        [Serializable]
        public class Status2_Struct
        {
            public uint RequestID;

            public ushort VisuLive;

            public Status2_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                RequestID = 0u;
                VisuLive = 0;
            }
        }

        [Serializable]
        public class Status3_Struct
        {
            public uint RequestID;

            public ushort VisuLive;

            public Status3_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                RequestID = 0u;
                VisuLive = 0;
            }
        }

        [Serializable]
        public class SaveSys_Struct
        {
            public byte RequestBlock;

            public SaveSys_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                RequestBlock = 0;
            }
        }

        [Serializable]
        public class ErrorSys_Struct
        {
            public uint Num;

            public ushort[] Text;

            public byte SpindleTest;

            public byte Warning;

            public byte Quit;

            public uint PlcError;

            public ErrorSys_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Num = 0u;
                Text = new ushort[1000];
                SpindleTest = 0;
                Warning = 0;
                Quit = 0;
                PlcError = 0u;
            }
        }

        [Serializable]
        public class LogBookSys_Struct
        {
            public uint Position;

            public uint Length;

            public LogMessageStruct[] logMessBuffer;

            public LogBookSys_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Position = 0u;
                Length = 0u;
                logMessBuffer = new LogMessageStruct[2500];
                for (int i = 0; i < 2500; i++)
                {
                    logMessBuffer[i] = new LogMessageStruct();
                }
            }
        }

        [Serializable]
        public class Result_Struct
        {
            public ProgStruct Prog;

            public byte UnitTorque;

            public short IONIO;

            public byte LastStep;

            public DateTimeStruct Time;

            public uint Cycle;

            public float ScrewDuration;

            public StepResultStruct[] StepResult;

            public byte Valid;

            public sbyte ResultStep1;

            public float Res1;

            public sbyte ResultStep2;

            public float Res2;

            public sbyte ResultStep3;

            public float Res3;

            public byte[] ScrewID;

            public byte[] ExtendedResult;

            public Result_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Prog = new ProgStruct();
                UnitTorque = 0;
                IONIO = 0;
                LastStep = 0;
                Time = new DateTimeStruct();
                Cycle = 0u;
                ScrewDuration = 0f;
                StepResult = new StepResultStruct[250];
                for (int i = 0; i < 250; i++)
                {
                    StepResult[i] = new StepResultStruct();
                }

                Valid = 0;
                ResultStep1 = 0;
                Res1 = 0f;
                ResultStep2 = 0;
                Res2 = 0f;
                ResultStep3 = 0;
                Res3 = 0f;
                ScrewID = new byte[32];
                ExtendedResult = new byte[20];
            }
        }

        [Serializable]
        public class LastNIOResults_Struct
        {
            public uint ID1;

            public StatResultStruct[] Num;

            public uint ID2;

            public LastNIOResults_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                ID1 = 0u;
                Num = new StatResultStruct[100];
                for (int i = 0; i < 100; i++)
                {
                    Num[i] = new StatResultStruct();
                }

                ID2 = 0u;
            }
        }

        [Serializable]
        public class PProg_Struct
        {
            public float PProgVersion;

            public ProgStruct[] Num;

            public PProg_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                PProgVersion = 22.15f;
                Num = new ProgStruct[1024];
                for (int i = 0; i < 1024; i++)
                {
                    Num[i] = new ProgStruct();
                }
            }
        }

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

        [Serializable]
        public class ProcessInfo_Struct
        {
            public ProgInfoStruct[] ProgInfo;

            public ProcessInfo_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                ProgInfo = new ProgInfoStruct[1024];
                for (int i = 0; i < 1024; i++)
                {
                    ProgInfo[i] = new ProgInfoStruct();
                }
            }
        }

        [Serializable]
        public class PProgXChanged_Struct
        {
            public byte[] Changed;

            public PProgXChanged_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Changed = new byte[1024];
            }
        }

        [Serializable]
        public class PProgX_Struct
        {
            public float PProgVersion;

            public ProgStruct[] Num;

            public PProgX_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                PProgVersion = 22.12f;
                Num = new ProgStruct[32];
                for (int i = 0; i < 32; i++)
                {
                    Num[i] = new ProgStruct();
                }
            }
        }

        [Serializable]
        public class CurveDef_Struct
        {
            public uint Points;

            public float SampleTime;

            public float SpeedSetScale;

            public float SpeedActScale;

            public byte UnitTorque;

            public CurveDef_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Points = 0u;
                SampleTime = 0f;
                SpeedSetScale = 0f;
                SpeedActScale = 0f;
                UnitTorque = 0;
            }
        }

        [Serializable]
        public class CurveData_Struct
        {
            public CurveDataStruct[] Point;

            public CurveData_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Point = new CurveDataStruct[20000];
                for (int i = 0; i < 20000; i++)
                {
                    Point[i] = new CurveDataStruct();
                }
            }
        }

        [Serializable]
        public class CycleCount_Struct
        {
            public uint ID1;

            public uint Machine;

            public uint Customer;

            public uint[] Count;

            public uint MachineNIO;

            public uint CustomerNIO;

            public uint CountReserve;

            public uint ID2;

            public CycleCount_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                ID1 = 0u;
                Machine = 0u;
                Customer = 0u;
                Count = new uint[5];
                MachineNIO = 0u;
                CustomerNIO = 0u;
                CountReserve = 0u;
                ID2 = 0u;
            }
        }

        [Serializable]
        public class StatSample_Struct
        {
            public uint ID1;

            public StatSampleInfoStruct Info;

            public StatResultStruct[] Data;

            public uint ID2;

            public StatSample_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                ID1 = 0u;
                Info = new StatSampleInfoStruct();
                Data = new StatResultStruct[2000];
                for (int i = 0; i < 2000; i++)
                {
                    Data[i] = new StatResultStruct();
                }

                ID2 = 0u;
            }
        }

        [Serializable]
        public class StatControl_Struct
        {
            public byte Command;

            public StatControl_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Command = 0;
            }
        }

        [Serializable]
        public class TestDataRawIn_Struct
        {
            public uint ISRCount;

            public ushort DI;

            public ushort DO;

            public byte HexSwitch;

            public float Ui0;

            public float Ui1;

            public float Ui2;

            public float Ui3;

            public byte UErr;

            public short Enc0;

            public short Enc1;

            public short Enc3;

            public byte EncErr;

            public ushort GRADCount;

            public float Nact;

            public float Torque1;

            public float Angle1;

            public float Torque2;

            public float Angle2;

            public float ADepth;

            public float ExtAna;

            public byte BatteryOk;

            public TestDataRawIn_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                ISRCount = 0u;
                DI = 0;
                DO = 0;
                HexSwitch = 0;
                Ui0 = 0f;
                Ui1 = 0f;
                Ui2 = 0f;
                Ui3 = 0f;
                UErr = 0;
                Enc0 = 0;
                Enc1 = 0;
                Enc3 = 0;
                EncErr = 0;
                GRADCount = 0;
                Nact = 0f;
                Torque1 = 0f;
                Angle1 = 0f;
                Torque2 = 0f;
                Angle2 = 0f;
                ADepth = 0f;
                ExtAna = 0f;
                BatteryOk = 0;
            }
        }

        [Serializable]
        public class TestDataRawOut_Struct
        {
            public byte Command;

            public ushort DO16;

            public byte DONr;

            public byte DOState;

            public float Uo0;

            public float Uo1;

            public float Uo2;

            public float Uo3;

            public byte ResetEnc0;

            public byte ResetEnc1;

            public byte ResetEnc2;

            public byte ResetEncErr;

            public byte ResetAngle;

            public float STSpeed;

            public byte STEnable;

            public TestDataRawOut_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Command = 0;
                DO16 = 0;
                DONr = 0;
                DOState = 0;
                Uo0 = 0f;
                Uo1 = 0f;
                Uo2 = 0f;
                Uo3 = 0f;
                ResetEnc0 = 0;
                ResetEnc1 = 0;
                ResetEnc2 = 0;
                ResetEncErr = 0;
                ResetAngle = 0;
                STSpeed = 0f;
                STEnable = 0;
            }
        }

        [Serializable]
        public class PLCCommData_Struct
        {
            public PLCInputStruct Input;

            public PLCOutputStruct Output;

            public PLCResultStruct Result;

            public PLCErrorStruct Error;

            public PLCDIDOStruct DI_DO;

            public PLCProgAccessStruct ProgAccess;

            public PLCExternalAnalogSignalStruct ExternalAnalogSignal;

            public PLCCommData_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Input = new PLCInputStruct();
                Output = new PLCOutputStruct();
                Result = new PLCResultStruct();
                Error = new PLCErrorStruct();
                DI_DO = new PLCDIDOStruct();
                ProgAccess = new PLCProgAccessStruct();
                ExternalAnalogSignal = new PLCExternalAnalogSignalStruct();
            }
        }

        [Serializable]
        public class UserRelatedData_Struct
        {
            public byte UserLevel;

            public byte[] UserName;

            public byte Reserve1;

            public byte Reserve2;

            public byte Reserve3;

            public byte Reserve4;

            public byte Reserve5;

            public byte ReserveSignals;

            public byte[] IpAddress;

            public UserRelatedData_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                UserLevel = 0;
                UserName = new byte[5];
                Reserve1 = 0;
                Reserve2 = 0;
                Reserve3 = 0;
                Reserve4 = 0;
                Reserve5 = 0;
                ReserveSignals = 0;
                IpAddress = new byte[10];
            }
        }

        [Serializable]
        public class ManualStartControl_Struct
        {
            public byte Command;

            public uint ProgNum;

            public ManualStartControl_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Command = 0;
                ProgNum = 0u;
            }
        }

        [Serializable]
        public class LogBookWriteControl_Struct
        {
            public byte Command;

            public LogBookWriteControl_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Command = 0;
            }
        }

        [Serializable]
        public class LogBookWriteData_Struct
        {
            public LogMessageWriteBufferStruct[] LogData;

            public LogBookWriteData_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                LogData = new LogMessageWriteBufferStruct[2500];
                for (int i = 0; i < 2500; i++)
                {
                    LogData[i] = new LogMessageWriteBufferStruct();
                }
            }
        }

        [Serializable]
        public class StorageSystem_Struct
        {
            public byte Signal;

            public StorageSystem_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Signal = 0;
            }
        }

        [Serializable]
        public class MaintenanceDataBlock_Struct
        {
            public uint BlockNum;

            public uint LastBlock;

            public uint NextBlock;

            public MaintenanceBufferStruct[] MaintenanceData;

            public MaintenanceDataBlock_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                BlockNum = 0u;
                LastBlock = 0u;
                NextBlock = 255u;
                MaintenanceData = new MaintenanceBufferStruct[32];
                for (int i = 0; i < 32; i++)
                {
                    MaintenanceData[i] = new MaintenanceBufferStruct();
                }
            }
        }

        [Serializable]
        public class ComponentDataBlock_Struct
        {
            public uint BlockNum;

            public uint LastBlock;

            public uint NextBlock;

            public ComponentBufferStruct[] ComponentData;

            public ComponentDataBlock_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                BlockNum = 0u;
                LastBlock = 0u;
                NextBlock = 255u;
                ComponentData = new ComponentBufferStruct[32];
                for (int i = 0; i < 32; i++)
                {
                    ComponentData[i] = new ComponentBufferStruct();
                }
            }
        }

        [Serializable]
        public class DownloadRequest_Struct
        {
            public uint Request;

            public byte[] Info;

            public DownloadRequest_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Request = 0u;
                Info = new byte[32];
            }
        }

        [Serializable]
        public class DownloadConfirmation_Struct
        {
            public uint Result;

            public byte[] Info;

            public DownloadConfirmation_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                Result = 0u;
                Info = new byte[32];
            }
        }

        [Serializable]
        public class PlcLogBookSys_Struct
        {
            public uint ID1;

            public uint Position;

            public uint Length;

            public PlcLogMessageStruct[] plcLogMessBuffer;

            public uint ID2;

            public PlcLogBookSys_Struct()
            {
                Initialize();
            }

            public void Initialize()
            {
                ID1 = 0u;
                Position = 0u;
                Length = 0u;
                plcLogMessBuffer = new PlcLogMessageStruct[4000];
                for (int i = 0; i < 4000; i++)
                {
                    plcLogMessBuffer[i] = new PlcLogMessageStruct();
                }

                ID2 = 0u;
            }
        } 
        #endregion

        #region Properties, Constants, Events
        public const int ADEPTH_UNIT_INC = 0;

        public const int ADEPTH_UNIT_TIME = 1;

        public const int ADEPTHGRAD_SWITCH_END = 1;

        public const int ADEPTHGRAD_SWITCH_PERMANENT = 2;

        public const int ADVANCED_WARNING_LEN = 5;

        public const int AREACODE_LEN = 16;

        public const int CLEAR_COUNT1 = 11;

        public const int CLEAR_COUNT2 = 12;

        public const int CLEAR_COUNT3 = 13;

        public const int CLEAR_COUNT4 = 14;

        public const int CLEAR_COUNT5 = 15;

        public const int CLEAR_CUSTOMERCOUNTER = 1;

        public const int CLEAR_CUSTOMERCOUNTER_NIO = 3;

        public const int CLEAR_SAMPLESTAT = 2;

        public const int CLEAR_SAMPLESTAT_NIO = 4;

        public const int CLEAR_TOTAL_NOK_COUNTER = 5;

        public const int COMMAND_LOGBOOK_ERROR = 255;

        public const int COMMAND_LOGBOOK_WRITE = 1;

        public const int COMMAND_MANUALSTART_ERROR = 255;

        public const int COMMAND_MANUALSTART_RUN = 1;

        public const int COMMAND_MANUALSTART_RUNNING = 2;

        public const int COMMAND_STATERROR = 255;

        public const int COMMAND_TESTERROR = 255;

        public const int COMPONENTBLOCKLEN = 32;

        public const int COMPONENTTEXTLEN = 100;

        public const int CURVEDATASIZE = 20000;

        public const int CURVEVERCTORNUM = 10;

        public const int CYCLECOUNTNAME_LEN = 32;

        public const int DATABASE_RESEVE_SIZE = 990;

        public const int DIGSIG_DIG = 3;

        public const int DIGSIG_DIG_OUT1 = 1;

        public const int DIGSIG_DIG_OUT2 = 2;

        public const int DIGSIG_SYNC_IN1 = 4;

        public const int DIGSIG_SYNC_IN2 = 5;

        public const int DIGSIG_SYNC_OUT1 = 3;

        public const int DIGSIG_SYNC_OUT2 = 4;

        public const int DIGSIG_TM1 = 1;

        public const int DIGSIG_TM2 = 2;

        public const int DOWNLOAD_INFO_LEN = 32;

        public const int ERRORTEXTLEN = 1000;

        public const int EXTENDED_POINTNAME_LEN = 20;

        public const int FOUR_STEPNUM = 4;

        public const int GRADIENT_SWITCH_END = 1;

        public const int GRADIENT_SWITCH_PERMANENT = 2;

        public const int IDENTSERVERNAMELEN = 32;

        public const int LASTNIORESULTNUM = 100;

        public const int LOGBOOK_QUEUE_SIZE = 2500;

        public const int LOGBOOKTYPE_BACKUP = 6;

        public const int LOGBOOKTYPE_ERROR = 1;

        public const int LOGBOOKTYPE_PROG_CHANGE = 2;

        public const int LOGBOOKTYPE_SPINDLE_CONST_CHANGE = 4;

        public const int LOGBOOKTYPE_STANDARD = 0;

        public const int LOGBOOKTYPE_STAT_DELETE = 5;

        public const int LOGBOOKTYPE_SYS_CONST_CHANGE = 3;

        public const int MAINTENANCEBLOCKLEN = 32;

        public const int MAINTENANCETEXTLEN = 100;

        public const int NIO_ABNORMALTERMINATE = 10000;

        public const int NIO_ANAMAX = 81;

        public const int NIO_ANAMIN = 80;

        public const int NIO_COUNTPASSMAX = 10001;

        public const int NIO_DIGMAX = 95;

        public const int NIO_DIGMIN = 94;

        public const int NIO_ERROR = 10002;

        public const int NIO_LGMAX = 76;

        public const int NIO_LGMIN = 75;

        public const int NIO_LMAX = 71;

        public const int NIO_LMIN = 70;

        public const int NIO_MFMAX = 31;

        public const int NIO_MFMIN = 30;

        public const int NIO_MGMAX = 41;

        public const int NIO_MGMIN = 40;

        public const int NIO_MMAX = 11;

        public const int NIO_MMIN = 10;

        public const int NIO_MN = 13;

        public const int NIO_MS = 12;

        public const int NIO_START_ABORTED = 10004;

        public const int NIO_STOP_NIO = 110;

        public const int NIO_SYNC1MAX = 97;

        public const int NIO_SYNC1MIN = 96;

        public const int NIO_SYNC2MAX = 99;

        public const int NIO_SYNC2MIN = 98;

        public const int NIO_TM1MAX = 91;

        public const int NIO_TM1MIN = 90;

        public const int NIO_TM2MAX = 93;

        public const int NIO_TM2MIN = 92;

        public const int NIO_TMAX = 61;

        public const int NIO_TMIN = 60;

        public const int NIO_TPROGMAX = 10003;

        public const int NIO_WMAX = 51;

        public const int NIO_WMIN = 50;

        public const int PASSCODES_NUM = 20;

        public const int PLCLOG_QUEUE_SIZE = 4000;

        public const int PLCLOGBOOKTYPE_DISAPPEAR = 17;

        public const int PLCLOGBOOKTYPE_OCCURE = 16;

        public const int PLCLOGBOOKTYPE_RESTART_SYSTEM = 19;

        public const int PLCLOGBOOKTYPE_WARNING = 18;

        public const int PLCPOSITIONIDLEN = 32;

        public const int PLCPROGDATALEN = 4;

        public const int PROGNAMELEN = 32;

        public const int PROGNUM = 1024;

        public const int PROGNUM_X = 32;

        public const int RESERVETEXTLEN = 10;

        public const int RESULT_EMPTY = 0;

        public const int RESULT_FROM_NORMALSTEP = 0;

        public const int RESULT_FROM_ORGANIZING = 1;

        public const int RESULT_IO = 1;

        public const int RESULT1_IS_VALID = 1;

        public const int RESULT2_IS_VALID = 2;

        public const int RESULT3_IS_VALID = 4;

        public const int RESULTPARAM_ADEPTHGRAD = 12;

        public const int RESULTPARAM_ANA = 8;

        public const int RESULTPARAM_DIG = 9;

        public const int RESULTPARAM_L = 7;

        public const int RESULTPARAM_M = 1;

        public const int RESULTPARAM_M360FOLLOW = 11;

        public const int RESULTPARAM_MF = 3;

        public const int RESULTPARAM_MFDELAY = 10;

        public const int RESULTPARAM_MG = 4;

        public const int RESULTPARAM_MMAX = 2;

        public const int RESULTPARAM_T = 6;

        public const int RESULTPARAM_W = 5;

        public const int SAVEBLOCK_ERROR = 255;

        public const int SAVEBLOCK_LOAD_ALL_SETTINGS = 9;

        public const int SAVEBLOCK_LOAD_CUSTOMERBACKUP1 = 8;

        public const int SAVEBLOCK_LOAD_WEBERBACKUP = 6;

        public const int SAVEBLOCK_PPROG = 3;

        public const int SAVEBLOCK_READ_COMPONENT_FIRST_DATA = 81;

        public const int SAVEBLOCK_READ_COMPONENT_NEXT_DATA = 82;

        public const int SAVEBLOCK_READ_MAINTENANCE_FIRST_DATA = 71;

        public const int SAVEBLOCK_READ_MAINTENANCE_NEXT_DATA = 72;

        public const int SAVEBLOCK_SAVE_CUSTOMERBACKUP1 = 7;

        public const int SAVEBLOCK_SAVE_WEBERBACKUP = 5;

        public const int SAVEBLOCK_SPINDLECONST = 2;

        public const int SAVEBLOCK_SYSCONST = 1;

        public const int SAVEBLOCK_SYSCONST_TIME = 4;

        public const int SAVEBLOCK_USER_RELATED_DATA = 44;

        public const int SAVEBLOCK_WRITE_COMPONENT_DATA = 80;

        public const int SAVEBLOCK_WRITE_MAINTENANCE_DATA = 70;

        public const int SERIALNUMBERLEN = 31;

        public const int SERIALPORT_PARITY_EVEN = 2;

        public const int SERIALPORT_PARITY_NO = 0;

        public const int SERIALPORT_PARITY_ODD = 1;

        public const int SET_TEST_KALSIG = 7;

        public const int SET_TEST_MOTORSPEED = 5;

        public const int SET_TEST_OUTPUTS_ANALOG = 3;

        public const int SET_TEST_OUTPUTS_DO16 = 1;

        public const int SET_TEST_OUTPUTS_DOSINGLE = 2;

        public const int SET_TEST_OUTPUTS_RESET = 4;

        public const int STAT_SAMPLE_SIZE = 2000;

        public const int STEP_ADEPTHGRAD_DOWN = 55;

        public const int STEP_ADEPTHGRAD_UP = 10;

        public const int STEP_ANADEPTH_DOWN = 53;

        public const int STEP_ANADEPTH_UP = 7;

        public const int STEP_ANASIG_DOWN = 54;

        public const int STEP_ANASIG_UP = 8;

        public const int STEP_ANGLE_UP = 5;

        public const int STEP_DIGSIG_UP = 9;

        public const int STEP_EMPTY = 0;

        public const int STEP_FILTTORQUE_DOWN = 51;

        public const int STEP_FILTTORQUE_UP = 3;

        public const int STEP_FRICTIONTEST_L2 = 60002;

        public const int STEP_FRICTIONTEST_L3 = 60003;

        public const int STEP_FRICTIONTEST_R0 = 60000;

        public const int STEP_FRICTIONTEST_R1 = 60001;

        public const int STEP_GRADIENT_DOWN = 52;

        public const int STEP_GRADIENT_UP = 4;

        public const int STEP_JUMP_ALWAYS = 1002;

        public const int STEP_JUMP_NOK = 1001;

        public const int STEP_JUMP_OK = 1000;

        public const int STEP_M360_UP = 11;

        public const int STEP_RELTORQUE_UP = 2;

        public const int STEP_RESET_ANGLE = 1020;

        public const int STEP_RESETADEPTH = 1040;

        public const int STEP_SET_DIGOUT = 1030;

        public const int STEP_STOP = 1010;

        public const int STEP_STOP_NOK = 1012;

        public const int STEP_STOP_OK = 1011;

        public const int STEP_TIME_UP = 6;

        public const int STEP_TORQUE_DOWN = 50;

        public const int STEP_TORQUE_UP = 1;

        public const int STEPATOMS_FOURTH_STEP_NUM = 7;

        public const int STEPNUM = 25;

        public const int STEPRESULTNUM = 250;

        public const int STEPTYPE_DRIVING = 2;

        public const int STEPTYPE_EMPTY = 0;

        public const int STEPTYPE_FINALIZING = 3;

        public const int STEPTYPE_FRICTIONTEST = 10;

        public const int STEPTYPE_ORGANIZING = 1;

        public const int SYSTEMID_LEN = 20;

        public const int TEST_TEACH_ANALOGOFFSET = 6;

        public const int USERNAMELEN = 5;

        public const int VERSIONTEXTLEN = 10;

        public const int STATUS0_BLOCKNUM = 0;

        public const int STATUS1_BLOCKNUM = 1;

        public const int STATUS2_BLOCKNUM = 2;

        public const int STATUS3_BLOCKNUM = 3;

        public const int SAVESYS_BLOCKNUM = 4;

        public const int ERRORSYS_BLOCKNUM = 5;

        public const int LOGBOOKSYS_BLOCKNUM = 7;

        public const int RESULT_BLOCKNUM = 8;

        public const int LASTNIORESULTS_BLOCKNUM = 9;

        public const int PPROG_BLOCKNUM = 10;

        public const int SPCONST_BLOCKNUM = 11;

        public const int SYSCONST_BLOCKNUM = 12;

        public const int PROCESSINFO_BLOCKNUM = 15;

        public const int PPROGXCHANGED_BLOCKNUM = 17;

        public const int PPROGX_BLOCKNUM = 18;

        public const int CURVEDEF_BLOCKNUM = 20;

        public const int CURVEDATA_BLOCKNUM = 21;

        public const int CYCLECOUNT_BLOCKNUM = 30;

        public const int STATSAMPLE_BLOCKNUM = 32;

        public const int STATCONTROL_BLOCKNUM = 33;

        public const int TESTDATARAWIN_BLOCKNUM = 40;

        public const int TESTDATARAWOUT_BLOCKNUM = 41;

        public const int PLCCOMMDATA_BLOCKNUM = 42;

        public const int USERRELATEDDATA_BLOCKNUM = 44;

        public const int MANUALSTARTCONTROL_BLOCKNUM = 50;

        public const int LOGBOOKWRITECONTROL_BLOCKNUM = 60;

        public const int LOGBOOKWRITEDATA_BLOCKNUM = 61;

        public const int STORAGESYSTEM_BLOCKNUM = 63;

        public const int MAINTENANCEDATABLOCK_BLOCKNUM = 70;

        public const int COMPONENTDATABLOCK_BLOCKNUM = 71;

        public const int DOWNLOADREQUEST_BLOCKNUM = 80;

        public const int DOWNLOADCONFIRMATION_BLOCKNUM = 81;

        public const int PLCLOGBOOKSYS_BLOCKNUM = 86;

        private const int Status0_size = 2041;

        private const int Status1_size = 6;

        private const int Status2_size = 6;

        private const int Status3_size = 6;

        private const int SaveSys_size = 1;

        private const int ErrorSys_size = 2011;

        private const int LogBookSys_size = 105008;

        private const int Result_size = 16995;

        private const int LastNIOResults_size = 10608;

        private const int PProg_size = 4513796;

        private const int SpConst_size = 107;

        private const int SysConst_size = 754;

        private const int ProcessInfo_size = 73728;

        private const int PProgXChanged_size = 1024;

        private const int PProgX_size = 141060;

        private const int CurveDef_size = 17;

        private const int CurveData_size = 34;

        private const int CycleCount_size = 48;

        private const int StatSample_size = 212012;

        private const int StatControl_size = 1;

        private const int TestDataRawIn_size = 64;

        private const int TestDataRawOut_size = 31;

        private const int PLCCommData_size = 338;

        private const int UserRelatedData_size = 22;

        private const int ManualStartControl_size = 5;

        private const int LogBookWriteControl_size = 1;

        private const int LogBookWriteData_size = 31;

        private const int StorageSystem_size = 1;

        private const int MaintenanceDataBlock_size = 7724;

        private const int ComponentDataBlock_size = 15628;

        private const int DownloadRequest_size = 36;

        private const int DownloadConfirmation_size = 36;

        private const int PlcLogBookSys_size = 92016;

        private const int VARCOMM_PORT = 23387;

        private const int VARCOMM_EVENTNUM = 255;

        private const int VARCOMM_COMMANDSIZE = 266;

        private const int VARCOMM_SERVER_TO_CLIENT = 0;

        private const int VARCOMM_CLIENT_TO_SERVER = 1;

        private const int VARCOMM_TRANSBUFFERSIZE = 10000000;

        private const int VARCOMM_COMMANDBUFFERSIZE = 366;

        private const int VARCOMM_ERROR_NONE = 1;

        private const int VARCOMM_ERROR_CLOSED = 0;

        private const int VARCOMM_ERROR_TIMEOUT = -1;

        private const int VARCOMM_ERROR_FATAL = -2;

        private const int VARCOMM_BLOCKTIMEOUT = 15000;

        private const int VARCOMM_COMMANDTIMEOUT = 1000;

        private const int VARCOMM_EVENTINVOKATIONPOLLING = 20;

        private const int VARCOMM_BLOCKPOLLING = 10;

        private const int VARCOMM_THREADSLEEPTIME = 20;

        private const uint VARCOMM_COMMANDID = 117575940u;

        private const uint VARCOMM_BLOCKID = 337776900u;

        private const int VARCOMM_MAX_COMMANDS_IN_RECEIVE_BLOCK = 10;

        private const int HIRARCY_LEVELS = 50;

        public bool EnableMessageBoxes = false;

        public uint CurveData_elements;

        public uint LogBookWriteData_elements;

        public Status0_Struct Status0;

        public Status1_Struct Status1;

        public Status2_Struct Status2;

        public Status3_Struct Status3;

        public SaveSys_Struct SaveSys;

        public ErrorSys_Struct ErrorSys;

        public LogBookSys_Struct LogBookSys;

        public Result_Struct Result;

        public LastNIOResults_Struct LastNIOResults;

        public PProg_Struct PProg;

        public SpConst_Struct SpConst;

        public SysConst_Struct SysConst;

        public ProcessInfo_Struct ProcessInfo;

        public PProgXChanged_Struct PProgXChanged;

        public PProgX_Struct PProgX;

        public CurveDef_Struct CurveDef;

        public CurveData_Struct CurveData;

        public CycleCount_Struct CycleCount;

        public StatSample_Struct StatSample;

        public StatControl_Struct StatControl;

        public TestDataRawIn_Struct TestDataRawIn;

        public TestDataRawOut_Struct TestDataRawOut;

        public PLCCommData_Struct PLCCommData;

        public UserRelatedData_Struct UserRelatedData;

        public ManualStartControl_Struct ManualStartControl;

        public LogBookWriteControl_Struct LogBookWriteControl;

        public LogBookWriteData_Struct LogBookWriteData;

        public StorageSystem_Struct StorageSystem;

        public MaintenanceDataBlock_Struct MaintenanceDataBlock;

        public ComponentDataBlock_Struct ComponentDataBlock;

        public DownloadRequest_Struct DownloadRequest;

        public DownloadConfirmation_Struct DownloadConfirmation;

        public PlcLogBookSys_Struct PlcLogBookSys;

        private static byte[] SendBuffer;

        private static byte[] ReadBuffer;

        private static byte[] CommandBuffer;

        private Socket ClientSocket;

        private Thread VCThread;

        private Thread EventThread;

        private int ReceiveBlockNum = -1;

        private bool ReceiveBlockResult;

        private bool[] EventIntern;

        private ILogger log;

        public string TorqueUnitName = "";

        public float TorqueConvert = 1f;

        private byte SaveBlockStatus;

        //private int LifeSign;

        private byte Block1Status;

        private byte Block2Status;

        private byte StatDeleteStatus;

        //private IPAddress ipAddress;
	    public delegate void VARServerEventHandler(object sender, VarServerEventArgs arg);
        public event VARServerEventHandler VARSERVEREVENT_Error;

        public event VARServerEventHandler VARSERVEREVENT_Result;

        public event VARServerEventHandler VARSERVEREVENT_SaveControl;

        public event VARServerEventHandler VARSERVEREVENT_StatControl;

        public event VARServerEventHandler VARSERVEREVENT_StatusAccessControl;

        public event VARServerEventHandler VARSERVEREVENT_StatusAutomatic;
        #endregion

        #region Events methods
        protected void OnVARServerEvent_3(VarServerEventArgs arg)
        {
            VARSERVEREVENT_Error?.Invoke(this, arg);
        }

        protected void OnVARServerEvent_4(VarServerEventArgs arg)
        {
            VARSERVEREVENT_Result?.Invoke(this, arg);
        }

        protected void OnVARServerEvent_2(VarServerEventArgs arg)
        {
            VARSERVEREVENT_SaveControl?.Invoke(this, arg);
        }

        protected void OnVARServerEvent_5(VarServerEventArgs arg)
        {
            VARSERVEREVENT_StatControl?.Invoke(this, arg);
        }

        protected void OnVARServerEvent_0(VarServerEventArgs arg)
        {
            VARSERVEREVENT_StatusAccessControl?.Invoke(this, arg);
        }

        protected void OnVARServerEvent_1(VarServerEventArgs arg)
        {
            VARSERVEREVENT_StatusAutomatic?.Invoke(this, arg);
        }

        private void VSE_SaveControl(object sender, VarServerEventArgs e)
        {
            log.LogDebug("entering VSE_SaveControl", Array.Empty<object>());
            switch (SaveBlockStatus)
            {
                case 0:
                    break;
                case 1:
                    if (!ReceiveVarBlock(4))
                    {
                        log.LogError("Could not receive SafeBlockVerification!", Array.Empty<object>());
                    }
                    SaveBlockStatus = 0;
                    break;
                default:
                    log.LogError("Wrong SaveBlockStatus in VSE_SaveControl() of MainForm!", Array.Empty<object>());
                    SaveBlockStatus = 0;
                    break;
            }
        }

        private void VSE_Error(object sender, VarServerEventArgs e)
        {
            log.LogDebug("entering VSE_Error", Array.Empty<object>());
            log.LogError("VSE_ERROR", e);
        }

        private void VSE_StatusAutomatic(object sender, VarServerEventArgs e)
        {
            log.LogDebug("entering VSE_StatusAutomatic", Array.Empty<object>());
            if (!ReceiveVarBlock(0))
            {
                log.LogError("Could not receive Status0Block!", Array.Empty<object>());
            }
            else
            {
                log.LogInformation("Received Status0Block", Array.Empty<object>());
            }
        }

        private void VSE_StatusAccessControl(object sender, VarServerEventArgs e)
        {
            log.LogDebug("entering VSE_StatusAccessControl", Array.Empty<object>());
            //LifeSign = 0;
            switch (Block1Status)
            {
                case 1:
                    if (!ReceiveVarBlock(0))
                    {
                        Block1Status = 0;
                        log.LogError("Could not receive Status0Block!", Array.Empty<object>());
                        return;
                    }
                    if (Status0.OwnerID1 == Status1.RequestID)
                    {
                        Block1Status = 2;
                    }
                    break;
                case 2:
                    Status1.VisuLive++;
                    if (SendVarBlock(1))
                    {
                        break;
                    }
                    Block1Status = 0;
                    log.LogError("Could not send Status1Block!", Array.Empty<object>());
                    return;
                default:
                    log.LogError("Wrong Block1Status in VSE_StatusAccessControl() of MainForm", Array.Empty<object>());
                    Block1Status = 0;
                    break;
                case 0:
                    break;
            }
            switch (Block2Status)
            {
                case 0:
                    break;
                case 1:
                    if (!ReceiveVarBlock(0))
                    {
                        Block2Status = 0;
                        log.LogError("Could not receive Status0Block!", Array.Empty<object>());
                    }
                    else if (Status0.OwnerID2 == Status2.RequestID)
                    {
                        Block2Status = 2;
                    }
                    break;
                case 2:
                    Status2.VisuLive++;
                    if (!SendVarBlock(2))
                    {
                        Block2Status = 0;
                        log.LogError("Could not receive Status2Block!", Array.Empty<object>());
                    }
                    break;
                default:
                    log.LogError("Wrong Block2Status in VSE_StatusAccessControl() of MainForm", Array.Empty<object>());
                    Block2Status = 0;
                    break;
            }
        }

        private void VSE_StatControl(object sender, VarServerEventArgs e)
        {
            log.LogDebug("entering VSE_StatControl", Array.Empty<object>());
            switch (StatDeleteStatus)
            {
                case 0:
                    break;
                case 1:
                    if (!ReceiveVarBlock(33))
                    {
                        log.LogError("Could not receive StatControlBlock!", Array.Empty<object>());
                    }
                    StatDeleteStatus = 0;
                    break;
                default:
                    log.LogError("Wrong StatDeleteStatus in VSE_StatControl() of MainForm", Array.Empty<object>());
                    StatDeleteStatus = 0;
                    break;
            }
        } 
        #endregion

        public VarComm(ILogger logger, IPAddress ipaddress)
		{
			VARSERVEREVENT_StatusAccessControl += VSE_StatusAccessControl;
			VARSERVEREVENT_SaveControl += VSE_SaveControl;
			VARSERVEREVENT_Error += VSE_Error;
			VARSERVEREVENT_StatControl += VSE_StatControl;
			VARSERVEREVENT_StatusAutomatic += VSE_StatusAutomatic;
			log = logger;

			SendBuffer = new byte[10000000];
			ReadBuffer = new byte[10000000];
			CommandBuffer = new byte[366];
			EventIntern = new bool[255];
			for (int i = 0; i < 255; i++)
			{
				EventIntern[i] = false;
			}

			Status0 = new Status0_Struct();
			Status1 = new Status1_Struct();
			Status2 = new Status2_Struct();
			Status3 = new Status3_Struct();
			SaveSys = new SaveSys_Struct();
			ErrorSys = new ErrorSys_Struct();
			LogBookSys = new LogBookSys_Struct();
			Result = new Result_Struct();
			LastNIOResults = new LastNIOResults_Struct();
			PProg = new PProg_Struct();
			SpConst = new SpConst_Struct();
			SysConst = new SysConst_Struct();
			ProcessInfo = new ProcessInfo_Struct();
			PProgXChanged = new PProgXChanged_Struct();
			PProgX = new PProgX_Struct();
			CurveDef = new CurveDef_Struct();
			CurveData = new CurveData_Struct();
			CycleCount = new CycleCount_Struct();
			StatSample = new StatSample_Struct();
			StatControl = new StatControl_Struct();
			TestDataRawIn = new TestDataRawIn_Struct();
			TestDataRawOut = new TestDataRawOut_Struct();
			PLCCommData = new PLCCommData_Struct();
			UserRelatedData = new UserRelatedData_Struct();
			ManualStartControl = new ManualStartControl_Struct();
			LogBookWriteControl = new LogBookWriteControl_Struct();
			LogBookWriteData = new LogBookWriteData_Struct();
			StorageSystem = new StorageSystem_Struct();
			MaintenanceDataBlock = new MaintenanceDataBlock_Struct();
			ComponentDataBlock = new ComponentDataBlock_Struct();
			DownloadRequest = new DownloadRequest_Struct();
			DownloadConfirmation = new DownloadConfirmation_Struct();
			PlcLogBookSys = new PlcLogBookSys_Struct();

			StartupVarConnection(ipaddress);
			if (!ReceiveVarBlock(32))
			{
				log.LogError("Could not receive StatSampleBlock!", Array.Empty<object>());
			}
		}

        #region Connection methods
        private void StartupVarConnection(IPAddress ip)
        {
            string text = string.Empty;
            char[] separator = new char[1]
            {
                '/'
            };
            CloseConnection();

            string[] array = text.Split(separator);
            if (ConnectToServer(ip))
            {
                if (ReceiveVarBlock(12))
                {
                    switch (SysConst.UnitTorque)
                    {
                        case 0:
                            TorqueConvert = 1f;
                            TorqueUnitName = "TorqueNm";
                            break;
                        case 1:
                            TorqueConvert = 100f;
                            TorqueUnitName = "TorqueNcm";
                            break;
                        case 2:
                            TorqueConvert = 8.850745f;
                            TorqueUnitName = "Torqueinlb";
                            break;
                        case 3:
                            TorqueConvert = 0.7375621f;
                            TorqueUnitName = "Torqueftlb";
                            break;
                        case 4:
                            TorqueConvert = 141.6119f;
                            TorqueUnitName = "Torqueinoz";
                            break;
                        case 5:
                            TorqueConvert = 0.1019716f;
                            TorqueUnitName = "Torquekgm";
                            break;
                        case 6:
                            TorqueConvert = 10.19716f;
                            TorqueUnitName = "Torquekgcm";
                            break;
                        default:
                            log.LogError("Wrong SysConst.UnitTorque in StartupVarConnection() of VarComm", Array.Empty<object>());
                            TorqueConvert = 1f;
                            TorqueUnitName = "Nm";
                            break;
                    }
                }
                else
                {
                    TorqueConvert = 1f;
                    TorqueUnitName = "Nm";
                    log.LogError("Could not receive SysConstBlock!", Array.Empty<object>());
                }
                if (!ReceiveVarBlock(0))
                {
                    log.LogError("Could not receive Status0Block!", Array.Empty<object>());
                }
            }
            else
            {
                log.LogError("no connection to ipaddress {0}", ip.ToString());
            }
        }

        public bool ConnectToServer(IPAddress ip)
        {
            if (ClientSocket != null && ClientSocket.Connected)
            {
                log.LogError("Client has already a connection to the server!\nOnly one connection is allowed.");

                return false;
            }

            try
            {
                IPEndPoint remoteEP = new IPEndPoint(ip, 23387);
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientSocket.Connect(remoteEP);
            }
            catch (Exception)
            {
                return false;
            }

            ClientSocket.Blocking = false;
            VCThread = new Thread(VCThreadFunc);
            VCThread.Start();
            EventThread = new Thread(EventThreadFunc);
            EventThread.Start();
            return true;
        }

        public void CloseConnection()
        {
            if (EventThread != null && EventThread.IsAlive)
            {
                EventThread.Abort();
                EventThread = null;
            }

            if (VCThread != null && VCThread.IsAlive)
            {
                VCThread.Abort();
                VCThread = null;
            }

            if (ClientSocket != null && ClientSocket.Connected)
            {
                try
                {
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close();
                }
                catch (Exception ex)
                {
                    log.LogError(ex, ex.Message);
                }
                finally
                {
                    ClientSocket = null;
                }
            }

            SendBuffer = null;
            CommandBuffer = null;
            ReadBuffer = null;
            Status0 = null;
            Status1 = null;
            Status2 = null;
            Status3 = null;
            SaveSys = null;
            ErrorSys = null;
            LogBookSys = null;
            Result = null;
            LastNIOResults = null;
            PProg = null;
            SpConst = null;
            SysConst = null;
            ProcessInfo = null;
            PProgXChanged = null;
            PProgX = null;
            CurveDef = null;
            CurveData = null;
            CycleCount = null;
            StatSample = null;
            StatControl = null;
            TestDataRawIn = null;
            TestDataRawOut = null;
            PLCCommData = null;
            UserRelatedData = null;
            ManualStartControl = null;
            LogBookWriteControl = null;
            LogBookWriteData = null;
            StorageSystem = null;
            MaintenanceDataBlock = null;
            ComponentDataBlock = null;
            DownloadRequest = null;
            DownloadConfirmation = null;
            PlcLogBookSys = null;
        }
        #endregion

        #region Receive/Send methods
        private int ProcessCommand(bool normal)
        {
            VARCOMMCommand vARCOMMCommand = default(VARCOMMCommand);
            vARCOMMCommand.ID1 = 0u;
            vARCOMMCommand.Direction = 0;
            vARCOMMCommand.Event = new byte[255];
            for (int i = 0; i < 255; i++)
            {
                vARCOMMCommand.Event[i] = 0;
            }

            vARCOMMCommand.Block = 0;
            vARCOMMCommand.ID2 = 0u;
            int len = (!normal) ? 262 : 266;
            switch (SocketReadCommand(ClientSocket, len, CommandBuffer))
            {
                case 1:
                    {
                        MemoryStream memoryStream = new MemoryStream(CommandBuffer);
                        BinaryReader binaryReader = new BinaryReader(memoryStream);
                        if (normal)
                        {
                            vARCOMMCommand.ID1 = binaryReader.ReadUInt32();
                        }
                        else
                        {
                            vARCOMMCommand.ID1 = 117575940u;
                        }

                        vARCOMMCommand.Direction = binaryReader.ReadSByte();
                        vARCOMMCommand.Block = binaryReader.ReadUInt16();
                        for (int i = 0; i < 255; i++)
                        {
                            vARCOMMCommand.Event[i] = binaryReader.ReadByte();
                        }

                        vARCOMMCommand.ID2 = binaryReader.ReadUInt32();
                        binaryReader.Close();
                        memoryStream.Close();
                        if (vARCOMMCommand.ID1 == 117575940 && vARCOMMCommand.ID2 == 4177391355u)
                        {
                            for (int i = 0; i < 255; i++)
                            {
                                if (vARCOMMCommand.Event[i] > 0)
                                {
                                    EventIntern[i] = true;
                                }
                            }
                        }

                        break;
                    }
                case -2:
                case 0:
                    return -1;
            }

            return 0;
        }

        public bool ReceiveVarBlock(ushort block)
        {
            while (ReceiveBlockNum != -1)
            {
                Thread.Sleep(50);
            }

            ReceiveBlockNum = block;
            int num = 0;
            while (ReceiveBlockNum != -1)
            {
                if (num++ <= 300)
                {
                    Thread.Sleep(50);
                    continue;
                }

                ReceiveBlockNum = -1;
                ReceiveBlockResult = false;
                break;
            }

            return ReceiveBlockResult;
        }

        private bool ReceiveVarBlockIntern(ushort block)
        {
            int[] array = new int[50];
            int num = 0;
            bool result = false;
            if (ClientSocket != null && ClientSocket.Connected)
            {
                VARCOMMCommand vARCOMMCommand = default(VARCOMMCommand);
                vARCOMMCommand.ID1 = 117575940u;
                vARCOMMCommand.Direction = 0;
                vARCOMMCommand.Event = new byte[255];
                for (int i = 0; i < 255; i++)
                {
                    vARCOMMCommand.Event[i] = 0;
                }

                vARCOMMCommand.Block = block;
                vARCOMMCommand.ID2 = 4177391355u;
                MemoryStream memoryStream = new MemoryStream(ReadBuffer);
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(vARCOMMCommand.ID1);
                binaryWriter.Write(vARCOMMCommand.Direction);
                binaryWriter.Write(vARCOMMCommand.Block);
                for (int i = 0; i < 255; i++)
                {
                    binaryWriter.Write(vARCOMMCommand.Event[i]);
                }

                binaryWriter.Write(vARCOMMCommand.ID2);
                Monitor.Enter(ClientSocket);
                int num2 = SocketWrite(ClientSocket, (int)memoryStream.Position, 1000, ReadBuffer);
                Monitor.Exit(ClientSocket);
                if (num2 == 1)
                {
                    binaryWriter.Close();
                    memoryStream.Close();
                    while (true)
                    {
                        num2 = SocketRead(ClientSocket, 4, 15000, ReadBuffer);
                        if (num2 == 1)
                        {
                            memoryStream = new MemoryStream(ReadBuffer);
                            BinaryReader binaryReader = new BinaryReader(memoryStream);
                            uint num3 = binaryReader.ReadUInt32();
                            binaryReader.Close();
                            memoryStream.Close();
                            if (num3 == 117575940)
                            {
                                num2 = ProcessCommand(false);
                                num++;
                                if (num > 10)
                                {
                                    log.LogError("Data block could not be received!");

                                    break;
                                }
                            }

                            if (num3 == 117575940)
                            {
                                continue;
                            }

                            num2 = SocketRead(ClientSocket, 4, 15000, ReadBuffer);
                            if (num2 == 1)
                            {
                                memoryStream = new MemoryStream(ReadBuffer);
                                binaryReader = new BinaryReader(memoryStream);
                                uint num4 = binaryReader.ReadUInt32();
                                binaryReader.Close();
                                memoryStream.Close();
                                uint num5;
                                switch (block)
                                {
                                    case 0:
                                        num5 = 2041u;
                                        goto IL_04c8;
                                    case 1:
                                        num5 = 6u;
                                        goto IL_04c8;
                                    case 2:
                                        num5 = 6u;
                                        goto IL_04c8;
                                    case 3:
                                        num5 = 6u;
                                        goto IL_04c8;
                                    case 4:
                                        num5 = 1u;
                                        goto IL_04c8;
                                    case 5:
                                        num5 = 2011u;
                                        goto IL_04c8;
                                    case 7:
                                        num5 = 105008u;
                                        goto IL_04c8;
                                    case 8:
                                        num5 = 16995u;
                                        goto IL_04c8;
                                    case 9:
                                        num5 = 10608u;
                                        goto IL_04c8;
                                    case 10:
                                        num5 = 4513796u;
                                        goto IL_04c8;
                                    case 11:
                                        num5 = 107u;
                                        goto IL_04c8;
                                    case 12:
                                        num5 = 754u;
                                        goto IL_04c8;
                                    case 15:
                                        num5 = 73728u;
                                        goto IL_04c8;
                                    case 17:
                                        num5 = 1024u;
                                        goto IL_04c8;
                                    case 18:
                                        num5 = 141060u;
                                        goto IL_04c8;
                                    case 20:
                                        num5 = 17u;
                                        goto IL_04c8;
                                    case 21:
                                        num5 = num4 * 34;
                                        CurveData_elements = num4;
                                        goto IL_04c8;
                                    case 30:
                                        num5 = 48u;
                                        goto IL_04c8;
                                    case 32:
                                        num5 = 212012u;
                                        goto IL_04c8;
                                    case 33:
                                        num5 = 1u;
                                        goto IL_04c8;
                                    case 40:
                                        num5 = 64u;
                                        goto IL_04c8;
                                    case 41:
                                        num5 = 31u;
                                        goto IL_04c8;
                                    case 42:
                                        num5 = 338u;
                                        goto IL_04c8;
                                    case 44:
                                        num5 = 22u;
                                        goto IL_04c8;
                                    case 50:
                                        num5 = 5u;
                                        goto IL_04c8;
                                    case 60:
                                        num5 = 1u;
                                        goto IL_04c8;
                                    case 61:
                                        num5 = num4 * 31;
                                        LogBookWriteData_elements = num4;
                                        goto IL_04c8;
                                    case 63:
                                        num5 = 1u;
                                        goto IL_04c8;
                                    case 70:
                                        num5 = 7724u;
                                        goto IL_04c8;
                                    case 71:
                                        num5 = 15628u;
                                        goto IL_04c8;
                                    case 80:
                                        num5 = 36u;
                                        goto IL_04c8;
                                    case 81:
                                        num5 = 36u;
                                        goto IL_04c8;
                                    case 86:
                                        num5 = 92016u;
                                        goto IL_04c8;
                                    default:
                                        {
                                            log.LogError("Implementation to determine Size of Block {0} is missing!", block.ToString());

                                            result = false;
                                            break;
                                        }
                                        IL_04c8:
                                        num2 = SocketRead(ClientSocket, (int)(num5 + 4), 15000, ReadBuffer);
                                        if (num2 == 1)
                                        {
                                            memoryStream = new MemoryStream(ReadBuffer);
                                            binaryReader = new BinaryReader(memoryStream);
                                            memoryStream.Position = num5;
                                            uint num6 = binaryReader.ReadUInt32();
                                            if (num3 != (uint)(337776900 + block) ||
                                                num6 != (uint)(~(337776900 + block)))
                                            {
                                                log.LogError("Received data Block has wrong ID!");

                                                result = false;
                                            }
                                            else
                                            {
                                                memoryStream.Position = 0L;
                                                switch (block)
                                                {
                                                    case 0:
                                                        Status0.AutoMode = binaryReader.ReadByte();
                                                        Status0.PowerEnabled = binaryReader.ReadByte();
                                                        Status0.ParameterMode = binaryReader.ReadByte();
                                                        Status0.TestMode = binaryReader.ReadByte();
                                                        Status0.StorageSystemMode = binaryReader.ReadByte();
                                                        Status0.OwnerID1 = binaryReader.ReadUInt32();
                                                        Status0.OwnerID2 = binaryReader.ReadUInt32();
                                                        Status0.OwnerID3 = binaryReader.ReadUInt32();
                                                        array[1] = 0;
                                                        while (array[1] < 10)
                                                        {
                                                            Status0.Version[array[1]] = binaryReader.ReadUInt16();
                                                            array[1]++;
                                                        }

                                                        Status0.TestError = binaryReader.ReadUInt32();
                                                        array[1] = 0;
                                                        while (array[1] < 1000)
                                                        {
                                                            Status0.TestErrorText[array[1]] = binaryReader.ReadUInt16();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 1:
                                                        Status1.RequestID = binaryReader.ReadUInt32();
                                                        Status1.VisuLive = binaryReader.ReadUInt16();
                                                        goto IL_5de6;
                                                    case 2:
                                                        Status2.RequestID = binaryReader.ReadUInt32();
                                                        Status2.VisuLive = binaryReader.ReadUInt16();
                                                        goto IL_5de6;
                                                    case 3:
                                                        Status3.RequestID = binaryReader.ReadUInt32();
                                                        Status3.VisuLive = binaryReader.ReadUInt16();
                                                        goto IL_5de6;
                                                    case 4:
                                                        SaveSys.RequestBlock = binaryReader.ReadByte();
                                                        goto IL_5de6;
                                                    case 5:
                                                        ErrorSys.Num = binaryReader.ReadUInt32();
                                                        array[1] = 0;
                                                        while (array[1] < 1000)
                                                        {
                                                            ErrorSys.Text[array[1]] = binaryReader.ReadUInt16();
                                                            array[1]++;
                                                        }

                                                        ErrorSys.SpindleTest = binaryReader.ReadByte();
                                                        ErrorSys.Warning = binaryReader.ReadByte();
                                                        ErrorSys.Quit = binaryReader.ReadByte();
                                                        ErrorSys.PlcError = binaryReader.ReadUInt32();
                                                        goto IL_5de6;
                                                    case 7:
                                                        LogBookSys.Position = binaryReader.ReadUInt32();
                                                        LogBookSys.Length = binaryReader.ReadUInt32();
                                                        array[1] = 0;
                                                        while (array[1] < 2500)
                                                        {
                                                            LogBookSys.logMessBuffer[array[1]].Time.Year = binaryReader.ReadUInt16();
                                                            LogBookSys.logMessBuffer[array[1]].Time.Month = binaryReader.ReadByte();
                                                            LogBookSys.logMessBuffer[array[1]].Time.Day = binaryReader.ReadByte();
                                                            LogBookSys.logMessBuffer[array[1]].Time.Hour = binaryReader.ReadByte();
                                                            LogBookSys.logMessBuffer[array[1]].Time.Minute = binaryReader.ReadByte();
                                                            LogBookSys.logMessBuffer[array[1]].Time.Second = binaryReader.ReadByte();
                                                            LogBookSys.logMessBuffer[array[1]].Code = binaryReader.ReadUInt32();
                                                            LogBookSys.logMessBuffer[array[1]].Type = binaryReader.ReadUInt16();
                                                            LogBookSys.logMessBuffer[array[1]].ProgNum = binaryReader.ReadUInt32();
                                                            LogBookSys.logMessBuffer[array[1]].Step = binaryReader.ReadByte();
                                                            LogBookSys.logMessBuffer[array[1]].Value1 = binaryReader.ReadSingle();
                                                            LogBookSys.logMessBuffer[array[1]].Value2 = binaryReader.ReadSingle();
                                                            array[2] = 0;
                                                            while (array[2] < 5)
                                                            {
                                                                LogBookSys.logMessBuffer[array[1]]
                                                                    .userName[array[2]] = binaryReader.ReadUInt16();
                                                                array[2]++;
                                                            }

                                                            LogBookSys.logMessBuffer[array[1]].cycNum = binaryReader.ReadUInt32();
                                                            LogBookSys.logMessBuffer[array[1]].UnitIndex = binaryReader.ReadByte();
                                                            LogBookSys.logMessBuffer[array[1]].res = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 8:
                                                        Result.Prog.byteReserve_1 = binaryReader.ReadByte();
                                                        Result.Prog.Info.ProgNum = binaryReader.ReadUInt32();
                                                        array[3] = 0;
                                                        while (array[3] < 32)
                                                        {
                                                            Result.Prog.Info.Name[array[3]] = binaryReader.ReadUInt16();
                                                            array[3]++;
                                                        }

                                                        Result.Prog.Info.Steps = binaryReader.ReadByte();
                                                        Result.Prog.Info.ResultParam1 = binaryReader.ReadByte();
                                                        Result.Prog.Info.ResultParam2 = binaryReader.ReadByte();
                                                        Result.Prog.Info.ResultParam3 = binaryReader.ReadByte();
                                                        Result.Prog.M1FilterTime = binaryReader.ReadSingle();
                                                        Result.Prog.GradientLength = binaryReader.ReadUInt16();
                                                        Result.Prog.GradientFilter = binaryReader.ReadSByte();
                                                        Result.Prog.ADepthFilterTime = binaryReader.ReadSingle();
                                                        Result.Prog.ADepthGradientLength = binaryReader.ReadUInt16();
                                                        Result.Prog.MaxTime = binaryReader.ReadSingle();
                                                        Result.Prog.PressureHolder = binaryReader.ReadSingle();
                                                        Result.Prog.EndSetDigOut1 = binaryReader.ReadByte();
                                                        Result.Prog.EndValueDigOut1 = binaryReader.ReadByte();
                                                        Result.Prog.EndSetDigOut2 = binaryReader.ReadByte();
                                                        Result.Prog.EndValueDigOut2 = binaryReader.ReadByte();
                                                        Result.Prog.EndSetSync1 = binaryReader.ReadByte();
                                                        Result.Prog.EndValueSync1 = binaryReader.ReadByte();
                                                        Result.Prog.EndSetSync2 = binaryReader.ReadByte();
                                                        Result.Prog.EndValueSync2 = binaryReader.ReadByte();
                                                        array[2] = 0;
                                                        while (array[2] < 25)
                                                        {
                                                            Result.Prog.Step[array[2]].Type = binaryReader.ReadUInt16();
                                                            Result.Prog.Step[array[2]].Switch = binaryReader.ReadUInt16();
                                                            Result.Prog.Step[array[2]].IsResult1 = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].IsResult2 = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].IsResult3 = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].Enable.Torque = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].Enable.Snug = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].Enable.FTorque = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].Enable.GradientMin = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].Enable.GradientMax = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].Enable.Angle = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].Enable.Time = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].Enable.ADepth = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].Enable.ADepthGradMin = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].Enable.ADepthGradMax = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].Enable.Ana = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].Enable.Release = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].NA = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].TM = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].MP = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].Mmin = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].Mmax = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].MS = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].MRP = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].MRStep = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].MRType = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].MDelayTime = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].MFP = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].MFmin = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].MFmax = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].MGP = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].MGmin = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].MGmax = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].WP = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].Wmin = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].Wmax = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].WN = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].TP = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].Tmin = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].Tmax = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].TN = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].LP = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].Lmin = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].Lmax = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].LGP = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].LGmin = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].LGmax = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].AnaP = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].AnaMin = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].AnaMax = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].DigP = binaryReader.ReadSByte();
                                                            Result.Prog.Step[array[2]].DigMin = binaryReader.ReadSByte();
                                                            Result.Prog.Step[array[2]].DigMax = binaryReader.ReadSByte();
                                                            Result.Prog.Step[array[2]].JumpTo = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].ModDigOut = binaryReader.ReadSByte();
                                                            Result.Prog.Step[array[2]].PressureSpindle = binaryReader.ReadSingle();
                                                            Result.Prog.Step[array[2]].CountPassMax = binaryReader.ReadByte();
                                                            Result.Prog.Step[array[2]].UserRights = binaryReader.ReadByte();
                                                            array[2]++;
                                                        }

                                                        Result.Prog.UseLocalJawSettings = binaryReader.ReadByte();
                                                        Result.Prog.JawLocalWrittenOnce = binaryReader.ReadByte();
                                                        Result.Prog.JawOpenDistance = binaryReader.ReadSingle();
                                                        Result.Prog.JawOpenDepthGradMax = binaryReader.ReadSingle();
                                                        Result.Prog.JawOpenDepthGradMin = binaryReader.ReadSingle();
                                                        array[3] = 0;
                                                        while (array[3] < 4)
                                                        {
                                                            array[4] = 0;
                                                            while (array[4] < 7)
                                                            {
                                                                Result.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].StepIndex = binaryReader.ReadByte();
                                                                Result.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].TypeOfData = binaryReader.ReadByte();
                                                                Result.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].Value = binaryReader.ReadSingle();
                                                                Result.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MinValue = binaryReader.ReadSingle();
                                                                Result.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MaxValue = binaryReader.ReadSingle();
                                                                array[4]++;
                                                            }

                                                            array[3]++;
                                                        }

                                                        Result.UnitTorque = binaryReader.ReadByte();
                                                        Result.IONIO = binaryReader.ReadInt16();
                                                        Result.LastStep = binaryReader.ReadByte();
                                                        Result.Time.Year = binaryReader.ReadUInt16();
                                                        Result.Time.Month = binaryReader.ReadByte();
                                                        Result.Time.Day = binaryReader.ReadByte();
                                                        Result.Time.Hour = binaryReader.ReadByte();
                                                        Result.Time.Minute = binaryReader.ReadByte();
                                                        Result.Time.Second = binaryReader.ReadByte();
                                                        Result.Cycle = binaryReader.ReadUInt32();
                                                        Result.ScrewDuration = binaryReader.ReadSingle();
                                                        array[1] = 0;
                                                        while (array[1] < 250)
                                                        {
                                                            Result.StepResult[array[1]].Step = binaryReader.ReadByte();
                                                            Result.StepResult[array[1]].IONIO = binaryReader.ReadInt16();
                                                            Result.StepResult[array[1]].OrgStep = binaryReader.ReadByte();
                                                            Result.StepResult[array[1]].Torque = binaryReader.ReadSingle();
                                                            Result.StepResult[array[1]].MaxTorque = binaryReader.ReadSingle();
                                                            Result.StepResult[array[1]].FTorque = binaryReader.ReadSingle();
                                                            Result.StepResult[array[1]].DelayTorque = binaryReader.ReadSingle();
                                                            Result.StepResult[array[1]].M360Follow = binaryReader.ReadSingle();
                                                            Result.StepResult[array[1]].Gradient = binaryReader.ReadSingle();
                                                            Result.StepResult[array[1]].Angle = binaryReader.ReadSingle();
                                                            Result.StepResult[array[1]].Time = binaryReader.ReadSingle();
                                                            Result.StepResult[array[1]].ADepth = binaryReader.ReadSingle();
                                                            Result.StepResult[array[1]].ADepthGrad = binaryReader.ReadSingle();
                                                            Result.StepResult[array[1]].Ana = binaryReader.ReadSingle();
                                                            Result.StepResult[array[1]].Dig = binaryReader.ReadUInt16();
                                                            array[1]++;
                                                        }

                                                        Result.Valid = binaryReader.ReadByte();
                                                        Result.ResultStep1 = binaryReader.ReadSByte();
                                                        Result.Res1 = binaryReader.ReadSingle();
                                                        Result.ResultStep2 = binaryReader.ReadSByte();
                                                        Result.Res2 = binaryReader.ReadSingle();
                                                        Result.ResultStep3 = binaryReader.ReadSByte();
                                                        Result.Res3 = binaryReader.ReadSingle();
                                                        array[1] = 0;
                                                        while (array[1] < 32)
                                                        {
                                                            Result.ScrewID[array[1]] = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        array[1] = 0;
                                                        while (array[1] < 20)
                                                        {
                                                            Result.ExtendedResult[array[1]] = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 9:
                                                        LastNIOResults.ID1 = binaryReader.ReadUInt32();
                                                        array[1] = 0;
                                                        while (array[1] < 100)
                                                        {
                                                            LastNIOResults.Num[array[1]].ProgNum = binaryReader.ReadUInt32();
                                                            array[2] = 0;
                                                            while (array[2] < 32)
                                                            {
                                                                LastNIOResults.Num[array[1]].ProgName[array[2]] = binaryReader.ReadByte();
                                                                array[2]++;
                                                            }

                                                            LastNIOResults.Num[array[1]].IONIO = binaryReader.ReadInt16();
                                                            LastNIOResults.Num[array[1]].LastStep = binaryReader.ReadByte();
                                                            LastNIOResults.Num[array[1]].Time.Year = binaryReader.ReadUInt16();
                                                            LastNIOResults.Num[array[1]].Time.Month = binaryReader.ReadByte();
                                                            LastNIOResults.Num[array[1]].Time.Day = binaryReader.ReadByte();
                                                            LastNIOResults.Num[array[1]].Time.Hour = binaryReader.ReadByte();
                                                            LastNIOResults.Num[array[1]].Time.Minute = binaryReader.ReadByte();
                                                            LastNIOResults.Num[array[1]].Time.Second = binaryReader.ReadByte();
                                                            LastNIOResults.Num[array[1]].Cycle = binaryReader.ReadUInt32();
                                                            LastNIOResults.Num[array[1]].ScrewDuration = binaryReader.ReadSingle();
                                                            LastNIOResults.Num[array[1]].Valid = binaryReader.ReadByte();
                                                            LastNIOResults.Num[array[1]].ResultStep1 = binaryReader.ReadSByte();
                                                            LastNIOResults.Num[array[1]].ResultParam1 = binaryReader.ReadByte();
                                                            LastNIOResults.Num[array[1]].Res1 = binaryReader.ReadSingle();
                                                            LastNIOResults.Num[array[1]].ResultStep2 = binaryReader.ReadSByte();
                                                            LastNIOResults.Num[array[1]].ResultParam2 = binaryReader.ReadByte();
                                                            LastNIOResults.Num[array[1]].Res2 = binaryReader.ReadSingle();
                                                            LastNIOResults.Num[array[1]].ResultStep3 = binaryReader.ReadSByte();
                                                            LastNIOResults.Num[array[1]].ResultParam3 = binaryReader.ReadByte();
                                                            LastNIOResults.Num[array[1]].Res3 = binaryReader.ReadSingle();
                                                            array[2] = 0;
                                                            while (array[2] < 32)
                                                            {
                                                                LastNIOResults.Num[array[1]].ScrewID[array[2]] = binaryReader.ReadByte();
                                                                array[2]++;
                                                            }

                                                            LastNIOResults.Num[array[1]].res = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        LastNIOResults.ID2 = binaryReader.ReadUInt32();
                                                        goto IL_5de6;
                                                    case 10:
                                                        PProg.PProgVersion = binaryReader.ReadSingle();
                                                        array[1] = 0;
                                                        while (array[1] < 1024)
                                                        {
                                                            PProg.Num[array[1]].byteReserve_1 = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].Info.ProgNum = binaryReader.ReadUInt32();
                                                            array[3] = 0;
                                                            while (array[3] < 32)
                                                            {
                                                                PProg.Num[array[1]].Info.Name[array[3]] = binaryReader.ReadUInt16();
                                                                array[3]++;
                                                            }

                                                            PProg.Num[array[1]].Info.Steps = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].Info.ResultParam1 = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].Info.ResultParam2 = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].Info.ResultParam3 = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].M1FilterTime = binaryReader.ReadSingle();
                                                            PProg.Num[array[1]].GradientLength = binaryReader.ReadUInt16();
                                                            PProg.Num[array[1]].GradientFilter = binaryReader.ReadSByte();
                                                            PProg.Num[array[1]].ADepthFilterTime = binaryReader.ReadSingle();
                                                            PProg.Num[array[1]].ADepthGradientLength = binaryReader.ReadUInt16();
                                                            PProg.Num[array[1]].MaxTime = binaryReader.ReadSingle();
                                                            PProg.Num[array[1]].PressureHolder = binaryReader.ReadSingle();
                                                            PProg.Num[array[1]].EndSetDigOut1 = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].EndValueDigOut1 = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].EndSetDigOut2 = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].EndValueDigOut2 = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].EndSetSync1 = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].EndValueSync1 = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].EndSetSync2 = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].EndValueSync2 = binaryReader.ReadByte();
                                                            array[2] = 0;
                                                            while (array[2] < 25)
                                                            {
                                                                PProg.Num[array[1]].Step[array[2]].Type = binaryReader.ReadUInt16();
                                                                PProg.Num[array[1]].Step[array[2]].Switch = binaryReader.ReadUInt16();
                                                                PProg.Num[array[1]].Step[array[2]].IsResult1 = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].IsResult2 = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].IsResult3 = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].Enable.Torque = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].Enable.Snug = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].Enable.FTorque = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].Enable.GradientMin = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].Enable.GradientMax = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].Enable.Angle = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].Enable.Time = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].Enable.ADepth = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].Enable.ADepthGradMin = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].Enable.ADepthGradMax = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].Enable.Ana = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].Enable.Release = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].NA = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].TM = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].MP = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].Mmin = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].Mmax = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].MS = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].MRP = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].MRStep = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].MRType = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].MDelayTime = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].MFP = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].MFmin = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].MFmax = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].MGP = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].MGmin = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].MGmax = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].WP = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].Wmin = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].Wmax = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].WN = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].TP = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].Tmin = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].Tmax = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].TN = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].LP = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].Lmin = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].Lmax = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].LGP = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].LGmin = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].LGmax = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].AnaP = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].AnaMin = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].AnaMax = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].DigP = binaryReader.ReadSByte();
                                                                PProg.Num[array[1]].Step[array[2]].DigMin = binaryReader.ReadSByte();
                                                                PProg.Num[array[1]].Step[array[2]].DigMax = binaryReader.ReadSByte();
                                                                PProg.Num[array[1]].Step[array[2]].JumpTo = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].ModDigOut = binaryReader.ReadSByte();
                                                                PProg.Num[array[1]].Step[array[2]].PressureSpindle = binaryReader.ReadSingle();
                                                                PProg.Num[array[1]].Step[array[2]].CountPassMax = binaryReader.ReadByte();
                                                                PProg.Num[array[1]].Step[array[2]].UserRights = binaryReader.ReadByte();
                                                                array[2]++;
                                                            }

                                                            PProg.Num[array[1]].UseLocalJawSettings = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].JawLocalWrittenOnce = binaryReader.ReadByte();
                                                            PProg.Num[array[1]].JawOpenDistance = binaryReader.ReadSingle();
                                                            PProg.Num[array[1]].JawOpenDepthGradMax = binaryReader.ReadSingle();
                                                            PProg.Num[array[1]].JawOpenDepthGradMin = binaryReader.ReadSingle();
                                                            array[3] = 0;
                                                            while (array[3] < 4)
                                                            {
                                                                array[4] = 0;
                                                                while (array[4] < 7)
                                                                {
                                                                    PProg.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].StepIndex = binaryReader.ReadByte();
                                                                    PProg.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].TypeOfData = binaryReader.ReadByte();
                                                                    PProg.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].Value = binaryReader.ReadSingle();
                                                                    PProg.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MinValue = binaryReader.ReadSingle();
                                                                    PProg.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MaxValue = binaryReader.ReadSingle();
                                                                    array[4]++;
                                                                }

                                                                array[3]++;
                                                            }

                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 11:
                                                        SpConst.TorqueSensorScale = binaryReader.ReadSingle();
                                                        SpConst.TorqueSensorTolerance = binaryReader.ReadSingle();
                                                        SpConst.AngleSensorScale = binaryReader.ReadSingle();
                                                        SpConst.TorqueSensorInvers = binaryReader.ReadByte();
                                                        SpConst.AngleSensorInvers = binaryReader.ReadByte();
                                                        SpConst.RedundantSensorActive = binaryReader.ReadByte();
                                                        SpConst.TorqueRedundantTime = binaryReader.ReadSingle();
                                                        SpConst.TorqueRedundantTolerance = binaryReader.ReadSingle();
                                                        SpConst.AngleRedundantTolerance = binaryReader.ReadInt16();
                                                        SpConst.DriveUnitRpm = binaryReader.ReadSingle();
                                                        SpConst.DriveUnitInvers = binaryReader.ReadByte();
                                                        SpConst.DepthSensorScale = binaryReader.ReadSingle();
                                                        SpConst.DepthSensorOffset = binaryReader.ReadSingle();
                                                        SpConst.DepthSensorOffsetMin = binaryReader.ReadSingle();
                                                        SpConst.DepthSensorOffsetMax = binaryReader.ReadSingle();
                                                        SpConst.DepthSensorOffsetPreset = binaryReader.ReadSingle();
                                                        SpConst.DepthSensorInvers = binaryReader.ReadByte();
                                                        SpConst.AnaSigScale = binaryReader.ReadSingle();
                                                        SpConst.AnaSigOffset = binaryReader.ReadSingle();
                                                        SpConst.SpindleTorque = binaryReader.ReadSingle();
                                                        SpConst.SpindleGearFactor = binaryReader.ReadSingle();
                                                        SpConst.ReleaseSpeed = binaryReader.ReadSingle();
                                                        SpConst.FrictionTorque = binaryReader.ReadSingle();
                                                        SpConst.FrictionSpeed = binaryReader.ReadSingle();
                                                        SpConst.FrictionTestStartup = binaryReader.ReadByte();
                                                        SpConst.FrictionTestEMG = binaryReader.ReadByte();
                                                        SpConst.PressureScaleSpindle = binaryReader.ReadSingle();
                                                        SpConst.ReserveFloat_1 = binaryReader.ReadSingle();
                                                        SpConst.PressureScaleHolder = binaryReader.ReadSingle();
                                                        SpConst.JawOpenDistance = binaryReader.ReadSingle();
                                                        SpConst.JawOpenDepthGradMax = binaryReader.ReadSingle();
                                                        SpConst.JawOpenDepthGradMin = binaryReader.ReadSingle();
                                                        SpConst.ReserveByte_1 = binaryReader.ReadByte();
                                                        SpConst.UsbKeyActivated = binaryReader.ReadByte();
                                                        goto IL_5de6;
                                                    case 12:
                                                        SysConst.ActualTime.Year = binaryReader.ReadUInt16();
                                                        SysConst.ActualTime.Month = binaryReader.ReadByte();
                                                        SysConst.ActualTime.Day = binaryReader.ReadByte();
                                                        SysConst.ActualTime.Hour = binaryReader.ReadByte();
                                                        SysConst.ActualTime.Minute = binaryReader.ReadByte();
                                                        SysConst.ActualTime.Second = binaryReader.ReadByte();
                                                        SysConst.SettingTime.Year = binaryReader.ReadUInt16();
                                                        SysConst.SettingTime.Month = binaryReader.ReadByte();
                                                        SysConst.SettingTime.Day = binaryReader.ReadByte();
                                                        SysConst.SettingTime.Hour = binaryReader.ReadByte();
                                                        SysConst.SettingTime.Minute = binaryReader.ReadByte();
                                                        SysConst.SettingTime.Second = binaryReader.ReadByte();
                                                        array[1] = 0;
                                                        while (array[1] < 20)
                                                        {
                                                            SysConst.SystemID[array[1]] = binaryReader.ReadUInt16();
                                                            array[1]++;
                                                        }

                                                        array[1] = 0;
                                                        while (array[1] < 32)
                                                        {
                                                            SysConst.IdentServerName[array[1]] = binaryReader.ReadUInt16();
                                                            array[1]++;
                                                        }

                                                        SysConst.IPAddress.Byte1 = binaryReader.ReadByte();
                                                        SysConst.IPAddress.Byte2 = binaryReader.ReadByte();
                                                        SysConst.IPAddress.Byte3 = binaryReader.ReadByte();
                                                        SysConst.IPAddress.Byte4 = binaryReader.ReadByte();
                                                        SysConst.DHCP = binaryReader.ReadUInt32();
                                                        SysConst.SubNetMask.Byte1 = binaryReader.ReadByte();
                                                        SysConst.SubNetMask.Byte2 = binaryReader.ReadByte();
                                                        SysConst.SubNetMask.Byte3 = binaryReader.ReadByte();
                                                        SysConst.SubNetMask.Byte4 = binaryReader.ReadByte();
                                                        SysConst.DefaultGateway.Byte1 = binaryReader.ReadByte();
                                                        SysConst.DefaultGateway.Byte2 = binaryReader.ReadByte();
                                                        SysConst.DefaultGateway.Byte3 = binaryReader.ReadByte();
                                                        SysConst.DefaultGateway.Byte4 = binaryReader.ReadByte();
                                                        array[1] = 0;
                                                        while (array[1] < 5)
                                                        {
                                                            array[2] = 0;
                                                            while (array[2] < 32)
                                                            {
                                                                SysConst.AdvancedWarnings[array[1]].Name[array[2]] = binaryReader.ReadByte();
                                                                array[2]++;
                                                            }

                                                            SysConst.AdvancedWarnings[array[1]].Limit = binaryReader.ReadUInt32();
                                                            SysConst.AdvancedWarnings[array[1]].Advance = binaryReader.ReadUInt32();
                                                            SysConst.AdvancedWarnings[array[1]].AdvancedWarningTime.Year = binaryReader.ReadUInt16();
                                                            SysConst.AdvancedWarnings[array[1]].AdvancedWarningTime.Month = binaryReader.ReadByte();
                                                            SysConst.AdvancedWarnings[array[1]].AdvancedWarningTime.Day = binaryReader.ReadByte();
                                                            SysConst.AdvancedWarnings[array[1]].AdvancedWarningTime.Hour = binaryReader.ReadByte();
                                                            SysConst.AdvancedWarnings[array[1]].AdvancedWarningTime.Minute = binaryReader.ReadByte();
                                                            SysConst.AdvancedWarnings[array[1]].AdvancedWarningTime.Second = binaryReader.ReadByte();
                                                            SysConst.AdvancedWarnings[array[1]].AdvancedDays = binaryReader.ReadUInt32();
                                                            SysConst.AdvancedWarnings[array[1]].EnableAdvancedWarningTime = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        SysConst.UnitTorque = binaryReader.ReadByte();
                                                        SysConst.Com1.BaudRate = binaryReader.ReadUInt32();
                                                        SysConst.Com1.Parity = binaryReader.ReadByte();
                                                        array[1] = 0;
                                                        while (array[1] < 20)
                                                        {
                                                            array[2] = 0;
                                                            while (array[2] < 5)
                                                            {
                                                                SysConst.PassCodes[array[1]].Name[array[2]] = binaryReader.ReadUInt16();
                                                                array[2]++;
                                                            }

                                                            SysConst.PassCodes[array[1]].Code = binaryReader.ReadUInt32();
                                                            SysConst.PassCodes[array[1]].Level = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        SysConst.UserLevels.UserLevel_BackupForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_CheckParamForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_CycleCounterForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_EditSteps = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_PrgOptParameterForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_ProgramOverviewForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_SpindleConstantsForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_StatisticsLastResForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_StepOverviewForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_SystemConstantsForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_TestIOForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_TestMotorSensorForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_VisualisationParamForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_FourStepEditForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_Maintenance = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_HandStartForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_BrowserForm = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_Reserve1 = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_Reserve2 = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_Reserve3 = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_Reserve4 = binaryReader.ReadByte();
                                                        SysConst.UserLevels.UserLevel_Reserve5 = binaryReader.ReadByte();
                                                        array[1] = 0;
                                                        while (array[1] < 16)
                                                        {
                                                            SysConst.AreaCode[array[1]] = binaryReader.ReadUInt16();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 15:
                                                        array[1] = 0;
                                                        while (array[1] < 1024)
                                                        {
                                                            ProcessInfo.ProgInfo[array[1]].ProgNum = binaryReader.ReadUInt32();
                                                            array[2] = 0;
                                                            while (array[2] < 32)
                                                            {
                                                                ProcessInfo.ProgInfo[array[1]].Name[array[2]] = binaryReader.ReadUInt16();
                                                                array[2]++;
                                                            }

                                                            ProcessInfo.ProgInfo[array[1]].Steps = binaryReader.ReadByte();
                                                            ProcessInfo.ProgInfo[array[1]].ResultParam1 = binaryReader.ReadByte();
                                                            ProcessInfo.ProgInfo[array[1]].ResultParam2 = binaryReader.ReadByte();
                                                            ProcessInfo.ProgInfo[array[1]].ResultParam3 = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 17:
                                                        array[1] = 0;
                                                        while (array[1] < 1024)
                                                        {
                                                            PProgXChanged.Changed[array[1]] = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 18:
                                                        PProgX.PProgVersion = binaryReader.ReadSingle();
                                                        array[1] = 0;
                                                        while (array[1] < 32)
                                                        {
                                                            PProgX.Num[array[1]].byteReserve_1 = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].Info.ProgNum = binaryReader.ReadUInt32();
                                                            array[3] = 0;
                                                            while (array[3] < 32)
                                                            {
                                                                PProgX.Num[array[1]].Info.Name[array[3]] = binaryReader.ReadUInt16();
                                                                array[3]++;
                                                            }

                                                            PProgX.Num[array[1]].Info.Steps = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].Info.ResultParam1 = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].Info.ResultParam2 = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].Info.ResultParam3 = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].M1FilterTime = binaryReader.ReadSingle();
                                                            PProgX.Num[array[1]].GradientLength = binaryReader.ReadUInt16();
                                                            PProgX.Num[array[1]].GradientFilter = binaryReader.ReadSByte();
                                                            PProgX.Num[array[1]].ADepthFilterTime = binaryReader.ReadSingle();
                                                            PProgX.Num[array[1]].ADepthGradientLength = binaryReader.ReadUInt16();
                                                            PProgX.Num[array[1]].MaxTime = binaryReader.ReadSingle();
                                                            PProgX.Num[array[1]].PressureHolder = binaryReader.ReadSingle();
                                                            PProgX.Num[array[1]].EndSetDigOut1 = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].EndValueDigOut1 = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].EndSetDigOut2 = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].EndValueDigOut2 = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].EndSetSync1 = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].EndValueSync1 = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].EndSetSync2 = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].EndValueSync2 = binaryReader.ReadByte();
                                                            array[2] = 0;
                                                            while (array[2] < 25)
                                                            {
                                                                PProgX.Num[array[1]].Step[array[2]].Type = binaryReader.ReadUInt16();
                                                                PProgX.Num[array[1]].Step[array[2]].Switch = binaryReader.ReadUInt16();
                                                                PProgX.Num[array[1]].Step[array[2]].IsResult1 = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].IsResult2 = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].IsResult3 = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].Enable.Torque = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].Enable.Snug = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].Enable.FTorque = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].Enable.GradientMin = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].Enable.GradientMax = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].Enable.Angle = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].Enable.Time = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].Enable.ADepth = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].Enable.ADepthGradMin = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].Enable.ADepthGradMax = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].Enable.Ana = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].Enable.Release = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].NA = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].TM = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].MP = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].Mmin = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].Mmax = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].MS = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].MRP = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].MRStep = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].MRType = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].MDelayTime = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].MFP = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].MFmin = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].MFmax = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].MGP = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].MGmin = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].MGmax = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].WP = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].Wmin = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].Wmax = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].WN = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].TP = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].Tmin = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].Tmax = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].TN = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].LP = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].Lmin = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].Lmax = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].LGP = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].LGmin = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].LGmax = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].AnaP = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].AnaMin = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].AnaMax = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].DigP = binaryReader.ReadSByte();
                                                                PProgX.Num[array[1]].Step[array[2]].DigMin = binaryReader.ReadSByte();
                                                                PProgX.Num[array[1]].Step[array[2]].DigMax = binaryReader.ReadSByte();
                                                                PProgX.Num[array[1]].Step[array[2]].ModDigOut = binaryReader.ReadSByte();
                                                                PProgX.Num[array[1]].Step[array[2]].PressureSpindle = binaryReader.ReadSingle();
                                                                PProgX.Num[array[1]].Step[array[2]].CountPassMax = binaryReader.ReadByte();
                                                                PProgX.Num[array[1]].Step[array[2]].UserRights = binaryReader.ReadByte();
                                                                array[2]++;
                                                            }

                                                            PProgX.Num[array[1]].UseLocalJawSettings = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].JawLocalWrittenOnce = binaryReader.ReadByte();
                                                            PProgX.Num[array[1]].JawOpenDistance = binaryReader.ReadSingle();
                                                            PProgX.Num[array[1]].JawOpenDepthGradMax = binaryReader.ReadSingle();
                                                            PProgX.Num[array[1]].JawOpenDepthGradMin = binaryReader.ReadSingle();
                                                            array[3] = 0;
                                                            while (array[3] < 4)
                                                            {
                                                                array[4] = 0;
                                                                while (array[4] < 7)
                                                                {
                                                                    PProgX.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].StepIndex = binaryReader.ReadByte();
                                                                    PProgX.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].TypeOfData = binaryReader.ReadByte();
                                                                    PProgX.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].Value = binaryReader.ReadSingle();
                                                                    PProgX.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MinValue = binaryReader.ReadSingle();
                                                                    PProgX.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MaxValue = binaryReader.ReadSingle();
                                                                    array[4]++;
                                                                }

                                                                array[3]++;
                                                            }

                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 20:
                                                        CurveDef.Points = binaryReader.ReadUInt32();
                                                        CurveDef.SampleTime = binaryReader.ReadSingle();
                                                        CurveDef.SpeedSetScale = binaryReader.ReadSingle();
                                                        CurveDef.SpeedActScale = binaryReader.ReadSingle();
                                                        CurveDef.UnitTorque = binaryReader.ReadByte();
                                                        goto IL_5de6;
                                                    case 21:
                                                        array[1] = 0;
                                                        while (array[1] < num4)
                                                        {
                                                            CurveData.Point[array[1]].Nset = binaryReader.ReadInt16();
                                                            CurveData.Point[array[1]].Nact = binaryReader.ReadInt16();
                                                            CurveData.Point[array[1]].Torque = binaryReader.ReadSingle();
                                                            CurveData.Point[array[1]].FiltTorque = binaryReader.ReadSingle();
                                                            CurveData.Point[array[1]].Angle = binaryReader.ReadSingle();
                                                            CurveData.Point[array[1]].DDepth = binaryReader.ReadByte();
                                                            CurveData.Point[array[1]].ADepth = binaryReader.ReadSingle();
                                                            CurveData.Point[array[1]].ADepthGrad = binaryReader.ReadSingle();
                                                            CurveData.Point[array[1]].AExt = binaryReader.ReadSingle();
                                                            CurveData.Point[array[1]].Gradient = binaryReader.ReadSingle();
                                                            CurveData.Point[array[1]].CurrentStep = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 30:
                                                        CycleCount.ID1 = binaryReader.ReadUInt32();
                                                        CycleCount.Machine = binaryReader.ReadUInt32();
                                                        CycleCount.Customer = binaryReader.ReadUInt32();
                                                        array[1] = 0;
                                                        while (array[1] < 5)
                                                        {
                                                            CycleCount.Count[array[1]] = binaryReader.ReadUInt32();
                                                            array[1]++;
                                                        }

                                                        CycleCount.MachineNIO = binaryReader.ReadUInt32();
                                                        CycleCount.CustomerNIO = binaryReader.ReadUInt32();
                                                        CycleCount.CountReserve = binaryReader.ReadUInt32();
                                                        CycleCount.ID2 = binaryReader.ReadUInt32();
                                                        goto IL_5de6;
                                                    case 32:
                                                        StatSample.ID1 = binaryReader.ReadUInt32();
                                                        StatSample.Info.Length = binaryReader.ReadUInt16();
                                                        StatSample.Info.Position = binaryReader.ReadUInt16();
                                                        array[1] = 0;
                                                        while (array[1] < 2000)
                                                        {
                                                            StatSample.Data[array[1]].ProgNum = binaryReader.ReadUInt32();
                                                            array[2] = 0;
                                                            while (array[2] < 32)
                                                            {
                                                                StatSample.Data[array[1]].ProgName[array[2]] = binaryReader.ReadByte();
                                                                array[2]++;
                                                            }

                                                            StatSample.Data[array[1]].IONIO = binaryReader.ReadInt16();
                                                            StatSample.Data[array[1]].LastStep = binaryReader.ReadByte();
                                                            StatSample.Data[array[1]].Time.Year = binaryReader.ReadUInt16();
                                                            StatSample.Data[array[1]].Time.Month = binaryReader.ReadByte();
                                                            StatSample.Data[array[1]].Time.Day = binaryReader.ReadByte();
                                                            StatSample.Data[array[1]].Time.Hour = binaryReader.ReadByte();
                                                            StatSample.Data[array[1]].Time.Minute = binaryReader.ReadByte();
                                                            StatSample.Data[array[1]].Time.Second = binaryReader.ReadByte();
                                                            StatSample.Data[array[1]].Cycle = binaryReader.ReadUInt32();
                                                            StatSample.Data[array[1]].ScrewDuration = binaryReader.ReadSingle();
                                                            StatSample.Data[array[1]].Valid = binaryReader.ReadByte();
                                                            StatSample.Data[array[1]].ResultStep1 = binaryReader.ReadSByte();
                                                            StatSample.Data[array[1]].ResultParam1 = binaryReader.ReadByte();
                                                            StatSample.Data[array[1]].Res1 = binaryReader.ReadSingle();
                                                            StatSample.Data[array[1]].ResultStep2 = binaryReader.ReadSByte();
                                                            StatSample.Data[array[1]].ResultParam2 = binaryReader.ReadByte();
                                                            StatSample.Data[array[1]].Res2 = binaryReader.ReadSingle();
                                                            StatSample.Data[array[1]].ResultStep3 = binaryReader.ReadSByte();
                                                            StatSample.Data[array[1]].ResultParam3 = binaryReader.ReadByte();
                                                            StatSample.Data[array[1]].Res3 = binaryReader.ReadSingle();
                                                            array[2] = 0;
                                                            while (array[2] < 32)
                                                            {
                                                                StatSample.Data[array[1]].ScrewID[array[2]] = binaryReader.ReadByte();
                                                                array[2]++;
                                                            }

                                                            StatSample.Data[array[1]].res = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        StatSample.ID2 = binaryReader.ReadUInt32();
                                                        goto IL_5de6;
                                                    case 33:
                                                        StatControl.Command = binaryReader.ReadByte();
                                                        goto IL_5de6;
                                                    case 40:
                                                        TestDataRawIn.ISRCount = binaryReader.ReadUInt32();
                                                        TestDataRawIn.DI = binaryReader.ReadUInt16();
                                                        TestDataRawIn.DO = binaryReader.ReadUInt16();
                                                        TestDataRawIn.HexSwitch = binaryReader.ReadByte();
                                                        TestDataRawIn.Ui0 = binaryReader.ReadSingle();
                                                        TestDataRawIn.Ui1 = binaryReader.ReadSingle();
                                                        TestDataRawIn.Ui2 = binaryReader.ReadSingle();
                                                        TestDataRawIn.Ui3 = binaryReader.ReadSingle();
                                                        TestDataRawIn.UErr = binaryReader.ReadByte();
                                                        TestDataRawIn.Enc0 = binaryReader.ReadInt16();
                                                        TestDataRawIn.Enc1 = binaryReader.ReadInt16();
                                                        TestDataRawIn.Enc3 = binaryReader.ReadInt16();
                                                        TestDataRawIn.EncErr = binaryReader.ReadByte();
                                                        TestDataRawIn.GRADCount = binaryReader.ReadUInt16();
                                                        TestDataRawIn.Nact = binaryReader.ReadSingle();
                                                        TestDataRawIn.Torque1 = binaryReader.ReadSingle();
                                                        TestDataRawIn.Angle1 = binaryReader.ReadSingle();
                                                        TestDataRawIn.Torque2 = binaryReader.ReadSingle();
                                                        TestDataRawIn.Angle2 = binaryReader.ReadSingle();
                                                        TestDataRawIn.ADepth = binaryReader.ReadSingle();
                                                        TestDataRawIn.ExtAna = binaryReader.ReadSingle();
                                                        TestDataRawIn.BatteryOk = binaryReader.ReadByte();
                                                        goto IL_5de6;
                                                    case 41:
                                                        TestDataRawOut.Command = binaryReader.ReadByte();
                                                        TestDataRawOut.DO16 = binaryReader.ReadUInt16();
                                                        TestDataRawOut.DONr = binaryReader.ReadByte();
                                                        TestDataRawOut.DOState = binaryReader.ReadByte();
                                                        TestDataRawOut.Uo0 = binaryReader.ReadSingle();
                                                        TestDataRawOut.Uo1 = binaryReader.ReadSingle();
                                                        TestDataRawOut.Uo2 = binaryReader.ReadSingle();
                                                        TestDataRawOut.Uo3 = binaryReader.ReadSingle();
                                                        TestDataRawOut.ResetEnc0 = binaryReader.ReadByte();
                                                        TestDataRawOut.ResetEnc1 = binaryReader.ReadByte();
                                                        TestDataRawOut.ResetEnc2 = binaryReader.ReadByte();
                                                        TestDataRawOut.ResetEncErr = binaryReader.ReadByte();
                                                        TestDataRawOut.ResetAngle = binaryReader.ReadByte();
                                                        TestDataRawOut.STSpeed = binaryReader.ReadSingle();
                                                        TestDataRawOut.STEnable = binaryReader.ReadByte();
                                                        goto IL_5de6;
                                                    case 42:
                                                        PLCCommData.Input.Automatic = binaryReader.ReadByte();
                                                        PLCCommData.Input.Start = binaryReader.ReadByte();
                                                        PLCCommData.Input.Quit = binaryReader.ReadByte();
                                                        PLCCommData.Input.Sync1 = binaryReader.ReadByte();
                                                        PLCCommData.Input.Sync2 = binaryReader.ReadByte();
                                                        PLCCommData.Input.KalDisable = binaryReader.ReadByte();
                                                        PLCCommData.Input.TeachAnalogDepth = binaryReader.ReadByte();
                                                        PLCCommData.Input.ProgNum = binaryReader.ReadUInt32();
                                                        array[2] = 0;
                                                        while (array[2] < 32)
                                                        {
                                                            PLCCommData.Input.ScrewID[array[2]] = binaryReader.ReadByte();
                                                            array[2]++;
                                                        }

                                                        PLCCommData.Input.LivingSignRequest = binaryReader.ReadByte();
                                                        PLCCommData.Input.Reserve1 = binaryReader.ReadByte();
                                                        PLCCommData.Input.Reserve2 = binaryReader.ReadByte();
                                                        PLCCommData.Input.Reserve3 = binaryReader.ReadByte();
                                                        PLCCommData.Input.AnalogsignalCurve = binaryReader.ReadByte();
                                                        PLCCommData.Input.LivingSignEnabled = binaryReader.ReadByte();
                                                        PLCCommData.Input.ReserveSignals = binaryReader.ReadByte();
                                                        array[2] = 0;
                                                        while (array[2] < 10)
                                                        {
                                                            PLCCommData.Input.ReserveStr[array[2]] = binaryReader.ReadByte();
                                                            array[2]++;
                                                        }

                                                        PLCCommData.Input.UsingProgName = binaryReader.ReadByte();
                                                        PLCCommData.Input.Reserve5 = binaryReader.ReadByte();
                                                        array[2] = 0;
                                                        while (array[2] < 32)
                                                        {
                                                            PLCCommData.Input.ProgName[array[2]] = binaryReader.ReadByte();
                                                            array[2]++;
                                                        }

                                                        array[2] = 0;
                                                        while (array[2] < 20)
                                                        {
                                                            PLCCommData.Input.ExtendedResult[array[2]] = binaryReader.ReadByte();
                                                            array[2]++;
                                                        }

                                                        PLCCommData.Input.PlcError = binaryReader.ReadUInt32();
                                                        PLCCommData.Input.CounterNIO = binaryReader.ReadUInt32();
                                                        PLCCommData.Input.CounterIOTotal = binaryReader.ReadUInt32();
                                                        PLCCommData.Input.CounterNIOTotal = binaryReader.ReadUInt32();
                                                        PLCCommData.Input.PressureSpindle = binaryReader.ReadSingle();
                                                        PLCCommData.Input.PressureHolder = binaryReader.ReadSingle();
                                                        PLCCommData.Input.Res1 = binaryReader.ReadSingle();
                                                        PLCCommData.Output.SystemOK = binaryReader.ReadByte();
                                                        PLCCommData.Output.ReadyToStart = binaryReader.ReadByte();
                                                        PLCCommData.Output.ProcessRunning = binaryReader.ReadByte();
                                                        PLCCommData.Output.IO = binaryReader.ReadByte();
                                                        PLCCommData.Output.NIO = binaryReader.ReadByte();
                                                        PLCCommData.Output.Sync1 = binaryReader.ReadByte();
                                                        PLCCommData.Output.Sync2 = binaryReader.ReadByte();
                                                        PLCCommData.Output.PowerEnabled = binaryReader.ReadByte();
                                                        PLCCommData.Output.TM1 = binaryReader.ReadByte();
                                                        PLCCommData.Output.TM2 = binaryReader.ReadByte();
                                                        PLCCommData.Output.ExtDigIn = binaryReader.ReadByte();
                                                        PLCCommData.Output.StorageSignals = binaryReader.ReadByte();
                                                        PLCCommData.Output.AnaDepthMM = binaryReader.ReadSingle();
                                                        PLCCommData.Output.AnaDepthVolt = binaryReader.ReadSingle();
                                                        PLCCommData.Output.ExtAna = binaryReader.ReadSingle();
                                                        PLCCommData.Output.LivingSignResponse = binaryReader.ReadByte();
                                                        PLCCommData.Output.LivingMonitor = binaryReader.ReadByte();
                                                        PLCCommData.Output.UserLevel = binaryReader.ReadByte();
                                                        array[2] = 0;
                                                        while (array[2] < 5)
                                                        {
                                                            PLCCommData.Output.UserName[array[2]] = binaryReader.ReadByte();
                                                            array[2]++;
                                                        }

                                                        PLCCommData.Output.Reserve1 = binaryReader.ReadByte();
                                                        PLCCommData.Output.Reserve2 = binaryReader.ReadByte();
                                                        PLCCommData.Output.Reserve3 = binaryReader.ReadByte();
                                                        PLCCommData.Output.DriveUnitInvers = binaryReader.ReadByte();
                                                        PLCCommData.Output.LivingSignEnabled = binaryReader.ReadByte();
                                                        PLCCommData.Output.ReserveSignals = binaryReader.ReadByte();
                                                        array[2] = 0;
                                                        while (array[2] < 10)
                                                        {
                                                            PLCCommData.Output.IpAddress[array[2]] = binaryReader.ReadByte();
                                                            array[2]++;
                                                        }

                                                        PLCCommData.Output.MaintenanceCounterReached = binaryReader.ReadByte();
                                                        PLCCommData.Output.AdvancedCounterReached = binaryReader.ReadByte();
                                                        PLCCommData.Output.PressureSpindle = binaryReader.ReadSingle();
                                                        PLCCommData.Output.PressureHolder = binaryReader.ReadSingle();
                                                        PLCCommData.Output.PressureScaleSpindle = binaryReader.ReadSingle();
                                                        PLCCommData.Output.PressureScaleHolder = binaryReader.ReadSingle();
                                                        PLCCommData.Output.Res1 = binaryReader.ReadSingle();
                                                        PLCCommData.Result.UnitTorque = binaryReader.ReadByte();
                                                        PLCCommData.Result.ProgNum = binaryReader.ReadUInt32();
                                                        PLCCommData.Result.IONIO = binaryReader.ReadInt16();
                                                        PLCCommData.Result.LastStep = binaryReader.ReadByte();
                                                        PLCCommData.Result.Time.Year = binaryReader.ReadUInt16();
                                                        PLCCommData.Result.Time.Month = binaryReader.ReadByte();
                                                        PLCCommData.Result.Time.Day = binaryReader.ReadByte();
                                                        PLCCommData.Result.Time.Hour = binaryReader.ReadByte();
                                                        PLCCommData.Result.Time.Minute = binaryReader.ReadByte();
                                                        PLCCommData.Result.Time.Second = binaryReader.ReadByte();
                                                        PLCCommData.Result.Cycle = binaryReader.ReadUInt32();
                                                        PLCCommData.Result.ScrewDuration = binaryReader.ReadSingle();
                                                        PLCCommData.Result.ResultStep1 = binaryReader.ReadByte();
                                                        PLCCommData.Result.ResultParam1 = binaryReader.ReadByte();
                                                        PLCCommData.Result.ResultStep2 = binaryReader.ReadByte();
                                                        PLCCommData.Result.ResultParam2 = binaryReader.ReadByte();
                                                        PLCCommData.Result.ResultStep3 = binaryReader.ReadByte();
                                                        PLCCommData.Result.ResultParam3 = binaryReader.ReadByte();
                                                        PLCCommData.Result.Valid = binaryReader.ReadByte();
                                                        PLCCommData.Result.Res1 = binaryReader.ReadSingle();
                                                        PLCCommData.Result.Res2 = binaryReader.ReadSingle();
                                                        PLCCommData.Result.Res3 = binaryReader.ReadSingle();
                                                        array[2] = 0;
                                                        while (array[2] < 32)
                                                        {
                                                            PLCCommData.Result.ScrewID[array[2]] = binaryReader.ReadByte();
                                                            array[2]++;
                                                        }

                                                        PLCCommData.Error.Num = binaryReader.ReadUInt32();
                                                        PLCCommData.Error.Warning = binaryReader.ReadByte();
                                                        PLCCommData.DI_DO.DI0_8 = binaryReader.ReadByte();
                                                        PLCCommData.DI_DO.DO2_8 = binaryReader.ReadByte();
                                                        PLCCommData.ProgAccess.SpindleTorque = binaryReader.ReadSingle();
                                                        PLCCommData.ProgAccess.DriveUnitRpm = binaryReader.ReadSingle();
                                                        PLCCommData.ProgAccess.Signal = binaryReader.ReadByte();
                                                        PLCCommData.ProgAccess.Length0 = binaryReader.ReadByte();
                                                        PLCCommData.ProgAccess.Length1 = binaryReader.ReadByte();
                                                        PLCCommData.ProgAccess.Length2 = binaryReader.ReadByte();
                                                        PLCCommData.ProgAccess.Address0 = binaryReader.ReadUInt32();
                                                        PLCCommData.ProgAccess.Address1 = binaryReader.ReadUInt32();
                                                        PLCCommData.ProgAccess.Address2 = binaryReader.ReadUInt32();
                                                        array[2] = 0;
                                                        while (array[2] < 4)
                                                        {
                                                            PLCCommData.ProgAccess.Data0[array[2]] = binaryReader.ReadByte();
                                                            array[2]++;
                                                        }

                                                        array[2] = 0;
                                                        while (array[2] < 4)
                                                        {
                                                            PLCCommData.ProgAccess.Data1[array[2]] = binaryReader.ReadByte();
                                                            array[2]++;
                                                        }

                                                        array[2] = 0;
                                                        while (array[2] < 4)
                                                        {
                                                            PLCCommData.ProgAccess.Data2[array[2]] = binaryReader.ReadByte();
                                                            array[2]++;
                                                        }

                                                        PLCCommData.ExternalAnalogSignal.SetSignal = binaryReader.ReadByte();
                                                        PLCCommData.ExternalAnalogSignal.Pressure1 = binaryReader.ReadSingle();
                                                        PLCCommData.ExternalAnalogSignal.Pressure2 = binaryReader.ReadSingle();
                                                        goto IL_5de6;
                                                    case 44:
                                                        UserRelatedData.UserLevel = binaryReader.ReadByte();
                                                        array[1] = 0;
                                                        while (array[1] < 5)
                                                        {
                                                            UserRelatedData.UserName[array[1]] = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        UserRelatedData.Reserve1 = binaryReader.ReadByte();
                                                        UserRelatedData.Reserve2 = binaryReader.ReadByte();
                                                        UserRelatedData.Reserve3 = binaryReader.ReadByte();
                                                        UserRelatedData.Reserve4 = binaryReader.ReadByte();
                                                        UserRelatedData.Reserve5 = binaryReader.ReadByte();
                                                        UserRelatedData.ReserveSignals = binaryReader.ReadByte();
                                                        array[1] = 0;
                                                        while (array[1] < 10)
                                                        {
                                                            UserRelatedData.IpAddress[array[1]] = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 50:
                                                        ManualStartControl.Command = binaryReader.ReadByte();
                                                        ManualStartControl.ProgNum = binaryReader.ReadUInt32();
                                                        goto IL_5de6;
                                                    case 60:
                                                        LogBookWriteControl.Command = binaryReader.ReadByte();
                                                        goto IL_5de6;
                                                    case 61:
                                                        array[1] = 0;
                                                        while (array[1] < num4)
                                                        {
                                                            LogBookWriteData.LogData[array[1]].Code = binaryReader.ReadUInt32();
                                                            LogBookWriteData.LogData[array[1]].Type = binaryReader.ReadUInt16();
                                                            LogBookWriteData.LogData[array[1]].ProgNum = binaryReader.ReadUInt32();
                                                            LogBookWriteData.LogData[array[1]].Step = binaryReader.ReadByte();
                                                            LogBookWriteData.LogData[array[1]].Value1 = binaryReader.ReadSingle();
                                                            LogBookWriteData.LogData[array[1]].Value2 = binaryReader.ReadSingle();
                                                            array[2] = 0;
                                                            while (array[2] < 5)
                                                            {
                                                                LogBookWriteData.LogData[array[1]]
                                                                    .userName[array[2]] = binaryReader.ReadUInt16();
                                                                array[2]++;
                                                            }

                                                            LogBookWriteData.LogData[array[1]].UnitIndex = binaryReader.ReadByte();
                                                            LogBookWriteData.LogData[array[1]].res = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 63:
                                                        StorageSystem.Signal = binaryReader.ReadByte();
                                                        goto IL_5de6;
                                                    case 70:
                                                        MaintenanceDataBlock.BlockNum = binaryReader.ReadUInt32();
                                                        MaintenanceDataBlock.LastBlock = binaryReader.ReadUInt32();
                                                        MaintenanceDataBlock.NextBlock = binaryReader.ReadUInt32();
                                                        array[1] = 0;
                                                        while (array[1] < 32)
                                                        {
                                                            array[2] = 0;
                                                            while (array[2] < 5)
                                                            {
                                                                MaintenanceDataBlock.MaintenanceData[array[1]].userName[array[2]] = binaryReader.ReadUInt16();
                                                                array[2]++;
                                                            }

                                                            MaintenanceDataBlock.MaintenanceData[array[1]].Time.Year = binaryReader.ReadUInt16();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].Time.Month = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].Time.Day = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].Time.Hour = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].Time.Minute = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].Time.Second = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].ScheduledTime.Year = binaryReader.ReadUInt16();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].ScheduledTime.Month = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].ScheduledTime.Day = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].ScheduledTime.Hour = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].ScheduledTime.Minute = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].ScheduledTime.Second = binaryReader.ReadByte();
                                                            array[2] = 0;
                                                            while (array[2] < 100)
                                                            {
                                                                MaintenanceDataBlock.MaintenanceData[array[1]].MaintenanceText[array[2]] = binaryReader.ReadUInt16();
                                                                array[2]++;
                                                            }

                                                            MaintenanceDataBlock.MaintenanceData[array[1]].Reminder = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].ReserveByte1 = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].ReserveByte2 = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].Index = binaryReader.ReadUInt32();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].BlockNum = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].NewEntry = binaryReader.ReadByte();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].Cycle = binaryReader.ReadUInt32();
                                                            MaintenanceDataBlock.MaintenanceData[array[1]].NextCycle = binaryReader.ReadUInt32();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 71:
                                                        ComponentDataBlock.BlockNum = binaryReader.ReadUInt32();
                                                        ComponentDataBlock.LastBlock = binaryReader.ReadUInt32();
                                                        ComponentDataBlock.NextBlock = binaryReader.ReadUInt32();
                                                        array[1] = 0;
                                                        while (array[1] < 32)
                                                        {
                                                            array[2] = 0;
                                                            while (array[2] < 5)
                                                            {
                                                                ComponentDataBlock.ComponentData[array[1]].userName[array[2]] = binaryReader.ReadUInt16();
                                                                array[2]++;
                                                            }

                                                            ComponentDataBlock.ComponentData[array[1]].Time.Year = binaryReader.ReadUInt16();
                                                            ComponentDataBlock.ComponentData[array[1]].Time.Month = binaryReader.ReadByte();
                                                            ComponentDataBlock.ComponentData[array[1]].Time.Day = binaryReader.ReadByte();
                                                            ComponentDataBlock.ComponentData[array[1]].Time.Hour = binaryReader.ReadByte();
                                                            ComponentDataBlock.ComponentData[array[1]].Time.Minute = binaryReader.ReadByte();
                                                            ComponentDataBlock.ComponentData[array[1]].Time.Second = binaryReader.ReadByte();
                                                            array[2] = 0;
                                                            while (array[2] < 100)
                                                            {
                                                                ComponentDataBlock.ComponentData[array[1]].ComponentOrPartText[array[2]] = binaryReader.ReadUInt16();
                                                                array[2]++;
                                                            }

                                                            array[2] = 0;
                                                            while (array[2] < 100)
                                                            {
                                                                ComponentDataBlock.ComponentData[array[1]].ReasonText[array[2]] = binaryReader.ReadUInt16();
                                                                array[2]++;
                                                            }

                                                            array[2] = 0;
                                                            while (array[2] < 31)
                                                            {
                                                                ComponentDataBlock.ComponentData[array[1]].SerialNumber[array[2]] = binaryReader.ReadUInt16();
                                                                array[2]++;
                                                            }

                                                            ComponentDataBlock.ComponentData[array[1]].NewEntry = binaryReader.ReadByte();
                                                            ComponentDataBlock.ComponentData[array[1]].PieceCount = binaryReader.ReadUInt32();
                                                            ComponentDataBlock.ComponentData[array[1]].Cycle = binaryReader.ReadUInt32();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 80:
                                                        DownloadRequest.Request = binaryReader.ReadUInt32();
                                                        array[1] = 0;
                                                        while (array[1] < 32)
                                                        {
                                                            DownloadRequest.Info[array[1]] = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 81:
                                                        DownloadConfirmation.Result = binaryReader.ReadUInt32();
                                                        array[1] = 0;
                                                        while (array[1] < 32)
                                                        {
                                                            DownloadConfirmation.Info[array[1]] = binaryReader.ReadByte();
                                                            array[1]++;
                                                        }

                                                        goto IL_5de6;
                                                    case 86:
                                                        PlcLogBookSys.ID1 = binaryReader.ReadUInt32();
                                                        PlcLogBookSys.Position = binaryReader.ReadUInt32();
                                                        PlcLogBookSys.Length = binaryReader.ReadUInt32();
                                                        array[1] = 0;
                                                        while (array[1] < 4000)
                                                        {
                                                            PlcLogBookSys.plcLogMessBuffer[array[1]].Time.Year = binaryReader.ReadUInt16();
                                                            PlcLogBookSys.plcLogMessBuffer[array[1]].Time.Month = binaryReader.ReadByte();
                                                            PlcLogBookSys.plcLogMessBuffer[array[1]].Time.Day = binaryReader.ReadByte();
                                                            PlcLogBookSys.plcLogMessBuffer[array[1]].Time.Hour = binaryReader.ReadByte();
                                                            PlcLogBookSys.plcLogMessBuffer[array[1]].Time.Minute = binaryReader.ReadByte();
                                                            PlcLogBookSys.plcLogMessBuffer[array[1]].Time.Second = binaryReader.ReadByte();
                                                            PlcLogBookSys.plcLogMessBuffer[array[1]].Code = binaryReader.ReadUInt32();
                                                            PlcLogBookSys.plcLogMessBuffer[array[1]].Type = binaryReader.ReadUInt16();
                                                            PlcLogBookSys.plcLogMessBuffer[array[1]].cycNum = binaryReader.ReadUInt32();
                                                            PlcLogBookSys.plcLogMessBuffer[array[1]].UnitIndex = binaryReader.ReadByte();
                                                            PlcLogBookSys.plcLogMessBuffer[array[1]].ResByte = binaryReader.ReadByte();
                                                            PlcLogBookSys.plcLogMessBuffer[array[1]].ResUint1 = binaryReader.ReadUInt32();
                                                            array[1]++;
                                                        }

                                                        PlcLogBookSys.ID2 = binaryReader.ReadUInt32();
                                                        goto IL_5de6;
                                                    default:
                                                        {
                                                            log.LogError("Implementation to deserialize Block {0} is missing!", block.ToString());

                                                            result = false;
                                                            break;
                                                        }
                                                        IL_5de6:
                                                        binaryReader.Close();
                                                        memoryStream.Close();
                                                        result = true;
                                                        break;
                                                }
                                            }
                                        }

                                        break;
                                }
                            }
                        }

                        break;
                    }
                }

                return result;
            }

            log.LogError("Client has no connection to the server!");

            return false;
        }

        public bool SendVarBlock(ushort block)
        {
            int[] array = new int[50];
            bool result = false;
            if (ClientSocket != null && ClientSocket.Connected)
            {
                Monitor.Enter(ClientSocket);
                VARCOMMCommand vARCOMMCommand = default(VARCOMMCommand);
                vARCOMMCommand.ID1 = 117575940u;
                vARCOMMCommand.Direction = 1;
                vARCOMMCommand.Event = new byte[255];
                for (int i = 0; i < 255; i++)
                {
                    vARCOMMCommand.Event[i] = 0;
                }

                vARCOMMCommand.Block = block;
                vARCOMMCommand.ID2 = 4177391355u;
                MemoryStream memoryStream = new MemoryStream(SendBuffer);
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(vARCOMMCommand.ID1);
                binaryWriter.Write(vARCOMMCommand.Direction);
                binaryWriter.Write(vARCOMMCommand.Block);
                for (int i = 0; i < 255; i++)
                {
                    binaryWriter.Write(vARCOMMCommand.Event[i]);
                }

                binaryWriter.Write(vARCOMMCommand.ID2);
                int num = SocketWrite(ClientSocket, (int)memoryStream.Position, 1000, SendBuffer);
                if (num == 1)
                {
                    binaryWriter.Close();
                    memoryStream.Close();
                    memoryStream = new MemoryStream(SendBuffer);
                    BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream);
                    binaryWriter2.Write((uint)(337776900 + block));
                    switch (block)
                    {
                        case 0:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(Status0.AutoMode);
                            binaryWriter2.Write(Status0.PowerEnabled);
                            binaryWriter2.Write(Status0.ParameterMode);
                            binaryWriter2.Write(Status0.TestMode);
                            binaryWriter2.Write(Status0.StorageSystemMode);
                            binaryWriter2.Write(Status0.OwnerID1);
                            binaryWriter2.Write(Status0.OwnerID2);
                            binaryWriter2.Write(Status0.OwnerID3);
                            array[1] = 0;
                            while (array[1] < 10)
                            {
                                binaryWriter2.Write(Status0.Version[array[1]]);
                                array[1]++;
                            }

                            binaryWriter2.Write(Status0.TestError);
                            array[1] = 0;
                            while (array[1] < 1000)
                            {
                                binaryWriter2.Write(Status0.TestErrorText[array[1]]);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 1:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(Status1.RequestID);
                            binaryWriter2.Write(Status1.VisuLive);
                            goto IL_5e01;
                        case 2:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(Status2.RequestID);
                            binaryWriter2.Write(Status2.VisuLive);
                            goto IL_5e01;
                        case 3:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(Status3.RequestID);
                            binaryWriter2.Write(Status3.VisuLive);
                            goto IL_5e01;
                        case 4:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(SaveSys.RequestBlock);
                            goto IL_5e01;
                        case 5:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(ErrorSys.Num);
                            array[1] = 0;
                            while (array[1] < 1000)
                            {
                                binaryWriter2.Write(ErrorSys.Text[array[1]]);
                                array[1]++;
                            }

                            binaryWriter2.Write(ErrorSys.SpindleTest);
                            binaryWriter2.Write(ErrorSys.Warning);
                            binaryWriter2.Write(ErrorSys.Quit);
                            binaryWriter2.Write(ErrorSys.PlcError);
                            goto IL_5e01;
                        case 7:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(LogBookSys.Position);
                            binaryWriter2.Write(LogBookSys.Length);
                            array[1] = 0;
                            while (array[1] < 2500)
                            {
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].Time.Year);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].Time.Month);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].Time.Day);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].Time.Hour);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].Time.Minute);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].Time.Second);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].Code);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].Type);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].ProgNum);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].Step);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].Value1);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].Value2);
                                array[2] = 0;
                                while (array[2] < 5)
                                {
                                    binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].userName[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].cycNum);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].UnitIndex);
                                binaryWriter2.Write(LogBookSys.logMessBuffer[array[1]].res);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 8:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(Result.Prog.byteReserve_1);
                            binaryWriter2.Write(Result.Prog.Info.ProgNum);
                            array[3] = 0;
                            while (array[3] < 32)
                            {
                                binaryWriter2.Write(Result.Prog.Info.Name[array[3]]);
                                array[3]++;
                            }

                            binaryWriter2.Write(Result.Prog.Info.Steps);
                            binaryWriter2.Write(Result.Prog.Info.ResultParam1);
                            binaryWriter2.Write(Result.Prog.Info.ResultParam2);
                            binaryWriter2.Write(Result.Prog.Info.ResultParam3);
                            binaryWriter2.Write(Result.Prog.M1FilterTime);
                            binaryWriter2.Write(Result.Prog.GradientLength);
                            binaryWriter2.Write(Result.Prog.GradientFilter);
                            binaryWriter2.Write(Result.Prog.ADepthFilterTime);
                            binaryWriter2.Write(Result.Prog.ADepthGradientLength);
                            binaryWriter2.Write(Result.Prog.MaxTime);
                            binaryWriter2.Write(Result.Prog.PressureHolder);
                            binaryWriter2.Write(Result.Prog.EndSetDigOut1);
                            binaryWriter2.Write(Result.Prog.EndValueDigOut1);
                            binaryWriter2.Write(Result.Prog.EndSetDigOut2);
                            binaryWriter2.Write(Result.Prog.EndValueDigOut2);
                            binaryWriter2.Write(Result.Prog.EndSetSync1);
                            binaryWriter2.Write(Result.Prog.EndValueSync1);
                            binaryWriter2.Write(Result.Prog.EndSetSync2);
                            binaryWriter2.Write(Result.Prog.EndValueSync2);
                            array[2] = 0;
                            while (array[2] < 25)
                            {
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Type);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Switch);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].IsResult1);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].IsResult2);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].IsResult3);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Enable.Torque);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Enable.Snug);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Enable.FTorque);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Enable.GradientMin);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Enable.GradientMax);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Enable.Angle);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Enable.Time);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Enable.ADepth);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Enable.ADepthGradMin);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Enable.ADepthGradMax);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Enable.Ana);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Enable.Release);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].NA);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].TM);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].MP);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Mmin);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Mmax);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].MS);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].MRP);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].MRStep);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].MRType);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].MDelayTime);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].MFP);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].MFmin);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].MFmax);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].MGP);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].MGmin);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].MGmax);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].WP);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Wmin);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Wmax);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].WN);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].TP);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Tmin);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Tmax);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].TN);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].LP);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Lmin);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].Lmax);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].LGP);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].LGmin);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].LGmax);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].AnaP);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].AnaMin);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].AnaMax);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].DigP);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].DigMin);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].DigMax);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].JumpTo);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].ModDigOut);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].PressureSpindle);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].CountPassMax);
                                binaryWriter2.Write(Result.Prog.Step[array[2]].UserRights);
                                array[2]++;
                            }

                            binaryWriter2.Write(Result.Prog.UseLocalJawSettings);
                            binaryWriter2.Write(Result.Prog.JawLocalWrittenOnce);
                            binaryWriter2.Write(Result.Prog.JawOpenDistance);
                            binaryWriter2.Write(Result.Prog.JawOpenDepthGradMax);
                            binaryWriter2.Write(Result.Prog.JawOpenDepthGradMin);
                            array[3] = 0;
                            while (array[3] < 4)
                            {
                                array[4] = 0;
                                while (array[4] < 7)
                                {
                                    binaryWriter2.Write(Result.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].StepIndex);
                                    binaryWriter2.Write(Result.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].TypeOfData);
                                    binaryWriter2.Write(Result.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].Value);
                                    binaryWriter2.Write(Result.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MinValue);
                                    binaryWriter2.Write(Result.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MaxValue);
                                    array[4]++;
                                }

                                array[3]++;
                            }

                            binaryWriter2.Write(Result.UnitTorque);
                            binaryWriter2.Write(Result.IONIO);
                            binaryWriter2.Write(Result.LastStep);
                            binaryWriter2.Write(Result.Time.Year);
                            binaryWriter2.Write(Result.Time.Month);
                            binaryWriter2.Write(Result.Time.Day);
                            binaryWriter2.Write(Result.Time.Hour);
                            binaryWriter2.Write(Result.Time.Minute);
                            binaryWriter2.Write(Result.Time.Second);
                            binaryWriter2.Write(Result.Cycle);
                            binaryWriter2.Write(Result.ScrewDuration);
                            array[1] = 0;
                            while (array[1] < 250)
                            {
                                binaryWriter2.Write(Result.StepResult[array[1]].Step);
                                binaryWriter2.Write(Result.StepResult[array[1]].IONIO);
                                binaryWriter2.Write(Result.StepResult[array[1]].OrgStep);
                                binaryWriter2.Write(Result.StepResult[array[1]].Torque);
                                binaryWriter2.Write(Result.StepResult[array[1]].MaxTorque);
                                binaryWriter2.Write(Result.StepResult[array[1]].FTorque);
                                binaryWriter2.Write(Result.StepResult[array[1]].DelayTorque);
                                binaryWriter2.Write(Result.StepResult[array[1]].M360Follow);
                                binaryWriter2.Write(Result.StepResult[array[1]].Gradient);
                                binaryWriter2.Write(Result.StepResult[array[1]].Angle);
                                binaryWriter2.Write(Result.StepResult[array[1]].Time);
                                binaryWriter2.Write(Result.StepResult[array[1]].ADepth);
                                binaryWriter2.Write(Result.StepResult[array[1]].ADepthGrad);
                                binaryWriter2.Write(Result.StepResult[array[1]].Ana);
                                binaryWriter2.Write(Result.StepResult[array[1]].Dig);
                                array[1]++;
                            }

                            binaryWriter2.Write(Result.Valid);
                            binaryWriter2.Write(Result.ResultStep1);
                            binaryWriter2.Write(Result.Res1);
                            binaryWriter2.Write(Result.ResultStep2);
                            binaryWriter2.Write(Result.Res2);
                            binaryWriter2.Write(Result.ResultStep3);
                            binaryWriter2.Write(Result.Res3);
                            array[1] = 0;
                            while (array[1] < 32)
                            {
                                binaryWriter2.Write(Result.ScrewID[array[1]]);
                                array[1]++;
                            }

                            array[1] = 0;
                            while (array[1] < 20)
                            {
                                binaryWriter2.Write(Result.ExtendedResult[array[1]]);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 9:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(LastNIOResults.ID1);
                            array[1] = 0;
                            while (array[1] < 100)
                            {
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].ProgNum);
                                array[2] = 0;
                                while (array[2] < 32)
                                {
                                    binaryWriter2.Write(LastNIOResults.Num[array[1]].ProgName[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(LastNIOResults.Num[array[1]].IONIO);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].LastStep);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].Time.Year);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].Time.Month);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].Time.Day);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].Time.Hour);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].Time.Minute);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].Time.Second);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].Cycle);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].ScrewDuration);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].Valid);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].ResultStep1);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].ResultParam1);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].Res1);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].ResultStep2);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].ResultParam2);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].Res2);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].ResultStep3);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].ResultParam3);
                                binaryWriter2.Write(LastNIOResults.Num[array[1]].Res3);
                                array[2] = 0;
                                while (array[2] < 32)
                                {
                                    binaryWriter2.Write(LastNIOResults.Num[array[1]].ScrewID[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(LastNIOResults.Num[array[1]].res);
                                array[1]++;
                            }

                            binaryWriter2.Write(LastNIOResults.ID2);
                            goto IL_5e01;
                        case 10:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(PProg.PProgVersion);
                            array[1] = 0;
                            while (array[1] < 1024)
                            {
                                binaryWriter2.Write(PProg.Num[array[1]].byteReserve_1);
                                binaryWriter2.Write(PProg.Num[array[1]].Info.ProgNum);
                                array[3] = 0;
                                while (array[3] < 32)
                                {
                                    binaryWriter2.Write(PProg.Num[array[1]].Info.Name[array[3]]);
                                    array[3]++;
                                }

                                binaryWriter2.Write(PProg.Num[array[1]].Info.Steps);
                                binaryWriter2.Write(PProg.Num[array[1]].Info.ResultParam1);
                                binaryWriter2.Write(PProg.Num[array[1]].Info.ResultParam2);
                                binaryWriter2.Write(PProg.Num[array[1]].Info.ResultParam3);
                                binaryWriter2.Write(PProg.Num[array[1]].M1FilterTime);
                                binaryWriter2.Write(PProg.Num[array[1]].GradientLength);
                                binaryWriter2.Write(PProg.Num[array[1]].GradientFilter);
                                binaryWriter2.Write(PProg.Num[array[1]].ADepthFilterTime);
                                binaryWriter2.Write(PProg.Num[array[1]].ADepthGradientLength);
                                binaryWriter2.Write(PProg.Num[array[1]].MaxTime);
                                binaryWriter2.Write(PProg.Num[array[1]].PressureHolder);
                                binaryWriter2.Write(PProg.Num[array[1]].EndSetDigOut1);
                                binaryWriter2.Write(PProg.Num[array[1]].EndValueDigOut1);
                                binaryWriter2.Write(PProg.Num[array[1]].EndSetDigOut2);
                                binaryWriter2.Write(PProg.Num[array[1]].EndValueDigOut2);
                                binaryWriter2.Write(PProg.Num[array[1]].EndSetSync1);
                                binaryWriter2.Write(PProg.Num[array[1]].EndValueSync1);
                                binaryWriter2.Write(PProg.Num[array[1]].EndSetSync2);
                                binaryWriter2.Write(PProg.Num[array[1]].EndValueSync2);
                                array[2] = 0;
                                while (array[2] < 25)
                                {
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Type);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Switch);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].IsResult1);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].IsResult2);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].IsResult3);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Enable.Torque);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Enable.Snug);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Enable.FTorque);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Enable.GradientMin);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Enable.GradientMax);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Enable.Angle);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Enable.Time);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Enable.ADepth);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Enable.ADepthGradMin);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Enable.ADepthGradMax);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Enable.Ana);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Enable.Release);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].NA);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].TM);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].MP);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Mmin);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Mmax);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].MS);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].MRP);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].MRStep);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].MRType);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].MDelayTime);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].MFP);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].MFmin);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].MFmax);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].MGP);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].MGmin);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].MGmax);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].WP);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Wmin);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Wmax);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].WN);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].TP);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Tmin);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Tmax);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].TN);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].LP);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Lmin);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].Lmax);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].LGP);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].LGmin);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].LGmax);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].AnaP);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].AnaMin);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].AnaMax);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].DigP);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].DigMin);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].DigMax);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].JumpTo);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].ModDigOut);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].PressureSpindle);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].CountPassMax);
                                    binaryWriter2.Write(PProg.Num[array[1]].Step[array[2]].UserRights);
                                    array[2]++;
                                }

                                binaryWriter2.Write(PProg.Num[array[1]].UseLocalJawSettings);
                                binaryWriter2.Write(PProg.Num[array[1]].JawLocalWrittenOnce);
                                binaryWriter2.Write(PProg.Num[array[1]].JawOpenDistance);
                                binaryWriter2.Write(PProg.Num[array[1]].JawOpenDepthGradMax);
                                binaryWriter2.Write(PProg.Num[array[1]].JawOpenDepthGradMin);
                                array[3] = 0;
                                while (array[3] < 4)
                                {
                                    array[4] = 0;
                                    while (array[4] < 7)
                                    {
                                        binaryWriter2.Write(PProg.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].StepIndex);
                                        binaryWriter2.Write(PProg.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].TypeOfData);
                                        binaryWriter2.Write(PProg.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].Value);
                                        binaryWriter2.Write(PProg.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MinValue);
                                        binaryWriter2.Write(PProg.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MaxValue);
                                        array[4]++;
                                    }

                                    array[3]++;
                                }

                                array[1]++;
                            }

                            goto IL_5e01;
                        case 11:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(SpConst.TorqueSensorScale);
                            binaryWriter2.Write(SpConst.TorqueSensorTolerance);
                            binaryWriter2.Write(SpConst.AngleSensorScale);
                            binaryWriter2.Write(SpConst.TorqueSensorInvers);
                            binaryWriter2.Write(SpConst.AngleSensorInvers);
                            binaryWriter2.Write(SpConst.RedundantSensorActive);
                            binaryWriter2.Write(SpConst.TorqueRedundantTime);
                            binaryWriter2.Write(SpConst.TorqueRedundantTolerance);
                            binaryWriter2.Write(SpConst.AngleRedundantTolerance);
                            binaryWriter2.Write(SpConst.DriveUnitRpm);
                            binaryWriter2.Write(SpConst.DriveUnitInvers);
                            binaryWriter2.Write(SpConst.DepthSensorScale);
                            binaryWriter2.Write(SpConst.DepthSensorOffset);
                            binaryWriter2.Write(SpConst.DepthSensorOffsetMin);
                            binaryWriter2.Write(SpConst.DepthSensorOffsetMax);
                            binaryWriter2.Write(SpConst.DepthSensorOffsetPreset);
                            binaryWriter2.Write(SpConst.DepthSensorInvers);
                            binaryWriter2.Write(SpConst.AnaSigScale);
                            binaryWriter2.Write(SpConst.AnaSigOffset);
                            binaryWriter2.Write(SpConst.SpindleTorque);
                            binaryWriter2.Write(SpConst.SpindleGearFactor);
                            binaryWriter2.Write(SpConst.ReleaseSpeed);
                            binaryWriter2.Write(SpConst.FrictionTorque);
                            binaryWriter2.Write(SpConst.FrictionSpeed);
                            binaryWriter2.Write(SpConst.FrictionTestStartup);
                            binaryWriter2.Write(SpConst.FrictionTestEMG);
                            binaryWriter2.Write(SpConst.PressureScaleSpindle);
                            binaryWriter2.Write(SpConst.ReserveFloat_1);
                            binaryWriter2.Write(SpConst.PressureScaleHolder);
                            binaryWriter2.Write(SpConst.JawOpenDistance);
                            binaryWriter2.Write(SpConst.JawOpenDepthGradMax);
                            binaryWriter2.Write(SpConst.JawOpenDepthGradMin);
                            binaryWriter2.Write(SpConst.ReserveByte_1);
                            binaryWriter2.Write(SpConst.UsbKeyActivated);
                            goto IL_5e01;
                        case 12:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(SysConst.ActualTime.Year);
                            binaryWriter2.Write(SysConst.ActualTime.Month);
                            binaryWriter2.Write(SysConst.ActualTime.Day);
                            binaryWriter2.Write(SysConst.ActualTime.Hour);
                            binaryWriter2.Write(SysConst.ActualTime.Minute);
                            binaryWriter2.Write(SysConst.ActualTime.Second);
                            binaryWriter2.Write(SysConst.SettingTime.Year);
                            binaryWriter2.Write(SysConst.SettingTime.Month);
                            binaryWriter2.Write(SysConst.SettingTime.Day);
                            binaryWriter2.Write(SysConst.SettingTime.Hour);
                            binaryWriter2.Write(SysConst.SettingTime.Minute);
                            binaryWriter2.Write(SysConst.SettingTime.Second);
                            array[1] = 0;
                            while (array[1] < 20)
                            {
                                binaryWriter2.Write(SysConst.SystemID[array[1]]);
                                array[1]++;
                            }

                            array[1] = 0;
                            while (array[1] < 32)
                            {
                                binaryWriter2.Write(SysConst.IdentServerName[array[1]]);
                                array[1]++;
                            }

                            binaryWriter2.Write(SysConst.IPAddress.Byte1);
                            binaryWriter2.Write(SysConst.IPAddress.Byte2);
                            binaryWriter2.Write(SysConst.IPAddress.Byte3);
                            binaryWriter2.Write(SysConst.IPAddress.Byte4);
                            binaryWriter2.Write(SysConst.DHCP);
                            binaryWriter2.Write(SysConst.SubNetMask.Byte1);
                            binaryWriter2.Write(SysConst.SubNetMask.Byte2);
                            binaryWriter2.Write(SysConst.SubNetMask.Byte3);
                            binaryWriter2.Write(SysConst.SubNetMask.Byte4);
                            binaryWriter2.Write(SysConst.DefaultGateway.Byte1);
                            binaryWriter2.Write(SysConst.DefaultGateway.Byte2);
                            binaryWriter2.Write(SysConst.DefaultGateway.Byte3);
                            binaryWriter2.Write(SysConst.DefaultGateway.Byte4);
                            array[1] = 0;
                            while (array[1] < 5)
                            {
                                array[2] = 0;
                                while (array[2] < 32)
                                {
                                    binaryWriter2.Write(SysConst.AdvancedWarnings[array[1]].Name[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(SysConst.AdvancedWarnings[array[1]].Limit);
                                binaryWriter2.Write(SysConst.AdvancedWarnings[array[1]].Advance);
                                binaryWriter2.Write(SysConst.AdvancedWarnings[array[1]].AdvancedWarningTime.Year);
                                binaryWriter2.Write(SysConst.AdvancedWarnings[array[1]].AdvancedWarningTime.Month);
                                binaryWriter2.Write(SysConst.AdvancedWarnings[array[1]].AdvancedWarningTime.Day);
                                binaryWriter2.Write(SysConst.AdvancedWarnings[array[1]].AdvancedWarningTime.Hour);
                                binaryWriter2.Write(SysConst.AdvancedWarnings[array[1]].AdvancedWarningTime
                                    .Minute);
                                binaryWriter2.Write(SysConst.AdvancedWarnings[array[1]].AdvancedWarningTime
                                    .Second);
                                binaryWriter2.Write(SysConst.AdvancedWarnings[array[1]].AdvancedDays);
                                binaryWriter2.Write(SysConst.AdvancedWarnings[array[1]].EnableAdvancedWarningTime);
                                array[1]++;
                            }

                            binaryWriter2.Write(SysConst.UnitTorque);
                            binaryWriter2.Write(SysConst.Com1.BaudRate);
                            binaryWriter2.Write(SysConst.Com1.Parity);
                            array[1] = 0;
                            while (array[1] < 20)
                            {
                                array[2] = 0;
                                while (array[2] < 5)
                                {
                                    binaryWriter2.Write(SysConst.PassCodes[array[1]].Name[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(SysConst.PassCodes[array[1]].Code);
                                binaryWriter2.Write(SysConst.PassCodes[array[1]].Level);
                                array[1]++;
                            }

                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_BackupForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_CheckParamForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_CycleCounterForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_EditSteps);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_PrgOptParameterForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_ProgramOverviewForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_SpindleConstantsForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_StatisticsLastResForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_StepOverviewForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_SystemConstantsForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_TestIOForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_TestMotorSensorForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_VisualisationParamForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_FourStepEditForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_Maintenance);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_HandStartForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_BrowserForm);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_Reserve1);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_Reserve2);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_Reserve3);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_Reserve4);
                            binaryWriter2.Write(SysConst.UserLevels.UserLevel_Reserve5);
                            array[1] = 0;
                            while (array[1] < 16)
                            {
                                binaryWriter2.Write(SysConst.AreaCode[array[1]]);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 15:
                            binaryWriter2.Write(0u);
                            array[1] = 0;
                            while (array[1] < 1024)
                            {
                                binaryWriter2.Write(ProcessInfo.ProgInfo[array[1]].ProgNum);
                                array[2] = 0;
                                while (array[2] < 32)
                                {
                                    binaryWriter2.Write(ProcessInfo.ProgInfo[array[1]].Name[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(ProcessInfo.ProgInfo[array[1]].Steps);
                                binaryWriter2.Write(ProcessInfo.ProgInfo[array[1]].ResultParam1);
                                binaryWriter2.Write(ProcessInfo.ProgInfo[array[1]].ResultParam2);
                                binaryWriter2.Write(ProcessInfo.ProgInfo[array[1]].ResultParam3);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 17:
                            binaryWriter2.Write(0u);
                            array[1] = 0;
                            while (array[1] < 1024)
                            {
                                binaryWriter2.Write(PProgXChanged.Changed[array[1]]);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 18:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(PProgX.PProgVersion);
                            array[1] = 0;
                            while (array[1] < 32)
                            {
                                binaryWriter2.Write(PProgX.Num[array[1]].byteReserve_1);
                                binaryWriter2.Write(PProgX.Num[array[1]].Info.ProgNum);
                                array[3] = 0;
                                while (array[3] < 32)
                                {
                                    binaryWriter2.Write(PProgX.Num[array[1]].Info.Name[array[3]]);
                                    array[3]++;
                                }

                                binaryWriter2.Write(PProgX.Num[array[1]].Info.Steps);
                                binaryWriter2.Write(PProgX.Num[array[1]].Info.ResultParam1);
                                binaryWriter2.Write(PProgX.Num[array[1]].Info.ResultParam2);
                                binaryWriter2.Write(PProgX.Num[array[1]].Info.ResultParam3);
                                binaryWriter2.Write(PProgX.Num[array[1]].M1FilterTime);
                                binaryWriter2.Write(PProgX.Num[array[1]].GradientLength);
                                binaryWriter2.Write(PProgX.Num[array[1]].GradientFilter);
                                binaryWriter2.Write(PProgX.Num[array[1]].ADepthFilterTime);
                                binaryWriter2.Write(PProgX.Num[array[1]].ADepthGradientLength);
                                binaryWriter2.Write(PProgX.Num[array[1]].MaxTime);
                                binaryWriter2.Write(PProgX.Num[array[1]].PressureHolder);
                                binaryWriter2.Write(PProgX.Num[array[1]].EndSetDigOut1);
                                binaryWriter2.Write(PProgX.Num[array[1]].EndValueDigOut1);
                                binaryWriter2.Write(PProgX.Num[array[1]].EndSetDigOut2);
                                binaryWriter2.Write(PProgX.Num[array[1]].EndValueDigOut2);
                                binaryWriter2.Write(PProgX.Num[array[1]].EndSetSync1);
                                binaryWriter2.Write(PProgX.Num[array[1]].EndValueSync1);
                                binaryWriter2.Write(PProgX.Num[array[1]].EndSetSync2);
                                binaryWriter2.Write(PProgX.Num[array[1]].EndValueSync2);
                                array[2] = 0;
                                while (array[2] < 25)
                                {
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Type);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Switch);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].IsResult1);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].IsResult2);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].IsResult3);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Enable.Torque);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Enable.Snug);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Enable.FTorque);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Enable.GradientMin);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Enable.GradientMax);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Enable.Angle);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Enable.Time);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Enable.ADepth);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Enable.ADepthGradMin);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Enable.ADepthGradMax);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Enable.Ana);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Enable.Release);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].NA);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].TM);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].MP);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Mmin);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Mmax);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].MS);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].MRP);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].MRStep);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].MRType);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].MDelayTime);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].MFP);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].MFmin);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].MFmax);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].MGP);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].MGmin);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].MGmax);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].WP);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Wmin);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Wmax);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].WN);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].TP);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Tmin);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Tmax);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].TN);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].LP);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Lmin);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].Lmax);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].LGP);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].LGmin);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].LGmax);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].AnaP);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].AnaMin);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].AnaMax);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].DigP);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].DigMin);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].DigMax);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].JumpTo);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].ModDigOut);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].PressureSpindle);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].CountPassMax);
                                    binaryWriter2.Write(PProgX.Num[array[1]].Step[array[2]].UserRights);
                                    array[2]++;
                                }

                                binaryWriter2.Write(PProgX.Num[array[1]].UseLocalJawSettings);
                                binaryWriter2.Write(PProgX.Num[array[1]].JawLocalWrittenOnce);
                                binaryWriter2.Write(PProgX.Num[array[1]].JawOpenDistance);
                                binaryWriter2.Write(PProgX.Num[array[1]].JawOpenDepthGradMax);
                                binaryWriter2.Write(PProgX.Num[array[1]].JawOpenDepthGradMin);
                                array[3] = 0;
                                while (array[3] < 4)
                                {
                                    array[4] = 0;
                                    while (array[4] < 7)
                                    {
                                        binaryWriter2.Write(PProgX.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].StepIndex);
                                        binaryWriter2.Write(PProgX.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].TypeOfData);
                                        binaryWriter2.Write(PProgX.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].Value);
                                        binaryWriter2.Write(PProgX.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MinValue);
                                        binaryWriter2.Write(PProgX.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MaxValue);
                                        array[4]++;
                                    }

                                    array[3]++;
                                }

                                array[1]++;
                            }

                            goto IL_5e01;
                        case 20:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(CurveDef.Points);
                            binaryWriter2.Write(CurveDef.SampleTime);
                            binaryWriter2.Write(CurveDef.SpeedSetScale);
                            binaryWriter2.Write(CurveDef.SpeedActScale);
                            binaryWriter2.Write(CurveDef.UnitTorque);
                            goto IL_5e01;
                        case 21:
                            binaryWriter2.Write(CurveData_elements);
                            array[1] = 0;
                            while (array[1] < CurveData_elements)
                            {
                                binaryWriter2.Write(CurveData.Point[array[1]].Nset);
                                binaryWriter2.Write(CurveData.Point[array[1]].Nact);
                                binaryWriter2.Write(CurveData.Point[array[1]].Torque);
                                binaryWriter2.Write(CurveData.Point[array[1]].FiltTorque);
                                binaryWriter2.Write(CurveData.Point[array[1]].Angle);
                                binaryWriter2.Write(CurveData.Point[array[1]].DDepth);
                                binaryWriter2.Write(CurveData.Point[array[1]].ADepth);
                                binaryWriter2.Write(CurveData.Point[array[1]].ADepthGrad);
                                binaryWriter2.Write(CurveData.Point[array[1]].AExt);
                                binaryWriter2.Write(CurveData.Point[array[1]].Gradient);
                                binaryWriter2.Write(CurveData.Point[array[1]].CurrentStep);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 30:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(CycleCount.ID1);
                            binaryWriter2.Write(CycleCount.Machine);
                            binaryWriter2.Write(CycleCount.Customer);
                            array[1] = 0;
                            while (array[1] < 5)
                            {
                                binaryWriter2.Write(CycleCount.Count[array[1]]);
                                array[1]++;
                            }

                            binaryWriter2.Write(CycleCount.MachineNIO);
                            binaryWriter2.Write(CycleCount.CustomerNIO);
                            binaryWriter2.Write(CycleCount.CountReserve);
                            binaryWriter2.Write(CycleCount.ID2);
                            goto IL_5e01;
                        case 32:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(StatSample.ID1);
                            binaryWriter2.Write(StatSample.Info.Length);
                            binaryWriter2.Write(StatSample.Info.Position);
                            array[1] = 0;
                            while (array[1] < 2000)
                            {
                                binaryWriter2.Write(StatSample.Data[array[1]].ProgNum);
                                array[2] = 0;
                                while (array[2] < 32)
                                {
                                    binaryWriter2.Write(StatSample.Data[array[1]].ProgName[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(StatSample.Data[array[1]].IONIO);
                                binaryWriter2.Write(StatSample.Data[array[1]].LastStep);
                                binaryWriter2.Write(StatSample.Data[array[1]].Time.Year);
                                binaryWriter2.Write(StatSample.Data[array[1]].Time.Month);
                                binaryWriter2.Write(StatSample.Data[array[1]].Time.Day);
                                binaryWriter2.Write(StatSample.Data[array[1]].Time.Hour);
                                binaryWriter2.Write(StatSample.Data[array[1]].Time.Minute);
                                binaryWriter2.Write(StatSample.Data[array[1]].Time.Second);
                                binaryWriter2.Write(StatSample.Data[array[1]].Cycle);
                                binaryWriter2.Write(StatSample.Data[array[1]].ScrewDuration);
                                binaryWriter2.Write(StatSample.Data[array[1]].Valid);
                                binaryWriter2.Write(StatSample.Data[array[1]].ResultStep1);
                                binaryWriter2.Write(StatSample.Data[array[1]].ResultParam1);
                                binaryWriter2.Write(StatSample.Data[array[1]].Res1);
                                binaryWriter2.Write(StatSample.Data[array[1]].ResultStep2);
                                binaryWriter2.Write(StatSample.Data[array[1]].ResultParam2);
                                binaryWriter2.Write(StatSample.Data[array[1]].Res2);
                                binaryWriter2.Write(StatSample.Data[array[1]].ResultStep3);
                                binaryWriter2.Write(StatSample.Data[array[1]].ResultParam3);
                                binaryWriter2.Write(StatSample.Data[array[1]].Res3);
                                array[2] = 0;
                                while (array[2] < 32)
                                {
                                    binaryWriter2.Write(StatSample.Data[array[1]].ScrewID[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(StatSample.Data[array[1]].res);
                                array[1]++;
                            }

                            binaryWriter2.Write(StatSample.ID2);
                            goto IL_5e01;
                        case 33:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(StatControl.Command);
                            goto IL_5e01;
                        case 40:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(TestDataRawIn.ISRCount);
                            binaryWriter2.Write(TestDataRawIn.DI);
                            binaryWriter2.Write(TestDataRawIn.DO);
                            binaryWriter2.Write(TestDataRawIn.HexSwitch);
                            binaryWriter2.Write(TestDataRawIn.Ui0);
                            binaryWriter2.Write(TestDataRawIn.Ui1);
                            binaryWriter2.Write(TestDataRawIn.Ui2);
                            binaryWriter2.Write(TestDataRawIn.Ui3);
                            binaryWriter2.Write(TestDataRawIn.UErr);
                            binaryWriter2.Write(TestDataRawIn.Enc0);
                            binaryWriter2.Write(TestDataRawIn.Enc1);
                            binaryWriter2.Write(TestDataRawIn.Enc3);
                            binaryWriter2.Write(TestDataRawIn.EncErr);
                            binaryWriter2.Write(TestDataRawIn.GRADCount);
                            binaryWriter2.Write(TestDataRawIn.Nact);
                            binaryWriter2.Write(TestDataRawIn.Torque1);
                            binaryWriter2.Write(TestDataRawIn.Angle1);
                            binaryWriter2.Write(TestDataRawIn.Torque2);
                            binaryWriter2.Write(TestDataRawIn.Angle2);
                            binaryWriter2.Write(TestDataRawIn.ADepth);
                            binaryWriter2.Write(TestDataRawIn.ExtAna);
                            binaryWriter2.Write(TestDataRawIn.BatteryOk);
                            goto IL_5e01;
                        case 41:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(TestDataRawOut.Command);
                            binaryWriter2.Write(TestDataRawOut.DO16);
                            binaryWriter2.Write(TestDataRawOut.DONr);
                            binaryWriter2.Write(TestDataRawOut.DOState);
                            binaryWriter2.Write(TestDataRawOut.Uo0);
                            binaryWriter2.Write(TestDataRawOut.Uo1);
                            binaryWriter2.Write(TestDataRawOut.Uo2);
                            binaryWriter2.Write(TestDataRawOut.Uo3);
                            binaryWriter2.Write(TestDataRawOut.ResetEnc0);
                            binaryWriter2.Write(TestDataRawOut.ResetEnc1);
                            binaryWriter2.Write(TestDataRawOut.ResetEnc2);
                            binaryWriter2.Write(TestDataRawOut.ResetEncErr);
                            binaryWriter2.Write(TestDataRawOut.ResetAngle);
                            binaryWriter2.Write(TestDataRawOut.STSpeed);
                            binaryWriter2.Write(TestDataRawOut.STEnable);
                            goto IL_5e01;
                        case 42:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(PLCCommData.Input.Automatic);
                            binaryWriter2.Write(PLCCommData.Input.Start);
                            binaryWriter2.Write(PLCCommData.Input.Quit);
                            binaryWriter2.Write(PLCCommData.Input.Sync1);
                            binaryWriter2.Write(PLCCommData.Input.Sync2);
                            binaryWriter2.Write(PLCCommData.Input.KalDisable);
                            binaryWriter2.Write(PLCCommData.Input.TeachAnalogDepth);
                            binaryWriter2.Write(PLCCommData.Input.ProgNum);
                            array[2] = 0;
                            while (array[2] < 32)
                            {
                                binaryWriter2.Write(PLCCommData.Input.ScrewID[array[2]]);
                                array[2]++;
                            }

                            binaryWriter2.Write(PLCCommData.Input.LivingSignRequest);
                            binaryWriter2.Write(PLCCommData.Input.Reserve1);
                            binaryWriter2.Write(PLCCommData.Input.Reserve2);
                            binaryWriter2.Write(PLCCommData.Input.Reserve3);
                            binaryWriter2.Write(PLCCommData.Input.AnalogsignalCurve);
                            binaryWriter2.Write(PLCCommData.Input.LivingSignEnabled);
                            binaryWriter2.Write(PLCCommData.Input.ReserveSignals);
                            array[2] = 0;
                            while (array[2] < 10)
                            {
                                binaryWriter2.Write(PLCCommData.Input.ReserveStr[array[2]]);
                                array[2]++;
                            }

                            binaryWriter2.Write(PLCCommData.Input.UsingProgName);
                            binaryWriter2.Write(PLCCommData.Input.Reserve5);
                            array[2] = 0;
                            while (array[2] < 32)
                            {
                                binaryWriter2.Write(PLCCommData.Input.ProgName[array[2]]);
                                array[2]++;
                            }

                            array[2] = 0;
                            while (array[2] < 20)
                            {
                                binaryWriter2.Write(PLCCommData.Input.ExtendedResult[array[2]]);
                                array[2]++;
                            }

                            binaryWriter2.Write(PLCCommData.Input.PlcError);
                            binaryWriter2.Write(PLCCommData.Input.CounterNIO);
                            binaryWriter2.Write(PLCCommData.Input.CounterIOTotal);
                            binaryWriter2.Write(PLCCommData.Input.CounterNIOTotal);
                            binaryWriter2.Write(PLCCommData.Input.PressureSpindle);
                            binaryWriter2.Write(PLCCommData.Input.PressureHolder);
                            binaryWriter2.Write(PLCCommData.Input.Res1);
                            binaryWriter2.Write(PLCCommData.Output.SystemOK);
                            binaryWriter2.Write(PLCCommData.Output.ReadyToStart);
                            binaryWriter2.Write(PLCCommData.Output.ProcessRunning);
                            binaryWriter2.Write(PLCCommData.Output.IO);
                            binaryWriter2.Write(PLCCommData.Output.NIO);
                            binaryWriter2.Write(PLCCommData.Output.Sync1);
                            binaryWriter2.Write(PLCCommData.Output.Sync2);
                            binaryWriter2.Write(PLCCommData.Output.PowerEnabled);
                            binaryWriter2.Write(PLCCommData.Output.TM1);
                            binaryWriter2.Write(PLCCommData.Output.TM2);
                            binaryWriter2.Write(PLCCommData.Output.ExtDigIn);
                            binaryWriter2.Write(PLCCommData.Output.StorageSignals);
                            binaryWriter2.Write(PLCCommData.Output.AnaDepthMM);
                            binaryWriter2.Write(PLCCommData.Output.AnaDepthVolt);
                            binaryWriter2.Write(PLCCommData.Output.ExtAna);
                            binaryWriter2.Write(PLCCommData.Output.LivingSignResponse);
                            binaryWriter2.Write(PLCCommData.Output.LivingMonitor);
                            binaryWriter2.Write(PLCCommData.Output.UserLevel);
                            array[2] = 0;
                            while (array[2] < 5)
                            {
                                binaryWriter2.Write(PLCCommData.Output.UserName[array[2]]);
                                array[2]++;
                            }

                            binaryWriter2.Write(PLCCommData.Output.Reserve1);
                            binaryWriter2.Write(PLCCommData.Output.Reserve2);
                            binaryWriter2.Write(PLCCommData.Output.Reserve3);
                            binaryWriter2.Write(PLCCommData.Output.DriveUnitInvers);
                            binaryWriter2.Write(PLCCommData.Output.LivingSignEnabled);
                            binaryWriter2.Write(PLCCommData.Output.ReserveSignals);
                            array[2] = 0;
                            while (array[2] < 10)
                            {
                                binaryWriter2.Write(PLCCommData.Output.IpAddress[array[2]]);
                                array[2]++;
                            }

                            binaryWriter2.Write(PLCCommData.Output.MaintenanceCounterReached);
                            binaryWriter2.Write(PLCCommData.Output.AdvancedCounterReached);
                            binaryWriter2.Write(PLCCommData.Output.PressureSpindle);
                            binaryWriter2.Write(PLCCommData.Output.PressureHolder);
                            binaryWriter2.Write(PLCCommData.Output.PressureScaleSpindle);
                            binaryWriter2.Write(PLCCommData.Output.PressureScaleHolder);
                            binaryWriter2.Write(PLCCommData.Output.Res1);
                            binaryWriter2.Write(PLCCommData.Result.UnitTorque);
                            binaryWriter2.Write(PLCCommData.Result.ProgNum);
                            binaryWriter2.Write(PLCCommData.Result.IONIO);
                            binaryWriter2.Write(PLCCommData.Result.LastStep);
                            binaryWriter2.Write(PLCCommData.Result.Time.Year);
                            binaryWriter2.Write(PLCCommData.Result.Time.Month);
                            binaryWriter2.Write(PLCCommData.Result.Time.Day);
                            binaryWriter2.Write(PLCCommData.Result.Time.Hour);
                            binaryWriter2.Write(PLCCommData.Result.Time.Minute);
                            binaryWriter2.Write(PLCCommData.Result.Time.Second);
                            binaryWriter2.Write(PLCCommData.Result.Cycle);
                            binaryWriter2.Write(PLCCommData.Result.ScrewDuration);
                            binaryWriter2.Write(PLCCommData.Result.ResultStep1);
                            binaryWriter2.Write(PLCCommData.Result.ResultParam1);
                            binaryWriter2.Write(PLCCommData.Result.ResultStep2);
                            binaryWriter2.Write(PLCCommData.Result.ResultParam2);
                            binaryWriter2.Write(PLCCommData.Result.ResultStep3);
                            binaryWriter2.Write(PLCCommData.Result.ResultParam3);
                            binaryWriter2.Write(PLCCommData.Result.Valid);
                            binaryWriter2.Write(PLCCommData.Result.Res1);
                            binaryWriter2.Write(PLCCommData.Result.Res2);
                            binaryWriter2.Write(PLCCommData.Result.Res3);
                            array[2] = 0;
                            while (array[2] < 32)
                            {
                                binaryWriter2.Write(PLCCommData.Result.ScrewID[array[2]]);
                                array[2]++;
                            }

                            binaryWriter2.Write(PLCCommData.Error.Num);
                            binaryWriter2.Write(PLCCommData.Error.Warning);
                            binaryWriter2.Write(PLCCommData.DI_DO.DI0_8);
                            binaryWriter2.Write(PLCCommData.DI_DO.DO2_8);
                            binaryWriter2.Write(PLCCommData.ProgAccess.SpindleTorque);
                            binaryWriter2.Write(PLCCommData.ProgAccess.DriveUnitRpm);
                            binaryWriter2.Write(PLCCommData.ProgAccess.Signal);
                            binaryWriter2.Write(PLCCommData.ProgAccess.Length0);
                            binaryWriter2.Write(PLCCommData.ProgAccess.Length1);
                            binaryWriter2.Write(PLCCommData.ProgAccess.Length2);
                            binaryWriter2.Write(PLCCommData.ProgAccess.Address0);
                            binaryWriter2.Write(PLCCommData.ProgAccess.Address1);
                            binaryWriter2.Write(PLCCommData.ProgAccess.Address2);
                            array[2] = 0;
                            while (array[2] < 4)
                            {
                                binaryWriter2.Write(PLCCommData.ProgAccess.Data0[array[2]]);
                                array[2]++;
                            }

                            array[2] = 0;
                            while (array[2] < 4)
                            {
                                binaryWriter2.Write(PLCCommData.ProgAccess.Data1[array[2]]);
                                array[2]++;
                            }

                            array[2] = 0;
                            while (array[2] < 4)
                            {
                                binaryWriter2.Write(PLCCommData.ProgAccess.Data2[array[2]]);
                                array[2]++;
                            }

                            binaryWriter2.Write(PLCCommData.ExternalAnalogSignal.SetSignal);
                            binaryWriter2.Write(PLCCommData.ExternalAnalogSignal.Pressure1);
                            binaryWriter2.Write(PLCCommData.ExternalAnalogSignal.Pressure2);
                            goto IL_5e01;
                        case 44:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(UserRelatedData.UserLevel);
                            array[1] = 0;
                            while (array[1] < 5)
                            {
                                binaryWriter2.Write(UserRelatedData.UserName[array[1]]);
                                array[1]++;
                            }

                            binaryWriter2.Write(UserRelatedData.Reserve1);
                            binaryWriter2.Write(UserRelatedData.Reserve2);
                            binaryWriter2.Write(UserRelatedData.Reserve3);
                            binaryWriter2.Write(UserRelatedData.Reserve4);
                            binaryWriter2.Write(UserRelatedData.Reserve5);
                            binaryWriter2.Write(UserRelatedData.ReserveSignals);
                            array[1] = 0;
                            while (array[1] < 10)
                            {
                                binaryWriter2.Write(UserRelatedData.IpAddress[array[1]]);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 50:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(ManualStartControl.Command);
                            binaryWriter2.Write(ManualStartControl.ProgNum);
                            goto IL_5e01;
                        case 60:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(LogBookWriteControl.Command);
                            goto IL_5e01;
                        case 61:
                            binaryWriter2.Write(LogBookWriteData_elements);
                            array[1] = 0;
                            while (array[1] < LogBookWriteData_elements)
                            {
                                binaryWriter2.Write(LogBookWriteData.LogData[array[1]].Code);
                                binaryWriter2.Write(LogBookWriteData.LogData[array[1]].Type);
                                binaryWriter2.Write(LogBookWriteData.LogData[array[1]].ProgNum);
                                binaryWriter2.Write(LogBookWriteData.LogData[array[1]].Step);
                                binaryWriter2.Write(LogBookWriteData.LogData[array[1]].Value1);
                                binaryWriter2.Write(LogBookWriteData.LogData[array[1]].Value2);
                                array[2] = 0;
                                while (array[2] < 5)
                                {
                                    binaryWriter2.Write(LogBookWriteData.LogData[array[1]].userName[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(LogBookWriteData.LogData[array[1]].UnitIndex);
                                binaryWriter2.Write(LogBookWriteData.LogData[array[1]].res);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 63:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(StorageSystem.Signal);
                            goto IL_5e01;
                        case 70:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(MaintenanceDataBlock.BlockNum);
                            binaryWriter2.Write(MaintenanceDataBlock.LastBlock);
                            binaryWriter2.Write(MaintenanceDataBlock.NextBlock);
                            array[1] = 0;
                            while (array[1] < 32)
                            {
                                array[2] = 0;
                                while (array[2] < 5)
                                {
                                    binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]]
                                        .userName[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].Time.Year);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].Time.Month);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].Time.Day);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].Time.Hour);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].Time.Minute);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].Time.Second);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].ScheduledTime.Year);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].ScheduledTime.Month);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].ScheduledTime.Day);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].ScheduledTime.Hour);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].ScheduledTime.Minute);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].ScheduledTime.Second);
                                array[2] = 0;
                                while (array[2] < 100)
                                {
                                    binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]]
                                        .MaintenanceText[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].Reminder);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].ReserveByte1);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].ReserveByte2);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].Index);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].BlockNum);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].NewEntry);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].Cycle);
                                binaryWriter2.Write(MaintenanceDataBlock.MaintenanceData[array[1]].NextCycle);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 71:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(ComponentDataBlock.BlockNum);
                            binaryWriter2.Write(ComponentDataBlock.LastBlock);
                            binaryWriter2.Write(ComponentDataBlock.NextBlock);
                            array[1] = 0;
                            while (array[1] < 32)
                            {
                                array[2] = 0;
                                while (array[2] < 5)
                                {
                                    binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].userName[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].Time.Year);
                                binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].Time.Month);
                                binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].Time.Day);
                                binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].Time.Hour);
                                binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].Time.Minute);
                                binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].Time.Second);
                                array[2] = 0;
                                while (array[2] < 100)
                                {
                                    binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].ComponentOrPartText[array[2]]);
                                    array[2]++;
                                }

                                array[2] = 0;
                                while (array[2] < 100)
                                {
                                    binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].ReasonText[array[2]]);
                                    array[2]++;
                                }

                                array[2] = 0;
                                while (array[2] < 31)
                                {
                                    binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].SerialNumber[array[2]]);
                                    array[2]++;
                                }

                                binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].NewEntry);
                                binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].PieceCount);
                                binaryWriter2.Write(ComponentDataBlock.ComponentData[array[1]].Cycle);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 80:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(DownloadRequest.Request);
                            array[1] = 0;
                            while (array[1] < 32)
                            {
                                binaryWriter2.Write(DownloadRequest.Info[array[1]]);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 81:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(DownloadConfirmation.Result);
                            array[1] = 0;
                            while (array[1] < 32)
                            {
                                binaryWriter2.Write(DownloadConfirmation.Info[array[1]]);
                                array[1]++;
                            }

                            goto IL_5e01;
                        case 86:
                            binaryWriter2.Write(0u);
                            binaryWriter2.Write(PlcLogBookSys.ID1);
                            binaryWriter2.Write(PlcLogBookSys.Position);
                            binaryWriter2.Write(PlcLogBookSys.Length);
                            array[1] = 0;
                            while (array[1] < 4000)
                            {
                                binaryWriter2.Write(PlcLogBookSys.plcLogMessBuffer[array[1]].Time.Year);
                                binaryWriter2.Write(PlcLogBookSys.plcLogMessBuffer[array[1]].Time.Month);
                                binaryWriter2.Write(PlcLogBookSys.plcLogMessBuffer[array[1]].Time.Day);
                                binaryWriter2.Write(PlcLogBookSys.plcLogMessBuffer[array[1]].Time.Hour);
                                binaryWriter2.Write(PlcLogBookSys.plcLogMessBuffer[array[1]].Time.Minute);
                                binaryWriter2.Write(PlcLogBookSys.plcLogMessBuffer[array[1]].Time.Second);
                                binaryWriter2.Write(PlcLogBookSys.plcLogMessBuffer[array[1]].Code);
                                binaryWriter2.Write(PlcLogBookSys.plcLogMessBuffer[array[1]].Type);
                                binaryWriter2.Write(PlcLogBookSys.plcLogMessBuffer[array[1]].cycNum);
                                binaryWriter2.Write(PlcLogBookSys.plcLogMessBuffer[array[1]].UnitIndex);
                                binaryWriter2.Write(PlcLogBookSys.plcLogMessBuffer[array[1]].ResByte);
                                binaryWriter2.Write(PlcLogBookSys.plcLogMessBuffer[array[1]].ResUint1);
                                array[1]++;
                            }

                            binaryWriter2.Write(PlcLogBookSys.ID2);
                            goto IL_5e01;
                        default:
                            {
                                log.LogError("Implementation to serialize Block {0} is missing!", block.ToString());

                                result = false;
                                break;
                            }
                            IL_5e01:
                            binaryWriter2.Write((uint)(~(337776900 + block)));
                            num = SocketWrite(ClientSocket, (int)memoryStream.Position, 15000, SendBuffer);
                            if (num == 1)
                            {
                                binaryWriter2.Close();
                                memoryStream.Close();
                                result = true;
                            }

                            break;
                    }
                }

                Monitor.Exit(ClientSocket);
                return result;
            }

            log.LogError("Client has no connection to the server!");

            return false;
        }

        #endregion

        #region Thread methods
        private void EventThreadFunc()
        {
            while (true)
            {
                for (int i = 0; i < 255; i++)
                {
                    if (EventIntern[i])
                    {
                        EventIntern[i] = false;
                        switch (i)
                        {
                            case 3:
                                OnVARServerEvent_3(new VarServerEventArgs((ushort)i));
                                break;
                            case 4:
                                OnVARServerEvent_4(new VarServerEventArgs((ushort)i));
                                break;
                            case 2:
                                OnVARServerEvent_2(new VarServerEventArgs((ushort)i));
                                break;
                            case 5:
                                OnVARServerEvent_5(new VarServerEventArgs((ushort)i));
                                break;
                            case 0:
                                OnVARServerEvent_0(new VarServerEventArgs((ushort)i));
                                break;
                            case 1:
                                OnVARServerEvent_1(new VarServerEventArgs((ushort)i));
                                break;
                        }
                    }
                }

                Thread.Sleep(20);
            }
        }

        private void VCThreadFunc()
        {
            while (true)
            {
                if (ProcessCommand(true) != 0)
                {
                    CloseConnection();
                }

                if (ReceiveBlockNum != -1)
                {
                    ReceiveBlockResult = ReceiveVarBlockIntern((ushort)ReceiveBlockNum);
                    ReceiveBlockNum = -1;
                }

                Thread.Sleep(20);
            }
        } 
        #endregion

        #region Socket methods
        private int SocketWrite(Socket sock, int len, int timeout, byte[] data)
        {
            int num = 0;
            while (true)
            {
                try
                {
                    do
                    {
                        int num2 = sock.Send(data, num, len, SocketFlags.None);
                        num += num2;
                        len -= num2;
                    } while (len != 0);

                    return 1;
                }
                catch (Exception ex)
                {
                    SocketException ex2 = (SocketException)ex;
                    if (ex2.ErrorCode == 10035)
                    {
                        Thread.Sleep(10);
                        timeout -= 10;
                        if (timeout >= 0)
                        {
                            goto end_IL_0021;
                        }

                        return -1;
                    }

                    log.LogError(ex, ex.Message);

                    return -2;
                    end_IL_0021:;
                }
            }
        }

        private int SocketRead(Socket sock, int len, int timeout, byte[] data)
        {
            int num = 0;
            while (true)
            {
                try
                {
                    do
                    {
                        int num2 = sock.Receive(data, num, len, SocketFlags.None);
                        if (num2 == 0)
                        {
                            return 0;
                        }

                        num += num2;
                        len -= num2;
                    } while (len != 0);

                    return 1;
                }
                catch (SocketException ex)
                {
                    if (ex.ErrorCode == 10035)
                    {
                        Thread.Sleep(10);
                        timeout -= 10;
                        if (timeout >= 0)
                        {
                            goto end_IL_0027;
                        }

                        return -1;
                    }

                    log.LogError(ex, ex.Message);

                    return -2;
                    end_IL_0027:;
                }
            }
        }

        private int SocketReadCommand(Socket sock, int len, byte[] data)
        {
            int num = 0;
            try
            {
                do
                {
                    int num2 = sock.Receive(data, num, len, SocketFlags.None);
                    if (num2 == 0)
                    {
                        return 0;
                    }

                    num += num2;
                    len -= num2;
                } while (len != 0);

                return 1;
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10035)
                {
                    return -1;
                }

                return -2;
            }
        } 
        #endregion

        /*
        public bool ReceiveVarBlock(ushort block, int waitMaxMilliSeconds)
		{
			int num = waitMaxMilliSeconds / 50;
			while (ReceiveBlockNum != -1)
			{
				Thread.Sleep(50);
			}

			ReceiveBlockNum = block;
			int num2 = 0;
			while (ReceiveBlockNum != -1)
			{
				if (num2++ <= num)
				{
					Thread.Sleep(50);
					continue;
				}

				ReceiveBlockNum = -1;
				ReceiveBlockResult = false;
				break;
			}

			return ReceiveBlockResult;
		}

		public void makeCopyDateTimeStruct(ref DateTimeStruct dest, DateTimeStruct source)
		{
			dest.Year = source.Year;
			dest.Month = source.Month;
			dest.Day = source.Day;
			dest.Hour = source.Hour;
			dest.Minute = source.Minute;
			dest.Second = source.Second;
		}

		public void makeCopyAdvancedWarningStruct(ref AdvancedWarningStruct dest, AdvancedWarningStruct source)
		{
			int[] array = new int[50]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			while (array[1] < 32)
			{
				dest.Name[array[1]] = source.Name[array[1]];
				array[1]++;
			}

			dest.Limit = source.Limit;
			dest.Advance = source.Advance;
			dest.AdvancedWarningTime.Year = source.AdvancedWarningTime.Year;
			dest.AdvancedWarningTime.Month = source.AdvancedWarningTime.Month;
			dest.AdvancedWarningTime.Day = source.AdvancedWarningTime.Day;
			dest.AdvancedWarningTime.Hour = source.AdvancedWarningTime.Hour;
			dest.AdvancedWarningTime.Minute = source.AdvancedWarningTime.Minute;
			dest.AdvancedWarningTime.Second = source.AdvancedWarningTime.Second;
			dest.AdvancedDays = source.AdvancedDays;
			dest.EnableAdvancedWarningTime = source.EnableAdvancedWarningTime;
		}

		public void makeCopyPlcLogMessageStruct(ref PlcLogMessageStruct dest, PlcLogMessageStruct source)
		{
			dest.Time.Year = source.Time.Year;
			dest.Time.Month = source.Time.Month;
			dest.Time.Day = source.Time.Day;
			dest.Time.Hour = source.Time.Hour;
			dest.Time.Minute = source.Time.Minute;
			dest.Time.Second = source.Time.Second;
			dest.Code = source.Code;
			dest.Type = source.Type;
			dest.cycNum = source.cycNum;
			dest.UnitIndex = source.UnitIndex;
			dest.ResByte = source.ResByte;
			dest.ResUint1 = source.ResUint1;
		}

		public void makeCopyLogMessageStruct(ref LogMessageStruct dest, LogMessageStruct source)
		{
			int[] array = new int[50];
			dest.Time.Year = source.Time.Year;
			dest.Time.Month = source.Time.Month;
			dest.Time.Day = source.Time.Day;
			dest.Time.Hour = source.Time.Hour;
			dest.Time.Minute = source.Time.Minute;
			dest.Time.Second = source.Time.Second;
			dest.Code = source.Code;
			dest.Type = source.Type;
			dest.ProgNum = source.ProgNum;
			dest.Step = source.Step;
			dest.Value1 = source.Value1;
			dest.Value2 = source.Value2;
			array[1] = 0;
			while (array[1] < 5)
			{
				dest.userName[array[1]] = source.userName[array[1]];
				array[1]++;
			}

			dest.cycNum = source.cycNum;
			dest.UnitIndex = source.UnitIndex;
			dest.res = source.res;
		}

		public void makeCopyLogMessageWriteBufferStruct(ref LogMessageWriteBufferStruct dest,
			LogMessageWriteBufferStruct source)
		{
			int[] array = new int[50];
			dest.Code = source.Code;
			dest.Type = source.Type;
			dest.ProgNum = source.ProgNum;
			dest.Step = source.Step;
			dest.Value1 = source.Value1;
			dest.Value2 = source.Value2;
			array[1] = 0;
			while (array[1] < 5)
			{
				dest.userName[array[1]] = source.userName[array[1]];
				array[1]++;
			}

			dest.UnitIndex = source.UnitIndex;
			dest.res = source.res;
		}

		public void makeCopyMaintenanceBufferStruct(ref MaintenanceBufferStruct dest, MaintenanceBufferStruct source)
		{
			int[] array = new int[50]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			while (array[1] < 5)
			{
				dest.userName[array[1]] = source.userName[array[1]];
				array[1]++;
			}

			dest.Time.Year = source.Time.Year;
			dest.Time.Month = source.Time.Month;
			dest.Time.Day = source.Time.Day;
			dest.Time.Hour = source.Time.Hour;
			dest.Time.Minute = source.Time.Minute;
			dest.Time.Second = source.Time.Second;
			dest.ScheduledTime.Year = source.ScheduledTime.Year;
			dest.ScheduledTime.Month = source.ScheduledTime.Month;
			dest.ScheduledTime.Day = source.ScheduledTime.Day;
			dest.ScheduledTime.Hour = source.ScheduledTime.Hour;
			dest.ScheduledTime.Minute = source.ScheduledTime.Minute;
			dest.ScheduledTime.Second = source.ScheduledTime.Second;
			array[1] = 0;
			while (array[1] < 100)
			{
				dest.MaintenanceText[array[1]] = source.MaintenanceText[array[1]];
				array[1]++;
			}

			dest.Reminder = source.Reminder;
			dest.ReserveByte1 = source.ReserveByte1;
			dest.ReserveByte2 = source.ReserveByte2;
			dest.Index = source.Index;
			dest.BlockNum = source.BlockNum;
			dest.NewEntry = source.NewEntry;
			dest.Cycle = source.Cycle;
			dest.NextCycle = source.NextCycle;
		}

		public void makeCopyComponentBufferStruct(ref ComponentBufferStruct dest, ComponentBufferStruct source)
		{
			int[] array = new int[50]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			while (array[1] < 5)
			{
				dest.userName[array[1]] = source.userName[array[1]];
				array[1]++;
			}

			dest.Time.Year = source.Time.Year;
			dest.Time.Month = source.Time.Month;
			dest.Time.Day = source.Time.Day;
			dest.Time.Hour = source.Time.Hour;
			dest.Time.Minute = source.Time.Minute;
			dest.Time.Second = source.Time.Second;
			array[1] = 0;
			while (array[1] < 100)
			{
				dest.ComponentOrPartText[array[1]] = source.ComponentOrPartText[array[1]];
				array[1]++;
			}

			array[1] = 0;
			while (array[1] < 100)
			{
				dest.ReasonText[array[1]] = source.ReasonText[array[1]];
				array[1]++;
			}

			array[1] = 0;
			while (array[1] < 31)
			{
				dest.SerialNumber[array[1]] = source.SerialNumber[array[1]];
				array[1]++;
			}

			dest.NewEntry = source.NewEntry;
			dest.PieceCount = source.PieceCount;
			dest.Cycle = source.Cycle;
		}

		public void makeCopyEnableParamStruct(ref EnableParamStruct dest, EnableParamStruct source)
		{
			dest.Torque = source.Torque;
			dest.Snug = source.Snug;
			dest.FTorque = source.FTorque;
			dest.GradientMin = source.GradientMin;
			dest.GradientMax = source.GradientMax;
			dest.Angle = source.Angle;
			dest.Time = source.Time;
			dest.ADepth = source.ADepth;
			dest.ADepthGradMin = source.ADepthGradMin;
			dest.ADepthGradMax = source.ADepthGradMax;
			dest.Ana = source.Ana;
			dest.Release = source.Release;
		}

		public void makeCopyFourStepAtomStruct(ref FourStepAtomStruct dest, FourStepAtomStruct source)
		{
			dest.StepIndex = source.StepIndex;
			dest.TypeOfData = source.TypeOfData;
			dest.Value = source.Value;
			dest.MinValue = source.MinValue;
			dest.MaxValue = source.MaxValue;
		}

		public void makeCopyFourthStepStruct(ref FourthStepStruct dest, FourthStepStruct source)
		{
			int[] array = new int[50]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			while (array[1] < 7)
			{
				dest.FourStepAtoms[array[1]].StepIndex = source.FourStepAtoms[array[1]].StepIndex;
				dest.FourStepAtoms[array[1]].TypeOfData = source.FourStepAtoms[array[1]].TypeOfData;
				dest.FourStepAtoms[array[1]].Value = source.FourStepAtoms[array[1]].Value;
				dest.FourStepAtoms[array[1]].MinValue = source.FourStepAtoms[array[1]].MinValue;
				dest.FourStepAtoms[array[1]].MaxValue = source.FourStepAtoms[array[1]].MaxValue;
				array[1]++;
			}
		}

		public void makeCopyFourProgStruct(ref FourProgStruct dest, FourProgStruct source)
		{
			int[] array = new int[50]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			while (array[1] < 4)
			{
				array[2] = 0;
				while (array[2] < 7)
				{
					dest.FourthStep[array[1]].FourStepAtoms[array[2]].StepIndex = source.FourthStep[array[1]].FourStepAtoms[array[2]].StepIndex;
					dest.FourthStep[array[1]].FourStepAtoms[array[2]].TypeOfData = source.FourthStep[array[1]].FourStepAtoms[array[2]].TypeOfData;
					dest.FourthStep[array[1]].FourStepAtoms[array[2]].Value = source.FourthStep[array[1]].FourStepAtoms[array[2]].Value;
					dest.FourthStep[array[1]].FourStepAtoms[array[2]].MinValue = source.FourthStep[array[1]].FourStepAtoms[array[2]].MinValue;
					dest.FourthStep[array[1]].FourStepAtoms[array[2]].MaxValue = source.FourthStep[array[1]].FourStepAtoms[array[2]].MaxValue;
					array[2]++;
				}

				array[1]++;
			}
		}

		public void makeCopyStepStruct(ref StepStruct dest, StepStruct source)
		{
			dest.Type = source.Type;
			dest.Switch = source.Switch;
			dest.IsResult1 = source.IsResult1;
			dest.IsResult2 = source.IsResult2;
			dest.IsResult3 = source.IsResult3;
			dest.Enable.Torque = source.Enable.Torque;
			dest.Enable.Snug = source.Enable.Snug;
			dest.Enable.FTorque = source.Enable.FTorque;
			dest.Enable.GradientMin = source.Enable.GradientMin;
			dest.Enable.GradientMax = source.Enable.GradientMax;
			dest.Enable.Angle = source.Enable.Angle;
			dest.Enable.Time = source.Enable.Time;
			dest.Enable.ADepth = source.Enable.ADepth;
			dest.Enable.ADepthGradMin = source.Enable.ADepthGradMin;
			dest.Enable.ADepthGradMax = source.Enable.ADepthGradMax;
			dest.Enable.Ana = source.Enable.Ana;
			dest.Enable.Release = source.Enable.Release;
			dest.NA = source.NA;
			dest.TM = source.TM;
			dest.MP = source.MP;
			dest.Mmin = source.Mmin;
			dest.Mmax = source.Mmax;
			dest.MS = source.MS;
			dest.MRP = source.MRP;
			dest.MRStep = source.MRStep;
			dest.MRType = source.MRType;
			dest.MDelayTime = source.MDelayTime;
			dest.MFP = source.MFP;
			dest.MFmin = source.MFmin;
			dest.MFmax = source.MFmax;
			dest.MGP = source.MGP;
			dest.MGmin = source.MGmin;
			dest.MGmax = source.MGmax;
			dest.WP = source.WP;
			dest.Wmin = source.Wmin;
			dest.Wmax = source.Wmax;
			dest.WN = source.WN;
			dest.TP = source.TP;
			dest.Tmin = source.Tmin;
			dest.Tmax = source.Tmax;
			dest.TN = source.TN;
			dest.LP = source.LP;
			dest.Lmin = source.Lmin;
			dest.Lmax = source.Lmax;
			dest.LGP = source.LGP;
			dest.LGmin = source.LGmin;
			dest.LGmax = source.LGmax;
			dest.AnaP = source.AnaP;
			dest.AnaMin = source.AnaMin;
			dest.AnaMax = source.AnaMax;
			dest.DigP = source.DigP;
			dest.DigMin = source.DigMin;
			dest.DigMax = source.DigMax;
			dest.JumpTo = source.JumpTo;
			dest.ModDigOut = source.ModDigOut;
			dest.PressureSpindle = source.PressureSpindle;
			dest.CountPassMax = source.CountPassMax;
			dest.UserRights = source.UserRights;
		}

		public void makeCopyProgInfoStruct(ref ProgInfoStruct dest, ProgInfoStruct source)
		{
			int[] array = new int[50];
			dest.ProgNum = source.ProgNum;
			array[1] = 0;
			while (array[1] < 32)
			{
				dest.Name[array[1]] = source.Name[array[1]];
				array[1]++;
			}

			dest.Steps = source.Steps;
			dest.ResultParam1 = source.ResultParam1;
			dest.ResultParam2 = source.ResultParam2;
			dest.ResultParam3 = source.ResultParam3;
		}

		public void makeCopyProgStruct(ref ProgStruct dest, ProgStruct source)
		{
			int[] array = new int[50];
			dest.byteReserve_1 = source.byteReserve_1;
			dest.Info.ProgNum = source.Info.ProgNum;
			array[2] = 0;
			while (array[2] < 32)
			{
				dest.Info.Name[array[2]] = source.Info.Name[array[2]];
				array[2]++;
			}

			dest.Info.Steps = source.Info.Steps;
			dest.Info.ResultParam1 = source.Info.ResultParam1;
			dest.Info.ResultParam2 = source.Info.ResultParam2;
			dest.Info.ResultParam3 = source.Info.ResultParam3;
			dest.M1FilterTime = source.M1FilterTime;
			dest.GradientLength = source.GradientLength;
			dest.GradientFilter = source.GradientFilter;
			dest.ADepthFilterTime = source.ADepthFilterTime;
			dest.ADepthGradientLength = source.ADepthGradientLength;
			dest.MaxTime = source.MaxTime;
			dest.PressureHolder = source.PressureHolder;
			dest.EndSetDigOut1 = source.EndSetDigOut1;
			dest.EndValueDigOut1 = source.EndValueDigOut1;
			dest.EndSetDigOut2 = source.EndSetDigOut2;
			dest.EndValueDigOut2 = source.EndValueDigOut2;
			dest.EndSetSync1 = source.EndSetSync1;
			dest.EndValueSync1 = source.EndValueSync1;
			dest.EndSetSync2 = source.EndSetSync2;
			dest.EndValueSync2 = source.EndValueSync2;
			array[1] = 0;
			while (array[1] < 25)
			{
				dest.Step[array[1]].Type = source.Step[array[1]].Type;
				dest.Step[array[1]].Switch = source.Step[array[1]].Switch;
				dest.Step[array[1]].IsResult1 = source.Step[array[1]].IsResult1;
				dest.Step[array[1]].IsResult2 = source.Step[array[1]].IsResult2;
				dest.Step[array[1]].IsResult3 = source.Step[array[1]].IsResult3;
				dest.Step[array[1]].Enable.Torque = source.Step[array[1]].Enable.Torque;
				dest.Step[array[1]].Enable.Snug = source.Step[array[1]].Enable.Snug;
				dest.Step[array[1]].Enable.FTorque = source.Step[array[1]].Enable.FTorque;
				dest.Step[array[1]].Enable.GradientMin = source.Step[array[1]].Enable.GradientMin;
				dest.Step[array[1]].Enable.GradientMax = source.Step[array[1]].Enable.GradientMax;
				dest.Step[array[1]].Enable.Angle = source.Step[array[1]].Enable.Angle;
				dest.Step[array[1]].Enable.Time = source.Step[array[1]].Enable.Time;
				dest.Step[array[1]].Enable.ADepth = source.Step[array[1]].Enable.ADepth;
				dest.Step[array[1]].Enable.ADepthGradMin = source.Step[array[1]].Enable.ADepthGradMin;
				dest.Step[array[1]].Enable.ADepthGradMax = source.Step[array[1]].Enable.ADepthGradMax;
				dest.Step[array[1]].Enable.Ana = source.Step[array[1]].Enable.Ana;
				dest.Step[array[1]].Enable.Release = source.Step[array[1]].Enable.Release;
				dest.Step[array[1]].NA = source.Step[array[1]].NA;
				dest.Step[array[1]].TM = source.Step[array[1]].TM;
				dest.Step[array[1]].MP = source.Step[array[1]].MP;
				dest.Step[array[1]].Mmin = source.Step[array[1]].Mmin;
				dest.Step[array[1]].Mmax = source.Step[array[1]].Mmax;
				dest.Step[array[1]].MS = source.Step[array[1]].MS;
				dest.Step[array[1]].MRP = source.Step[array[1]].MRP;
				dest.Step[array[1]].MRStep = source.Step[array[1]].MRStep;
				dest.Step[array[1]].MRType = source.Step[array[1]].MRType;
				dest.Step[array[1]].MDelayTime = source.Step[array[1]].MDelayTime;
				dest.Step[array[1]].MFP = source.Step[array[1]].MFP;
				dest.Step[array[1]].MFmin = source.Step[array[1]].MFmin;
				dest.Step[array[1]].MFmax = source.Step[array[1]].MFmax;
				dest.Step[array[1]].MGP = source.Step[array[1]].MGP;
				dest.Step[array[1]].MGmin = source.Step[array[1]].MGmin;
				dest.Step[array[1]].MGmax = source.Step[array[1]].MGmax;
				dest.Step[array[1]].WP = source.Step[array[1]].WP;
				dest.Step[array[1]].Wmin = source.Step[array[1]].Wmin;
				dest.Step[array[1]].Wmax = source.Step[array[1]].Wmax;
				dest.Step[array[1]].WN = source.Step[array[1]].WN;
				dest.Step[array[1]].TP = source.Step[array[1]].TP;
				dest.Step[array[1]].Tmin = source.Step[array[1]].Tmin;
				dest.Step[array[1]].Tmax = source.Step[array[1]].Tmax;
				dest.Step[array[1]].TN = source.Step[array[1]].TN;
				dest.Step[array[1]].LP = source.Step[array[1]].LP;
				dest.Step[array[1]].Lmin = source.Step[array[1]].Lmin;
				dest.Step[array[1]].Lmax = source.Step[array[1]].Lmax;
				dest.Step[array[1]].LGP = source.Step[array[1]].LGP;
				dest.Step[array[1]].LGmin = source.Step[array[1]].LGmin;
				dest.Step[array[1]].LGmax = source.Step[array[1]].LGmax;
				dest.Step[array[1]].AnaP = source.Step[array[1]].AnaP;
				dest.Step[array[1]].AnaMin = source.Step[array[1]].AnaMin;
				dest.Step[array[1]].AnaMax = source.Step[array[1]].AnaMax;
				dest.Step[array[1]].DigP = source.Step[array[1]].DigP;
				dest.Step[array[1]].DigMin = source.Step[array[1]].DigMin;
				dest.Step[array[1]].DigMax = source.Step[array[1]].DigMax;
				dest.Step[array[1]].JumpTo = source.Step[array[1]].JumpTo;
				dest.Step[array[1]].ModDigOut = source.Step[array[1]].ModDigOut;
				dest.Step[array[1]].PressureSpindle = source.Step[array[1]].PressureSpindle;
				dest.Step[array[1]].CountPassMax = source.Step[array[1]].CountPassMax;
				dest.Step[array[1]].UserRights = source.Step[array[1]].UserRights;
				array[1]++;
			}

			dest.UseLocalJawSettings = source.UseLocalJawSettings;
			dest.JawLocalWrittenOnce = source.JawLocalWrittenOnce;
			dest.JawOpenDistance = source.JawOpenDistance;
			dest.JawOpenDepthGradMax = source.JawOpenDepthGradMax;
			dest.JawOpenDepthGradMin = source.JawOpenDepthGradMin;
			array[2] = 0;
			while (array[2] < 4)
			{
				array[3] = 0;
				while (array[3] < 7)
				{
					dest.FourProg.FourthStep[array[2]].FourStepAtoms[array[3]].StepIndex = source.FourProg.FourthStep[array[2]].FourStepAtoms[array[3]].StepIndex;
					dest.FourProg.FourthStep[array[2]].FourStepAtoms[array[3]].TypeOfData = source.FourProg.FourthStep[array[2]].FourStepAtoms[array[3]].TypeOfData;
					dest.FourProg.FourthStep[array[2]].FourStepAtoms[array[3]].Value = source.FourProg.FourthStep[array[2]].FourStepAtoms[array[3]].Value;
					dest.FourProg.FourthStep[array[2]].FourStepAtoms[array[3]].MinValue = source.FourProg.FourthStep[array[2]].FourStepAtoms[array[3]].MinValue;
					dest.FourProg.FourthStep[array[2]].FourStepAtoms[array[3]].MaxValue = source.FourProg.FourthStep[array[2]].FourStepAtoms[array[3]].MaxValue;
					array[3]++;
				}

				array[2]++;
			}
		}

		public void makeCopyStepResultStruct(ref StepResultStruct dest, StepResultStruct source)
		{
			dest.Step = source.Step;
			dest.IONIO = source.IONIO;
			dest.OrgStep = source.OrgStep;
			dest.Torque = source.Torque;
			dest.MaxTorque = source.MaxTorque;
			dest.FTorque = source.FTorque;
			dest.DelayTorque = source.DelayTorque;
			dest.M360Follow = source.M360Follow;
			dest.Gradient = source.Gradient;
			dest.Angle = source.Angle;
			dest.Time = source.Time;
			dest.ADepth = source.ADepth;
			dest.ADepthGrad = source.ADepthGrad;
			dest.Ana = source.Ana;
			dest.Dig = source.Dig;
		}

		public void makeCopySerialPortSettingStruct(ref SerialPortSettingStruct dest, SerialPortSettingStruct source)
		{
			dest.BaudRate = source.BaudRate;
			dest.Parity = source.Parity;
		}

		public void makeCopyIPAddressStruct(ref IPAddressStruct dest, IPAddressStruct source)
		{
			dest.Byte1 = source.Byte1;
			dest.Byte2 = source.Byte2;
			dest.Byte3 = source.Byte3;
			dest.Byte4 = source.Byte4;
		}

		public void makeCopySubNetMaskStruct(ref SubNetMaskStruct dest, SubNetMaskStruct source)
		{
			dest.Byte1 = source.Byte1;
			dest.Byte2 = source.Byte2;
			dest.Byte3 = source.Byte3;
			dest.Byte4 = source.Byte4;
		}

		public void makeCopyPassCodeStruct(ref PassCodeStruct dest, PassCodeStruct source)
		{
			int[] array = new int[50]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			while (array[1] < 5)
			{
				dest.Name[array[1]] = source.Name[array[1]];
				array[1]++;
			}

			dest.Code = source.Code;
			dest.Level = source.Level;
		}

		public void makeCopyUserLevelStruct(ref UserLevelStruct dest, UserLevelStruct source)
		{
			dest.UserLevel_BackupForm = source.UserLevel_BackupForm;
			dest.UserLevel_CheckParamForm = source.UserLevel_CheckParamForm;
			dest.UserLevel_CycleCounterForm = source.UserLevel_CycleCounterForm;
			dest.UserLevel_EditSteps = source.UserLevel_EditSteps;
			dest.UserLevel_PrgOptParameterForm = source.UserLevel_PrgOptParameterForm;
			dest.UserLevel_ProgramOverviewForm = source.UserLevel_ProgramOverviewForm;
			dest.UserLevel_SpindleConstantsForm = source.UserLevel_SpindleConstantsForm;
			dest.UserLevel_StatisticsLastResForm = source.UserLevel_StatisticsLastResForm;
			dest.UserLevel_StepOverviewForm = source.UserLevel_StepOverviewForm;
			dest.UserLevel_SystemConstantsForm = source.UserLevel_SystemConstantsForm;
			dest.UserLevel_TestIOForm = source.UserLevel_TestIOForm;
			dest.UserLevel_TestMotorSensorForm = source.UserLevel_TestMotorSensorForm;
			dest.UserLevel_VisualisationParamForm = source.UserLevel_VisualisationParamForm;
			dest.UserLevel_FourStepEditForm = source.UserLevel_FourStepEditForm;
			dest.UserLevel_Maintenance = source.UserLevel_Maintenance;
			dest.UserLevel_HandStartForm = source.UserLevel_HandStartForm;
			dest.UserLevel_BrowserForm = source.UserLevel_BrowserForm;
			dest.UserLevel_Reserve1 = source.UserLevel_Reserve1;
			dest.UserLevel_Reserve2 = source.UserLevel_Reserve2;
			dest.UserLevel_Reserve3 = source.UserLevel_Reserve3;
			dest.UserLevel_Reserve4 = source.UserLevel_Reserve4;
			dest.UserLevel_Reserve5 = source.UserLevel_Reserve5;
		}

		public void makeCopyCurveDataStruct(ref CurveDataStruct dest, CurveDataStruct source)
		{
			dest.Nset = source.Nset;
			dest.Nact = source.Nact;
			dest.Torque = source.Torque;
			dest.FiltTorque = source.FiltTorque;
			dest.Angle = source.Angle;
			dest.DDepth = source.DDepth;
			dest.ADepth = source.ADepth;
			dest.ADepthGrad = source.ADepthGrad;
			dest.AExt = source.AExt;
			dest.Gradient = source.Gradient;
			dest.CurrentStep = source.CurrentStep;
		}

		public void makeCopyStatSampleInfoStruct(ref StatSampleInfoStruct dest, StatSampleInfoStruct source)
		{
			dest.Length = source.Length;
			dest.Position = source.Position;
		}

		public void makeCopyStatResultStruct(ref StatResultStruct dest, StatResultStruct source)
		{
			int[] array = new int[50];
			dest.ProgNum = source.ProgNum;
			array[1] = 0;
			while (array[1] < 32)
			{
				dest.ProgName[array[1]] = source.ProgName[array[1]];
				array[1]++;
			}

			dest.IONIO = source.IONIO;
			dest.LastStep = source.LastStep;
			dest.Time.Year = source.Time.Year;
			dest.Time.Month = source.Time.Month;
			dest.Time.Day = source.Time.Day;
			dest.Time.Hour = source.Time.Hour;
			dest.Time.Minute = source.Time.Minute;
			dest.Time.Second = source.Time.Second;
			dest.Cycle = source.Cycle;
			dest.ScrewDuration = source.ScrewDuration;
			dest.Valid = source.Valid;
			dest.ResultStep1 = source.ResultStep1;
			dest.ResultParam1 = source.ResultParam1;
			dest.Res1 = source.Res1;
			dest.ResultStep2 = source.ResultStep2;
			dest.ResultParam2 = source.ResultParam2;
			dest.Res2 = source.Res2;
			dest.ResultStep3 = source.ResultStep3;
			dest.ResultParam3 = source.ResultParam3;
			dest.Res3 = source.Res3;
			array[1] = 0;
			while (array[1] < 32)
			{
				dest.ScrewID[array[1]] = source.ScrewID[array[1]];
				array[1]++;
			}

			dest.res = source.res;
		}

		public void makeCopyPLCInputStruct(ref PLCInputStruct dest, PLCInputStruct source)
		{
			int[] array = new int[50];
			dest.Automatic = source.Automatic;
			dest.Start = source.Start;
			dest.Quit = source.Quit;
			dest.Sync1 = source.Sync1;
			dest.Sync2 = source.Sync2;
			dest.KalDisable = source.KalDisable;
			dest.TeachAnalogDepth = source.TeachAnalogDepth;
			dest.ProgNum = source.ProgNum;
			array[1] = 0;
			while (array[1] < 32)
			{
				dest.ScrewID[array[1]] = source.ScrewID[array[1]];
				array[1]++;
			}

			dest.LivingSignRequest = source.LivingSignRequest;
			dest.Reserve1 = source.Reserve1;
			dest.Reserve2 = source.Reserve2;
			dest.Reserve3 = source.Reserve3;
			dest.AnalogsignalCurve = source.AnalogsignalCurve;
			dest.LivingSignEnabled = source.LivingSignEnabled;
			dest.ReserveSignals = source.ReserveSignals;
			array[1] = 0;
			while (array[1] < 10)
			{
				dest.ReserveStr[array[1]] = source.ReserveStr[array[1]];
				array[1]++;
			}

			dest.UsingProgName = source.UsingProgName;
			dest.Reserve5 = source.Reserve5;
			array[1] = 0;
			while (array[1] < 32)
			{
				dest.ProgName[array[1]] = source.ProgName[array[1]];
				array[1]++;
			}

			array[1] = 0;
			while (array[1] < 20)
			{
				dest.ExtendedResult[array[1]] = source.ExtendedResult[array[1]];
				array[1]++;
			}

			dest.PlcError = source.PlcError;
			dest.CounterNIO = source.CounterNIO;
			dest.CounterIOTotal = source.CounterIOTotal;
			dest.CounterNIOTotal = source.CounterNIOTotal;
			dest.PressureSpindle = source.PressureSpindle;
			dest.PressureHolder = source.PressureHolder;
			dest.Res1 = source.Res1;
		}

		public void makeCopyPLCOutputStruct(ref PLCOutputStruct dest, PLCOutputStruct source)
		{
			int[] array = new int[50];
			dest.SystemOK = source.SystemOK;
			dest.ReadyToStart = source.ReadyToStart;
			dest.ProcessRunning = source.ProcessRunning;
			dest.IO = source.IO;
			dest.NIO = source.NIO;
			dest.Sync1 = source.Sync1;
			dest.Sync2 = source.Sync2;
			dest.PowerEnabled = source.PowerEnabled;
			dest.TM1 = source.TM1;
			dest.TM2 = source.TM2;
			dest.ExtDigIn = source.ExtDigIn;
			dest.StorageSignals = source.StorageSignals;
			dest.AnaDepthMM = source.AnaDepthMM;
			dest.AnaDepthVolt = source.AnaDepthVolt;
			dest.ExtAna = source.ExtAna;
			dest.LivingSignResponse = source.LivingSignResponse;
			dest.LivingMonitor = source.LivingMonitor;
			dest.UserLevel = source.UserLevel;
			array[1] = 0;
			while (array[1] < 5)
			{
				dest.UserName[array[1]] = source.UserName[array[1]];
				array[1]++;
			}

			dest.Reserve1 = source.Reserve1;
			dest.Reserve2 = source.Reserve2;
			dest.Reserve3 = source.Reserve3;
			dest.DriveUnitInvers = source.DriveUnitInvers;
			dest.LivingSignEnabled = source.LivingSignEnabled;
			dest.ReserveSignals = source.ReserveSignals;
			array[1] = 0;
			while (array[1] < 10)
			{
				dest.IpAddress[array[1]] = source.IpAddress[array[1]];
				array[1]++;
			}

			dest.MaintenanceCounterReached = source.MaintenanceCounterReached;
			dest.AdvancedCounterReached = source.AdvancedCounterReached;
			dest.PressureSpindle = source.PressureSpindle;
			dest.PressureHolder = source.PressureHolder;
			dest.PressureScaleSpindle = source.PressureScaleSpindle;
			dest.PressureScaleHolder = source.PressureScaleHolder;
			dest.Res1 = source.Res1;
		}

		public void makeCopyPLCResultStruct(ref PLCResultStruct dest, PLCResultStruct source)
		{
			int[] array = new int[50];
			dest.UnitTorque = source.UnitTorque;
			dest.ProgNum = source.ProgNum;
			dest.IONIO = source.IONIO;
			dest.LastStep = source.LastStep;
			dest.Time.Year = source.Time.Year;
			dest.Time.Month = source.Time.Month;
			dest.Time.Day = source.Time.Day;
			dest.Time.Hour = source.Time.Hour;
			dest.Time.Minute = source.Time.Minute;
			dest.Time.Second = source.Time.Second;
			dest.Cycle = source.Cycle;
			dest.ScrewDuration = source.ScrewDuration;
			dest.ResultStep1 = source.ResultStep1;
			dest.ResultParam1 = source.ResultParam1;
			dest.ResultStep2 = source.ResultStep2;
			dest.ResultParam2 = source.ResultParam2;
			dest.ResultStep3 = source.ResultStep3;
			dest.ResultParam3 = source.ResultParam3;
			dest.Valid = source.Valid;
			dest.Res1 = source.Res1;
			dest.Res2 = source.Res2;
			dest.Res3 = source.Res3;
			array[1] = 0;
			while (array[1] < 32)
			{
				dest.ScrewID[array[1]] = source.ScrewID[array[1]];
				array[1]++;
			}
		}

		public void makeCopyPLCErrorStruct(ref PLCErrorStruct dest, PLCErrorStruct source)
		{
			dest.Num = source.Num;
			dest.Warning = source.Warning;
		}

		public void makeCopyPLCDIDOStruct(ref PLCDIDOStruct dest, PLCDIDOStruct source)
		{
			dest.DI0_8 = source.DI0_8;
			dest.DO2_8 = source.DO2_8;
		}

		public void makeCopyPLCProgAccessStruct(ref PLCProgAccessStruct dest, PLCProgAccessStruct source)
		{
			int[] array = new int[50];
			dest.SpindleTorque = source.SpindleTorque;
			dest.DriveUnitRpm = source.DriveUnitRpm;
			dest.Signal = source.Signal;
			dest.Length0 = source.Length0;
			dest.Length1 = source.Length1;
			dest.Length2 = source.Length2;
			dest.Address0 = source.Address0;
			dest.Address1 = source.Address1;
			dest.Address2 = source.Address2;
			array[1] = 0;
			while (array[1] < 4)
			{
				dest.Data0[array[1]] = source.Data0[array[1]];
				array[1]++;
			}

			array[1] = 0;
			while (array[1] < 4)
			{
				dest.Data1[array[1]] = source.Data1[array[1]];
				array[1]++;
			}

			array[1] = 0;
			while (array[1] < 4)
			{
				dest.Data2[array[1]] = source.Data2[array[1]];
				array[1]++;
			}
		}

		public void makeCopyPLCExternalAnalogSignalStruct(ref PLCExternalAnalogSignalStruct dest,
			PLCExternalAnalogSignalStruct source)
		{
			dest.SetSignal = source.SetSignal;
			dest.Pressure1 = source.Pressure1;
			dest.Pressure2 = source.Pressure2;
		}

		public void makeCopyStatus0(ref Status0_Struct dest, Status0_Struct source)
		{
			int[] array = new int[50];
			dest.AutoMode = source.AutoMode;
			dest.PowerEnabled = source.PowerEnabled;
			dest.ParameterMode = source.ParameterMode;
			dest.TestMode = source.TestMode;
			dest.StorageSystemMode = source.StorageSystemMode;
			dest.OwnerID1 = source.OwnerID1;
			dest.OwnerID2 = source.OwnerID2;
			dest.OwnerID3 = source.OwnerID3;
			array[1] = 0;
			while (array[1] < 10)
			{
				dest.Version[array[1]] = source.Version[array[1]];
				array[1]++;
			}

			dest.TestError = source.TestError;
			array[1] = 0;
			while (array[1] < 1000)
			{
				dest.TestErrorText[array[1]] = source.TestErrorText[array[1]];
				array[1]++;
			}
		}

		public void makeCopyStatus1(ref Status1_Struct dest, Status1_Struct source)
		{
			dest.RequestID = source.RequestID;
			dest.VisuLive = source.VisuLive;
		}

		public void makeCopyStatus2(ref Status2_Struct dest, Status2_Struct source)
		{
			dest.RequestID = source.RequestID;
			dest.VisuLive = source.VisuLive;
		}

		public void makeCopyStatus3(ref Status3_Struct dest, Status3_Struct source)
		{
			dest.RequestID = source.RequestID;
			dest.VisuLive = source.VisuLive;
		}

		public void makeCopySaveSys(ref SaveSys_Struct dest, SaveSys_Struct source)
		{
			dest.RequestBlock = source.RequestBlock;
		}

		public void makeCopyErrorSys(ref ErrorSys_Struct dest, ErrorSys_Struct source)
		{
			int[] array = new int[50];
			dest.Num = source.Num;
			array[1] = 0;
			while (array[1] < 1000)
			{
				dest.Text[array[1]] = source.Text[array[1]];
				array[1]++;
			}

			dest.SpindleTest = source.SpindleTest;
			dest.Warning = source.Warning;
			dest.Quit = source.Quit;
			dest.PlcError = source.PlcError;
		}

		public void makeCopyLogBookSys(ref LogBookSys_Struct dest, LogBookSys_Struct source)
		{
			int[] array = new int[50];
			dest.Position = source.Position;
			dest.Length = source.Length;
			array[1] = 0;
			while (array[1] < 2500)
			{
				dest.logMessBuffer[array[1]].Time.Year = source.logMessBuffer[array[1]].Time.Year;
				dest.logMessBuffer[array[1]].Time.Month = source.logMessBuffer[array[1]].Time.Month;
				dest.logMessBuffer[array[1]].Time.Day = source.logMessBuffer[array[1]].Time.Day;
				dest.logMessBuffer[array[1]].Time.Hour = source.logMessBuffer[array[1]].Time.Hour;
				dest.logMessBuffer[array[1]].Time.Minute = source.logMessBuffer[array[1]].Time.Minute;
				dest.logMessBuffer[array[1]].Time.Second = source.logMessBuffer[array[1]].Time.Second;
				dest.logMessBuffer[array[1]].Code = source.logMessBuffer[array[1]].Code;
				dest.logMessBuffer[array[1]].Type = source.logMessBuffer[array[1]].Type;
				dest.logMessBuffer[array[1]].ProgNum = source.logMessBuffer[array[1]].ProgNum;
				dest.logMessBuffer[array[1]].Step = source.logMessBuffer[array[1]].Step;
				dest.logMessBuffer[array[1]].Value1 = source.logMessBuffer[array[1]].Value1;
				dest.logMessBuffer[array[1]].Value2 = source.logMessBuffer[array[1]].Value2;
				array[2] = 0;
				while (array[2] < 5)
				{
					dest.logMessBuffer[array[1]].userName[array[2]] = source.logMessBuffer[array[1]].userName[array[2]];
					array[2]++;
				}

				dest.logMessBuffer[array[1]].cycNum = source.logMessBuffer[array[1]].cycNum;
				dest.logMessBuffer[array[1]].UnitIndex = source.logMessBuffer[array[1]].UnitIndex;
				dest.logMessBuffer[array[1]].res = source.logMessBuffer[array[1]].res;
				array[1]++;
			}
		}

		public void makeCopyResult(ref Result_Struct dest, Result_Struct source)
		{
			int[] array = new int[50];
			dest.Prog.byteReserve_1 = source.Prog.byteReserve_1;
			dest.Prog.Info.ProgNum = source.Prog.Info.ProgNum;
			array[3] = 0;
			while (array[3] < 32)
			{
				dest.Prog.Info.Name[array[3]] = source.Prog.Info.Name[array[3]];
				array[3]++;
			}

			dest.Prog.Info.Steps = source.Prog.Info.Steps;
			dest.Prog.Info.ResultParam1 = source.Prog.Info.ResultParam1;
			dest.Prog.Info.ResultParam2 = source.Prog.Info.ResultParam2;
			dest.Prog.Info.ResultParam3 = source.Prog.Info.ResultParam3;
			dest.Prog.M1FilterTime = source.Prog.M1FilterTime;
			dest.Prog.GradientLength = source.Prog.GradientLength;
			dest.Prog.GradientFilter = source.Prog.GradientFilter;
			dest.Prog.ADepthFilterTime = source.Prog.ADepthFilterTime;
			dest.Prog.ADepthGradientLength = source.Prog.ADepthGradientLength;
			dest.Prog.MaxTime = source.Prog.MaxTime;
			dest.Prog.PressureHolder = source.Prog.PressureHolder;
			dest.Prog.EndSetDigOut1 = source.Prog.EndSetDigOut1;
			dest.Prog.EndValueDigOut1 = source.Prog.EndValueDigOut1;
			dest.Prog.EndSetDigOut2 = source.Prog.EndSetDigOut2;
			dest.Prog.EndValueDigOut2 = source.Prog.EndValueDigOut2;
			dest.Prog.EndSetSync1 = source.Prog.EndSetSync1;
			dest.Prog.EndValueSync1 = source.Prog.EndValueSync1;
			dest.Prog.EndSetSync2 = source.Prog.EndSetSync2;
			dest.Prog.EndValueSync2 = source.Prog.EndValueSync2;
			array[2] = 0;
			while (array[2] < 25)
			{
				dest.Prog.Step[array[2]].Type = source.Prog.Step[array[2]].Type;
				dest.Prog.Step[array[2]].Switch = source.Prog.Step[array[2]].Switch;
				dest.Prog.Step[array[2]].IsResult1 = source.Prog.Step[array[2]].IsResult1;
				dest.Prog.Step[array[2]].IsResult2 = source.Prog.Step[array[2]].IsResult2;
				dest.Prog.Step[array[2]].IsResult3 = source.Prog.Step[array[2]].IsResult3;
				dest.Prog.Step[array[2]].Enable.Torque = source.Prog.Step[array[2]].Enable.Torque;
				dest.Prog.Step[array[2]].Enable.Snug = source.Prog.Step[array[2]].Enable.Snug;
				dest.Prog.Step[array[2]].Enable.FTorque = source.Prog.Step[array[2]].Enable.FTorque;
				dest.Prog.Step[array[2]].Enable.GradientMin = source.Prog.Step[array[2]].Enable.GradientMin;
				dest.Prog.Step[array[2]].Enable.GradientMax = source.Prog.Step[array[2]].Enable.GradientMax;
				dest.Prog.Step[array[2]].Enable.Angle = source.Prog.Step[array[2]].Enable.Angle;
				dest.Prog.Step[array[2]].Enable.Time = source.Prog.Step[array[2]].Enable.Time;
				dest.Prog.Step[array[2]].Enable.ADepth = source.Prog.Step[array[2]].Enable.ADepth;
				dest.Prog.Step[array[2]].Enable.ADepthGradMin = source.Prog.Step[array[2]].Enable.ADepthGradMin;
				dest.Prog.Step[array[2]].Enable.ADepthGradMax = source.Prog.Step[array[2]].Enable.ADepthGradMax;
				dest.Prog.Step[array[2]].Enable.Ana = source.Prog.Step[array[2]].Enable.Ana;
				dest.Prog.Step[array[2]].Enable.Release = source.Prog.Step[array[2]].Enable.Release;
				dest.Prog.Step[array[2]].NA = source.Prog.Step[array[2]].NA;
				dest.Prog.Step[array[2]].TM = source.Prog.Step[array[2]].TM;
				dest.Prog.Step[array[2]].MP = source.Prog.Step[array[2]].MP;
				dest.Prog.Step[array[2]].Mmin = source.Prog.Step[array[2]].Mmin;
				dest.Prog.Step[array[2]].Mmax = source.Prog.Step[array[2]].Mmax;
				dest.Prog.Step[array[2]].MS = source.Prog.Step[array[2]].MS;
				dest.Prog.Step[array[2]].MRP = source.Prog.Step[array[2]].MRP;
				dest.Prog.Step[array[2]].MRStep = source.Prog.Step[array[2]].MRStep;
				dest.Prog.Step[array[2]].MRType = source.Prog.Step[array[2]].MRType;
				dest.Prog.Step[array[2]].MDelayTime = source.Prog.Step[array[2]].MDelayTime;
				dest.Prog.Step[array[2]].MFP = source.Prog.Step[array[2]].MFP;
				dest.Prog.Step[array[2]].MFmin = source.Prog.Step[array[2]].MFmin;
				dest.Prog.Step[array[2]].MFmax = source.Prog.Step[array[2]].MFmax;
				dest.Prog.Step[array[2]].MGP = source.Prog.Step[array[2]].MGP;
				dest.Prog.Step[array[2]].MGmin = source.Prog.Step[array[2]].MGmin;
				dest.Prog.Step[array[2]].MGmax = source.Prog.Step[array[2]].MGmax;
				dest.Prog.Step[array[2]].WP = source.Prog.Step[array[2]].WP;
				dest.Prog.Step[array[2]].Wmin = source.Prog.Step[array[2]].Wmin;
				dest.Prog.Step[array[2]].Wmax = source.Prog.Step[array[2]].Wmax;
				dest.Prog.Step[array[2]].WN = source.Prog.Step[array[2]].WN;
				dest.Prog.Step[array[2]].TP = source.Prog.Step[array[2]].TP;
				dest.Prog.Step[array[2]].Tmin = source.Prog.Step[array[2]].Tmin;
				dest.Prog.Step[array[2]].Tmax = source.Prog.Step[array[2]].Tmax;
				dest.Prog.Step[array[2]].TN = source.Prog.Step[array[2]].TN;
				dest.Prog.Step[array[2]].LP = source.Prog.Step[array[2]].LP;
				dest.Prog.Step[array[2]].Lmin = source.Prog.Step[array[2]].Lmin;
				dest.Prog.Step[array[2]].Lmax = source.Prog.Step[array[2]].Lmax;
				dest.Prog.Step[array[2]].LGP = source.Prog.Step[array[2]].LGP;
				dest.Prog.Step[array[2]].LGmin = source.Prog.Step[array[2]].LGmin;
				dest.Prog.Step[array[2]].LGmax = source.Prog.Step[array[2]].LGmax;
				dest.Prog.Step[array[2]].AnaP = source.Prog.Step[array[2]].AnaP;
				dest.Prog.Step[array[2]].AnaMin = source.Prog.Step[array[2]].AnaMin;
				dest.Prog.Step[array[2]].AnaMax = source.Prog.Step[array[2]].AnaMax;
				dest.Prog.Step[array[2]].DigP = source.Prog.Step[array[2]].DigP;
				dest.Prog.Step[array[2]].DigMin = source.Prog.Step[array[2]].DigMin;
				dest.Prog.Step[array[2]].DigMax = source.Prog.Step[array[2]].DigMax;
				dest.Prog.Step[array[2]].JumpTo = source.Prog.Step[array[2]].JumpTo;
				dest.Prog.Step[array[2]].ModDigOut = source.Prog.Step[array[2]].ModDigOut;
				dest.Prog.Step[array[2]].PressureSpindle = source.Prog.Step[array[2]].PressureSpindle;
				dest.Prog.Step[array[2]].CountPassMax = source.Prog.Step[array[2]].CountPassMax;
				dest.Prog.Step[array[2]].UserRights = source.Prog.Step[array[2]].UserRights;
				array[2]++;
			}

			dest.Prog.UseLocalJawSettings = source.Prog.UseLocalJawSettings;
			dest.Prog.JawLocalWrittenOnce = source.Prog.JawLocalWrittenOnce;
			dest.Prog.JawOpenDistance = source.Prog.JawOpenDistance;
			dest.Prog.JawOpenDepthGradMax = source.Prog.JawOpenDepthGradMax;
			dest.Prog.JawOpenDepthGradMin = source.Prog.JawOpenDepthGradMin;
			array[3] = 0;
			while (array[3] < 4)
			{
				array[4] = 0;
				while (array[4] < 7)
				{
					dest.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].StepIndex = source.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].StepIndex;
					dest.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].TypeOfData = source.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].TypeOfData;
					dest.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].Value = source.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].Value;
					dest.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MinValue = source.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MinValue;
					dest.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MaxValue = source.Prog.FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MaxValue;
					array[4]++;
				}

				array[3]++;
			}

			dest.UnitTorque = source.UnitTorque;
			dest.IONIO = source.IONIO;
			dest.LastStep = source.LastStep;
			dest.Time.Year = source.Time.Year;
			dest.Time.Month = source.Time.Month;
			dest.Time.Day = source.Time.Day;
			dest.Time.Hour = source.Time.Hour;
			dest.Time.Minute = source.Time.Minute;
			dest.Time.Second = source.Time.Second;
			dest.Cycle = source.Cycle;
			dest.ScrewDuration = source.ScrewDuration;
			array[1] = 0;
			while (array[1] < 250)
			{
				dest.StepResult[array[1]].Step = source.StepResult[array[1]].Step;
				dest.StepResult[array[1]].IONIO = source.StepResult[array[1]].IONIO;
				dest.StepResult[array[1]].OrgStep = source.StepResult[array[1]].OrgStep;
				dest.StepResult[array[1]].Torque = source.StepResult[array[1]].Torque;
				dest.StepResult[array[1]].MaxTorque = source.StepResult[array[1]].MaxTorque;
				dest.StepResult[array[1]].FTorque = source.StepResult[array[1]].FTorque;
				dest.StepResult[array[1]].DelayTorque = source.StepResult[array[1]].DelayTorque;
				dest.StepResult[array[1]].M360Follow = source.StepResult[array[1]].M360Follow;
				dest.StepResult[array[1]].Gradient = source.StepResult[array[1]].Gradient;
				dest.StepResult[array[1]].Angle = source.StepResult[array[1]].Angle;
				dest.StepResult[array[1]].Time = source.StepResult[array[1]].Time;
				dest.StepResult[array[1]].ADepth = source.StepResult[array[1]].ADepth;
				dest.StepResult[array[1]].ADepthGrad = source.StepResult[array[1]].ADepthGrad;
				dest.StepResult[array[1]].Ana = source.StepResult[array[1]].Ana;
				dest.StepResult[array[1]].Dig = source.StepResult[array[1]].Dig;
				array[1]++;
			}

			dest.Valid = source.Valid;
			dest.ResultStep1 = source.ResultStep1;
			dest.Res1 = source.Res1;
			dest.ResultStep2 = source.ResultStep2;
			dest.Res2 = source.Res2;
			dest.ResultStep3 = source.ResultStep3;
			dest.Res3 = source.Res3;
			array[1] = 0;
			while (array[1] < 32)
			{
				dest.ScrewID[array[1]] = source.ScrewID[array[1]];
				array[1]++;
			}

			array[1] = 0;
			while (array[1] < 20)
			{
				dest.ExtendedResult[array[1]] = source.ExtendedResult[array[1]];
				array[1]++;
			}
		}

		public void makeCopyLastNIOResults(ref LastNIOResults_Struct dest, LastNIOResults_Struct source)
		{
			int[] array = new int[50];
			dest.ID1 = source.ID1;
			array[1] = 0;
			while (array[1] < 100)
			{
				dest.Num[array[1]].ProgNum = source.Num[array[1]].ProgNum;
				array[2] = 0;
				while (array[2] < 32)
				{
					dest.Num[array[1]].ProgName[array[2]] = source.Num[array[1]].ProgName[array[2]];
					array[2]++;
				}

				dest.Num[array[1]].IONIO = source.Num[array[1]].IONIO;
				dest.Num[array[1]].LastStep = source.Num[array[1]].LastStep;
				dest.Num[array[1]].Time.Year = source.Num[array[1]].Time.Year;
				dest.Num[array[1]].Time.Month = source.Num[array[1]].Time.Month;
				dest.Num[array[1]].Time.Day = source.Num[array[1]].Time.Day;
				dest.Num[array[1]].Time.Hour = source.Num[array[1]].Time.Hour;
				dest.Num[array[1]].Time.Minute = source.Num[array[1]].Time.Minute;
				dest.Num[array[1]].Time.Second = source.Num[array[1]].Time.Second;
				dest.Num[array[1]].Cycle = source.Num[array[1]].Cycle;
				dest.Num[array[1]].ScrewDuration = source.Num[array[1]].ScrewDuration;
				dest.Num[array[1]].Valid = source.Num[array[1]].Valid;
				dest.Num[array[1]].ResultStep1 = source.Num[array[1]].ResultStep1;
				dest.Num[array[1]].ResultParam1 = source.Num[array[1]].ResultParam1;
				dest.Num[array[1]].Res1 = source.Num[array[1]].Res1;
				dest.Num[array[1]].ResultStep2 = source.Num[array[1]].ResultStep2;
				dest.Num[array[1]].ResultParam2 = source.Num[array[1]].ResultParam2;
				dest.Num[array[1]].Res2 = source.Num[array[1]].Res2;
				dest.Num[array[1]].ResultStep3 = source.Num[array[1]].ResultStep3;
				dest.Num[array[1]].ResultParam3 = source.Num[array[1]].ResultParam3;
				dest.Num[array[1]].Res3 = source.Num[array[1]].Res3;
				array[2] = 0;
				while (array[2] < 32)
				{
					dest.Num[array[1]].ScrewID[array[2]] = source.Num[array[1]].ScrewID[array[2]];
					array[2]++;
				}

				dest.Num[array[1]].res = source.Num[array[1]].res;
				array[1]++;
			}

			dest.ID2 = source.ID2;
		}

		public void makeCopyPProg(ref PProg_Struct dest, PProg_Struct source)
		{
			int[] array = new int[50];
			dest.PProgVersion = source.PProgVersion;
			array[1] = 0;
			while (array[1] < 1024)
			{
				dest.Num[array[1]].byteReserve_1 = source.Num[array[1]].byteReserve_1;
				dest.Num[array[1]].Info.ProgNum = source.Num[array[1]].Info.ProgNum;
				array[3] = 0;
				while (array[3] < 32)
				{
					dest.Num[array[1]].Info.Name[array[3]] = source.Num[array[1]].Info.Name[array[3]];
					array[3]++;
				}

				dest.Num[array[1]].Info.Steps = source.Num[array[1]].Info.Steps;
				dest.Num[array[1]].Info.ResultParam1 = source.Num[array[1]].Info.ResultParam1;
				dest.Num[array[1]].Info.ResultParam2 = source.Num[array[1]].Info.ResultParam2;
				dest.Num[array[1]].Info.ResultParam3 = source.Num[array[1]].Info.ResultParam3;
				dest.Num[array[1]].M1FilterTime = source.Num[array[1]].M1FilterTime;
				dest.Num[array[1]].GradientLength = source.Num[array[1]].GradientLength;
				dest.Num[array[1]].GradientFilter = source.Num[array[1]].GradientFilter;
				dest.Num[array[1]].ADepthFilterTime = source.Num[array[1]].ADepthFilterTime;
				dest.Num[array[1]].ADepthGradientLength = source.Num[array[1]].ADepthGradientLength;
				dest.Num[array[1]].MaxTime = source.Num[array[1]].MaxTime;
				dest.Num[array[1]].PressureHolder = source.Num[array[1]].PressureHolder;
				dest.Num[array[1]].EndSetDigOut1 = source.Num[array[1]].EndSetDigOut1;
				dest.Num[array[1]].EndValueDigOut1 = source.Num[array[1]].EndValueDigOut1;
				dest.Num[array[1]].EndSetDigOut2 = source.Num[array[1]].EndSetDigOut2;
				dest.Num[array[1]].EndValueDigOut2 = source.Num[array[1]].EndValueDigOut2;
				dest.Num[array[1]].EndSetSync1 = source.Num[array[1]].EndSetSync1;
				dest.Num[array[1]].EndValueSync1 = source.Num[array[1]].EndValueSync1;
				dest.Num[array[1]].EndSetSync2 = source.Num[array[1]].EndSetSync2;
				dest.Num[array[1]].EndValueSync2 = source.Num[array[1]].EndValueSync2;
				array[2] = 0;
				while (array[2] < 25)
				{
					dest.Num[array[1]].Step[array[2]].Type = source.Num[array[1]].Step[array[2]].Type;
					dest.Num[array[1]].Step[array[2]].Switch = source.Num[array[1]].Step[array[2]].Switch;
					dest.Num[array[1]].Step[array[2]].IsResult1 = source.Num[array[1]].Step[array[2]].IsResult1;
					dest.Num[array[1]].Step[array[2]].IsResult2 = source.Num[array[1]].Step[array[2]].IsResult2;
					dest.Num[array[1]].Step[array[2]].IsResult3 = source.Num[array[1]].Step[array[2]].IsResult3;
					dest.Num[array[1]].Step[array[2]].Enable.Torque = source.Num[array[1]].Step[array[2]].Enable.Torque;
					dest.Num[array[1]].Step[array[2]].Enable.Snug = source.Num[array[1]].Step[array[2]].Enable.Snug;
					dest.Num[array[1]].Step[array[2]].Enable.FTorque = source.Num[array[1]].Step[array[2]].Enable.FTorque;
					dest.Num[array[1]].Step[array[2]].Enable.GradientMin = source.Num[array[1]].Step[array[2]].Enable.GradientMin;
					dest.Num[array[1]].Step[array[2]].Enable.GradientMax = source.Num[array[1]].Step[array[2]].Enable.GradientMax;
					dest.Num[array[1]].Step[array[2]].Enable.Angle = source.Num[array[1]].Step[array[2]].Enable.Angle;
					dest.Num[array[1]].Step[array[2]].Enable.Time = source.Num[array[1]].Step[array[2]].Enable.Time;
					dest.Num[array[1]].Step[array[2]].Enable.ADepth = source.Num[array[1]].Step[array[2]].Enable.ADepth;
					dest.Num[array[1]].Step[array[2]].Enable.ADepthGradMin = source.Num[array[1]].Step[array[2]].Enable.ADepthGradMin;
					dest.Num[array[1]].Step[array[2]].Enable.ADepthGradMax = source.Num[array[1]].Step[array[2]].Enable.ADepthGradMax;
					dest.Num[array[1]].Step[array[2]].Enable.Ana = source.Num[array[1]].Step[array[2]].Enable.Ana;
					dest.Num[array[1]].Step[array[2]].Enable.Release = source.Num[array[1]].Step[array[2]].Enable.Release;
					dest.Num[array[1]].Step[array[2]].NA = source.Num[array[1]].Step[array[2]].NA;
					dest.Num[array[1]].Step[array[2]].TM = source.Num[array[1]].Step[array[2]].TM;
					dest.Num[array[1]].Step[array[2]].MP = source.Num[array[1]].Step[array[2]].MP;
					dest.Num[array[1]].Step[array[2]].Mmin = source.Num[array[1]].Step[array[2]].Mmin;
					dest.Num[array[1]].Step[array[2]].Mmax = source.Num[array[1]].Step[array[2]].Mmax;
					dest.Num[array[1]].Step[array[2]].MS = source.Num[array[1]].Step[array[2]].MS;
					dest.Num[array[1]].Step[array[2]].MRP = source.Num[array[1]].Step[array[2]].MRP;
					dest.Num[array[1]].Step[array[2]].MRStep = source.Num[array[1]].Step[array[2]].MRStep;
					dest.Num[array[1]].Step[array[2]].MRType = source.Num[array[1]].Step[array[2]].MRType;
					dest.Num[array[1]].Step[array[2]].MDelayTime = source.Num[array[1]].Step[array[2]].MDelayTime;
					dest.Num[array[1]].Step[array[2]].MFP = source.Num[array[1]].Step[array[2]].MFP;
					dest.Num[array[1]].Step[array[2]].MFmin = source.Num[array[1]].Step[array[2]].MFmin;
					dest.Num[array[1]].Step[array[2]].MFmax = source.Num[array[1]].Step[array[2]].MFmax;
					dest.Num[array[1]].Step[array[2]].MGP = source.Num[array[1]].Step[array[2]].MGP;
					dest.Num[array[1]].Step[array[2]].MGmin = source.Num[array[1]].Step[array[2]].MGmin;
					dest.Num[array[1]].Step[array[2]].MGmax = source.Num[array[1]].Step[array[2]].MGmax;
					dest.Num[array[1]].Step[array[2]].WP = source.Num[array[1]].Step[array[2]].WP;
					dest.Num[array[1]].Step[array[2]].Wmin = source.Num[array[1]].Step[array[2]].Wmin;
					dest.Num[array[1]].Step[array[2]].Wmax = source.Num[array[1]].Step[array[2]].Wmax;
					dest.Num[array[1]].Step[array[2]].WN = source.Num[array[1]].Step[array[2]].WN;
					dest.Num[array[1]].Step[array[2]].TP = source.Num[array[1]].Step[array[2]].TP;
					dest.Num[array[1]].Step[array[2]].Tmin = source.Num[array[1]].Step[array[2]].Tmin;
					dest.Num[array[1]].Step[array[2]].Tmax = source.Num[array[1]].Step[array[2]].Tmax;
					dest.Num[array[1]].Step[array[2]].TN = source.Num[array[1]].Step[array[2]].TN;
					dest.Num[array[1]].Step[array[2]].LP = source.Num[array[1]].Step[array[2]].LP;
					dest.Num[array[1]].Step[array[2]].Lmin = source.Num[array[1]].Step[array[2]].Lmin;
					dest.Num[array[1]].Step[array[2]].Lmax = source.Num[array[1]].Step[array[2]].Lmax;
					dest.Num[array[1]].Step[array[2]].LGP = source.Num[array[1]].Step[array[2]].LGP;
					dest.Num[array[1]].Step[array[2]].LGmin = source.Num[array[1]].Step[array[2]].LGmin;
					dest.Num[array[1]].Step[array[2]].LGmax = source.Num[array[1]].Step[array[2]].LGmax;
					dest.Num[array[1]].Step[array[2]].AnaP = source.Num[array[1]].Step[array[2]].AnaP;
					dest.Num[array[1]].Step[array[2]].AnaMin = source.Num[array[1]].Step[array[2]].AnaMin;
					dest.Num[array[1]].Step[array[2]].AnaMax = source.Num[array[1]].Step[array[2]].AnaMax;
					dest.Num[array[1]].Step[array[2]].DigP = source.Num[array[1]].Step[array[2]].DigP;
					dest.Num[array[1]].Step[array[2]].DigMin = source.Num[array[1]].Step[array[2]].DigMin;
					dest.Num[array[1]].Step[array[2]].DigMax = source.Num[array[1]].Step[array[2]].DigMax;
					dest.Num[array[1]].Step[array[2]].JumpTo = source.Num[array[1]].Step[array[2]].JumpTo;
					dest.Num[array[1]].Step[array[2]].ModDigOut = source.Num[array[1]].Step[array[2]].ModDigOut;
					dest.Num[array[1]].Step[array[2]].PressureSpindle = source.Num[array[1]].Step[array[2]].PressureSpindle;
					dest.Num[array[1]].Step[array[2]].CountPassMax = source.Num[array[1]].Step[array[2]].CountPassMax;
					dest.Num[array[1]].Step[array[2]].UserRights = source.Num[array[1]].Step[array[2]].UserRights;
					array[2]++;
				}

				dest.Num[array[1]].UseLocalJawSettings = source.Num[array[1]].UseLocalJawSettings;
				dest.Num[array[1]].JawLocalWrittenOnce = source.Num[array[1]].JawLocalWrittenOnce;
				dest.Num[array[1]].JawOpenDistance = source.Num[array[1]].JawOpenDistance;
				dest.Num[array[1]].JawOpenDepthGradMax = source.Num[array[1]].JawOpenDepthGradMax;
				dest.Num[array[1]].JawOpenDepthGradMin = source.Num[array[1]].JawOpenDepthGradMin;
				array[3] = 0;
				while (array[3] < 4)
				{
					array[4] = 0;
					while (array[4] < 7)
					{
						dest.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].StepIndex = source.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].StepIndex;
						dest.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].TypeOfData = source.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].TypeOfData;
						dest.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].Value = source.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].Value;
						dest.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MinValue = source.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MinValue;
						dest.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MaxValue = source.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MaxValue;
						array[4]++;
					}

					array[3]++;
				}

				array[1]++;
			}
		}

		public void makeCopySpConst(ref SpConst_Struct dest, SpConst_Struct source)
		{
			dest.TorqueSensorScale = source.TorqueSensorScale;
			dest.TorqueSensorTolerance = source.TorqueSensorTolerance;
			dest.AngleSensorScale = source.AngleSensorScale;
			dest.TorqueSensorInvers = source.TorqueSensorInvers;
			dest.AngleSensorInvers = source.AngleSensorInvers;
			dest.RedundantSensorActive = source.RedundantSensorActive;
			dest.TorqueRedundantTime = source.TorqueRedundantTime;
			dest.TorqueRedundantTolerance = source.TorqueRedundantTolerance;
			dest.AngleRedundantTolerance = source.AngleRedundantTolerance;
			dest.DriveUnitRpm = source.DriveUnitRpm;
			dest.DriveUnitInvers = source.DriveUnitInvers;
			dest.DepthSensorScale = source.DepthSensorScale;
			dest.DepthSensorOffset = source.DepthSensorOffset;
			dest.DepthSensorOffsetMin = source.DepthSensorOffsetMin;
			dest.DepthSensorOffsetMax = source.DepthSensorOffsetMax;
			dest.DepthSensorOffsetPreset = source.DepthSensorOffsetPreset;
			dest.DepthSensorInvers = source.DepthSensorInvers;
			dest.AnaSigScale = source.AnaSigScale;
			dest.AnaSigOffset = source.AnaSigOffset;
			dest.SpindleTorque = source.SpindleTorque;
			dest.SpindleGearFactor = source.SpindleGearFactor;
			dest.ReleaseSpeed = source.ReleaseSpeed;
			dest.FrictionTorque = source.FrictionTorque;
			dest.FrictionSpeed = source.FrictionSpeed;
			dest.FrictionTestStartup = source.FrictionTestStartup;
			dest.FrictionTestEMG = source.FrictionTestEMG;
			dest.PressureScaleSpindle = source.PressureScaleSpindle;
			dest.ReserveFloat_1 = source.ReserveFloat_1;
			dest.PressureScaleHolder = source.PressureScaleHolder;
			dest.JawOpenDistance = source.JawOpenDistance;
			dest.JawOpenDepthGradMax = source.JawOpenDepthGradMax;
			dest.JawOpenDepthGradMin = source.JawOpenDepthGradMin;
			dest.ReserveByte_1 = source.ReserveByte_1;
			dest.UsbKeyActivated = source.UsbKeyActivated;
		}

		public void makeCopySysConst(ref SysConst_Struct dest, SysConst_Struct source)
		{
			int[] array = new int[50];
			dest.ActualTime.Year = source.ActualTime.Year;
			dest.ActualTime.Month = source.ActualTime.Month;
			dest.ActualTime.Day = source.ActualTime.Day;
			dest.ActualTime.Hour = source.ActualTime.Hour;
			dest.ActualTime.Minute = source.ActualTime.Minute;
			dest.ActualTime.Second = source.ActualTime.Second;
			dest.SettingTime.Year = source.SettingTime.Year;
			dest.SettingTime.Month = source.SettingTime.Month;
			dest.SettingTime.Day = source.SettingTime.Day;
			dest.SettingTime.Hour = source.SettingTime.Hour;
			dest.SettingTime.Minute = source.SettingTime.Minute;
			dest.SettingTime.Second = source.SettingTime.Second;
			array[1] = 0;
			while (array[1] < 20)
			{
				dest.SystemID[array[1]] = source.SystemID[array[1]];
				array[1]++;
			}

			array[1] = 0;
			while (array[1] < 32)
			{
				dest.IdentServerName[array[1]] = source.IdentServerName[array[1]];
				array[1]++;
			}

			dest.IPAddress.Byte1 = source.IPAddress.Byte1;
			dest.IPAddress.Byte2 = source.IPAddress.Byte2;
			dest.IPAddress.Byte3 = source.IPAddress.Byte3;
			dest.IPAddress.Byte4 = source.IPAddress.Byte4;
			dest.DHCP = source.DHCP;
			dest.SubNetMask.Byte1 = source.SubNetMask.Byte1;
			dest.SubNetMask.Byte2 = source.SubNetMask.Byte2;
			dest.SubNetMask.Byte3 = source.SubNetMask.Byte3;
			dest.SubNetMask.Byte4 = source.SubNetMask.Byte4;
			dest.DefaultGateway.Byte1 = source.DefaultGateway.Byte1;
			dest.DefaultGateway.Byte2 = source.DefaultGateway.Byte2;
			dest.DefaultGateway.Byte3 = source.DefaultGateway.Byte3;
			dest.DefaultGateway.Byte4 = source.DefaultGateway.Byte4;
			array[1] = 0;
			while (array[1] < 5)
			{
				array[2] = 0;
				while (array[2] < 32)
				{
					dest.AdvancedWarnings[array[1]].Name[array[2]] = source.AdvancedWarnings[array[1]].Name[array[2]];
					array[2]++;
				}

				dest.AdvancedWarnings[array[1]].Limit = source.AdvancedWarnings[array[1]].Limit;
				dest.AdvancedWarnings[array[1]].Advance = source.AdvancedWarnings[array[1]].Advance;
				dest.AdvancedWarnings[array[1]].AdvancedWarningTime.Year = source.AdvancedWarnings[array[1]].AdvancedWarningTime.Year;
				dest.AdvancedWarnings[array[1]].AdvancedWarningTime.Month = source.AdvancedWarnings[array[1]].AdvancedWarningTime.Month;
				dest.AdvancedWarnings[array[1]].AdvancedWarningTime.Day = source.AdvancedWarnings[array[1]].AdvancedWarningTime.Day;
				dest.AdvancedWarnings[array[1]].AdvancedWarningTime.Hour = source.AdvancedWarnings[array[1]].AdvancedWarningTime.Hour;
				dest.AdvancedWarnings[array[1]].AdvancedWarningTime.Minute = source.AdvancedWarnings[array[1]].AdvancedWarningTime.Minute;
				dest.AdvancedWarnings[array[1]].AdvancedWarningTime.Second = source.AdvancedWarnings[array[1]].AdvancedWarningTime.Second;
				dest.AdvancedWarnings[array[1]].AdvancedDays = source.AdvancedWarnings[array[1]].AdvancedDays;
				dest.AdvancedWarnings[array[1]].EnableAdvancedWarningTime = source.AdvancedWarnings[array[1]].EnableAdvancedWarningTime;
				array[1]++;
			}

			dest.UnitTorque = source.UnitTorque;
			dest.Com1.BaudRate = source.Com1.BaudRate;
			dest.Com1.Parity = source.Com1.Parity;
			array[1] = 0;
			while (array[1] < 20)
			{
				array[2] = 0;
				while (array[2] < 5)
				{
					dest.PassCodes[array[1]].Name[array[2]] = source.PassCodes[array[1]].Name[array[2]];
					array[2]++;
				}

				dest.PassCodes[array[1]].Code = source.PassCodes[array[1]].Code;
				dest.PassCodes[array[1]].Level = source.PassCodes[array[1]].Level;
				array[1]++;
			}

			dest.UserLevels.UserLevel_BackupForm = source.UserLevels.UserLevel_BackupForm;
			dest.UserLevels.UserLevel_CheckParamForm = source.UserLevels.UserLevel_CheckParamForm;
			dest.UserLevels.UserLevel_CycleCounterForm = source.UserLevels.UserLevel_CycleCounterForm;
			dest.UserLevels.UserLevel_EditSteps = source.UserLevels.UserLevel_EditSteps;
			dest.UserLevels.UserLevel_PrgOptParameterForm = source.UserLevels.UserLevel_PrgOptParameterForm;
			dest.UserLevels.UserLevel_ProgramOverviewForm = source.UserLevels.UserLevel_ProgramOverviewForm;
			dest.UserLevels.UserLevel_SpindleConstantsForm = source.UserLevels.UserLevel_SpindleConstantsForm;
			dest.UserLevels.UserLevel_StatisticsLastResForm = source.UserLevels.UserLevel_StatisticsLastResForm;
			dest.UserLevels.UserLevel_StepOverviewForm = source.UserLevels.UserLevel_StepOverviewForm;
			dest.UserLevels.UserLevel_SystemConstantsForm = source.UserLevels.UserLevel_SystemConstantsForm;
			dest.UserLevels.UserLevel_TestIOForm = source.UserLevels.UserLevel_TestIOForm;
			dest.UserLevels.UserLevel_TestMotorSensorForm = source.UserLevels.UserLevel_TestMotorSensorForm;
			dest.UserLevels.UserLevel_VisualisationParamForm = source.UserLevels.UserLevel_VisualisationParamForm;
			dest.UserLevels.UserLevel_FourStepEditForm = source.UserLevels.UserLevel_FourStepEditForm;
			dest.UserLevels.UserLevel_Maintenance = source.UserLevels.UserLevel_Maintenance;
			dest.UserLevels.UserLevel_HandStartForm = source.UserLevels.UserLevel_HandStartForm;
			dest.UserLevels.UserLevel_BrowserForm = source.UserLevels.UserLevel_BrowserForm;
			dest.UserLevels.UserLevel_Reserve1 = source.UserLevels.UserLevel_Reserve1;
			dest.UserLevels.UserLevel_Reserve2 = source.UserLevels.UserLevel_Reserve2;
			dest.UserLevels.UserLevel_Reserve3 = source.UserLevels.UserLevel_Reserve3;
			dest.UserLevels.UserLevel_Reserve4 = source.UserLevels.UserLevel_Reserve4;
			dest.UserLevels.UserLevel_Reserve5 = source.UserLevels.UserLevel_Reserve5;
			array[1] = 0;
			while (array[1] < 16)
			{
				dest.AreaCode[array[1]] = source.AreaCode[array[1]];
				array[1]++;
			}
		}

		public void makeCopyProcessInfo(ref ProcessInfo_Struct dest, ProcessInfo_Struct source)
		{
			int[] array = new int[50]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			while (array[1] < 1024)
			{
				dest.ProgInfo[array[1]].ProgNum = source.ProgInfo[array[1]].ProgNum;
				array[2] = 0;
				while (array[2] < 32)
				{
					dest.ProgInfo[array[1]].Name[array[2]] = source.ProgInfo[array[1]].Name[array[2]];
					array[2]++;
				}

				dest.ProgInfo[array[1]].Steps = source.ProgInfo[array[1]].Steps;
				dest.ProgInfo[array[1]].ResultParam1 = source.ProgInfo[array[1]].ResultParam1;
				dest.ProgInfo[array[1]].ResultParam2 = source.ProgInfo[array[1]].ResultParam2;
				dest.ProgInfo[array[1]].ResultParam3 = source.ProgInfo[array[1]].ResultParam3;
				array[1]++;
			}
		}

		public void makeCopyPProgXChanged(ref PProgXChanged_Struct dest, PProgXChanged_Struct source)
		{
			int[] array = new int[50]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			while (array[1] < 1024)
			{
				dest.Changed[array[1]] = source.Changed[array[1]];
				array[1]++;
			}
		}

		public void makeCopyPProgX(ref PProgX_Struct dest, PProgX_Struct source)
		{
			int[] array = new int[50];
			dest.PProgVersion = source.PProgVersion;
			array[1] = 0;
			while (array[1] < 32)
			{
				dest.Num[array[1]].byteReserve_1 = source.Num[array[1]].byteReserve_1;
				dest.Num[array[1]].Info.ProgNum = source.Num[array[1]].Info.ProgNum;
				array[3] = 0;
				while (array[3] < 32)
				{
					dest.Num[array[1]].Info.Name[array[3]] = source.Num[array[1]].Info.Name[array[3]];
					array[3]++;
				}

				dest.Num[array[1]].Info.Steps = source.Num[array[1]].Info.Steps;
				dest.Num[array[1]].Info.ResultParam1 = source.Num[array[1]].Info.ResultParam1;
				dest.Num[array[1]].Info.ResultParam2 = source.Num[array[1]].Info.ResultParam2;
				dest.Num[array[1]].Info.ResultParam3 = source.Num[array[1]].Info.ResultParam3;
				dest.Num[array[1]].M1FilterTime = source.Num[array[1]].M1FilterTime;
				dest.Num[array[1]].GradientLength = source.Num[array[1]].GradientLength;
				dest.Num[array[1]].GradientFilter = source.Num[array[1]].GradientFilter;
				dest.Num[array[1]].ADepthFilterTime = source.Num[array[1]].ADepthFilterTime;
				dest.Num[array[1]].ADepthGradientLength = source.Num[array[1]].ADepthGradientLength;
				dest.Num[array[1]].MaxTime = source.Num[array[1]].MaxTime;
				dest.Num[array[1]].PressureHolder = source.Num[array[1]].PressureHolder;
				dest.Num[array[1]].EndSetDigOut1 = source.Num[array[1]].EndSetDigOut1;
				dest.Num[array[1]].EndValueDigOut1 = source.Num[array[1]].EndValueDigOut1;
				dest.Num[array[1]].EndSetDigOut2 = source.Num[array[1]].EndSetDigOut2;
				dest.Num[array[1]].EndValueDigOut2 = source.Num[array[1]].EndValueDigOut2;
				dest.Num[array[1]].EndSetSync1 = source.Num[array[1]].EndSetSync1;
				dest.Num[array[1]].EndValueSync1 = source.Num[array[1]].EndValueSync1;
				dest.Num[array[1]].EndSetSync2 = source.Num[array[1]].EndSetSync2;
				dest.Num[array[1]].EndValueSync2 = source.Num[array[1]].EndValueSync2;
				array[2] = 0;
				while (array[2] < 25)
				{
					dest.Num[array[1]].Step[array[2]].Type = source.Num[array[1]].Step[array[2]].Type;
					dest.Num[array[1]].Step[array[2]].Switch = source.Num[array[1]].Step[array[2]].Switch;
					dest.Num[array[1]].Step[array[2]].IsResult1 = source.Num[array[1]].Step[array[2]].IsResult1;
					dest.Num[array[1]].Step[array[2]].IsResult2 = source.Num[array[1]].Step[array[2]].IsResult2;
					dest.Num[array[1]].Step[array[2]].IsResult3 = source.Num[array[1]].Step[array[2]].IsResult3;
					dest.Num[array[1]].Step[array[2]].Enable.Torque = source.Num[array[1]].Step[array[2]].Enable.Torque;
					dest.Num[array[1]].Step[array[2]].Enable.Snug = source.Num[array[1]].Step[array[2]].Enable.Snug;
					dest.Num[array[1]].Step[array[2]].Enable.FTorque = source.Num[array[1]].Step[array[2]].Enable.FTorque;
					dest.Num[array[1]].Step[array[2]].Enable.GradientMin = source.Num[array[1]].Step[array[2]].Enable.GradientMin;
					dest.Num[array[1]].Step[array[2]].Enable.GradientMax = source.Num[array[1]].Step[array[2]].Enable.GradientMax;
					dest.Num[array[1]].Step[array[2]].Enable.Angle = source.Num[array[1]].Step[array[2]].Enable.Angle;
					dest.Num[array[1]].Step[array[2]].Enable.Time = source.Num[array[1]].Step[array[2]].Enable.Time;
					dest.Num[array[1]].Step[array[2]].Enable.ADepth = source.Num[array[1]].Step[array[2]].Enable.ADepth;
					dest.Num[array[1]].Step[array[2]].Enable.ADepthGradMin = source.Num[array[1]].Step[array[2]].Enable.ADepthGradMin;
					dest.Num[array[1]].Step[array[2]].Enable.ADepthGradMax = source.Num[array[1]].Step[array[2]].Enable.ADepthGradMax;
					dest.Num[array[1]].Step[array[2]].Enable.Ana = source.Num[array[1]].Step[array[2]].Enable.Ana;
					dest.Num[array[1]].Step[array[2]].Enable.Release = source.Num[array[1]].Step[array[2]].Enable.Release;
					dest.Num[array[1]].Step[array[2]].NA = source.Num[array[1]].Step[array[2]].NA;
					dest.Num[array[1]].Step[array[2]].TM = source.Num[array[1]].Step[array[2]].TM;
					dest.Num[array[1]].Step[array[2]].MP = source.Num[array[1]].Step[array[2]].MP;
					dest.Num[array[1]].Step[array[2]].Mmin = source.Num[array[1]].Step[array[2]].Mmin;
					dest.Num[array[1]].Step[array[2]].Mmax = source.Num[array[1]].Step[array[2]].Mmax;
					dest.Num[array[1]].Step[array[2]].MS = source.Num[array[1]].Step[array[2]].MS;
					dest.Num[array[1]].Step[array[2]].MRP = source.Num[array[1]].Step[array[2]].MRP;
					dest.Num[array[1]].Step[array[2]].MRStep = source.Num[array[1]].Step[array[2]].MRStep;
					dest.Num[array[1]].Step[array[2]].MRType = source.Num[array[1]].Step[array[2]].MRType;
					dest.Num[array[1]].Step[array[2]].MDelayTime = source.Num[array[1]].Step[array[2]].MDelayTime;
					dest.Num[array[1]].Step[array[2]].MFP = source.Num[array[1]].Step[array[2]].MFP;
					dest.Num[array[1]].Step[array[2]].MFmin = source.Num[array[1]].Step[array[2]].MFmin;
					dest.Num[array[1]].Step[array[2]].MFmax = source.Num[array[1]].Step[array[2]].MFmax;
					dest.Num[array[1]].Step[array[2]].MGP = source.Num[array[1]].Step[array[2]].MGP;
					dest.Num[array[1]].Step[array[2]].MGmin = source.Num[array[1]].Step[array[2]].MGmin;
					dest.Num[array[1]].Step[array[2]].MGmax = source.Num[array[1]].Step[array[2]].MGmax;
					dest.Num[array[1]].Step[array[2]].WP = source.Num[array[1]].Step[array[2]].WP;
					dest.Num[array[1]].Step[array[2]].Wmin = source.Num[array[1]].Step[array[2]].Wmin;
					dest.Num[array[1]].Step[array[2]].Wmax = source.Num[array[1]].Step[array[2]].Wmax;
					dest.Num[array[1]].Step[array[2]].WN = source.Num[array[1]].Step[array[2]].WN;
					dest.Num[array[1]].Step[array[2]].TP = source.Num[array[1]].Step[array[2]].TP;
					dest.Num[array[1]].Step[array[2]].Tmin = source.Num[array[1]].Step[array[2]].Tmin;
					dest.Num[array[1]].Step[array[2]].Tmax = source.Num[array[1]].Step[array[2]].Tmax;
					dest.Num[array[1]].Step[array[2]].TN = source.Num[array[1]].Step[array[2]].TN;
					dest.Num[array[1]].Step[array[2]].LP = source.Num[array[1]].Step[array[2]].LP;
					dest.Num[array[1]].Step[array[2]].Lmin = source.Num[array[1]].Step[array[2]].Lmin;
					dest.Num[array[1]].Step[array[2]].Lmax = source.Num[array[1]].Step[array[2]].Lmax;
					dest.Num[array[1]].Step[array[2]].LGP = source.Num[array[1]].Step[array[2]].LGP;
					dest.Num[array[1]].Step[array[2]].LGmin = source.Num[array[1]].Step[array[2]].LGmin;
					dest.Num[array[1]].Step[array[2]].LGmax = source.Num[array[1]].Step[array[2]].LGmax;
					dest.Num[array[1]].Step[array[2]].AnaP = source.Num[array[1]].Step[array[2]].AnaP;
					dest.Num[array[1]].Step[array[2]].AnaMin = source.Num[array[1]].Step[array[2]].AnaMin;
					dest.Num[array[1]].Step[array[2]].AnaMax = source.Num[array[1]].Step[array[2]].AnaMax;
					dest.Num[array[1]].Step[array[2]].DigP = source.Num[array[1]].Step[array[2]].DigP;
					dest.Num[array[1]].Step[array[2]].DigMin = source.Num[array[1]].Step[array[2]].DigMin;
					dest.Num[array[1]].Step[array[2]].DigMax = source.Num[array[1]].Step[array[2]].DigMax;
					dest.Num[array[1]].Step[array[2]].JumpTo = source.Num[array[1]].Step[array[2]].JumpTo;
					dest.Num[array[1]].Step[array[2]].ModDigOut = source.Num[array[1]].Step[array[2]].ModDigOut;
					dest.Num[array[1]].Step[array[2]].PressureSpindle = source.Num[array[1]].Step[array[2]].PressureSpindle;
					dest.Num[array[1]].Step[array[2]].CountPassMax = source.Num[array[1]].Step[array[2]].CountPassMax;
					dest.Num[array[1]].Step[array[2]].UserRights = source.Num[array[1]].Step[array[2]].UserRights;
					array[2]++;
				}

				dest.Num[array[1]].UseLocalJawSettings = source.Num[array[1]].UseLocalJawSettings;
				dest.Num[array[1]].JawLocalWrittenOnce = source.Num[array[1]].JawLocalWrittenOnce;
				dest.Num[array[1]].JawOpenDistance = source.Num[array[1]].JawOpenDistance;
				dest.Num[array[1]].JawOpenDepthGradMax = source.Num[array[1]].JawOpenDepthGradMax;
				dest.Num[array[1]].JawOpenDepthGradMin = source.Num[array[1]].JawOpenDepthGradMin;
				array[3] = 0;
				while (array[3] < 4)
				{
					array[4] = 0;
					while (array[4] < 7)
					{
						dest.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].StepIndex = source.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].StepIndex;
						dest.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].TypeOfData = source.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].TypeOfData;
						dest.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].Value = source.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].Value;
						dest.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MinValue = source.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MinValue;
						dest.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MaxValue = source.Num[array[1]].FourProg.FourthStep[array[3]].FourStepAtoms[array[4]].MaxValue;
						array[4]++;
					}

					array[3]++;
				}

				array[1]++;
			}
		}

		public void makeCopyCurveDef(ref CurveDef_Struct dest, CurveDef_Struct source)
		{
			dest.Points = source.Points;
			dest.SampleTime = source.SampleTime;
			dest.SpeedSetScale = source.SpeedSetScale;
			dest.SpeedActScale = source.SpeedActScale;
			dest.UnitTorque = source.UnitTorque;
		}

		public void makeCopyCurveData(ref CurveData_Struct dest, CurveData_Struct source)
		{
			int[] array = new int[50]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			while (array[1] < 20000)
			{
				dest.Point[array[1]].Nset = source.Point[array[1]].Nset;
				dest.Point[array[1]].Nact = source.Point[array[1]].Nact;
				dest.Point[array[1]].Torque = source.Point[array[1]].Torque;
				dest.Point[array[1]].FiltTorque = source.Point[array[1]].FiltTorque;
				dest.Point[array[1]].Angle = source.Point[array[1]].Angle;
				dest.Point[array[1]].DDepth = source.Point[array[1]].DDepth;
				dest.Point[array[1]].ADepth = source.Point[array[1]].ADepth;
				dest.Point[array[1]].ADepthGrad = source.Point[array[1]].ADepthGrad;
				dest.Point[array[1]].AExt = source.Point[array[1]].AExt;
				dest.Point[array[1]].Gradient = source.Point[array[1]].Gradient;
				dest.Point[array[1]].CurrentStep = source.Point[array[1]].CurrentStep;
				array[1]++;
			}
		}

		public void makeCopyCycleCount(ref CycleCount_Struct dest, CycleCount_Struct source)
		{
			int[] array = new int[50];
			dest.ID1 = source.ID1;
			dest.Machine = source.Machine;
			dest.Customer = source.Customer;
			array[1] = 0;
			while (array[1] < 5)
			{
				dest.Count[array[1]] = source.Count[array[1]];
				array[1]++;
			}

			dest.MachineNIO = source.MachineNIO;
			dest.CustomerNIO = source.CustomerNIO;
			dest.CountReserve = source.CountReserve;
			dest.ID2 = source.ID2;
		}

		public void makeCopyStatSample(ref StatSample_Struct dest, StatSample_Struct source)
		{
			int[] array = new int[50];
			dest.ID1 = source.ID1;
			dest.Info.Length = source.Info.Length;
			dest.Info.Position = source.Info.Position;
			array[1] = 0;
			while (array[1] < 2000)
			{
				dest.Data[array[1]].ProgNum = source.Data[array[1]].ProgNum;
				array[2] = 0;
				while (array[2] < 32)
				{
					dest.Data[array[1]].ProgName[array[2]] = source.Data[array[1]].ProgName[array[2]];
					array[2]++;
				}

				dest.Data[array[1]].IONIO = source.Data[array[1]].IONIO;
				dest.Data[array[1]].LastStep = source.Data[array[1]].LastStep;
				dest.Data[array[1]].Time.Year = source.Data[array[1]].Time.Year;
				dest.Data[array[1]].Time.Month = source.Data[array[1]].Time.Month;
				dest.Data[array[1]].Time.Day = source.Data[array[1]].Time.Day;
				dest.Data[array[1]].Time.Hour = source.Data[array[1]].Time.Hour;
				dest.Data[array[1]].Time.Minute = source.Data[array[1]].Time.Minute;
				dest.Data[array[1]].Time.Second = source.Data[array[1]].Time.Second;
				dest.Data[array[1]].Cycle = source.Data[array[1]].Cycle;
				dest.Data[array[1]].ScrewDuration = source.Data[array[1]].ScrewDuration;
				dest.Data[array[1]].Valid = source.Data[array[1]].Valid;
				dest.Data[array[1]].ResultStep1 = source.Data[array[1]].ResultStep1;
				dest.Data[array[1]].ResultParam1 = source.Data[array[1]].ResultParam1;
				dest.Data[array[1]].Res1 = source.Data[array[1]].Res1;
				dest.Data[array[1]].ResultStep2 = source.Data[array[1]].ResultStep2;
				dest.Data[array[1]].ResultParam2 = source.Data[array[1]].ResultParam2;
				dest.Data[array[1]].Res2 = source.Data[array[1]].Res2;
				dest.Data[array[1]].ResultStep3 = source.Data[array[1]].ResultStep3;
				dest.Data[array[1]].ResultParam3 = source.Data[array[1]].ResultParam3;
				dest.Data[array[1]].Res3 = source.Data[array[1]].Res3;
				array[2] = 0;
				while (array[2] < 32)
				{
					dest.Data[array[1]].ScrewID[array[2]] = source.Data[array[1]].ScrewID[array[2]];
					array[2]++;
				}

				dest.Data[array[1]].res = source.Data[array[1]].res;
				array[1]++;
			}

			dest.ID2 = source.ID2;
		}

		public void makeCopyStatControl(ref StatControl_Struct dest, StatControl_Struct source)
		{
			dest.Command = source.Command;
		}

		public void makeCopyTestDataRawIn(ref TestDataRawIn_Struct dest, TestDataRawIn_Struct source)
		{
			dest.ISRCount = source.ISRCount;
			dest.DI = source.DI;
			dest.DO = source.DO;
			dest.HexSwitch = source.HexSwitch;
			dest.Ui0 = source.Ui0;
			dest.Ui1 = source.Ui1;
			dest.Ui2 = source.Ui2;
			dest.Ui3 = source.Ui3;
			dest.UErr = source.UErr;
			dest.Enc0 = source.Enc0;
			dest.Enc1 = source.Enc1;
			dest.Enc3 = source.Enc3;
			dest.EncErr = source.EncErr;
			dest.GRADCount = source.GRADCount;
			dest.Nact = source.Nact;
			dest.Torque1 = source.Torque1;
			dest.Angle1 = source.Angle1;
			dest.Torque2 = source.Torque2;
			dest.Angle2 = source.Angle2;
			dest.ADepth = source.ADepth;
			dest.ExtAna = source.ExtAna;
			dest.BatteryOk = source.BatteryOk;
		}

		public void makeCopyTestDataRawOut(ref TestDataRawOut_Struct dest, TestDataRawOut_Struct source)
		{
			dest.Command = source.Command;
			dest.DO16 = source.DO16;
			dest.DONr = source.DONr;
			dest.DOState = source.DOState;
			dest.Uo0 = source.Uo0;
			dest.Uo1 = source.Uo1;
			dest.Uo2 = source.Uo2;
			dest.Uo3 = source.Uo3;
			dest.ResetEnc0 = source.ResetEnc0;
			dest.ResetEnc1 = source.ResetEnc1;
			dest.ResetEnc2 = source.ResetEnc2;
			dest.ResetEncErr = source.ResetEncErr;
			dest.ResetAngle = source.ResetAngle;
			dest.STSpeed = source.STSpeed;
			dest.STEnable = source.STEnable;
		}

		public void makeCopyPLCCommData(ref PLCCommData_Struct dest, PLCCommData_Struct source)
		{
			int[] array = new int[50];
			dest.Input.Automatic = source.Input.Automatic;
			dest.Input.Start = source.Input.Start;
			dest.Input.Quit = source.Input.Quit;
			dest.Input.Sync1 = source.Input.Sync1;
			dest.Input.Sync2 = source.Input.Sync2;
			dest.Input.KalDisable = source.Input.KalDisable;
			dest.Input.TeachAnalogDepth = source.Input.TeachAnalogDepth;
			dest.Input.ProgNum = source.Input.ProgNum;
			array[2] = 0;
			while (array[2] < 32)
			{
				dest.Input.ScrewID[array[2]] = source.Input.ScrewID[array[2]];
				array[2]++;
			}

			dest.Input.LivingSignRequest = source.Input.LivingSignRequest;
			dest.Input.Reserve1 = source.Input.Reserve1;
			dest.Input.Reserve2 = source.Input.Reserve2;
			dest.Input.Reserve3 = source.Input.Reserve3;
			dest.Input.AnalogsignalCurve = source.Input.AnalogsignalCurve;
			dest.Input.LivingSignEnabled = source.Input.LivingSignEnabled;
			dest.Input.ReserveSignals = source.Input.ReserveSignals;
			array[2] = 0;
			while (array[2] < 10)
			{
				dest.Input.ReserveStr[array[2]] = source.Input.ReserveStr[array[2]];
				array[2]++;
			}

			dest.Input.UsingProgName = source.Input.UsingProgName;
			dest.Input.Reserve5 = source.Input.Reserve5;
			array[2] = 0;
			while (array[2] < 32)
			{
				dest.Input.ProgName[array[2]] = source.Input.ProgName[array[2]];
				array[2]++;
			}

			array[2] = 0;
			while (array[2] < 20)
			{
				dest.Input.ExtendedResult[array[2]] = source.Input.ExtendedResult[array[2]];
				array[2]++;
			}

			dest.Input.PlcError = source.Input.PlcError;
			dest.Input.CounterNIO = source.Input.CounterNIO;
			dest.Input.CounterIOTotal = source.Input.CounterIOTotal;
			dest.Input.CounterNIOTotal = source.Input.CounterNIOTotal;
			dest.Input.PressureSpindle = source.Input.PressureSpindle;
			dest.Input.PressureHolder = source.Input.PressureHolder;
			dest.Input.Res1 = source.Input.Res1;
			dest.Output.SystemOK = source.Output.SystemOK;
			dest.Output.ReadyToStart = source.Output.ReadyToStart;
			dest.Output.ProcessRunning = source.Output.ProcessRunning;
			dest.Output.IO = source.Output.IO;
			dest.Output.NIO = source.Output.NIO;
			dest.Output.Sync1 = source.Output.Sync1;
			dest.Output.Sync2 = source.Output.Sync2;
			dest.Output.PowerEnabled = source.Output.PowerEnabled;
			dest.Output.TM1 = source.Output.TM1;
			dest.Output.TM2 = source.Output.TM2;
			dest.Output.ExtDigIn = source.Output.ExtDigIn;
			dest.Output.StorageSignals = source.Output.StorageSignals;
			dest.Output.AnaDepthMM = source.Output.AnaDepthMM;
			dest.Output.AnaDepthVolt = source.Output.AnaDepthVolt;
			dest.Output.ExtAna = source.Output.ExtAna;
			dest.Output.LivingSignResponse = source.Output.LivingSignResponse;
			dest.Output.LivingMonitor = source.Output.LivingMonitor;
			dest.Output.UserLevel = source.Output.UserLevel;
			array[2] = 0;
			while (array[2] < 5)
			{
				dest.Output.UserName[array[2]] = source.Output.UserName[array[2]];
				array[2]++;
			}

			dest.Output.Reserve1 = source.Output.Reserve1;
			dest.Output.Reserve2 = source.Output.Reserve2;
			dest.Output.Reserve3 = source.Output.Reserve3;
			dest.Output.DriveUnitInvers = source.Output.DriveUnitInvers;
			dest.Output.LivingSignEnabled = source.Output.LivingSignEnabled;
			dest.Output.ReserveSignals = source.Output.ReserveSignals;
			array[2] = 0;
			while (array[2] < 10)
			{
				dest.Output.IpAddress[array[2]] = source.Output.IpAddress[array[2]];
				array[2]++;
			}

			dest.Output.MaintenanceCounterReached = source.Output.MaintenanceCounterReached;
			dest.Output.AdvancedCounterReached = source.Output.AdvancedCounterReached;
			dest.Output.PressureSpindle = source.Output.PressureSpindle;
			dest.Output.PressureHolder = source.Output.PressureHolder;
			dest.Output.PressureScaleSpindle = source.Output.PressureScaleSpindle;
			dest.Output.PressureScaleHolder = source.Output.PressureScaleHolder;
			dest.Output.Res1 = source.Output.Res1;
			dest.Result.UnitTorque = source.Result.UnitTorque;
			dest.Result.ProgNum = source.Result.ProgNum;
			dest.Result.IONIO = source.Result.IONIO;
			dest.Result.LastStep = source.Result.LastStep;
			dest.Result.Time.Year = source.Result.Time.Year;
			dest.Result.Time.Month = source.Result.Time.Month;
			dest.Result.Time.Day = source.Result.Time.Day;
			dest.Result.Time.Hour = source.Result.Time.Hour;
			dest.Result.Time.Minute = source.Result.Time.Minute;
			dest.Result.Time.Second = source.Result.Time.Second;
			dest.Result.Cycle = source.Result.Cycle;
			dest.Result.ScrewDuration = source.Result.ScrewDuration;
			dest.Result.ResultStep1 = source.Result.ResultStep1;
			dest.Result.ResultParam1 = source.Result.ResultParam1;
			dest.Result.ResultStep2 = source.Result.ResultStep2;
			dest.Result.ResultParam2 = source.Result.ResultParam2;
			dest.Result.ResultStep3 = source.Result.ResultStep3;
			dest.Result.ResultParam3 = source.Result.ResultParam3;
			dest.Result.Valid = source.Result.Valid;
			dest.Result.Res1 = source.Result.Res1;
			dest.Result.Res2 = source.Result.Res2;
			dest.Result.Res3 = source.Result.Res3;
			array[2] = 0;
			while (array[2] < 32)
			{
				dest.Result.ScrewID[array[2]] = source.Result.ScrewID[array[2]];
				array[2]++;
			}

			dest.Error.Num = source.Error.Num;
			dest.Error.Warning = source.Error.Warning;
			dest.DI_DO.DI0_8 = source.DI_DO.DI0_8;
			dest.DI_DO.DO2_8 = source.DI_DO.DO2_8;
			dest.ProgAccess.SpindleTorque = source.ProgAccess.SpindleTorque;
			dest.ProgAccess.DriveUnitRpm = source.ProgAccess.DriveUnitRpm;
			dest.ProgAccess.Signal = source.ProgAccess.Signal;
			dest.ProgAccess.Length0 = source.ProgAccess.Length0;
			dest.ProgAccess.Length1 = source.ProgAccess.Length1;
			dest.ProgAccess.Length2 = source.ProgAccess.Length2;
			dest.ProgAccess.Address0 = source.ProgAccess.Address0;
			dest.ProgAccess.Address1 = source.ProgAccess.Address1;
			dest.ProgAccess.Address2 = source.ProgAccess.Address2;
			array[2] = 0;
			while (array[2] < 4)
			{
				dest.ProgAccess.Data0[array[2]] = source.ProgAccess.Data0[array[2]];
				array[2]++;
			}

			array[2] = 0;
			while (array[2] < 4)
			{
				dest.ProgAccess.Data1[array[2]] = source.ProgAccess.Data1[array[2]];
				array[2]++;
			}

			array[2] = 0;
			while (array[2] < 4)
			{
				dest.ProgAccess.Data2[array[2]] = source.ProgAccess.Data2[array[2]];
				array[2]++;
			}

			dest.ExternalAnalogSignal.SetSignal = source.ExternalAnalogSignal.SetSignal;
			dest.ExternalAnalogSignal.Pressure1 = source.ExternalAnalogSignal.Pressure1;
			dest.ExternalAnalogSignal.Pressure2 = source.ExternalAnalogSignal.Pressure2;
		}

		public void makeCopyUserRelatedData(ref UserRelatedData_Struct dest, UserRelatedData_Struct source)
		{
			int[] array = new int[50];
			dest.UserLevel = source.UserLevel;
			array[1] = 0;
			while (array[1] < 5)
			{
				dest.UserName[array[1]] = source.UserName[array[1]];
				array[1]++;
			}

			dest.Reserve1 = source.Reserve1;
			dest.Reserve2 = source.Reserve2;
			dest.Reserve3 = source.Reserve3;
			dest.Reserve4 = source.Reserve4;
			dest.Reserve5 = source.Reserve5;
			dest.ReserveSignals = source.ReserveSignals;
			array[1] = 0;
			while (array[1] < 10)
			{
				dest.IpAddress[array[1]] = source.IpAddress[array[1]];
				array[1]++;
			}
		}

		public void makeCopyManualStartControl(ref ManualStartControl_Struct dest, ManualStartControl_Struct source)
		{
			dest.Command = source.Command;
			dest.ProgNum = source.ProgNum;
		}

		public void makeCopyLogBookWriteControl(ref LogBookWriteControl_Struct dest, LogBookWriteControl_Struct source)
		{
			dest.Command = source.Command;
		}

		public void makeCopyLogBookWriteData(ref LogBookWriteData_Struct dest, LogBookWriteData_Struct source)
		{
			int[] array = new int[50]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			while (array[1] < 2500)
			{
				dest.LogData[array[1]].Code = source.LogData[array[1]].Code;
				dest.LogData[array[1]].Type = source.LogData[array[1]].Type;
				dest.LogData[array[1]].ProgNum = source.LogData[array[1]].ProgNum;
				dest.LogData[array[1]].Step = source.LogData[array[1]].Step;
				dest.LogData[array[1]].Value1 = source.LogData[array[1]].Value1;
				dest.LogData[array[1]].Value2 = source.LogData[array[1]].Value2;
				array[2] = 0;
				while (array[2] < 5)
				{
					dest.LogData[array[1]].userName[array[2]] = source.LogData[array[1]].userName[array[2]];
					array[2]++;
				}

				dest.LogData[array[1]].UnitIndex = source.LogData[array[1]].UnitIndex;
				dest.LogData[array[1]].res = source.LogData[array[1]].res;
				array[1]++;
			}
		}

		public void makeCopyStorageSystem(ref StorageSystem_Struct dest, StorageSystem_Struct source)
		{
			dest.Signal = source.Signal;
		}

		public void makeCopyMaintenanceDataBlock(ref MaintenanceDataBlock_Struct dest,
			MaintenanceDataBlock_Struct source)
		{
			int[] array = new int[50];
			dest.BlockNum = source.BlockNum;
			dest.LastBlock = source.LastBlock;
			dest.NextBlock = source.NextBlock;
			array[1] = 0;
			while (array[1] < 32)
			{
				array[2] = 0;
				while (array[2] < 5)
				{
					dest.MaintenanceData[array[1]].userName[array[2]] =
						source.MaintenanceData[array[1]].userName[array[2]];
					array[2]++;
				}

				dest.MaintenanceData[array[1]].Time.Year = source.MaintenanceData[array[1]].Time.Year;
				dest.MaintenanceData[array[1]].Time.Month = source.MaintenanceData[array[1]].Time.Month;
				dest.MaintenanceData[array[1]].Time.Day = source.MaintenanceData[array[1]].Time.Day;
				dest.MaintenanceData[array[1]].Time.Hour = source.MaintenanceData[array[1]].Time.Hour;
				dest.MaintenanceData[array[1]].Time.Minute = source.MaintenanceData[array[1]].Time.Minute;
				dest.MaintenanceData[array[1]].Time.Second = source.MaintenanceData[array[1]].Time.Second;
				dest.MaintenanceData[array[1]].ScheduledTime.Year = source.MaintenanceData[array[1]].ScheduledTime.Year;
				dest.MaintenanceData[array[1]].ScheduledTime.Month = source.MaintenanceData[array[1]].ScheduledTime.Month;
				dest.MaintenanceData[array[1]].ScheduledTime.Day = source.MaintenanceData[array[1]].ScheduledTime.Day;
				dest.MaintenanceData[array[1]].ScheduledTime.Hour = source.MaintenanceData[array[1]].ScheduledTime.Hour;
				dest.MaintenanceData[array[1]].ScheduledTime.Minute = source.MaintenanceData[array[1]].ScheduledTime.Minute;
				dest.MaintenanceData[array[1]].ScheduledTime.Second = source.MaintenanceData[array[1]].ScheduledTime.Second;
				array[2] = 0;
				while (array[2] < 100)
				{
					dest.MaintenanceData[array[1]].MaintenanceText[array[2]] = source.MaintenanceData[array[1]].MaintenanceText[array[2]];
					array[2]++;
				}

				dest.MaintenanceData[array[1]].Reminder = source.MaintenanceData[array[1]].Reminder;
				dest.MaintenanceData[array[1]].ReserveByte1 = source.MaintenanceData[array[1]].ReserveByte1;
				dest.MaintenanceData[array[1]].ReserveByte2 = source.MaintenanceData[array[1]].ReserveByte2;
				dest.MaintenanceData[array[1]].Index = source.MaintenanceData[array[1]].Index;
				dest.MaintenanceData[array[1]].BlockNum = source.MaintenanceData[array[1]].BlockNum;
				dest.MaintenanceData[array[1]].NewEntry = source.MaintenanceData[array[1]].NewEntry;
				dest.MaintenanceData[array[1]].Cycle = source.MaintenanceData[array[1]].Cycle;
				dest.MaintenanceData[array[1]].NextCycle = source.MaintenanceData[array[1]].NextCycle;
				array[1]++;
			}
		}

		public void makeCopyComponentDataBlock(ref ComponentDataBlock_Struct dest, ComponentDataBlock_Struct source)
		{
			int[] array = new int[50];
			dest.BlockNum = source.BlockNum;
			dest.LastBlock = source.LastBlock;
			dest.NextBlock = source.NextBlock;
			array[1] = 0;
			while (array[1] < 32)
			{
				array[2] = 0;
				while (array[2] < 5)
				{
					dest.ComponentData[array[1]].userName[array[2]] = source.ComponentData[array[1]].userName[array[2]];
					array[2]++;
				}

				dest.ComponentData[array[1]].Time.Year = source.ComponentData[array[1]].Time.Year;
				dest.ComponentData[array[1]].Time.Month = source.ComponentData[array[1]].Time.Month;
				dest.ComponentData[array[1]].Time.Day = source.ComponentData[array[1]].Time.Day;
				dest.ComponentData[array[1]].Time.Hour = source.ComponentData[array[1]].Time.Hour;
				dest.ComponentData[array[1]].Time.Minute = source.ComponentData[array[1]].Time.Minute;
				dest.ComponentData[array[1]].Time.Second = source.ComponentData[array[1]].Time.Second;
				array[2] = 0;
				while (array[2] < 100)
				{
					dest.ComponentData[array[1]].ComponentOrPartText[array[2]] = source.ComponentData[array[1]].ComponentOrPartText[array[2]];
					array[2]++;
				}

				array[2] = 0;
				while (array[2] < 100)
				{
					dest.ComponentData[array[1]].ReasonText[array[2]] = source.ComponentData[array[1]].ReasonText[array[2]];
					array[2]++;
				}

				array[2] = 0;
				while (array[2] < 31)
				{
					dest.ComponentData[array[1]].SerialNumber[array[2]] = source.ComponentData[array[1]].SerialNumber[array[2]];
					array[2]++;
				}

				dest.ComponentData[array[1]].NewEntry = source.ComponentData[array[1]].NewEntry;
				dest.ComponentData[array[1]].PieceCount = source.ComponentData[array[1]].PieceCount;
				dest.ComponentData[array[1]].Cycle = source.ComponentData[array[1]].Cycle;
				array[1]++;
			}
		}

		public void makeCopyDownloadRequest(ref DownloadRequest_Struct dest, DownloadRequest_Struct source)
		{
			int[] array = new int[50];
			dest.Request = source.Request;
			array[1] = 0;
			while (array[1] < 32)
			{
				dest.Info[array[1]] = source.Info[array[1]];
				array[1]++;
			}
		}

		public void makeCopyDownloadConfirmation(ref DownloadConfirmation_Struct dest,
			DownloadConfirmation_Struct source)
		{
			int[] array = new int[50];
			dest.Result = source.Result;
			array[1] = 0;
			while (array[1] < 32)
			{
				dest.Info[array[1]] = source.Info[array[1]];
				array[1]++;
			}
		}

		public void makeCopyPlcLogBookSys(ref PlcLogBookSys_Struct dest, PlcLogBookSys_Struct source)
		{
			int[] array = new int[50];
			dest.ID1 = source.ID1;
			dest.Position = source.Position;
			dest.Length = source.Length;
			array[1] = 0;
			while (array[1] < 4000)
			{
				dest.plcLogMessBuffer[array[1]].Time.Year = source.plcLogMessBuffer[array[1]].Time.Year;
				dest.plcLogMessBuffer[array[1]].Time.Month = source.plcLogMessBuffer[array[1]].Time.Month;
				dest.plcLogMessBuffer[array[1]].Time.Day = source.plcLogMessBuffer[array[1]].Time.Day;
				dest.plcLogMessBuffer[array[1]].Time.Hour = source.plcLogMessBuffer[array[1]].Time.Hour;
				dest.plcLogMessBuffer[array[1]].Time.Minute = source.plcLogMessBuffer[array[1]].Time.Minute;
				dest.plcLogMessBuffer[array[1]].Time.Second = source.plcLogMessBuffer[array[1]].Time.Second;
				dest.plcLogMessBuffer[array[1]].Code = source.plcLogMessBuffer[array[1]].Code;
				dest.plcLogMessBuffer[array[1]].Type = source.plcLogMessBuffer[array[1]].Type;
				dest.plcLogMessBuffer[array[1]].cycNum = source.plcLogMessBuffer[array[1]].cycNum;
				dest.plcLogMessBuffer[array[1]].UnitIndex = source.plcLogMessBuffer[array[1]].UnitIndex;
				dest.plcLogMessBuffer[array[1]].ResByte = source.plcLogMessBuffer[array[1]].ResByte;
				dest.plcLogMessBuffer[array[1]].ResUint1 = source.plcLogMessBuffer[array[1]].ResUint1;
				array[1]++;
			}

			dest.ID2 = source.ID2;
		}
        */
    }
}