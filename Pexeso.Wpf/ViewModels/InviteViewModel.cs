using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using Pexeso.Wpf.Interfaces;
using Pexeso.Wpf.Model;

namespace Pexeso.Wpf.ViewModels
{
    public class InviteViewModel : ViewModelBase
    {
        public bool IsChecked3x2 { get; set; }
        public bool IsChecked4x3 { get; set; }
        public bool IsChecked4x4 { get; set; }
        public bool IsChecked5x4 { get; set; }
        public bool IsChecked6x5 { get; set; }
        public bool IsChecked6x6 { get; set; }
        public bool IsChecked8x7 { get; set; }
        public bool IsChecked8x8 { get; set; }

        public RelayCommand<IClosable> InviteCommand { get; set; }
        public RelayCommand<IClosable> RandomInviteCommand { get; set; }

        public ObservableCollection<Player> PlayerList { get; set; }
        public Player SelectedPlayer { get; set; }

        private IPexesoService PexesoService { get; set; }

        public InviteViewModel(IPexesoService pexesoService)
        {

            PlayerList = new ObservableCollection<Player>();
            PexesoService = pexesoService;
            CommandInit();
        }

        public InviteViewModel()
        {
        }

        private void CommandInit()
        {
            InviteCommand = new RelayCommand<IClosable>(Invite, CanInvite);
            RandomInviteCommand = new RelayCommand<IClosable>(RandomInvite, CanRandomInvite);
        }

        private bool CanInvite(IClosable win)
        {
            return SelectedPlayer != null;
        }

        private void Invite(IClosable win)
        {

        }

        private bool CanRandomInvite(IClosable win)
        {
            return true;
        }

        private void RandomInvite(IClosable win)
        {

        }
    }
}
