using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LovePlus.Pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
            ClearFields();
        }
        private void ClearFields()
        {
            FullNameTextBox.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { FullNameTextBox.Text = ""; }));
            LoginTextBox.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { LoginTextBox.Text = ""; }));
            PasswordTextBox.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { PasswordTextBox.Password = ""; }));
            PasswordTextBox.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { ContactTextBox.Text = ""; }));
            GenderMale.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { GenderMale.IsChecked = true; }));
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Task t = Task.Factory.StartNew(() =>
            {
                string FullName = "", Login = "", Password = "", Contact = "", Gender = "";
                FullNameTextBox.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { FullName = FullNameTextBox.Text; }));
                LoginTextBox.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { Login = LoginTextBox.Text; }));
                PasswordTextBox.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { Password = PasswordTextBox.Password; }));
                PasswordTextBox.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { Contact = ContactTextBox.Text; }));
                GenderMale.Dispatcher.Invoke(DispatcherPriority.Background, new Action(()=> { Gender = GenderMale.IsChecked == true ? "1" : "0"; }));
                if (MainWindow.logic.RegisterNewUser(FullName, Login, Password, Contact, Gender))
                {
                    ClearFields();
                    MessageBox.Show("Вы были успешно зарегистрированы!");
                }
                else
                    MessageBox.Show("Что-то пошло не так, проверьте правильность данных");
            });
        }
        private void ToLoginButton_Click(object sender, RoutedEventArgs e)
        { MainWindow.SetFrame("Pages/LoginPage.xaml"); }
    }
}