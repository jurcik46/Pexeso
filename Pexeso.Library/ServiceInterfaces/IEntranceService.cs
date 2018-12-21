using System.ServiceModel;
using Pexeso.Library.Models;

namespace Pexeso.Library.ServiceInterfaces
{
    [ServiceContract]
    public interface IEntranceService
    {
        [OperationContract]
        bool Registration(string userName, string password);

        [OperationContract]
        User ClientConnection(string userName);

        [OperationContract]
        User LogIn(string userName, string password);

    }
}
