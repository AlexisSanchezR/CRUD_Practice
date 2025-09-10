using CRUD_Practice.Infrastructure.Cliente;
using CRUD_Practice.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Practice.Infrastructure.Repositories
{
    public class DBRepository : IDBRepository
    {
        private readonly DBClient _client;
        public DBRepository(DBClient client)
        {
            _client = client;
        }

    }
}
