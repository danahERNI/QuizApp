using QuizApp.Entities;

namespace QuizApp.Repositories.Interfaces
{
    public interface IChoiceRepository
    {
        public Task<IEnumerable<Choice>> GetAllChoices();
        public Task<Choice?> GetChoiceId(int id);
        public Task<Choice?> AddChoice(Choice choice);
        public Task<Choice?> UpdateChoice(int id, Choice choice);
        public Task<Choice?> DeleteChoice(int id);
    }
}
