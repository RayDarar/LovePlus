using System.Collections.Generic;
using System.Linq;
using System.Text;
using LovePlus.DAL;
using LovePlus.DAL.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System;

namespace Worker
{
    public class WorkerMain
    {
        private static DispatcherTimer timer = new DispatcherTimer();
        private static void Main(string[] args)
        {
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += MakeCouples;
            timer.Start();
        }
        private static void MakeCouples(object sender, EventArgs e)
        {
            uint num = GetMessageAmount();
            if (num >= 6)
            {
                List<string> arr = new List<string>();
                List<string> tempArr = new List<string>();
                for (uint i = 0; i < num; i++)
                    arr.Add(Receive("UserRequests"));
                foreach (string item in arr)
                {
                    User temp = DataClass.GetUserList().FirstOrDefault(f => f.id == item);
                    User temp1 = DataClass.GetUserList().FirstOrDefault(f => arr.Contains(f.id) && f.id != temp.id && f.Gender != temp.Gender && f.LoveKey == temp.LoveKey);
                    if (temp1 != null)
                    {
                        if (!tempArr.Contains(temp1.id))
                        {
                            temp.Status = temp1.id;
                            temp1.Status = temp.id;
                            DataClass.UpdateUserInfo(temp);
                            DataClass.UpdateUserInfo(temp1);
                            SendMessage(temp.Contact, temp1.FullName);
                            SendMessage(temp1.Contact, temp.FullName);
                            Send("DashboardQueue", $"{temp.id}");
                            tempArr.Add(temp.id);
                        }
                    }
                    else
                    {
                        Send("UserRequests", $"{temp.id}");
                        tempArr.Add(temp.id);
                    }
                }
            }
        }
        private static void SendMessage(string address, string LoveName)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.yandex.com");
                mail.From = new MailAddress("RayDarar@yandex.kz");
                mail.To.Add(address);
                mail.Subject = $"Your love!";
                mail.Body = $"Your new love: {LoveName}";
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("RayDarar@yandex.kz", "Ansik@Asasin98");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (System.Exception) { }
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
        private static void Send(string queue, string data)
        {
            Task.Factory.StartNew(() => 
            {
                using (IConnection connection = GetRabbitConnection())
                {
                    using (IModel channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue, true, false, false, null);
                        channel.BasicPublish(string.Empty, queue, null, Encoding.UTF8.GetBytes(data));
                    }
                }
            });
        }
        private static string Receive(string queue)
        {
            string data = "";
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
            return data;
        }
        private static uint GetMessageAmount()
        {
            using (IConnection connection = GetRabbitConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare("UserRequests", true, false, false, null);
                    return channel.MessageCount("UserRequests");
                }
            }
        }
    }
}