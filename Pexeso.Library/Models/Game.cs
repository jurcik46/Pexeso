using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pexeso.Library.Models
{
    [DataContract]
    public class Game
    {
        [DataMember]
        public User Player { get; set; }

        [DataMember]
        public User EnemyPlayer { get; set; }

        [DataMember]
        public bool RandomInv { get; set; }
        [DataMember]
        public int BorderWidth { get; set; }

        [DataMember]
        public int BorderHeight { get; set; }


        public Game(User player, User enemyPlayer, bool randomInv, int borderHeight, int borderWidth)
        {
            Player = player;
            EnemyPlayer = enemyPlayer;
            RandomInv = randomInv;
            BorderHeight = borderHeight;
            BorderWidth = borderWidth;
        }

    }
}
