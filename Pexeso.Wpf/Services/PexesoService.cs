using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Pexeso.Wpf.enums;
using Pexeso.Wpf.Extension;
using Pexeso.Wpf.Interfaces;
using Pexeso.Wpf.Model;

namespace Pexeso.Wpf.Services
{
    public class PexesoService : IPexesoService
    {

        private const string ImgsPath = "resources\\imgs";

        public int PictureWidth { get; set; }
        public int PictureHeight { get; set; }
        public bool InGame { get; set; }
        public bool InProcess { get; set; }
        public int Score { get; set; }
        private List<Card> AllCardList { get; set; }
        public List<Card> GameCards { get; set; }

        private int TurnLongInS = 60;
        private int ChoseSecondCardLongInS = 10;
        public DispatcherTimer RoundTimer { get; set; }
        public DispatcherTimer TurnTimer { get; set; }
        public DispatcherTimer ChoseSecondCardTimer { get; set; }
        public Stopwatch ChoseSecondCardStopwatch { get; set; }
        public Stopwatch RoundStopWatch { get; set; }
        public Stopwatch TurnStopWatch { get; set; }

        public PexesoService()
        {
            Score = 0;
            RoundTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(800) };
            TurnTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(800) };
            ChoseSecondCardTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(800) };
            TurnStopWatch = new Stopwatch();
            RoundStopWatch = new Stopwatch();
            ChoseSecondCardStopwatch = new Stopwatch();

            PictureWidth = 75;
            PictureHeight = 75;
            InGame = false;
            AllCardList = new List<Card>();
            GameCards = new List<Card>();
            LoadCardFromFile();
        }

        #region Timers
        public int TimeLeftForTurn()
        {
            if (TurnStopWatch.Elapsed.TotalSeconds > TurnLongInS)
            {
                return -1;
            }

            return (int)(TurnStopWatch.Elapsed.TotalSeconds / TurnLongInS * 100);
        }


        public int ChoseSecondCardTimeLeft()
        {
            if (ChoseSecondCardStopwatch.Elapsed.TotalSeconds > ChoseSecondCardLongInS)
            {
                return -1;
            }

            return (int)(ChoseSecondCardStopwatch.Elapsed.TotalSeconds / ChoseSecondCardLongInS * 100);

        }

        public double RoundTime()
        {
            return RoundStopWatch.Elapsed.TotalSeconds;
        }
        #endregion

        public void StartPexeso(int row, int columns)
        {
            CreateCardsForGame(row, columns);
            Score = 0;

            RoundTimer.Start();
            ChoseSecondCardTimer.Start();
            TurnTimer.Start();

            RoundStopWatch.Start();
            TurnStopWatch.Start();
            //ChoseSecondCardStopwatch.Start();
        }

        public void CreateCardsForGame(int row, int columns)
        {
            GameCards.Clear();


            var rnd = new Random();
            for (int i = 0; i < ((row * columns) / 2); i++)
            {
                int r = rnd.Next(AllCardList.Count);
                if (GameCards.Any(x => x.Number == AllCardList[r].Number))
                {
                    i--;
                    continue;
                }

                // Clone Card object
                var pomC = (Card)AllCardList[r].Clone();
                // Load img from file for first card
                BitmapImage bmp = new BitmapImage(AllCardList[r].UriPath);
                Image img = new Image();
                img.Source = bmp;
                img.Width = PictureWidth;
                img.Height = PictureHeight;

                AllCardList[r].Img = img;

                GameCards.Add(AllCardList[r]);
                // Load img from file for second same card
                bmp = new BitmapImage(pomC.UriPath);
                img = new Image();
                img.Source = bmp;
                img.Width = PictureWidth;
                img.Height = PictureHeight;
                pomC.Img = img;
                GameCards.Add(pomC);
            }

            GameCards.Shuffle();
        }

        public bool CompareCards(Card choosedCard, Card uncoveredCard = null)
        {
            var result = false;
            //TurnTimer.Stop();
            TurnStopWatch.Reset();
            if (uncoveredCard != null)
            {
                // uncover second card and comper
                ChoseSecondCardStopwatch.Reset();
                //ChoseSecondCardTimer.Stop();
                if (choosedCard.Number == uncoveredCard.Number)
                {
                    Score += 10;
                    choosedCard.Status = StatusCardEnum.Founded;
                    uncoveredCard.Status = StatusCardEnum.Founded;
                }
                else
                {
                    choosedCard.Status = StatusCardEnum.Covered;
                    uncoveredCard.Status = StatusCardEnum.Covered;
                    result = true;
                }
            }
            else
            {
                //uncover first card
                choosedCard.Status = StatusCardEnum.Uncovered;
                ChoseSecondCardStopwatch.Reset();
                //ChoseSecondCardTimer.Start();
                ChoseSecondCardStopwatch.Start();
            }

            TurnStopWatch.Start();
            return result;
        }

        public int GetCardIndex(Card card)
        {
            return GameCards.FindIndex(x => x == card);
        }


        public void LoadCardFromFile()
        {
            AllCardList.Clear();
            int pomI = 0;
            foreach (var path in GetListOfFilePath())
            {
                Uri uri = new Uri("pack://siteoforigin:,,,/" + path);
                AllCardList.Add(new Card() { UriPath = uri, Number = pomI });
                pomI++;
            }
        }

        private List<string> GetListOfFilePath()
        {
            return Directory.GetFiles(ImgsPath, "*.png", SearchOption.AllDirectories).ToList();
        }

    }
}
