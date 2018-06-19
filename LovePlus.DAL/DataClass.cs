using System;
using System.Collections.Generic;
using System.IO;
using LovePlus.DAL.Model;
using System.Xml.Linq;
using System.Linq;

namespace LovePlus.DAL
{
    public class DataClass
    {
        private static string DataBasePath = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.IndexOf(@"LovePlus\LovePlus\bin")) + "LPDB";
        private static void CheckDB()
        {
            if (!Directory.Exists(DataBasePath))
            {
                Directory.CreateDirectory(DataBasePath);
                Directory.CreateDirectory(DataBasePath + @"\Users");
                XElement root = new XElement("User",
                    new XElement("id", 1),
                    new XElement("Contact", "dfqgth400@gmail.com"),
                    new XElement("Login", "RayDarar"),
                    new XElement("Name", "Рыспеков Ансар Кайратович"),
                    new XElement("Password", "Zaka2"),
                    new XElement("LoveKey", 0),
                    new XElement("Status", 0),
                    new XElement("Gender", 1));//1 - Парень, 0 женщина
                root.Save(DataBasePath + @"\Users\user_1.xml");
            }
        }
        public static List<User> GetUserList()
        {
            CheckDB();
            List<User> arr = new List<User>();
            DirectoryInfo df = new DirectoryInfo(DataBasePath + @"\Users");
            foreach (FileInfo file in df.GetFiles())
            {
                XDocument doc = XDocument.Load(file.FullName);
                arr.Add(new User()
                {
                    id = doc.Element("User").Element("id").Value,
                    Contact = doc.Element("User").Element("Contact").Value,
                    Login = doc.Element("User").Element("Login").Value,
                    FullName = doc.Element("User").Element("Name").Value,
                    Password = doc.Element("User").Element("Password").Value,
                    LoveKey = doc.Element("User").Element("LoveKey").Value,
                    Status = doc.Element("User").Element("Status").Value,
                    Gender = doc.Element("User").Element("Gender").Value
                });
            }
            return arr;
        }
        public static void AddNewUser(string FullName, string Login, string Password, string Contact, string Gender)
        {
            CheckDB();
            int id = int.Parse(GetUserList().Max(m => m.id)) + 1;
            XElement root = new XElement("User",
                new XElement("id", id),
                new XElement("Contact", Contact),
                new XElement("Login", Login),
                new XElement("Name", FullName),
                new XElement("Password", Password),
                new XElement("LoveKey", 0),
                new XElement("Status", 0),
                new XElement("Gender", Gender));
            root.Save(DataBasePath + $@"\Users\user_{id}.xml");
        }
        public static void UpdateUserInfo(User temp)
        {
            CheckDB();
            XElement root = new XElement("User",
                new XElement("id", temp.id),
                new XElement("Contact", temp.Contact),
                new XElement("Login", temp.Login),
                new XElement("Name", temp.FullName),
                new XElement("Password", temp.Password),
                new XElement("LoveKey", temp.LoveKey),
                new XElement("Status", temp.Status),
                new XElement("Gender", temp.Gender));
            File.Delete(DataBasePath + $@"\Users\user_{temp.id}.xml");
            root.Save(DataBasePath + $@"\Users\user_{temp.id}.xml");
        }
        public static void DeleteUser(User temp)
        {
            DirectoryInfo df = new DirectoryInfo(DataBasePath + @"\Users");
            foreach (FileInfo file in df.GetFiles())
            {
                XDocument doc = XDocument.Load(file.FullName);
                if (doc.Element("User").Element("id").Value == temp.id)
                {
                    if(temp.Status != "0")
                    {
                        User temp1 = GetUserList().FirstOrDefault(f => f.id == temp.Status);
                        temp1.Status = "0";
                        UpdateUserInfo(temp1);
                    }
                    File.Delete(file.FullName);
                    break;
                }
            }
        }
    }
}