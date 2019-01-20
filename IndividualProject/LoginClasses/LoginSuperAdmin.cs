using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    public class LoginSuperAdmin
    {
        private string _connectionString;
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
        private bool isUser;
        private bool hasUsername;
        private bool isSuperAdmin;
        private Roles role;
        private CheckUserInput checkUserInput;



        public LoginSuperAdmin(string connectionString)
        {
            _connectionString = connectionString;
            checkUserInput = new CheckUserInput();
            LogSuperAdminStart();
        }

        public void LogSuperAdminStart()
        {
            Console.WriteLine("Hallo, super administrator.");
            Console.WriteLine("In order to enter, you must type username and password.");
            Console.Write("Give the UserName: ");
            AdminUsername = Console.ReadLine();
            Console.Write("Give the Password: ");
            AdminPassword = Console.ReadLine();
        }

        public bool printResult()
        {
            bool hasSuperAdminRights =false;
            role = Roles.Undefined;
            UserAuthentication userAuthentication = new UserAuthentication(_connectionString, AdminUsername, AdminPassword);
            isUser = userAuthentication.CheckUser(out hasUsername, out isSuperAdmin, out role);
            if (hasUsername)
            {
                if (isUser)
                {
                    if (isSuperAdmin)
                    {
                        hasSuperAdminRights = true;
                    }
                    else
                    {
                        Console.WriteLine("You have not Super Administrator rights.");
                        NotLoggedIn();
                    };
                }
                else
                {
                    Console.WriteLine("The password is wrong.");
                    NotLoggedIn();
                };
            }
            else
            {
                Console.WriteLine("Sorry, you are not registered in the system.");
                NotLoggedIn();

            }
            return hasSuperAdminRights;
        }

        public void NotLoggedIn()
        {
            Console.WriteLine("You did't manage to log in.");
            Console.WriteLine("Press any key to to return to the initial Login Screen");
            Console.ReadKey();
        }

    }
}
