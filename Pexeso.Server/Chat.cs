using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pexeso.Library;

namespace Pexeso.Server
{
    public class Chat
    {
        public List<User> ConnectedUser { get; set; } = new List<User>();
        public Dictionary<string, List<Message>> Messages { get; set; } = new Dictionary<string, List<Message>>();


        public User AddNewUser(User newUser)
        {
            if (!ConnectedUser.Exists(u => u.UserName == newUser.UserName))
            {
                ConnectedUser.Add(newUser);
                Messages.Add(newUser.UserName, new List<Message>()
                {
                    new Message("Joined to chat", newUser)
            });

                Console.WriteLine("New User Connected: " + newUser.UserName);
                return newUser;
            }
            else
            {
                return null;
            }
        }

        public void NewMessage(Message newMessage)
        {
            Console.WriteLine(newMessage.User + @" send : " + newMessage.ToString());

            var user = ConnectedUser.Find(u => u.UserName == newMessage.User.UserName);
            if (user != null)
            {
                Messages[user.UserName].Add(newMessage);
            }
        }

        public List<Message> GetNewMessages(User user)
        {
            List<Message> newMessage = Messages[user.UserName];
            Messages[user.UserName] = new List<Message>();

            if (newMessage.Count > 0)
            {
                return newMessage;
            }
            else
            {
                return null;
            }
        }


        public void RemoveUser(User user)
        {
            ConnectedUser.RemoveAll(u => u.UserName == user.UserName);
        }
    }
}
