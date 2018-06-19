using LovePlus.DAL;
using LovePlus.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Media;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace Dashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer timer = new DispatcherTimer();
        public SoundPlayer music = new SoundPlayer($@"{Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.IndexOf(@"\bin"))}\Content\Rock-Marsh.wav");
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += ListenForQueue;
            timer.Start();
        }
        public void ListenForQueue(object sender, EventArgs e)
        {
            UpdateMainList();
            if (GetMessageAmount() >= 1)
            {
                music.Play();
                SetCouplesList();
            }
        }
        public void SetCouplesList()
        {
            Task.Factory.StartNew(() =>
            {
                List<string> arr = new List<string>();
                for (uint i = 0; i < GetMessageAmount(); i++)
                    arr.Add(Receive("DashboardQueue"));
                foreach (string item in arr)
                {
                    User temp = DataClass.GetUserList().FirstOrDefault(f => f.id == item);
                    User temp1 = DataClass.GetUserList().FirstOrDefault(f => f.id == temp.Status);
                    CouplesTreeView.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                    { CouplesTreeView.Items.Add(new TreeViewItem() { Header = $"{temp.FullName} - {temp1.FullName}" }); }));
                }
            });
        }
        public void UpdateMainList()
        {
            Task.Factory.StartNew(() =>
            {
                List<Couples> arr = new List<Couples>();
                List<string> tempArr = new List<string>();
                List<User> users = DataClass.GetUserList();
                foreach (User item in users)
                {
                    User temp1 = DataClass.GetUserList().FirstOrDefault(f => f.id == item.Status);
                    if (temp1 != null)
                    {
                        if (!tempArr.Contains(temp1.id) && !tempArr.Contains(item.id))
                        {
                            arr.Add(new Couples() { Id = $"{item.id}-{temp1.id}", FirstFullName = item.FullName, SecondFullName = temp1.FullName });
                            tempArr.Add(item.id);
                            tempArr.Add(temp1.id);
                        }
                    }
                    else
                        tempArr.Add(item.id);
                }

                CouplesList.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { CouplesList.ItemsSource = arr; }));
            });
        }
        private static IConnection GetRabbitConnection()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "shag",
                Password = "shag",
                VirtualHost = "/",
                HostName = "192.168.111.199"
            };
            IConnection conn = factory.CreateConnection();
            return conn;
        }
        private static string Receive(string queue)
        {
            string data = "";
            Task.Factory.StartNew(() => 
            {
                using (IConnection connection = GetRabbitConnection())
                {
                    using (IModel channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue, true, false, false, null);
                        var consumer = new EventingBasicConsumer(channel);
                        BasicGetResult result = channel.BasicGet(queue, true);
                        if (result != null)
                            data = Encoding.UTF8.GetString(result.Body);
                    }
                }
            });
            return data;
        }
        private static uint GetMessageAmount()
        {
            using (IConnection connection = GetRabbitConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare("DashboardQueue", true, false, false, null);
                    return channel.MessageCount("DashboardQueue");
                }
            }
        }
    }
    public class Couples
    {
        public string Id { get; set; }
        public string FirstFullName { get; set; }
        public string SecondFullName { get; set; }
    }
}