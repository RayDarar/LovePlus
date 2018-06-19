using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LovePlus.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Task t = Task.Factory.StartNew(() => 
            {
                string Login = "", Password = "";
                LoginTextBox.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { Login = LoginTextBox.Text; }));
                PasswordTextBox.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { Password = PasswordTextBox.Password; }));
                if (MainWindow.logic.GetToken(Login, Password))
                {
                    MessageBox.Show("Успешная авторизация!");
                    MainWindow.MFrame.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => 
                    { MainWindow.MFrame.Source = new Uri("Pages/UserPage.xaml", UriKind.RelativeOrAbsolute); }));
                }
                else
                    MessageBox.Show("Что-то пошло не так, проверьте ваши данные");
            });
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        { MainWindow.SetFrame("Pages/RegistrationPage.xaml"); }
    }
}