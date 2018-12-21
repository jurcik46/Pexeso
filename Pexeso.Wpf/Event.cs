using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pexeso.Library;

namespace Pexeso.Wpf
{
    public class Event : IClientCallback
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
