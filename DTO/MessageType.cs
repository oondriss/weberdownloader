using System;

namespace TestApp.DTO
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