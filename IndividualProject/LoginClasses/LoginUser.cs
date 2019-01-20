using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace IndividualProject
{
    public class LoginUser
    {
        private string _connectionString;
        public string MyUsername { get; set; }
        public string MyPassword { get; set; }
        private Roles myRole;
        private bool isSuperAdmin;
        private bool hasUsername;

        public LoginUser(string connectionString)
        {
            _connectionString = connectionString;
            Console.WriteLine("In order to enter, you must type username and password.");
            Console.Write("Give the UserName: ");
            MyUsername = Console.ReadLine();
            Console.Write("Give the Password: ");
            MyPassword = Console.ReadLine();
            //secure string
            //SecureString securePassword = PasswordMasking.GetPassword();
            //PasswordMasking.ToSecureString(MyPassword);

        }

        public bool printResult(out Roles role)
        {
            UserAuthentication userAuthentication = new UserAuthentication(_connectionString, MyUsername, MyPassword);
            bool isUser = userAuthentication.CheckUser(out hasUsername, out isSuperAdmin, out myRole);
            if (hasUsername)
            {
                if (!isUser)
                {
                    Console.WriteLine("The password is wrong.");
                    NotLoggedIn();
                }
            }
            else
            {
                Console.WriteLine("Sorry, you are not registered in the system.");
                NotLoggedIn();
            }
            role = myRole;
            return isUser;
        }

        public void NotLoggedIn()
        {
            Console.WriteLine("You did't manage to log in.");
            Console.WriteLine("Press any key to to return to the initial Login Screen");
            Console.ReadKey();
        }

    }
}
