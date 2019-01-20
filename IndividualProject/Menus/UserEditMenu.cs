using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class UserEditMenu
    {
        private string myConnectionString;
        private string _username;
        

        public UserEditMenu(string username)
        {
            myConnectionString = "Server=KASSANDRAHP\\SQLEXPRESS;Database = PhysicsLab2;Integrated Security=SSPI;";
            _username = username;
            Console.WriteLine("1. Edit the username");
            Console.WriteLine("2. Edit the password");
            Console.WriteLine("3. Edit the first name");
            Console.WriteLine("4. Edit the last name");
        }

        public void EditFields()
        {
            CheckUserInput checkUserInput = new CheckUserInput();
            DatabaseAccessLayer databaseAccessLayer = new DatabaseAccessLayer(myConnectionString);
            string message = "Select a number";
            string submessage;
            string value;
            int fieldSelected = checkUserInput.EditMenu(message);
            if (fieldSelected == 1)
            {
                submessage = "Type the new username: ";
                value = checkUserInput.PreventNull(submessage);
                databaseAccessLayer.UpdateUser(_username, "UserName", value);
            }
            else if (fieldSelected == 2)
            {
                submessage = "Type the new password: ";
                value = checkUserInput.PreventNull(submessage);
                databaseAccessLayer.UpdateUser(_username, "Password", value);
            }
            else if (fieldSelected == 3)
            {
                submessage = "Type the new first name: ";
                value = checkUserInput.PreventNull(submessage);
                databaseAccessLayer.UpdateUser(_username, "FirstName", value);
            }
            else if (fieldSelected == 4)
            {
                submessage = "Type the new last name: ";
                value = checkUserInput.PreventNull(submessage);
                databaseAccessLayer.UpdateUser(_username, "LastName", value);
            }

        }
    }
}
