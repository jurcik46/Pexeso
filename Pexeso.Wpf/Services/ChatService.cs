using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Pexeso.Library;
using Pexeso.Wpf.ServicePexeso;
using Pexeso.Wpf.ViewModels;
using IChatService = Pexeso.Library.ServiceInterfaces.IChatService;
using Message = Pexeso.Library.Models.Message;
using User = Pexeso.Library.Models.User;

namespace Pexeso.Wpf.Services
{
    public class ChatService : Pexeso.Wpf.Interfaces.IChatService
    {

        private IChatService Server { get; set; }

        public User UserInfo { get; set; }


        public ChatService()
        {
            //= DuplexChannelFactory<IChatService>(new InstanceContext(new MainViewModel()), "NetTcpBinding_IChatService");

            //StartConnection();
        }

        public void StartConnection()
        {
            var duplexChannelFactory = new DuplexChannelFactory<IChatService>(new InstanceContext(ViewModelLocator.MainViewModel), "TcpPexesoChatService");
            Server = duplexChannelFactory.CreateChannel();
            UserInfo = Server.ClientConnection(UserInfo.UserName);
        }


        public void SendMessage(string text)
        {
            Server.SendMessage(new Message(text, UserInfo));
        }

        public List<Message> GetMessageFromServer()
        {
            return Server.GetMessages(UserInfo).ToList();
        }


        public void CloseConnection()
        {
            try
            {
                // Uzatvorime spojenie
                ((IClientChannel)Server).Close();
            }
            catch
            {
                try
                {
                    // V pripade chyby nastavime stav uzatvorenia
                    ((IClientChannel)Server).Abort();
                }
                catch
                {
                }
            }
        }



    }
}
