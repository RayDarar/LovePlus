using System;
using System.Collections.Generic;
using System.IO;
using LovePlus.DAL.Model;
using System.Xml.Linq;

namespace LovePlus.DAL
{
    public class DataClass
    {
        private static string DataBasePath = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.IndexOf(@"Worker\Worker\bin")) + "LPDB";
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
    }
}