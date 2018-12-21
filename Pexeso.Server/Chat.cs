using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pexeso.Library;
using Pexeso.Library.ClientCallbacks;
using Pexeso.Library.Models;

namespace Pexeso.Server
{
    public class Chat
    {
        public List<User> ConnectedUser { get; set; } = new List<User>();
        public Dictionary<string, List<Message>> Messages { get; set; } = new Dictionary<string, List<Message>>();

        private readonly Dictionary<int, IClientChatCallback> _clientCallbacks = new Dictionary<int, IClientChatCallback>();

        public User AddNewUser(User newUser)
        {
            if (!ConnectedUser.Exists(u => u.UserName == newUser.UserName))
            {
                ConnectedUser.Add(newUser);
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
            Console.WriteLine(@" send : " + newMessage.ToString());

            var user = ConnectedUser.Find(u => u.UserName == newMessage.User.UserName);
            if (user != null)
            {
                Messages[user.UserName].Add(newMessage);
                NotifyClients(newMessage);
            }
        }

        public List<User> GetAllUsers()
        {
            return ConnectedUser;
        }


        public void RemoveUser(User user)
        {
            ConnectedUser.RemoveAll(u => u.UserName == user.UserName);
        }

        private void NotifyClients(Message message)
        {
            try
            {
                _clientCallbacks[message.ToUser.Id].MessageReceived(message);
            }
            catch (Exception)
            {
                _clientCallbacks.Remove(message.ToUser.Id);
            }
        }

        public void AddClientCallback(IClientChatCallback clientCallback, User user)
        {
            _clientCallbacks.Add(user.Id, clientCallback);
        }
    }
}
