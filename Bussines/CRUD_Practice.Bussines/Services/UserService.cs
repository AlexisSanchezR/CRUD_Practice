using CRUD_Practice.Bussines.Interfaces;
using CRUD_Practice.Domain.models;
using CRUD_Practice.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Serilog;

namespace CRUD_Practice.Bussines.Services
{
    public class UserService : IUserService
    {
        private readonly IDBRepository _dBRepository;
        public UserService(IDBRepository dBRepository)
        {
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
                if (user == null)
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
        public async Task<List<UserModel>> GetAll()
        {
            try
            {
                return await _dBRepository.GetAll();
            }
            catch (Exception)
            {
                Log.Error("An error occurred while fetching all users.");
                throw;
            }

        }
        public async Task<bool> UpdateUser(string userId, UserModel updateUser)
        {
            var user = await _dBRepository.GetById(userId);
            try
            {
                if(user == null)
                {
                    throw new KeyNotFoundException($"user not found. {userId}");
                }
            }
            catch (KeyNotFoundException)
            {
                Log.Error("There was an error finding the user");
            }
            user.Username = updateUser.Username;
            user.Userlastname = updateUser.Userlastname;
            user.Password = updateUser.Password;
            user.Email = updateUser.Email;
            user.Phone = updateUser.Phone;

            await _dBRepository.UpdateUser(user);
            return true;
        }
    }
}
