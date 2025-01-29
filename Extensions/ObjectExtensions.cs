using System.Xml.Serialization;

namespace TestApp.Extensions;

public static class ObjectExtensions
{
    public static string SerializeObject<T>(this T toSerialize)
    {
        XmlSerializer xmlSerializer = new(toSerialize.GetType());

        using StringWriter textWriter = new();
        xmlSerializer.Serialize(textWriter, toSerialize);
        return textWriter.ToString();
    }

    public static T Deserialize<T>(this string toDeserialize)
    {
        XmlSerializer xmlSerializer = new(typeof(T));
        using StringReader textReader = new(toDeserialize);
        return (T)xmlSerializer.Deserialize(textReader);
    }
}
