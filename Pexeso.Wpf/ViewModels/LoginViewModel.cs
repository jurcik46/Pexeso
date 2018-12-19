using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Pexeso.Wpf.Interfaces;
using Pexeso.Wpf.Views;

namespace Pexeso.Wpf.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        public bool IsCheckedLogin { get; set; }
        public bool IsCheckedRegistration { get; set; }

        public string LoginNick { get; set; }
        public string LoginPassword { get; set; }

        public string FirstRegistrationPassword { get; set; }
        public string SecondRegistrationPassword { get; set; }

        public RelayCommand<IClosable> EnterCommand { get; set; }

        private IPexesoService PexesoService;
        private IChatService ChatSerivce;


        public LoginViewModel(IPexesoService pexesoService, IChatService chatSerivce)
        {
            PexesoService = pexesoService;
            ChatSerivce = chatSerivce;
            CommandInit();
        }




        private void CommandInit()
        {
            EnterCommand = new RelayCommand<IClosable>(Enter, CanEnter);
        }

        private bool CanEnter(IClosable win)
        {
            return true;
        }



        private void Enter(IClosable win)
        {
            if (IsCheckedRegistration)
            {
                if (FirstRegistrationPassword != SecondRegistrationPassword)
                {
                    MessageBox.Show("Hesla sa musia zhodovať");
                }
                MessageBox.Show("Registracia bola úspešná");

            }

            if (IsCheckedLogin)
            {
                MessageBox.Show("Prihlasenie boo úspešné");
                ChatSerivce.Nick = LoginNick;
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
