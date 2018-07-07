using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

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

			if (tempdata[AlertTag] != null)
			{
				var currentMessages = JsonConvert.DeserializeObject<List<MessageDefinion>>(tempdata[AlertTag].ToString());
				if (currentMessages != null)
				{
					messageList.AddRange(currentMessages);
				}
			}

			tempdata[AlertTag] = JsonConvert.SerializeObject(messageList);
		}

		public static List<MessageDefinion> GetAndRemoveMessages(this ITempDataDictionary tempdata)
		{
			var mess = tempdata[AlertTag];

			var result = 
				mess != null 
				? JsonConvert.DeserializeObject<List<MessageDefinion>>(mess.ToString()) 
				: null;

			tempdata[AlertTag] = null;
			return result;
		}
    }
}
