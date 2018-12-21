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

        private readonly Dictionary<int, List<IClientChatCallback>> _clientCallbacks = new Dictionary<int, List<IClientChatCallback>>();

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

            IClientChatCallback value;
            _clientCallbacks.TryGetValue(message.ToUser.Id, out value)


                _clientCallbacks[message.ToUser.Id];
            for (int i = 0; i < _clientCallbacks.Count; i++)
            {
                try
                {
                    // Volame metody klientov
                    _clientCallbacks[i].MessageReceived(message);
                }
                catch (Exception)
                {
                    // Ak nastane chyba, vyhodime klienta zo zoznamu
                    _clientCallbacks.RemoveAt(i);
                    i--;
                }
            }
        }

        public void AddClientCallback(IClientChatCallback clientCallback, User user)
        {
            _clientCallbacks.Add(clientCallback);
        }
    }
}
