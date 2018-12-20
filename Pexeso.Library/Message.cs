using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pexeso.Library
{
    [DataContract]
    public class Message
    {
        public DateTime Time { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public User User { get; set; }

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
