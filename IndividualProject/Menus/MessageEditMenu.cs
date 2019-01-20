using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    

    public class MessageEditMenu
    {
        private string myConnectionString;
        private int _messageID;

        public MessageEditMenu(int messageID)
        {
            myConnectionString = "Server=KASSANDRAHP\\SQLEXPRESS;Database = PhysicsLab2;Integrated Security=SSPI;";
            _messageID = messageID;
            Console.WriteLine("1. Edit the Title");
            Console.WriteLine("2. Edit the MessageData");
            Console.WriteLine("3. Edit the Receiver UserName");
            
        }

        public void EditFields()
        {
            CheckUserInput checkUserInput = new CheckUserInput();
            DatabaseAccessLayer databaseAccessLayer = new DatabaseAccessLayer(myConnectionString);
            string message = "Select a number";
            string submessage;
            string value="";
            int fieldSelected = checkUserInput.MenuChoice(message);
            if (fieldSelected == 1)
            {
                Console.Write("Type the new title: ");
                string newTitle = Console.ReadLine();
                databaseAccessLayer.UpdateMessage(_messageID, "Title", newTitle);
            }
            else if (fieldSelected == 2)
            {
                submessage = "Type the new message text: ";
                value = checkUserInput.PreventNull(submessage);
                databaseAccessLayer.UpdateMessage(_messageID, "MessageData", value);
            }
            else if (fieldSelected == 3)
            {
                submessage = "Type the ID of the new recipient: ";
                int receiverID = checkUserInput.PreventNullID(submessage);
                databaseAccessLayer.UpdateMessage(_messageID, "ReceiverID", receiverID);
            }
            //DateTime now = new DateTime();
            //now = DateTime.Now;

        }
    }
}
