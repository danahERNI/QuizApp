using QuizApp.Data;
using QuizApp.Entities;
using QuizApp.Repositories.Interfaces;

namespace QuizApp.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;

        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<Quiz> AddQuiz(Quiz quiz)
        {
            throw new NotImplementedException();
        }

        public Task<Quiz?> DeleteQuiz(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Quiz>> GetAllQuizzes()
        {
            throw new NotImplementedException();
        }

        public Task<Quiz?> GetQuizId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Quiz?> UpdateQuiz(int id, Quiz quiz)
        {
            throw new NotImplementedException();
        }
    }
}
