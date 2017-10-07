using Newtonsoft.Json;
using System;

namespace StarterProject.Web.Api
{
    public enum MessageType
    {
        Connect,
        BeginTurn,
        Action,
        Ping
    }

    public interface IMessage
    {
        MessageType Type { get; }
        string Content { get; }
    }

    public class Message : IMessage
    {
        public MessageType Type { get; set; }
        public string Content { get; set; }

        public Message(MessageType type, string content = "")
        {
            Type = type;
            Content = content;
        }
    }

    public class ActionContent
    {
        public string ActionName { get; set; }
        public string Content { get; set; }
        public ActionContent()
        {

        }

        public ActionContent(string name, Point content)
        {
            ActionName = name;
            Content = JsonConvert.SerializeObject(content);
        }

        public ActionContent(string name, UpgradeType content)
        {
            ActionName = name;
            Content = content.ToString();
        }

        public ActionContent(string name, PurchasableItem content)
        {
            ActionName = name;
            Content = content.ToString();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}