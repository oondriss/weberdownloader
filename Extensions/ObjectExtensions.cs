using System.IO;
using System.Xml.Serialization;

namespace TestApp.Extensions
{
	public static class ObjectExtensions
	{
		public static string SerializeObject<T>(this T toSerialize)
		{
			var xmlSerializer = new XmlSerializer(toSerialize.GetType());

			using (var textWriter = new StringWriter())
			{
				xmlSerializer.Serialize(textWriter, toSerialize);
				return textWriter.ToString();
			}
		}

		public static T Deserialize<T>(this string toDeserialize)
		{
			var xmlSerializer = new XmlSerializer(typeof(T));
			using (var textReader = new StringReader(toDeserialize))
			{
				return (T)xmlSerializer.Deserialize(textReader);
			}
		}
	}
}
