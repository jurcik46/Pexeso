using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Pexeso.Library;
using Pexeso.Library.Models;
using Pexeso.Library.ServiceInterfaces;

namespace Pexeso.Server
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class EntranceService : IEntranceService
    {
        Entrance Entrance = new Entrance();

        public bool Registration(string userName, string password)
        {
            return Entrance.Registration(userName, password);
        }

        public User LogIn(string userName, string password)
        {
            return Entrance.LogIn(userName, password);
        }

        public User ClientConnection(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
