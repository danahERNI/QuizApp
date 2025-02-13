using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Entities;
using QuizApp.Repositories.Interfaces;

namespace QuizApp.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public QuizRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Quiz> AddQuiz(Quiz quiz)
        {
            quiz.CreatedDate = DateTime.Now;
            quiz.IsDeleted = false;
            _context.Add(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }


        public async Task<Quiz?> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null) 
            { 
                return null;
            }
            quiz.IsDeleted = true;
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizzes()
        {
            var quizList = await _context.Quizzes.ToListAsync();
            return quizList;
        }

        public async Task<Quiz?> GetQuizId(int id)
        {
            var getQuiz = await _context.Quizzes
                .Include(q => q.Question)
                .ThenInclude(c => c.Choices)
                .FirstOrDefaultAsync(q => q.Id == id);
            return getQuiz;
        }

        public async Task<Quiz?> UpdateQuiz(int id, Quiz quiz)
        {
            var currentQuiz = await _context.Quizzes.FindAsync(id);

            if (currentQuiz == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(quiz.Title))
            {
                currentQuiz.Title = quiz.Title;
            }

            currentQuiz.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return currentQuiz;
        }

    }
}
