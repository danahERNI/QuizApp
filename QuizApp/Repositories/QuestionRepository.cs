using QuizApp.Data;
using QuizApp.Entities;
using QuizApp.Repositories.Interfaces;

namespace QuizApp.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<Question> AddQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<Question?> DeleteQuestion(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Question>> GetAllQuestions()
        {
            throw new NotImplementedException();
        }

        public Task<Question?> GetQuestionId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Question?> UpdateQuestion(int id, Question question)
        {
            throw new NotImplementedException();
        }
    }
}
