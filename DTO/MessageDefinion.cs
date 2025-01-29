using System;
using System.Xml.Serialization;

namespace TestApp.DTO;

[Serializable]
public class MessageDefinion
{
    [XmlElement] public MessageType MessageType;// { private get; set; }

    [XmlElement] public string Message;// { get; set; }

    public string Type()
    {
        return MessageType switch
        {
            MessageType.Success => "alert-success",
            MessageType.Info => "alert-info",
            MessageType.Warning => "alert-warning",
            MessageType.Danger => "alert-danger",
            _ => "alert-info",
        };
    }

    public string TypeStr()
    {
        return Enum.GetName(MessageType.GetType(), MessageType);
    }

}
