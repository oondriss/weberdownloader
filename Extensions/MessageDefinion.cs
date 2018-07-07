using System;

namespace TestApp.Extensions
{
	[Serializable]
    public class MessageDefinion
    {
		public MessageType MessageType { private get; set; }
		public string Message { get; set; }
		public string Type
		{
			get
			{
				switch (MessageType)
				{
					case MessageType.Success:
						return "alert-success";
					case MessageType.Info:
						return "alert-info";
					case MessageType.Warning:
						return "alert-warning";
					case MessageType.Danger:
						return "alert-danger";
					default:
						return "alert-info";
				}
			}
		}
	}
}
