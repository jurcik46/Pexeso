using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pexeso.Library;
using Pexeso.Library.ClientCallbacks;
using Pexeso.Library.Models;

namespace Pexeso.Wpf
{
    public class Event : IClientChatCallback
    {

        public Event()
        {

        }
        public void MessageReceived(Message message)
        {
            Console.WriteLine(message.ToString());
        }
    }
}
