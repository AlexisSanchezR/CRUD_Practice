using CRUD_Practice.Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Practice.Infrastructure.Interfaces
{
    public interface IDBRepositoryEF
    {
        public Task CreateUser(UserModel user);
        public Task<UserModel> GetUserById(string userId);
        public Task<List<UserModel>> GetAll();
        public Task<bool> UpdateUser(UserModel user);
        public Task<bool> DeleteUser(string userId);
    }
}
