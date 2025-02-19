using Microsoft.AspNetCore.JsonPatch;
using QuizApp.Data.DTO;
using QuizApp.Entities;

namespace QuizApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User?> GetUserId(int id);
        public Task<User> AddUser(User user);
        public Task<User?> UpdateUser(int id, User user);
        public Task<User?> DeleteUser(int id);
        public Task<User?> ValidateUser(string email, string password);
    }
}
