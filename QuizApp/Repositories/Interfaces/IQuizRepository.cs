using QuizApp.Entities;

namespace QuizApp.Repositories.Interfaces
{
    public interface IQuizRepository
    {
        public Task<IEnumerable<Quiz>> GetAllQuizzes();
        public Task<Quiz?> GetQuizId(int id);
        public Task<Quiz> AddQuiz(Quiz quiz);
        public Task<Quiz?> UpdateQuiz(int id, Quiz quiz);
        public Task<Quiz?> DeleteQuiz(int id);
        public Task<ICollection<Quiz?>> GetQuizByMentorId(int id);
        public Task<UserQuiz?> RecordQuizScore(UserQuiz userQuiz);
    }
}
