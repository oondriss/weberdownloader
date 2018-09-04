using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
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
	}
}