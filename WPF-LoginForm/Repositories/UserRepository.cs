using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{

    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository()
        {
            CreateDefaultUser(); // cria admin se não existir
        }

        //essa parte serve para ascesarmos a tabela user do banco de dados,e cadastrar um usuário padrão, caso ele não exista.
        private void CreateDefaultUser()
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = "SELECT COUNT(*) FROM [User] WHERE Username='admin'";
                int count = (int)command.ExecuteScalar();

                if (count == 0)
                {
                    command.CommandText = @"INSERT INTO [User]
                                            (Username, Password, Name, LastName, Email)
                                            VALUES ('admin','123','Admin','System','admin@gmail.com')";
                    command.ExecuteNonQuery();
                }
            }
        }
        //essa parte serve para cadastrar um usuário, recebendo um objeto do tipo UserModel, e inserindo os dados na tabela user do banco de dados.
        public void Add(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = @"INSERT INTO [User]
                                        (Username, Password, Name, LastName, Email)
                                        VALUES (@username,@password,@name,@lastname,@email)";

                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = userModel.Username;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = userModel.Password;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = userModel.Name;
                command.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = userModel.LastName;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = userModel.Email;

                command.ExecuteNonQuery();
            }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = @"SELECT COUNT(*) FROM [User]
                                        WHERE Username=@username AND Password=@password";

                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;

                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        public void Edit(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = @"UPDATE [User]
                                        SET Username=@username,
                                            Password=@password,
                                            Name=@name,
                                            LastName=@lastname,
                                            Email=@email
                                        WHERE Id=@id";

                command.Parameters.Add("@id", SqlDbType.Int).Value = userModel.Id;
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = userModel.Username;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = userModel.Password;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = userModel.Name;
                command.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = userModel.LastName;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = userModel.Email;

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<UserModel> GetByAll()
        {
            var users = new List<UserModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT * FROM [User]", connection))
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new UserModel
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = reader[2].ToString(),
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString()
                        });
                    }
                }
            }

            return users;
        }

        public UserModel GetById(int id)
        {
            UserModel user = null;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = "SELECT * FROM [User] WHERE Id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = reader[2].ToString(),
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString()
                        };
                    }
                }
            }

            return user;
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = "SELECT * FROM [User] WHERE Username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString()
                        };
                    }
                }
            }

            return user;
        }

        public void Remove(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = "DELETE FROM [User] WHERE Id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                command.ExecuteNonQuery();
            }
        }
    }
}
