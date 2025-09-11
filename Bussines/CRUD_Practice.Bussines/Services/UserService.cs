using CRUD_Practice.Bussines.Interfaces;
using CRUD_Practice.Domain.models;
using CRUD_Practice.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Practice.Bussines.Services
{
    public class UserService : IUserService
    {
        private readonly IDBRepository _dBRepository;
        public UserService(IDBRepository dBRepository) {
            _dBRepository = dBRepository;
        }
        public async Task CreateUser(UserModel userModel)
        {
            try
            {
                //CreateUser -> método definido en el repositorio, encargado de insertar el usuario en la base de datos.
                await _dBRepository.CreateUser(userModel);
            }
            catch (Exception)
            {
                Log.Error($"Error: {JsonConvert.SerializeObject(userModel)}");
                throw;
            }
            
        }
        public async Task<UserModel> GetById(string userId)
        {
            var user = await _dBRepository.GetById(userId);
            try
            {
                if(user == null)
                {
                    throw new KeyNotFoundException($"User not found. {userId}");
                }
            }
            catch (KeyNotFoundException)
            {
                Log.Error("There was an error finding the user by id");
            }
            return user;
        }
    }
}
