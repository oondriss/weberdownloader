using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TestApp.DTO;

namespace TestApp.Extensions
{
	public static class TempDataExtension
    {
	    private const string AlertTag = "Alerts";

		public static void AddMessage(this ITempDataDictionary tempdata, string message, MessageType type = MessageType.Info)
		{
			var messageList = new List<MessageDefinion>
			{
				new MessageDefinion
				{
					Message = message,
					MessageType = type
				} 
			};
            
			var currentMessages = tempdata[AlertTag]?.ToString().Deserialize<List<MessageDefinion>>();
			if (currentMessages != null)
			{
				messageList.AddRange(currentMessages);
			}

			tempdata[AlertTag] = messageList.SerializeObject();
		}

		public static List<MessageDefinion> GetAndRemoveMessages(this ITempDataDictionary tempdata)
		{
			var mess = tempdata[AlertTag];

			var result = mess?.ToString().Deserialize<List<MessageDefinion>>();

			tempdata[AlertTag] = null;
			return result;
		}
    }
}
