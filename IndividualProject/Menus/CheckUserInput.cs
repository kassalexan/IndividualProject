using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class CheckUserInput
    {
        
        //Welcome menu: Iterate until user enters 1,2 or 3
        public int MenuChoice(string message)
        {
            int response = 0;
            while (response != 1 && response != 2 && response != 3)
            {
                Console.WriteLine(message);
                ConsoleKeyInfo keypress = Console.ReadKey();
                if (keypress.KeyChar == '1')
                {
                    response = 1;
                }
                else if (keypress.KeyChar == '2')
                {
                    response = 2;
                }
                else if (keypress.KeyChar == '3')
                {
                    response = 3;
                }
                Console.WriteLine("");
            }
            return response;
        }

        //SuperAdmin choice between A (Message Manipulation) and B (User Manipulation)
        public string SuperAdminChoice(string message)
        {
            string response = "";
            string[] letters = { "A", "a", "B", "b", "C", "c" };
            while (!letters.Contains(response))
            {
                Console.WriteLine(message);
                ConsoleKeyInfo keypress = Console.ReadKey();
                if (keypress.KeyChar == 'A' || keypress.KeyChar == 'a')
                {
                    response = "A";
                }
                if (keypress.KeyChar == 'B' || keypress.KeyChar == 'b')
                {
                    response = "B";
                }
                if (keypress.KeyChar == 'C' || keypress.KeyChar == 'c')
                {
                    response = "C";
                }
                Console.WriteLine("");
            }
            return response;
        }

        //Application menu: Iterate until user enters 1-8 to choose an action
        public int ApplicationMenuChoice(string message, Roles role, int TotalChoices)
        {
            char[] menuItems = { '1', '2', '3', '4', '5', '6', '7', '8', '9'};
            int intChoice = 0;

            while (intChoice < 1 || intChoice > TotalChoices)
            {
                Console.WriteLine(message);
                ConsoleKeyInfo keypress = Console.ReadKey();
                //foreach (int item in Enum.GetValues(typeof(ApplicationMenuItems)))
                //{
                //if (keypress.KeyChar == item)
                //{
                //intChoice = item;
                //}
                //}
                for (int i = 0; i < TotalChoices; i++)
                {
                    if (keypress.KeyChar == menuItems[i])
                    {
                        intChoice = i + 1;
                    }
                }
                Console.WriteLine("");
            }
            return intChoice;
        }

        //SuperAdmin menu (User Manipulation)
        public int SuperAdminUserManipulation(string message)
        {
            char[] menuItems = { '1', '2', '3', '4', '5', '6' };
            int intChoice = 0;

            while (intChoice < 1 || intChoice > 6)
            {
                Console.WriteLine(message);
                ConsoleKeyInfo keypress = Console.ReadKey();
                for (int i = 0; i < 6; i++)
                {
                    if (keypress.KeyChar == menuItems[i])
                    {
                        intChoice = i + 1;
                    }
                }
                Console.WriteLine("");
            }
            return intChoice;
        }

        //Method that enables the user enter all the fields of the user that will be imported
        public User SetNewUserFields()
        {
            User newUser = new User();
            string message = "Give the username of the new user: ";
            newUser.Username = PreventNull(message);
            message = "Give the password: ";
            newUser.Password = PreventNull(message);
            message = "Give the first name: ";
            newUser.FirstName = PreventNull(message);
            message = "Give the last name: ";
            newUser.LastName = PreventNull(message);
            message = "Give the role of the new user: ";
            newUser.Role = RolePreventNull(message);
            return newUser;
        }

        public string PreventNull(string message)
        {
            Console.Write(message);
            string field = Console.ReadLine();
            while (field == null || field.Trim() == "")
            {
                Console.WriteLine("You must type a value, blank fields are not accepted!!");
                Console.Write(message);

               field = Console.ReadLine();
            }
            return field;
        }

        

        public int RolePreventNull(string message)
        {
            string field;
            int intField=0;
            while (intField != 2 && intField != 3 && intField != 4)
            {
                do
                {
                    Console.WriteLine("Please type 2 for administrator, 3 for teacher or 4 for student");
                    Console.Write(message);
                    field = Console.ReadLine();
                } while (!int.TryParse(field, out intField) || field == null || field.Trim() == "");
            }
            return intField;
        }

        public int PreventNullID(string message)
        {
            string result;
            int myID;
            do
            {
                Console.WriteLine(message);

                result = Console.ReadLine();
            } while (!int.TryParse(result, out myID));

            return myID;
        }

        public DateTime DateInput(string message, out string SQLDate)
        {
            DateTime startDate=new DateTime(1900,01,01);
            DateTime myDate = startDate;
            string userDate = "1900-01-01";
            while (myDate == startDate)
            {
                do
                {
                    Console.WriteLine("Please type a date in this format: 2018-11-16");
                    Console.Write(message);
                    userDate = Console.ReadLine();
                } while (!DateTime.TryParse(userDate, out myDate));
            }
            SQLDate = userDate;
            return myDate;
        }



        public int Select1or2()
        {
            string message = "Please type 1 or 2";
            int response = 0;
            while (response != 1 && response != 2)
            {
                Console.WriteLine(message);
                ConsoleKeyInfo keypress = Console.ReadKey();
                if (keypress.KeyChar == '1')
                {
                    response = 1;
                }
                else if (keypress.KeyChar == '2')
                {
                    response = 2;
                }
                Console.WriteLine("");
            }
            return response;
        }

        public int EditMenu(string message)
        {
            int response = 0;
            while (response != 1 && response != 2 && response != 3 && response != 4)
            {
                Console.WriteLine(message);
                ConsoleKeyInfo keypress = Console.ReadKey();
                if (keypress.KeyChar == '1')
                {
                    response = 1;
                }
                else if (keypress.KeyChar == '2')
                {
                    response = 2;
                }
                else if (keypress.KeyChar == '3')
                {
                    response = 3;
                }
                else if (keypress.KeyChar == '4')
                {
                    response = 4;
                }
                Console.WriteLine("");
            }
            return response;
        }

      

    }
}
