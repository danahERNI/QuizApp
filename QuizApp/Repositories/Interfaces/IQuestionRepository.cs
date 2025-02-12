using QuizApp.Entities;

namespace QuizApp.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        public Task<IEnumerable<Question>> GetAllQuestions();
        public Task<Question?> GetQuestionId(int id);
        public Task<Question> AddQuestion(Question question);
        public Task<Question?> UpdateQuestion(int id, Question question);
        public Task<Question?> DeleteQuestion(int id);
    }
}
