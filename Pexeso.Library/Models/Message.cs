using System;
using System.Runtime.Serialization;

namespace Pexeso.Library.Models
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public User ToUser { get; set; }

        public Message() { }

        public Message(string text, User user)
        {
            Time = DateTime.Now;
            Text = text;
            User = user;
        }

        public override string ToString()
        {
            return $"{Time:yyyy-MM-dd HH:mm:ss} {User.UserName}: {Text}";
        }
    }
}
