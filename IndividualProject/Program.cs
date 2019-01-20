using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string myConnectionString = "Server=KASSANDRAHP\\SQLEXPRESS;Database = PhysicsLab2;Integrated Security=SSPI;";
            Roles UserRole = Roles.Undefined;
            string UserName = string.Empty;
            //INTRO EFFECTS
            Intro.PlaySound();
            Intro.MultiLineAnimation();
            Intro.TypingEffect();

            //LOGIN SCREEN
            bool loginDone = false;

            while (!loginDone)
            {
                while (!loginDone)
                {
                    Console.Clear();
                    LoginScreen loginScreen = new LoginScreen(myConnectionString);
                    loginDone = loginScreen.LoginMenu(out UserRole, out UserName);
                }

                //APPLICATION MENUS
                if (UserRole == Roles.SuperAdministrator)
                {
                    SuperAdminMenu superAdminMenu = new SuperAdminMenu(myConnectionString);
                    superAdminMenu.SelectMenu();
                }
                if (UserRole == Roles.Administrator || UserRole == Roles.Teacher || UserRole == Roles.Student)
                {
                    UserMenu userMenu = new UserMenu(myConnectionString, UserRole, UserName);
                }

                loginDone = false;
                UserRole = Roles.Undefined;
                Console.WriteLine("Press any key to return to Login screen.");
                Console.ReadKey();
            }
        }
    }
}
