using System;

namespace TestApp.Services;

public partial class VarComm
{
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
}