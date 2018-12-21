using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Pexeso.Library;
using Pexeso.Library.ClientCallbacks;
using Pexeso.Library.Models;
using Pexeso.Library.ServiceInterfaces;

namespace Pexeso.Server
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {

        private Chat Chat = new Chat();

        public User ClientConnection(User user)
        {
            Chat.AddClientCallback(OperationContext.Current.GetCallbackChannel<IClientChatCallback>(), user);
            return Chat.AddNewUser(user);
        }

        public List<User> GetAllUsers()
        {
            return Chat.ConnectedUser;
        }

        public void RemoveUser(User user)
        {
            Chat.RemoveUser(user);
        }


        public void SendMessage(Message newMessage)
        {
            Chat.NewMessage(newMessage);
        }

    }
}

