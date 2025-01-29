using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TestApp.Services;

public partial class VarComm
{
    #region Constants, fields, properties
    public delegate void VARServerEventHandler(object sender, VarServerEventArgs arg);

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
    private readonly bool[] EventIntern;
    private readonly ILogger log;
    public string TorqueUnitName = "";
    public float TorqueConvert = 1f;
    private byte SaveBlockStatus;
    private byte Block1Status;
    private byte Block2Status;
    private byte StatDeleteStatus;
    public event VARServerEventHandler VARSERVEREVENT_Error;
    public event VARServerEventHandler VARSERVEREVENT_Result;
    public event VARServerEventHandler VARSERVEREVENT_SaveControl;
    public event VARServerEventHandler VARSERVEREVENT_StatControl;
    public event VARServerEventHandler VARSERVEREVENT_StatusAccessControl;
    public event VARServerEventHandler VARSERVEREVENT_StatusAutomatic;
    #endregion

    #region Delegates
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

    #region Class methods
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

    private void StartupVarConnection(IPAddress ip)
    {
        string text = string.Empty;
        char[] separator = new char[1]
        {
            '/'
        };
        CloseConnection();
        _ = text.Split(separator);
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
            IPEndPoint remoteEP = new(ip, 23387);
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

    private bool ReceiveVarBlockIntern(ushort block)
    {
        int[] array = new int[50];
        int num = 0;
        bool result = false;
        if (ClientSocket != null && ClientSocket.Connected)
        {
            VARCOMMCommand vARCOMMCommand = default;
            vARCOMMCommand.ID1 = 117575940u;
            vARCOMMCommand.Direction = 0;
            vARCOMMCommand.Event = new byte[255];
            for (int i = 0; i < 255; i++)
            {
                vARCOMMCommand.Event[i] = 0;
            }

            vARCOMMCommand.Block = block;
            vARCOMMCommand.ID2 = 4177391355u;
            MemoryStream memoryStream = new(ReadBuffer);
            BinaryWriter binaryWriter = new(memoryStream);
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
                        BinaryReader binaryReader = new(memoryStream);
                        uint num3 = binaryReader.ReadUInt32();
                        binaryReader.Close();
                        memoryStream.Close();
                        if (num3 == 117575940)
                        {
                            _ = ProcessCommand(false);
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
                                        num6 != (uint)~(337776900 + block))
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
            VARCOMMCommand vARCOMMCommand = default;
            vARCOMMCommand.ID1 = 117575940u;
            vARCOMMCommand.Direction = 1;
            vARCOMMCommand.Event = new byte[255];
            for (int i = 0; i < 255; i++)
            {
                vARCOMMCommand.Event[i] = 0;
            }

            vARCOMMCommand.Block = block;
            vARCOMMCommand.ID2 = 4177391355u;
            MemoryStream memoryStream = new(SendBuffer);
            BinaryWriter binaryWriter = new(memoryStream);
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
                BinaryWriter binaryWriter2 = new(memoryStream);
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
                    binaryWriter2.Write((uint)~(337776900 + block));
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

    public void CloseConnection(bool releaseObjects = false)
    {
        if (EventThread != null && EventThread.IsAlive)
        {
#pragma warning disable SYSLIB0006 // Type or member is obsolete
            EventThread.Abort();
#pragma warning restore SYSLIB0006 // Type or member is obsolete
        }

        if (VCThread != null && VCThread.IsAlive)
        {
#pragma warning disable SYSLIB0006 // Type or member is obsolete
            VCThread.Abort();
#pragma warning restore SYSLIB0006 // Type or member is obsolete
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
        }

        if (releaseObjects)
        {
            ClientSocket = null;
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
    }

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

    private int ProcessCommand(bool normal)
    {
        VARCOMMCommand vARCOMMCommand = default;
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
                MemoryStream memoryStream = new(CommandBuffer);
                BinaryReader binaryReader = new(memoryStream);
                vARCOMMCommand.ID1 = normal ? binaryReader.ReadUInt32() : 117575940u;

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
            return ex.ErrorCode == 10035 ? -1 : -2;
        }
    }
    #endregion
}