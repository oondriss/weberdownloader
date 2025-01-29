using System;

namespace TestApp.Services;

public partial class VarComm
{
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
}