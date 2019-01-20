using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IndividualProject
{
    public class FileAccessLayer
    {
        public Roles UserRole { get; set; }
        public string UserName { get; set; }
        private string fileName;

        public FileAccessLayer(string username)
        {
            UserName = username;
        }

        public void GetMenu(Roles userRole, out int TotalChoices)
        {
            TotalChoices = 0;
            switch (userRole)
            {
                case Roles.Administrator:
                    fileName = "AdminMenu.txt";
                    break;
                case Roles.Teacher:
                    fileName = "TeacherMenu.txt";
                    break;
                case Roles.Student:
                    fileName = "StudentMenu.txt";
                    break;
                default:
                    break;
            }
            StreamReader streamReader = new StreamReader(fileName);
            string FileLine;
            while ((FileLine = streamReader.ReadLine()) != null)
            {
                if (!FileLine.StartsWith("-"))
                TotalChoices += 1;

                Console.WriteLine(FileLine);
            }
        }

        public void GetSuperAdminMenu(string choice)
        {
            switch (choice)
            {
                case "A":
                    fileName = "AdminMenu.txt";
                    break;
                case "B":
                    fileName = "SuperAdminMenu.txt";
                    break;
                default:
                    break;
            }
            StreamReader streamReader = new StreamReader(fileName);
            string FileLine;
            while ((FileLine = streamReader.ReadLine()) != null)
            {
                Console.WriteLine(FileLine);
            }
        }

        public void CreateORAppendToFile(Message newMessage)
        {
            //Create a file that contains all the messages that this user have sent
            fileName = $"Messages_Of_{UserName}.txt";
            if (!File.Exists(fileName))
            {
                var myFile = File.Create(fileName);
                myFile.Close();
                using (var textWriter = new StreamWriter(fileName, true))
                {
                    textWriter.WriteLine("MessageID, Title, MessageData, DateOfSubmission, SenderID, ReceiverID");
                    textWriter.WriteLine($"{newMessage.MessageID.ToString()}, {newMessage.Title}, {newMessage.MessageData}, {newMessage.DateOfSubmission}, {newMessage.SenderID}, {newMessage.ReceiverID}");
                }
            }
            else if (File.Exists(fileName))
            {
                using (var tw = new StreamWriter(fileName, true))
                {
                    tw.WriteLine($"{newMessage.MessageID.ToString()}, {newMessage.Title}, {newMessage.MessageData}, {newMessage.DateOfSubmission}, {newMessage.SenderID}, {newMessage.ReceiverID}");
                }
            }
        }
    }
}
