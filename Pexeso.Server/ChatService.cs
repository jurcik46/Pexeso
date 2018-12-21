using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Pexeso.Library;

namespace Pexeso.Server
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {

        private Chat Chat = new Chat();


        private readonly List<IClientCallback> _clientCallbacks = new List<IClientCallback>();

        public User ClientConnection(string userName)
        {
            RegisterMessageNotification();
            return Chat.AddNewUser(new User() { UserName = userName });
        }

        public List<User> GetAllUsers()
        {
            return Chat.ConnectedUser;
        }

        public void RemoveUser(User user)
        {
            Chat.RemoveUser(user);
        }

        public List<Message> GetMessages(User user)
        {
            return Chat.GetNewMessages(user);
        }



        public void SendMessage(Message newMessage)
        {
            Chat.NewMessage(newMessage);
            NotifyClients(newMessage);
        }

        //public void SendMessage(string text, string nick)
        //{
        //    //var message = new Message(text, nick);
        //    //_messages.Add(message);


        //    //NotifyClients(message);
        //}

        private void NotifyClients(Message message)
        {
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

        public void RegisterMessageNotification()
        {
            var clientCallback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            _clientCallbacks.Add(clientCallback);
        }
    }
}

