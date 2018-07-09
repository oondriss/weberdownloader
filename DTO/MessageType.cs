using System;

namespace TestApp.Extensions
{
	[Serializable]
	public enum MessageType
	{
		Success = 0,
		Info,
		Warning,
		Danger
	}
}