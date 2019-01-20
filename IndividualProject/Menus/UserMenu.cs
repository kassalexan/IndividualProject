using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class UserMenu
    {
        public Roles Role { get; set; }
        public string UserName { get; set; }
        private CheckUserInput checkUserInput;
        private FileAccessLayer fileAccessLayer;
        private string _myconnectionString;
        private DatabaseAccessLayer databaseAccessLayer;


        public UserMenu(string myConnectionString, Roles role, string username)
        {
            _myconnectionString = myConnectionString;
            Role = role;
            UserName = username;
            checkUserInput = new CheckUserInput();
            fileAccessLayer = new FileAccessLayer(username);
            databaseAccessLayer = new DatabaseAccessLayer(_myconnectionString);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Welcome e-Lab user <" + username + "> !!");
            Console.WriteLine("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");  
            Menu();
        }

        public void Menu()
        {
            int TotalChoices = 0;
            int MessageManipulationType = -1;
            while (MessageManipulationType == -1)
            {
                //Get menu from file
                fileAccessLayer.GetMenu(Role, out TotalChoices);
                string Message = "Please, select a number to choose an action:";
                //Get user choice between numbers 1-9
                Console.ForegroundColor = ConsoleColor.Green;
                MessageManipulationType = checkUserInput.ApplicationMenuChoice(Message, Role, TotalChoices);
                Console.ForegroundColor = ConsoleColor.White;
                switch (MessageManipulationType)
                {
                    case 1:
                        List<MessageView> messagesIn = databaseAccessLayer.Inbox(UserName);
                        ResultTitles("Title --> MessageData --> Sender's Username");
                        messagesIn.ForEach(item => Console.WriteLine($"{item.Title} --> {item.MessageData} --> {item.SenderUserName}"));
                        break;
                    case 2:
                        List<MessageView> messagesSent = databaseAccessLayer.Sent(UserName);
                        ResultTitles("Title --> MessageData --> Receiver's Username");
                        messagesSent.ForEach(item => Console.WriteLine($"{item.Title} --> {item.MessageData} --> {item.ReceiverUserName}"));
                        break;
                    case 3:
                        SendMessage sendMessage = new SendMessage(UserName);
                        Message newMessage = new Message();
                        newMessage = sendMessage.SetNewMessageFields();
                        databaseAccessLayer.CreateMessage(newMessage);
                        int newMessageID = databaseAccessLayer.GetID(newMessage);
                        newMessage.MessageID = newMessageID;
                        fileAccessLayer.CreateORAppendToFile(newMessage);
                        break;
                    case 4:
                        DateTime startDate = checkUserInput.DateInput("Enter the starting date of the messages: ", out string SQLDate);
                        List<MessageView> messages = databaseAccessLayer.GetAllMessages(SQLDate);
                        ResultTitles("MessageID --> Title --> MessageData --> Sender's Username --> Receiver's Username");
                        messages.ForEach(item => Console.WriteLine($"{item.MessageID} --> {item.Title} --> {item.MessageData} --> {item.SenderUserName} --> {item.ReceiverUserName}"));
                        break;
                    case 5:
                        string aUserName = checkUserInput.PreventNull("Type the username of the user whom Inbox you want to see: ");
                        List<MessageView> messagesInSomeuser = databaseAccessLayer.Inbox(aUserName);
                        ResultTitles("Title --> MessageData --> Sender's Username");
                        messagesInSomeuser.ForEach(item => Console.WriteLine($"{item.Title} --> {item.MessageData} --> {item.SenderUserName}"));
                        break;
                    case 6:
                        string someUserName = checkUserInput.PreventNull("Type the username of the user whom Sent messages you want to see: ");
                        List<MessageView> messagesSomeSent = databaseAccessLayer.Sent(someUserName);
                        ResultTitles("Title --> MessageData --> Receiver's Username");
                        messagesSomeSent.ForEach(item => Console.WriteLine($"{item.Title} --> {item.MessageData} --> {item.ReceiverUserName}"));
                        break;
                    case 7:
                        if (Role == Roles.Student)
                        {
                            // LOGOUT
                            return;
                        }
                        int messageID = checkUserInput.PreventNullID("Give the Message ID");
                        bool exists = databaseAccessLayer.checkID(messageID);
                        if (exists)
                        {
                            MessageEditMenu messageEditMenu = new MessageEditMenu(messageID);
                            messageEditMenu.EditFields();
                        }
                        else
                        {
                            Console.WriteLine("There is not such a message in database");
                        }
                        break;
                    case 8:
                        if (Role == Roles.Teacher)
                        {
                            // LOGOUT
                            return;
                        }
                        int MessageID = checkUserInput.PreventNullID("Give the Message ID");
                        bool Exists = databaseAccessLayer.checkID(MessageID);
                        if (Exists)
                        {
                            databaseAccessLayer.DeleteMessage(MessageID);
                        }
                        else
                        {
                            Console.WriteLine("There is not such a message in database");
                        }
                        break;
                    case 9:
                        // LOGOUT
                        return;
                    default:
                        break;
                }
                MessageManipulationType = -1;
                Console.WriteLine("Press any key to return to menu");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void ResultTitles(string titles)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(titles);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
