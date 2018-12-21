using System.Collections.Generic;
using Pexeso.Library;
using Pexeso.Library.Models;

namespace Pexeso.Wpf.Interfaces
{
    public interface IChatService
    {
        User UserInfo { get; set; }

        void SendMessage(string text);
        void StartConnection();
        List<Message> GetMessageFromServer();
        void CloseConnection();


    }
}
