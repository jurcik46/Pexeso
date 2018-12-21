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
    public class GameService : IGameService
    {
        Game Game = new Game();
        public User ClientConnection(string userName)
        {
            throw new NotImplementedException();
        }

        public bool InvitePlayer(Library.Models.Game game)
        {
            throw new NotImplementedException();
        }
    }
}
