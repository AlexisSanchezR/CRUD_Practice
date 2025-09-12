using CRUD_Practice.Domain.models;
using CRUD_Practice.Infrastructure.Cliente;
using CRUD_Practice.Infrastructure.Interfaces;
using Npgsql;
using Serilog;

namespace CRUD_Practice.Infrastructure.Repositories
{
    public class DBRepository : IDBRepository
    {
        private readonly IDBClient _client;
        public DBRepository(IDBClient client)
        {
            _client = client;
        }
        public async Task CreateUser(UserModel userModel)
        {
            var connection = await _client.GetConnection();
            var sql = $"INSERT INTO \"CRUD\" (\"Id\", \"Username\", \"Userlastname\", \"Email\", \"Password\", \"Phone\") VALUES (@Id,@Username,@Userlastname,@Email,@Password,@Phone)";
            using (var cmd = new NpgsqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@Id", userModel.Id);
                cmd.Parameters.AddWithValue("@Username", userModel.Username);
                cmd.Parameters.AddWithValue("@Userlastname", userModel.Userlastname);
                cmd.Parameters.AddWithValue("@Email", userModel.Email);
                cmd.Parameters.AddWithValue("@Password", userModel.Password);
                cmd.Parameters.AddWithValue("@Phone", userModel.Phone);
                await cmd.ExecuteNonQueryAsync();
            }

        }
        public async Task<UserModel> GetById(string userId)
        {
            var connection = await _client.GetConnection();
            string sql = "SELECT \"Id\", \"Username\", \"Userlastname\", \"Email\", \"Phone\" FROM \"CRUD\" WHERE \"Id\" = @id";
            using (var cmd = new NpgsqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("id", userId);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync()) // Si hay fila
                    {
                        string Id = reader.GetString(0);
                        string Username = reader.GetString(1);
                        string Userlastname = reader.GetString(2);
                        string Email = reader.GetString(3);
                        string Phone = reader.GetString(4);

                        // Crear y retornar el modelo
                        return new UserModel
                        {
                            Id = Id,
                            Username = Username,
                            Userlastname = Userlastname,
                            Email = Email,
                            Phone = Phone
                        };
                    }
                    else
                    {
                        Log.Error("There was an error finding the user by id");
                        return null; // Retorna null si no hay usuario
                    }
                }

            }
        }
        public async Task<List<UserModel>> GetAll()
        {
            var users = new List<UserModel>();
            var connection = await _client.GetConnection();

            string sql = "SELECT \"Id\", \"Username\", \"Userlastname\", \"Email\", \"Phone\" FROM \"CRUD\"";

            using (var cmd = new NpgsqlCommand(sql, connection))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    users.Add(new UserModel
                    {
                        Id = reader.GetString(0),
                        Username = reader.GetString(1),
                        Userlastname = reader.GetString(2),
                        Email = reader.GetString(3),
                        Phone = reader.GetString(4),

                    });
                }
            }
            return users;
        }
        public async Task<bool> UpdateUser (UserModel user)
        {
            var connection = await _client.GetConnection();

            var sql = @"UPDATE ""CRUD"" SET ""Username"" = @Username, ""Userlastname"" = @Userlastname, ""Email"" = @Email, ""Password"" = @Password, ""Phone"" = @Phone WHERE ""Id"" = @Id";
             using ( var cmd = new NpgsqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("Id", user.Id);
                cmd.Parameters.AddWithValue ("Username", user.Username);
                cmd.Parameters.AddWithValue("Userlastname", user.Userlastname);
                cmd.Parameters.AddWithValue("Password", user.Password);
                cmd.Parameters.AddWithValue("Email", user.Email);
                cmd.Parameters.AddWithValue("Phone", user.Phone);

                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
        public async Task<bool> DeleteUser (string userId)
        {
            var connection = await _client.GetConnection();
            string sql = @"DELETE FROM ""CRUD"" WHERE ""Id"" = @Id";

            using (var cmd = new NpgsqlCommand(sql, connection)) 
            {
                cmd.Parameters.AddWithValue("Id", userId);
                //ExecuteNonQueryAsync() -> Devuelve en numero entero, la cantidad de filas afectadas
                                            //si no encuentra ese id, devuelve 0 pero si encuentra el id
                                            //ejecuta el comando sql y retorna 1.
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                //1=true, 0=false.
                return rowsAffected > 0;
            }
        }
    }
}