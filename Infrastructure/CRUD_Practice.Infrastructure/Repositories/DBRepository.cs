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
            string sql = "SELECT \"Id\", \"Username\", \"Userlastname\", \"Email\", \"Phone\" FROM \"CRUD\" WHERE \"Id\" = @Id";
            using (var cmd = new NpgsqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("$Id", userId);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync()) // Si hay fila
                    {
                        string id = reader.GetString(0);
                        string Username = reader.GetString(1);
                        string Userlastname = reader.GetString(2);
                        string Email = reader.GetString(3);
                        string Phone = reader.GetString(4);

                        // Crear y retornar el modelo
                        return new UserModel
                        {
                            Id = id,
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
    }
}