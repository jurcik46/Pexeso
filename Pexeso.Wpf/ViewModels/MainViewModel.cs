using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pexeso.Library;
using Pexeso.Wpf.enums;
using Pexeso.Wpf.Interfaces;
using Pexeso.Wpf.Model;
using Pexeso.Wpf.Views;
using IChatService = Pexeso.Wpf.Interfaces.IChatService;
using Message = Pexeso.Library.Message;

namespace Pexeso.Wpf.ViewModels
{
    public class MainViewModel : ViewModelBase, IClientCallback
    {
        private InviteViewModel InviteViewMdoel { get; set; }
        private InviteViewWindows InviteViewWindow { get; set; }

        private string _score;
        public int ClickCounter { get; set; }
        public string Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = $"Points: {value}";
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<string> ChatMessages { get; private set; }

        public ObservableCollection<Rectangle> Rectangles { get; private set; }
        public ObservableCollection<Image> Pictures { get; private set; }

        private int _inActiveTime;
        public int InActiveTime
        {
            get { return _inActiveTime; }
            set
            {
                _inActiveTime = value;
                RaisePropertyChanged();
            }
        }

        private int _choseSecondCardTimer;
        public int ChoseSecondCardTimer
        {
            get { return _choseSecondCardTimer; }
            set
            {
                _choseSecondCardTimer = value;
                RaisePropertyChanged();
            }
        }

        private string _roundTime;
        public string RoundTime
        {
            get { return _roundTime; }
            set
            {
                _roundTime = value;
                RaisePropertyChanged();
            }
        }
        public RelayCommand StartGameCommand { get; set; }
        public RelayCommand<EventArgs> MouseUpCommand { get; set; }


        private IPexesoService PexesoService { get; set; }

        private IChatService ChatService { get; set; }

        #region Chat

        public ObservableCollection<Message> Messages { get; set; }

        private string _sendingMessage;

        public string SendingMessage
        {
            get { return _sendingMessage; }
            set
            {
                _sendingMessage = value;
                SendMessageCommand.RaiseCanExecuteChanged();

            }
        }

        public RelayCommand SendMessageCommand { get; set; }

        #endregion

        public MainViewModel(IPexesoService pexesoService, IChatService chatService)
        {
            Pictures = new ObservableCollection<Image>();
            Rectangles = new ObservableCollection<Rectangle>();
            Messages = new ObservableCollection<Message>();
            PexesoService = pexesoService;
            ChatService = chatService;
            CommmandInit();

            PexesoService.ChoseSecondCardTimer.Tick += ChoseSecondCardTimerTick;
            PexesoService.TurnTimer.Tick += InActiveTimerTick;
            PexesoService.RoundTimer.Tick += RoundTImeTick;
            Score = PexesoService.Score.ToString();
        }

        private void LoadCardsToBorder()
        {
            Rectangles.Clear();
            Pictures.Clear();
            int widht = 8;
            int height = 8;
            PexesoService.StartPexeso(height, widht);
            int x = 2;
            int y = 2;
            int row = 0;
            foreach (var a in PexesoService.GameCards)
            {
                Canvas.SetLeft(a.Img, x);
                Canvas.SetTop(a.Img, y);
                // Rectangle 
                SolidColorBrush pomCo = new SolidColorBrush()
                {
                    Color = Colors.Black,
                    Opacity = 1
                };
                SolidColorBrush pomCoB = new SolidColorBrush()
                {
                    Color = Colors.BlueViolet
                };
                Rectangle pomR = new Rectangle()
                {
                    Width = PexesoService.PictureWidth,
                    Height = PexesoService.PictureHeight,
                    Fill = pomCo,
                    Stroke = pomCoB,

                };
                Canvas.SetLeft(pomR, x);
                Canvas.SetTop(pomR, y);
                Rectangles.Add(pomR);
                Pictures.Add(a.Img);

                a.PositionX = x;
                a.PositionY = y;
                a.Status = StatusCardEnum.Covered;
                x += PexesoService.PictureWidth + 5;
                if (Pictures.Count % widht == 0)
                {
                    row++;
                    x = 2;
                    y += PexesoService.PictureHeight + 5;
                }
                if (row == height)
                {
                    break;
                }
            }
        }

        public void ClickOnBoard(int indexOfObject)
        {
            if (!PexesoService.InGame)
                return;
            Card clickedCard = PexesoService.GameCards[indexOfObject];
            if (clickedCard.Status == StatusCardEnum.Founded || clickedCard.Status == StatusCardEnum.Uncovered)
                return;
            PexesoService.InProcess = true;
            var uncoveredCard = PexesoService.GameCards.Find(card => card.Status == StatusCardEnum.Uncovered);
            clickedCard.Status = StatusCardEnum.Uncovered;
            UpdateCardStatus(clickedCard, PexesoService.GetCardIndex(clickedCard));

            if (PexesoService.CompareCards(clickedCard, uncoveredCard))
            {
                Thread.Sleep(1000);
            }
            Score = PexesoService.Score.ToString();

            UpdateCardStatus(clickedCard, PexesoService.GetCardIndex(clickedCard));
            if (uncoveredCard != null)
            {
                UpdateCardStatus(uncoveredCard, PexesoService.GetCardIndex(uncoveredCard));
            }
            PexesoService.InProcess = false;
        }

        private void UpdateCardStatus(Card card, int indexForRecantgle)
        {
            SolidColorBrush cover = new SolidColorBrush()
            {
                Color = Colors.Black,
                Opacity = 1
            };
            SolidColorBrush uncover = new SolidColorBrush()
            {
                Color = Colors.Black,
                Opacity = 0
            };

            SolidColorBrush borderFounded = new SolidColorBrush()
            {
                Color = Colors.Chartreuse,
            };

            SolidColorBrush border = new SolidColorBrush()
            {
                Color = Colors.BlueViolet
            };
            Rectangle pomR = new Rectangle()
            {
                Width = PexesoService.PictureWidth,
                Height = PexesoService.PictureHeight,

            };
            Canvas.SetLeft(pomR, card.PositionX);
            Canvas.SetTop(pomR, card.PositionY);

            switch (card.Status)
            {
                case StatusCardEnum.Covered:
                    pomR.Fill = cover;
                    pomR.Stroke = border;
                    break;
                case StatusCardEnum.Founded:
                    pomR.Fill = uncover;
                    pomR.Stroke = borderFounded;
                    break;
                case StatusCardEnum.Uncovered:
                    pomR.Fill = uncover;
                    pomR.Stroke = border;
                    break;
            }
            Rectangles[indexForRecantgle] = pomR;
            ProcessUITasks();
        }

        public void ProcessUITasks()

        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(delegate (object parameter)
            {
                frame.Continue = false;
                return null;
            }), null);
            Dispatcher.PushFrame(frame);
        }


        private void ChoseSecondCardTimerTick(object sender, EventArgs e)
        {
            if (PexesoService.ChoseSecondCardTimeLeft() == -1)
            {
                // Failed for secnd card
                ChoseSecondCardTimer = 0;
                return;
            }

            ChoseSecondCardTimer = 100 - PexesoService.ChoseSecondCardTimeLeft();
        }

        private void InActiveTimerTick(object sender, EventArgs e)
        {
            if (PexesoService.TimeLeftForTurn() == -1)
            {
                // End Game

                InActiveTime = 0;
                return;
            }

            InActiveTime = 100 - PexesoService.TimeLeftForTurn();
        }

        private void RoundTImeTick(object sender, EventArgs e)
        {
            double pomInS = PexesoService.RoundTime();
            RoundTime = $"Time: {(int)pomInS / 60:D2}:{(int)pomInS % 60:D2}";
        }


        #region Command

        void CommmandInit()
        {
            StartGameCommand = new RelayCommand(StartGame, CanStartGame);
            MouseUpCommand = new RelayCommand<EventArgs>(MouseUp, CanMouseUp);
            SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);
        }

        public bool CanMouseUp(EventArgs args)
        {
            return !PexesoService.InProcess;
        }

        public void MouseUp(EventArgs args)
        {
            ClickOnBoard(Rectangles.IndexOf((Rectangle)((MouseButtonEventArgs)args).Device.Target));
        }


        private bool CanStartGame()
        {
            return true;
        }


        private void StartGame()
        {
            InviteViewMdoel = new InviteViewModel(PexesoService, ChatService);
            InviteViewWindow = new InviteViewWindows() { DataContext = InviteViewMdoel };
            InviteViewWindow.ShowDialog();

            ChatService.StartConnection();
            LoadCardsToBorder();
            PexesoService.InGame = true;
            ClickCounter = 0;
        }

        private bool CanSendMessage()
        {
            return SendingMessage != "";
        }

        private void SendMessage()
        {
            ChatService.SendMessage(SendingMessage);
        }
        #endregion


        private void ReloadCard()
        {
        }


        public void MessageReceived(Message message)
        {
            Messages.Add(message);
        }
    }
}
