using System;
using System.ServiceModel;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Pexeso.Library.ServiceInterfaces;
using Pexeso.Wpf.Interfaces;
using Pexeso.Wpf.Views;

namespace Pexeso.Wpf.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        public bool IsCheckedLogin { get; set; }
        public bool IsCheckedRegistration { get; set; }
        private bool Connection { get; set; }

        public string LoginNick { get; set; }
        public string LoginPassword { get; set; }

        public string FirstRegistrationPassword { get; set; }
        public string SecondRegistrationPassword { get; set; }

        public string RegistrationName { get; set; }

        public RelayCommand<IClosable> EnterCommand { get; set; }

        private IPexesoService PexesoService;
        private IEntranceService EntranceService;


        public LoginViewModel(IPexesoService pexesoService)
        {
            PexesoService = pexesoService;
            CommandInit();
            ConnectToEntranceService();
        }

        void ConnectToEntranceService()
        {
            try
            {
                var duplexChannelFactory = new ChannelFactory<IEntranceService>("TcpPexesoEntranceService");
                EntranceService = duplexChannelFactory.CreateChannel();
                Connection = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Nepodarilo sa pripojit na server" + e.Message);
                Connection = false;
            }

        }


        private void CommandInit()
        {
            EnterCommand = new RelayCommand<IClosable>(Enter, CanEnter);
        }

        private bool CanEnter(IClosable win)
        {
            return Connection;
        }

        private void Enter(IClosable win)
        {

            if (IsCheckedRegistration)
            {
                if (FirstRegistrationPassword != SecondRegistrationPassword)
                {
                    MessageBox.Show("Hesla sa musia zhodovať");
                    return;
                }

                if (EntranceService.Registration(RegistrationName, FirstRegistrationPassword))
                {
                    MessageBox.Show("Registracia bola úspešná mozte sa prihlasit");

                }
                else
                {
                    MessageBox.Show("Registracia sa nepodarila skuste pouzit ine meno");
                }
            }

            if (IsCheckedLogin)
            {
                var pomUser = EntranceService.LogIn(LoginNick, LoginPassword);
                if (pomUser == null)
                {
                    MessageBox.Show("Nepodarilo sa prihlasit do vasho uctu.");
                    return;
                }

                PexesoService.UserInfo = pomUser;
                MainWindow mainWin = new MainWindow();
                mainWin.Show();

                if (win != null)
                {
                    win.Close();
                }
            }

        }



    }
}
