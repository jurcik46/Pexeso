using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Pexeso.Server
{
    public class Program
    {
        static void Main(string[] args)
        {

            ServiceHost chatService = new ServiceHost(typeof(ChatService));
            ServiceHost gameService = new ServiceHost(typeof(GameService));
            ServiceHost entranceService = new ServiceHost(typeof(EntranceService));
            try
            {
                chatService.Open();
                gameService.Open();
                entranceService.Open();
                Console.WriteLine("The service is ready. Press <ENTER> to terminate service.");
                Console.ReadLine();
                chatService.Close();
                gameService.Close();
                entranceService.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                chatService.Abort();
                gameService.Abort();
                entranceService.Abort();
            }

        }
    }
}
