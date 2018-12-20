using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Pexeso.Library
{
    [ServiceContract(SessionMode = SessionMode.Allowed, CallbackContract = typeof(IClientCallback))]
    public interface IChatService
    {
        [OperationContract]
        User ClientConnection(string userName);

        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        void RemoveUser(User user);

        [OperationContract]
        List<Message> GetMessages(User user);

        [OperationContract(IsOneWay = true)]
        void SendMessage(Message newMessage);

        [OperationContract(IsOneWay = true)]
        void RegisterMessageNotification();
    }
}
