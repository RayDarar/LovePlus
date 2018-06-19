using LovePlus.BAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LovePlus.Pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private bool flag = false;
        private DispatcherTimer timer = new DispatcherTimer();
        public UserPage()
        {
            InitializeComponent();
            WelcomeLabel.Content = $"Добро пожаловать, {Token.FullName}";
            timer.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { timer.Stop(); }));
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += UpdateCoupleLabel;
            timer.Start();
        }
        private void UpdateCoupleLabel(object sender, EventArgs e) { StatusLabel.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => 
        { StatusLabel.Content = $"Ваш статус: {MainWindow.logic.GetStatus()}"; })); }
        private void UpdateLabelContent(string Content) => SettingLabel.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { SettingLabel.Content = Content; }));
        private void LeaveAccount_Click(object sender, RoutedEventArgs e)
        {
            if (!flag)
            {
                if (MessageBox.Show("Выход", "Вы уверены?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    timer.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { timer.Stop(); }));
                    Task.Factory.StartNew(() => { MessageBox.Show("Всего доброго!"); });
                    MainWindow.logic.LoseToken();
                    MainWindow.SetFrame("Pages/LoginPage.xaml");
                }

            }
            else
                MessageBox.Show("Подождите пока не завершится запрос!");
        }
        private void FindLove_Click(object sender, RoutedEventArgs e)
        {
            if (!flag)
            {
                flag = true;
                Task.Factory.StartNew(() =>
                {
                    Thread t = new Thread(() =>
                    {
                        for (int i = 1; i <= 15; i++)
                        {
                            UpdateLabelContent($"Осталось {i} сек.");
                            Thread.Sleep(1000);
                        }
                        UpdateLabelContent("");
                    });
                    t.Start();
                    int num = MainWindow.logic.GetNumber();
                    if (num != 0)
                    {
                        MessageBox.Show("Ваш запрос был помещен в очередь");
                        MessageBox.Show($"Ваше любовное число: {num}, ждите обработку!");
                    }
                    else
                    {
                        t.Abort();
                        UpdateLabelContent("");
                        MessageBox.Show("У вас уже есть пара или вы уже оставили запрос!");
                    }
                    flag = false;
                });
            }
            else
                MessageBox.Show("Подождите пока не завершится запрос!");
        }
        private void LoseLove_Click(object sender, RoutedEventArgs e)
        {
            if (!flag)
            {
                Task.Factory.StartNew(() =>
                {
                    if (MessageBox.Show("Бросить пару?", "Вы уверены?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (MainWindow.logic.LoseLove())
                        {
                            StatusLabel.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                            { StatusLabel.Content = $"Ваш статус: {MainWindow.logic.GetStatus()}"; }));
                            MessageBox.Show("Вы бросили вашу пару!..");
                        }
                        else
                            MessageBox.Show("Вам некого терять");
                    }
                });
            }
            else
                MessageBox.Show("Подождите пока не завершится запрос!");
        }
        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (!flag)
            {
                Task.Factory.StartNew(() =>
                {
                    if (MessageBox.Show("Удаление аккаунта", "Вы уверены?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        timer.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { timer.Stop(); }));
                        MainWindow.logic.DeleteAccount(Token.id);
                        MessageBox.Show("Аккаунт был успешно удален!");
                        MainWindow.logic.LoseToken();
                        MainWindow.SetFrame("Pages/LoginPage.xaml");
                    }
                });
            }
            else
                MessageBox.Show("Подождите пока не завершится запрос!");
        }
    }
}