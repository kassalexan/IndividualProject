using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace IndividualProject
{
    public class DatabaseAccessLayer
    {
        private string _connectionString; 
        
        public DatabaseAccessLayer(string connectionString)
        {
            _connectionString = connectionString;
        }

        //-------------------------METHODS FOR USERS----------------------------
        //VIEW ALL USERS
        public List<User> GetAllUsers()
        {
            const string SelectAllUsers = "SELECT * FROM Users WHERE Deleted=0";
            List<User> Users = new List<User>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(SelectAllUsers, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                User user = new User();
                                user.Username = reader.GetString(1);
                                user.Password = reader.GetString(2);
                                user.FirstName = reader.GetString(3);
                                user.LastName = reader.GetString(4);
                                user.Role = reader.GetInt32(5);
                                Users.Add(user);
                            }
                        }
                    }
                }
            }
            return Users;
        }

        //INSERT NEW USER
        //username is unique
        public void CreateUser(User NewUser)
        {
            const string InsertUser = "INSERT INTO Users" +
                " (UserName, Password, FirstName, LastName, Role) " +
                "VALUES (@UserName,@Password,@FirstName,@LastName,@Role)";
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(InsertUser, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.Parameters.AddWithValue("UserName", NewUser.Username);
                        command.Parameters.AddWithValue("Password", NewUser.Password);
                        command.Parameters.AddWithValue("FirstName", NewUser.FirstName);
                        command.Parameters.AddWithValue("LastName", NewUser.LastName);
                        command.Parameters.AddWithValue("Role", NewUser.Role);
                        int result = command.ExecuteNonQuery();
                        Console.WriteLine($"{result} user added succesfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Probably you gave too big username/password/names, " +
                            "or you tried to import a user with usersname same to an existing user");
                    }
                }
            }
        }

        //SEARCH A USER BASED ON USERNAME
        public User SearchUserByUserName(string username)
        {
            const string SelectUser1 = "SELECT * FROM Users WHERE UserName=@username AND Deleted=0";
            User user = new User();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(SelectUser1, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user.Username = reader.GetString(1);
                                user.Password = reader.GetString(2);
                                user.FirstName = reader.GetString(3);
                                user.LastName = reader.GetString(4);
                                user.Role = reader.GetInt32(5);
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is not such a user in database");
                        }
                    }
                }
            }
            return user;
        }

        //SEARCH USERS BASED ON LAST NAME
        public List<User> SearchUserByLastName(string lastname)
        {
            const string SelectUser2 = "SELECT * FROM Users WHERE LastName=@lastname AND Deleted=0";
            List<User> Users = new List<User>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(SelectUser2, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = lastname;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                User user = new User();
                                user.Username = reader.GetString(1);
                                user.Password = reader.GetString(2);
                                user.FirstName = reader.GetString(3);
                                user.LastName = reader.GetString(4);
                                user.Role = reader.GetInt32(5);
                                Users.Add(user);
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is not such a user in database");
                        }
                    }
                }
            }
            return Users;
        }

        //I give the username and I take the corresponding ID
        public int ReturnUserID(string username)
        {
            const string GetUserID = "SELECT UserID FROM Users WHERE UserName=@username AND Deleted=0";
            User user = new User();
            int UserID=0;
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(GetUserID, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user.UserID = reader.GetInt32(0);
                                UserID = user.UserID;
                            }
                        }
                    }
                }
            }
            return UserID;
        }

        //ASSIGN ROLE TO AN EXISTING USER
        public void AssignRole(int role, string username)
        {
            const string UpdateUserRole = "UPDATE Users SET Role=@role WHERE UserName=@username AND Deleted=0";
            User user = new User();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(UpdateUserRole, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@role", SqlDbType.Int).Value = role;
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    int result = command.ExecuteNonQuery();
                    Console.WriteLine($"{result} user's role changed succesfully");
                }
            }
        }

        //UPDATE A USER (EDIT A FIELD AT A TIME)
        public void UpdateUser(string username, string field, string value)
        {
            string UpdateUser = "UPDATE Users SET " + field + "=@value WHERE UserName=@username AND Deleted=0";
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(UpdateUser, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@value", SqlDbType.NVarChar).Value = value;
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    int result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine($"{result} user was updated succesfully");
                    }
                    else
                    {
                        Console.WriteLine("Change could not be done.");
                    }
                }
            }
        }

        //DELETE A USER
        /* 
        //REAL DELETE
        public void DeleteUser(string username)
        {
            const string DeleteUser = "DELETE FROM Users WHERE UserName = @username";
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(DeleteUser, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    int result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine($"{result} user was deleted succesfully. Antios, buddy!");
                    }
                    else
                    {
                        Console.WriteLine("Delete could not be done.");
                    }
                }
            }
        }
        */

        //LOGIC DELETE
        public void DeleteUser(string username)
        {
            const string UpdateUserDel = "UPDATE Users SET Deleted = 1 WHERE UserName=@username";
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(UpdateUserDel, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    int result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine($"{result} user was deleted succesfully");
                    }
                    else
                    {
                        Console.WriteLine("Change could not be done.");
                    }
                }
            }
        }


        //-------------------------METHODS FOR MESSAGES----------------------------
        //VIEW ALL MESSAGES FROM A PARTICULAR DATE
        public List<MessageView> GetAllMessages(string beginDate)
        {
            string SelectAllMessages = "SELECT M.MessageID, M.Title, M.MessageData, M.DateOfSubmission, " +
                "U1.UserName, U2.UserName FROM Messages M JOIN Users U1 ON M.SenderID=U1.UserID " +
                "JOIN Users U2 ON M.ReceiverID=U2.UserID WHERE M.DateOfSubmission>='"+ beginDate+"'";

            List<MessageView> Messages = new List<MessageView>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(SelectAllMessages, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MessageView message = new MessageView();
                                message.MessageID = reader.GetInt32(0);
                                message.Title = reader.GetString(1);
                                message.MessageData = reader.GetString(2);
                                message.DateOfSubmission = reader.GetDateTime(3);
                                message.SenderUserName = reader.GetString(4);
                                message.ReceiverUserName = reader.GetString(5);
                                Messages.Add(message);
                                i++;
                            }
                        }
                        if (i == 0)
                        {
                            Console.WriteLine("There are no messages");
                        }
                    }
                }
            }
            return Messages;
        }

        //READ INBOX
        public List<MessageView> Inbox(string username)
        {
            const string SelectInboxMessages = "SELECT M.Title, M.MessageData, M.DateOfSubmission, " +
                "U1.UserName FROM Messages M JOIN Users U1 ON M.SenderID=U1.UserID " +
                "JOIN Users U2 ON M.ReceiverID = U2.UserID WHERE U2.UserName=@username";

            List<MessageView> Messages = new List<MessageView>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(SelectInboxMessages, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MessageView message = new MessageView();
                                message.Title = reader.GetString(0);
                                message.MessageData = reader.GetString(1);
                                message.DateOfSubmission = reader.GetDateTime(2);
                                message.SenderUserName = reader.GetString(3);
                                Messages.Add(message);
                                i++;
                            }
                        }
                        if (i == 0)
                        {
                            Console.WriteLine("There are no messages");
                        }
                    }
                }
            }
            return Messages;
        }

        //READ SENT
        public List<MessageView> Sent(string username)
        {
            const string SelectSentMessages = "SELECT M.Title, M.MessageData, M.DateOfSubmission, " +
                "U1.UserName FROM Messages M JOIN Users U1 ON M.ReceiverID=U1.UserID " +
                "JOIN Users U2 ON M.SenderID = U2.UserID WHERE U2.UserName=@username";

            List<MessageView> Messages = new List<MessageView>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(SelectSentMessages, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MessageView message = new MessageView();
                                message.Title = (string)reader["Title"];
                                message.MessageData = (string)reader["MessageData"];
                                message.DateOfSubmission = (DateTime)reader["DateOfSubmission"];
                                message.ReceiverUserName = (string)reader["UserName"];
                                Messages.Add(message);
                                i++;
                            }
                        }
                        if (i == 0)
                        {
                            Console.WriteLine("There are no messages");
                        }
                    }
                }
            }
            return Messages;
        }

        //CREATE A MESSAGE
        public void CreateMessage(Message NewMessage)
        {
            const string InsertMessage = "INSERT INTO Messages" +
                " (Title, MessageData, DateOfSubmission, SenderID, ReceiverID) " +
                "VALUES (@Title,@MessageData,@DateOfSubmission,@SenderID,@ReceiverID)";
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(InsertMessage, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.Parameters.AddWithValue("@Title", NewMessage.Title);
                        command.Parameters.AddWithValue("@MessageData", NewMessage.MessageData);
                        command.Parameters.AddWithValue("@DateOfSubmission", NewMessage.DateOfSubmission);
                        command.Parameters.AddWithValue("@SenderID", NewMessage.SenderID);
                        command.Parameters.AddWithValue("@ReceiverID", NewMessage.ReceiverID);
                        int result = command.ExecuteNonQuery();
                        Console.WriteLine($"{result} message added succesfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        //Get MessageID
        public int GetID(Message myMessage)
        {
            const string SelectID = "SELECT MessageID FROM Messages WHERE Title=@Title AND " +
                "MessageData=@MessageData AND DateOfSubmission=@DateOfSubmission AND " +
                "SenderID=@SenderID AND ReceiverID=@ReceiverID";
            int myMessageID = 0;
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(SelectID, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = myMessage.Title;
                    command.Parameters.Add("@MessageData", SqlDbType.NVarChar).Value = myMessage.MessageData;
                    command.Parameters.Add("@DateOfSubmission", SqlDbType.DateTime).Value = myMessage.DateOfSubmission;
                    command.Parameters.Add("@SenderID", SqlDbType.Int).Value = myMessage.SenderID;
                    command.Parameters.Add("@ReceiverID", SqlDbType.Int).Value = myMessage.ReceiverID;
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                 myMessageID = (int)reader["MessageID"];
                            }
                        }
                       
                    }
                }
            }
            return myMessageID;
        }

        //Check if the given ID exists in table Messages
        public bool checkID(int messageID)
        {
            string SelectByID = "SELECT * FROM Messages WHERE MessageID=@MessageID";
            bool exists = false;
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(SelectByID, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@MessageID", SqlDbType.Int).Value = messageID;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                exists = true;
                            }
                        }
                    }
                }
            }
            return exists;
        }

        //UPDATE A MESSAGE (EDIT A FIELD AT A TIME)
        public void UpdateMessage(int messageID, string field, string value)
        {
            string UpdateMessage = "UPDATE Messages SET DateOfSubmission = GETDATE(), " + field + "=@value WHERE MessageID=@MessageID";
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(UpdateMessage, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@value", SqlDbType.NVarChar).Value = value;
                    command.Parameters.Add("@MessageID", SqlDbType.Int).Value = messageID;
                    int result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine($"{result} message was updated succesfully");
                    }
                    else
                    {
                        Console.WriteLine("Change could not be done.");
                    }
                }
            }
        }

        public void UpdateMessage(int messageID, string field, int value)
        {
            string UpdateMessage = "UPDATE Messages SET DateOfSubmission=GETDATE(), " + field + "= @value WHERE MessageID = @MessageID";
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(UpdateMessage, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@value", SqlDbType.Int).Value = value;
                    command.Parameters.Add("@MessageID", SqlDbType.Int).Value = messageID;
                    int result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine($"{result} message was updated succesfully");
                    }
                    else
                    {
                        Console.WriteLine("Change could not be done.");
                    }
                }
            }
        }

        //DELETE A MESSAGE
        public void DeleteMessage(int messageID)
        {
            const string DeleteMessage = "DELETE FROM Messages WHERE MessageID = @messageID";
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(DeleteMessage, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.Add("@MessageID", SqlDbType.Int).Value = messageID;
                    int result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine($"{result} message was deleted succesfully.");
                    }
                    else
                    {
                        Console.WriteLine("Delete could not be done.");
                    }
                }
            }
        }
    }
}
