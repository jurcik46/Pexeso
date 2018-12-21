using System.ServiceModel;
using Pexeso.Library.ClientCallbacks;
using Pexeso.Library.Models;

namespace Pexeso.Library.ServiceInterfaces
{
    [ServiceContract(CallbackContract = typeof(IClientChatCallback))]
    public interface IGameService
    {
        [OperationContract]
        User ClientConnection(string userName);

        [OperationContract]
        bool InvitePlayer(Game game);
    }
}
