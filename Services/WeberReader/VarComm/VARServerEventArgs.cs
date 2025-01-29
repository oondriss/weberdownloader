using System;

namespace TestApp.Services.WeberReader.VarComm;

public class VarServerEventArgs : EventArgs
{
    public ushort GetBlockNo { get; }

    public VarServerEventArgs(ushort bl)
    {
        GetBlockNo = bl;
    }
}