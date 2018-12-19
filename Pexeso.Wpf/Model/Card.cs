using System;
using System.Windows.Controls;
using Pexeso.Wpf.enums;

namespace Pexeso.Wpf.Model
{
    [Serializable]
    public class Card
    {
        public Uri UriPath { get; set; }
        public Image Img { get; set; }
        public int Number { get; set; }
        public StatusCardEnum Status { get; set; }

        public int PositionY { get; set; }
        public int PositionX { get; set; }





        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
