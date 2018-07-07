using System;

namespace TestApp.Services
{
	public class VarServerEventArgs : EventArgs
	{
		private ushort Block;

		public ushort GetBlockNo => Block;

		public VarServerEventArgs(ushort bl)
		{
			Block = bl;
		}
	}
}