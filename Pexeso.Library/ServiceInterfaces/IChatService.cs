using System.Collections.Generic;
using System.ServiceModel;
using Pexeso.Library.ClientCallbacks;
using Pexeso.Library.Models;

namespace Pexeso.Library.ServiceInterfaces
{
    [ServiceContract(CallbackContract = typeof(IClientChatCallback))]
    public interface IChatService
    {
        [OperationContract]
        User ClientConnection(User user);

        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        void RemoveUser(User user);



        [OperationContract(IsOneWay = true)]
        void SendMessage(Message newMessage);

    }
}
