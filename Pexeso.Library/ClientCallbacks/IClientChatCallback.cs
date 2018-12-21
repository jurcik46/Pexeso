using System.ServiceModel;
using Pexeso.Library.Models;

namespace Pexeso.Library.ClientCallbacks
{
    public interface IClientChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void MessageReceived(Message message);
    }
}
