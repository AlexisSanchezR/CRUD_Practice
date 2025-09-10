using CRUD_Practice.Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Practice.Bussines.Interfaces
{
    public interface IUserService
    {
        public async Task CreateUser(UserModel userModel);
    }
}
