using System.Collections.Generic;
using System.Windows.Threading;
using Pexeso.Library.Models;
using Pexeso.Wpf.Model;

namespace Pexeso.Wpf.Interfaces
{
    public interface IPexesoService
    {
        User UserInfo { get; set; }

        void StartPexeso(int row, int columns);
        List<Card> GameCards { get; set; }
        int PictureHeight { get; set; }
        int PictureWidth { get; set; }
        bool InProcess { get; set; }
        bool InGame { get; set; }
        int Score { get; set; }
        int TimeLeftForTurn();
        int ChoseSecondCardTimeLeft();
        double RoundTime();
        int GetCardIndex(Card card);
        bool CompareCards(Card choosedCard, Card uncoveredCard = null);
        DispatcherTimer RoundTimer { get; set; }
        DispatcherTimer TurnTimer { get; set; }
        DispatcherTimer ChoseSecondCardTimer { get; set; }
        //Stopwatch ChoseSecondCardStopwatch { get; set; }
        //Stopwatch RoundStopWatch { get; set; }
        //Stopwatch TurnStopWatch { get; set; }
    }
}
