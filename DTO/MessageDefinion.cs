using System;
using System.Xml.Serialization;

namespace TestApp.Extensions
{
	[Serializable]
    public class MessageDefinion
	{
		[XmlElement] public MessageType MessageType;// { private get; set; }

		[XmlElement] public string Message;// { get; set; }

		public string Type()
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
		
		public string TypeStr()
		{
			return Enum.GetName(MessageType.GetType(), MessageType);
		}
		
    }
}
