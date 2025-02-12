using QuizApp.Data;
using QuizApp.Entities;
using QuizApp.Repositories.Interfaces;

namespace QuizApp.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AppDbContext _context;

        public AnswerRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<Answer> AddAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }

        public Task<Answer?> DeleteAnswer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Answer>> GetAllAnswers()
        {
            throw new NotImplementedException();
        }

        public Task<Answer?> GetAnswerId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Answer?> UpdateAnswer(int id, Answer answer)
        {
            throw new NotImplementedException();
        }
    }
}
