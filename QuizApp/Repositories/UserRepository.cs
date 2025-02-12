using QuizApp.Data;
using QuizApp.Entities;
using QuizApp.Repositories.Interfaces;

namespace QuizApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<User> AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User?> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> UpdateUser(int id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
