using CRUD_Practice.Infrastructure.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Practice.Infrastructure.Cliente
{
    public class DBClient : IDBClient
    {
        private readonly string _dbConnectionString;
        public DBClient(string dbConnectionString) {
            _dbConnectionString = dbConnectionString;
        }

        //Npgsql-> es la clase que representa una conexión a PostgreSQL en .NET (usando el paquete Npgsql)
        public async Task<NpgsqlConnection> GetConnection()
        {
            var connection = new NpgsqlConnection(_dbConnectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
