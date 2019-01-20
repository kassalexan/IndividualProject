using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class SuperAdminMenu
    {
        private CheckUserInput checkUserInput;
        private FileAccessLayer fileAccessLayer;
        private string _myconnectionString;
        private DatabaseAccessLayer databaseAccessLayer;



        public SuperAdminMenu(string myConnectionString)
        {
            _myconnectionString = myConnectionString;
            checkUserInput = new CheckUserInput();
            fileAccessLayer = new FileAccessLayer("admin");
            databaseAccessLayer = new DatabaseAccessLayer(_myconnectionString);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Welcome dear super administrator!!!");
            Console.WriteLine("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            MenuStart();
        }

        public void MenuStart()
        { 
            Console.WriteLine("A. Select Message Manipulation");
            Console.WriteLine("B. Select User Manipulation");
            Console.WriteLine("C. Log out");
        }

        public void SelectMenu()
        {
            //Get the user choice, this can be "A" or "B" or "C", other choices are not accepted
            string message = "Type A or B to choose menu, or C to log out.";
            string superAdminChoice = checkUserInput.SuperAdminChoice(message);
            if (superAdminChoice == "C")
            {
                //LOG OUT
                return;
            }

            //Get menu from file
            fileAccessLayer.GetSuperAdminMenu(superAdminChoice);

            //Display super admin menu, depending on user's choice
            Console.WriteLine("");          
            if (superAdminChoice == "A")
            {
                MessageManipulationMenu();
            }
            if (superAdminChoice == "B")
            {
                UserManipulationMenu();
            } 
        }

        public void MessageManipulationMenu()
        {
            string Message = "Please, select a number to choose an action:";
            int MessageManipulationType = 0;
            //Get user choice between numbers 1-9
            Console.ForegroundColor = ConsoleColor.Green;
            MessageManipulationType = checkUserInput.ApplicationMenuChoice(Message, Roles.SuperAdministrator, 9);
            Console.ForegroundColor = ConsoleColor.White;
            switch (MessageManipulationType)
            {
                case 1:
                    List<MessageView> messagesIn = databaseAccessLayer.Inbox("admin");
                    ResultTitles("Title --> MessageData --> Sender's Username");
                    messagesIn.ForEach(item => Console.WriteLine($"{item.Title} --> {item.MessageData} --> {item.SenderUserName}"));
                    break;
                case 2:
                    List<MessageView> messagesSent = databaseAccessLayer.Sent("admin");
                    ResultTitles("Title --> MessageData --> Receiver's Username");
                    messagesSent.ForEach(item => Console.WriteLine($"{item.Title} --> {item.MessageData} --> {item.ReceiverUserName}"));
                    break;
                case 3:
                    SendMessage sendMessage = new SendMessage("admin");
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
                default:
                    break;
            }
            Console.WriteLine("");
            MenuStart();
            SelectMenu();
        }

        public void ResultTitles(string titles)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(titles);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void UserManipulationMenu()
        {
            string Message = "Please, select a number to choose an action:";
            int UserManipulationType = 0;
            //Get user choice between numbers 1-6
            Console.ForegroundColor = ConsoleColor.Green;
            UserManipulationType = checkUserInput.SuperAdminUserManipulation(Message);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-----------------------------------------");
            switch (UserManipulationType)
            {
                //VIEW ALL USERS
                case 1:
                    List<User> result = databaseAccessLayer.GetAllUsers();
                    result.ForEach(item => Console.WriteLine($"{item.Username} --> {item.FirstName} {item.LastName}"));
                    break;
                //SEARCH FOR A USER
                case 2:
                    Console.WriteLine("1. Search a user by username.");
                    Console.WriteLine("2. Search a user by last name.");
                    int typeOfSearch = checkUserInput.Select1or2();
                    //BASED ON USERNAME
                    if (typeOfSearch == 1)
                    {
                        string username = checkUserInput.PreventNull("Type the username: ");
                        User user = databaseAccessLayer.SearchUserByUserName(username);
                        if (user.Username != null)
                        {
                            Console.WriteLine($"{user.FirstName}, {user.LastName}, {(Roles)user.Role}");
                        }
                    }
                    //BASED ON LAST NAME
                    else if (typeOfSearch == 2)
                    {
                        string lastname = checkUserInput.PreventNull("Type the last name: ");
                        List<User> users = databaseAccessLayer.SearchUserByLastName(lastname);
                        if (users.Count() != 0)
                        {
                            users.ForEach(user => Console.WriteLine($"{user.Username} {user.FirstName} {user.LastName} {(Roles)user.Role}"));
                        }
                    }
                    break;
                //INSERT A NEW USER
                case 3:
                    User NewUser = new User();
                    NewUser = checkUserInput.SetNewUserFields();
                    databaseAccessLayer.CreateUser(NewUser);
                    break;
                //ASSIGN ROLE TO A USER
                case 4:
                    string myUsername = checkUserInput.PreventNull("Type the username of the user: ");
                    User myUser = new User();
                    myUser = databaseAccessLayer.SearchUserByUserName(myUsername);
                    if (myUser.Username != null)
                    {
                        int role = checkUserInput.RolePreventNull("Type the role of the user: ");
                        databaseAccessLayer.AssignRole(role, myUsername);
                    }
                    else
                    {
                        Console.WriteLine("There is not such a user in database");
                    }
                    break;
                //UPDATE (EDIT) A USER
                case 5:
                    string editedUsername = checkUserInput.PreventNull("Type the username of the user you want to edit: ");
                    User editedUser = new User();
                    editedUser = databaseAccessLayer.SearchUserByUserName(editedUsername);
                    if (editedUser.Username != null)
                    {
                        UserEditMenu userEditMenu = new UserEditMenu(editedUsername);
                        userEditMenu.EditFields();
                    }
                    else
                    {
                        Console.WriteLine("There is not such a user in database");
                    }
                    break;
                //DELETE A USER
                case 6:
                    string deletedUsername = checkUserInput.PreventNull("Type the username of the poor guy you want to delete: ");
                    User deletedUser = new User();
                    deletedUser = databaseAccessLayer.SearchUserByUserName(deletedUsername);
                    if (deletedUser.Username != null)
                    {
                        databaseAccessLayer.DeleteUser(deletedUsername);
                    }
                    else
                    {
                        Console.WriteLine("There is not such a user in database");
                    }
                    break;
                default:
                    break;
            }
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------");
            MenuStart();
            SelectMenu();

        }
    }
}
