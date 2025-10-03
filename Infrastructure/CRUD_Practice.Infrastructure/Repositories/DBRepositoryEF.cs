using CRUD_Practice.Domain.models;
using CRUD_Practice.Infrastructure.Data;
using CRUD_Practice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Practice.Infrastructure.Repositories
{
    public class DBRepositoryEF : IDBRepositoryEF
    {
        private readonly AppDbContext _context;
        public DBRepositoryEF(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateUser (UserModel user)
        {
            await _context.Users.AddAsync(user);  //Insert
            await _context.SaveChangesAsync();     //Aplica cambios en bd
        }

        public async Task<UserModel> GetUserById(string userId)
        {
                return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateUser (UserModel user)
        {
            //valida que el usuario exista por id
            var userId = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (userId == null)
            {
                return false;
            }

            //Actualiza propiedades
            userId.Username = user.Username;
            userId.Userlastname = user.Userlastname;
            userId.Password = user.Password;
            userId.Email = user.Email;
            userId.Phone = user.Phone;

            await _context.SaveChangesAsync(); //ef genera Update. 
            return true;
        }

        public async Task<bool> DeleteUser (string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user); //ef genera el Delete.
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
