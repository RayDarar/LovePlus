using System;
using System.Linq;
using System.Threading;
using LovePlus.DAL;
using LovePlus.DAL.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;

namespace LovePlus.BAL
{
    public class Logic
    {
        private bool IsAuthorized = false;
        public User GetUser(string userId)
        { return DataClass.GetUserList().FirstOrDefault(item => item.id == userId); }
        public bool RegisterNewUser(string FullName, string Login, string Password, string Contact, string Gender)
        {
            if (DataClass.GetUserList().FirstOrDefault(item => item.Login == Login) == null)//Нет такого пользователя
            {
                if (!String.IsNullOrEmpty(FullName) && !String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(Password) && !String.IsNullOrEmpty(Contact) && !String.IsNullOrEmpty(Gender))
                {
                    DataClass.AddNewUser(FullName, Login, Password, Contact, Gender);
                    return true;
                }
            }
            return false;
        }
        public bool GetToken(string Login, string Password)
        {
            User temp = DataClass.GetUserList().FirstOrDefault(item => item.Login == Login && item.Password == Password);
            if (temp != null)
            {
                Token.id = temp.id;
                Token.FullName = temp.FullName;
                IsAuthorized = true;
                return true;
            }
            return false;
        }
        public void LoseToken()
        {
            if (IsAuthorized)
            {
                IsAuthorized = true;
                Token.FullName = "";
                Token.id = "";
            }
        }
        public string GetStatus()
        {
            User temp = DataClass.GetUserList().FirstOrDefault(item => item.id == Token.id);
            if (temp.Status != "0")
                return $"Вы в паре с {DataClass.GetUserList().FirstOrDefault(item => item.id == temp.Status).FullName}";
            else
                return "Вы одиноки";
        }
        public int GetNumber()
        {
            User temp = DataClass.GetUserList().FirstOrDefault(item => item.id == Token.id);
            if (temp.Status == "0" && temp.LoveKey == "0")
            {
                Thread.Sleep(15000);
                int num = new Random().Next(1, 10);
                temp.LoveKey = num.ToString();
                DataClass.UpdateUserInfo(temp);
                Send("UserRequests", $"{temp.id}");
                return num;
            }
            else
                return 0;
        }
        private IConnection GetRabbitConnection()
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
        private void Send(string queue, string data)
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
        public string Receive(string queue)
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
        public bool LoseLove()
        {
            User temp = DataClass.GetUserList().FirstOrDefault(item => item.id == Token.id);
            User temp1 = DataClass.GetUserList().FirstOrDefault(item => item.id == temp.Status);
            if (temp.Status != "0")
            {
                temp.Status = "0";
                temp.LoveKey = "0";
                temp1.Status = "0";
                temp.LoveKey = "0";
                DataClass.UpdateUserInfo(temp);
                return true;
            }
            return false;
        }
        public void DeleteAccount(string id)
        {
            User temp = DataClass.GetUserList().FirstOrDefault(f => f.id == id);
            DataClass.DeleteUser(temp);
        }
    }
}