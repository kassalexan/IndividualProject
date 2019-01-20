using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class LoginScreen
    {
        public string message { get; set; }
        private string _connectionString;
        private CheckUserInput userInput;

        public LoginScreen(string connectionString)
        {
            _connectionString = connectionString;
            userInput = new CheckUserInput();
            Console.WriteLine("------------------------PHYSICS LAB--------------------------");
            Console.WriteLine("");
            Console.WriteLine("Please, select one of the following actions:");
            Console.WriteLine("");
            Console.WriteLine("1.Login as super administrator.");
            Console.WriteLine("2.Login as administrator, teacher or student");
            Console.WriteLine("3.Exit");
            Console.WriteLine("");
        }

        public int getChoice()
        {
            string message = "Type 1 or 2 or 3";
            int user_choice = userInput.MenuChoice(message);
            return user_choice;
        }

        public bool LoginMenu(out Roles UserRole, out string UserName)
        {
            int MenuItem = getChoice();
            bool isSuperAdmin = false;
            UserName = string.Empty;
            UserRole = Roles.Undefined;
            if (MenuItem == 1)
            {
                LoginSuperAdmin loginSuperAdmin = new LoginSuperAdmin(_connectionString);
                isSuperAdmin = loginSuperAdmin.printResult();
                if (isSuperAdmin)
                {
                    UserRole = Roles.SuperAdministrator;
                    UserName = loginSuperAdmin.AdminUsername;
                } else
                {
                    return false;
                }
            }
            if (MenuItem == 2)
            {
                LoginUser loginUser = new LoginUser(_connectionString);
                bool isUser = loginUser.printResult(out Roles userRole);
                if (isUser)
                {
                    UserRole = userRole;
                    UserName = loginUser.MyUsername;
                } else
                {
                    return false;
                }
            }
            if (MenuItem == 3)
            {
                ProgramExit.Exit();
            }
            return true;
        }
    }
}
