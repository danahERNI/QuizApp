using QuizApp.Entities;

namespace QuizApp.Repositories.Interfaces
{
    public interface IAnswerRepository
    {
        public Task<IEnumerable<Answer>> GetAllAnswers();
        public Task<Answer?> GetAnswerId(int id);
        public Task<Answer> AddAnswer(Answer answer);
        public Task<Answer?> UpdateAnswer(int id, Answer answer);
        public Task<Answer?> DeleteAnswer(int id);
    }
}
