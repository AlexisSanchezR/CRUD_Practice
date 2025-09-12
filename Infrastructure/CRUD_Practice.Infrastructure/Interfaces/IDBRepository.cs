using CRUD_Practice.Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Practice.Infrastructure.Interfaces
{
    public interface IDBRepository
    {
        public Task CreateUser(UserModel userModel);
        public Task<UserModel> GetById(string userId);
        public Task<List<UserModel>> GetAll();
        public Task<bool> UpdateUser(UserModel user);
    }
}
