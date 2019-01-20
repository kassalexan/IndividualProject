using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class SendMessage
    {
        private string myConnectionString;
        private string _senderUsername;
        private CheckUserInput checkUserInput;
        private DatabaseAccessLayer databaseAccessLayer;

        public SendMessage(string SenderUsername)
        {
            myConnectionString = "Server=KASSANDRAHP\\SQLEXPRESS;Database = PhysicsLab2;Integrated Security=SSPI;";
            _senderUsername = SenderUsername;
            checkUserInput = new CheckUserInput();
            databaseAccessLayer = new DatabaseAccessLayer(myConnectionString);
        }

        //Method that enables the user enter all the fields of the message that will be sended
        public Message SetNewMessageFields()
        {
            Message newMessage = new Message();
            //Title is optional (nullable)
            Console.Write("Give the title of the message: ");
            string newTitle = Console.ReadLine();
            newMessage.Title = newTitle;
            string message = "Type the message you want to send: ";
            newMessage.MessageData = checkUserInput.PreventNull(message);
            //Get the current time
            newMessage.DateOfSubmission = DateTime.Now;
            //Sender ID
            int senderID = databaseAccessLayer.ReturnUserID(_senderUsername);
            newMessage.SenderID = senderID;
            //Receiver ID
            newMessage.ReceiverID = CheckReceiver();
            return newMessage;
        }

        public int CheckReceiver()
        {
            string message = "Type the username of the user you want to send the message: ";
            int receiverID = 0;
            do
            {
                string receiverUsername = checkUserInput.PreventNull(message);
                User receiver = new User();
                receiver = databaseAccessLayer.SearchUserByUserName(receiverUsername);
                if (receiver.Username != null)
                {
                    receiverID = databaseAccessLayer.ReturnUserID(receiverUsername);
                }
                else
                {
                    Console.WriteLine("There is not such a user in database");
                }
            } while (receiverID == 0);
            return receiverID;
        }

    }
}
