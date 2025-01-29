using System;

namespace TestApp.Services;

public partial class VarComm
{
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
}