using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Pexeso.Wpf.Interfaces;
using Pexeso.Wpf.ViewModels;

namespace Pexeso.Wpf.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window, IClosable
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.LoginViewModel;
        }

        private void FirstRegistrationPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as LoginViewModel;
            var passwordBox = sender as PasswordBox;
            if (viewModel == null || passwordBox == null)
            {
                return;
            }
            viewModel.FirstRegistrationPassword = passwordBox.Password;
            VizualizePasswordValidation(passwordBox);
        }

        private void SecondRegistratonPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as LoginViewModel;
            var passwordBox = sender as PasswordBox;
            if (viewModel == null || passwordBox == null)
            {
                return;
            }
            viewModel.SecondRegistrationPassword = passwordBox.Password;
            VizualizePasswordValidation(passwordBox);
        }

        private void VizualizePasswordValidation(PasswordBox passwordBox)
        {
            if (string.IsNullOrWhiteSpace(passwordBox.Password) || passwordBox.Password.Length < 5)
            {
                passwordBox.BorderThickness = new Thickness(3);
                passwordBox.ToolTip = "Heslo nemôže byť prázdne alebo obsahovať medzery a musí byť dlhšie ako 5 znakov!";
                passwordBox.Background = Brushes.Red;
                EnterButton.IsEnabled = false;
            }
            else
            {
                passwordBox.BorderBrush = Brushes.Black;
                passwordBox.BorderThickness = new Thickness(1);
                passwordBox.ToolTip = null;
                passwordBox.Background = Brushes.White;
                EnterButton.IsEnabled = true;
            }
        }

        private void LoginRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            EnterButton.IsEnabled = true;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as LoginViewModel;
            var passwordBox = sender as PasswordBox;
            if (viewModel == null || passwordBox == null)
            {
                return;
            }
            viewModel.LoginPassword = passwordBox.Password;
        }
    }
}
