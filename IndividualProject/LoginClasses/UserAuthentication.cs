using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace IndividualProject
{
    public class UserAuthentication
    {
        public string username { get; set; }
        public string password { get; set; }
        public Roles role { get; set; }
        private string _connectionString;
        private string SelectUser;


        public UserAuthentication(string connectionString, string Username, string Password)
        {
            username = Username;
            password = Password;
            _connectionString = connectionString;
            SelectUser = "SELECT * FROM Users WHERE UserName=@username";
        }

        public bool CheckUser(out bool hasUsername, out bool isSuperAdmin, out Roles role)
        {
            hasUsername = false;
            isSuperAdmin = false;
            bool isUser = false;
            role = Roles.Undefined;

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(SelectUser, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                hasUsername = true;
                                string UserPassword = reader.GetString(2);
                                int UserRole = reader.GetInt32(5);
                                if (UserPassword.TrimEnd() == password)
                                {
                                    isUser = true;
                                    role = (Roles)UserRole;
                                    if (UserRole == 1)
                                    {
                                        isSuperAdmin = true;
                                    }  
                                }
                            }
                        }
                    }
                }
            }
            return isUser;
        }
    }
}

