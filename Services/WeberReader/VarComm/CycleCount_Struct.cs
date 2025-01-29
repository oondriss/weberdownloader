using System;

namespace TestApp.Services;

public partial class VarComm
{
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
}