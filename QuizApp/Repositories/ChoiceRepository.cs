using QuizApp.Data;
using QuizApp.Entities;
using QuizApp.Repositories.Interfaces;

namespace QuizApp.Repositories
{
    public class ChoiceRepository : IChoiceRepository
    {
        private readonly AppDbContext _context;

        public ChoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Choice?> AddChoice(Choice choice)
        {
            throw new NotImplementedException();
        }

        public Task<Choice?> DeleteChoice(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Choice>> GetAllChoices()
        {
            throw new NotImplementedException();
        }

        public Task<Choice?> GetChoiceId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Choice?> UpdateChoice(int id, Choice choice)
        {
            throw new NotImplementedException();
        }
    }
}
