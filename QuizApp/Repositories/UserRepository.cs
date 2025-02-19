using System.Xml.XPath;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Data.DTO;
using QuizApp.Entities;
using QuizApp.Repositories.Interfaces;

namespace QuizApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User?> ValidateUser(string email, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
        public async Task<User> AddUser(User user)
        {
            user.CreatedDate = DateTime.Now;
            user.IsDeleted = false;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var userList = await _context.Users.ToListAsync();
            return userList;
        }

        public async Task<User?> GetUserId(int id)
        {
            var getUser = await _context.Users.FindAsync(id);
            if (getUser == null)
            {
                return null;
            }
            return getUser;
        }

        public async Task<User?> UpdateUser(int id, User user)
        {
            var currentUser = await _context.Users.FindAsync(id);
            if (currentUser == null)
            {
                return null;
            }


            //currentUser.Email = user.Email;
            if (!string.IsNullOrWhiteSpace(user.Name))
            {
                currentUser.Name = user.Name;
            }
            //currentUser.Email = user.Email;
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                currentUser.Email = user.Email;
            }

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                currentUser.Password = user.Password;
            }

            //currentUser.Email = user.Email;
            //currentUser.Password = user.Password;
            currentUser.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return currentUser;
        }
    }
}
