namespace TestApp.Services;

public partial class VarComm
{
    private struct VARCOMMCommand
    {
        public uint ID1;

        public sbyte Direction;

        public ushort Block;

        public byte[] Event;

        public uint ID2;
    }
}