using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Data.DTO;
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
            var quizList = await _context.Quizzes
                .Where(q => q.IsDeleted == false)
                .ToListAsync();
            return quizList;
        }

        public async Task<ICollection<Quiz?>> GetQuizByMentorId(int id)
        {
            var getQuiz = await _context.Quizzes
                .Where(q => q.IsDeleted == false && q.UserId == id)
                .ToListAsync();
            return getQuiz;
        }

        public async Task<Quiz?> GetQuizId(int id)
        {
            var getQuiz = await _context.Quizzes
                .Include(q => q.Question)
                .ThenInclude(c => c.Choices)
                .FirstOrDefaultAsync(q => q.Id == id);
            return getQuiz;
        }

        public async Task<Quiz?> UpdateQuiz(int id, Quiz quizDto)
        {
            var currentQuiz = await _context.Quizzes
                .Include(q => q.Question)
                .ThenInclude(q => q.Choices)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (currentQuiz == null)
            {
                return null;
            }


            if (!string.IsNullOrWhiteSpace(quizDto.Title))
            {
                currentQuiz.Title = quizDto.Title;
            }

            var existingQuestionIds = currentQuiz.Question.Select(q => q.QuestionId).ToList();
            var newQuestions = new List<Question>();

            foreach (var questionDto in quizDto.Question)
            {
                var existingQuestion = currentQuiz.Question.FirstOrDefault(q => q.QuestionId == questionDto.QuestionId);

                if (existingQuestion != null)
                {

                    existingQuestion.Body = questionDto.Body;

                    var existingChoiceIds = existingQuestion.Choices.Select(c => c.Id).ToList();
                    var newChoices = new List<Choice>();

                    foreach (var choiceDto in questionDto.Choices)
                    {
                        var existingChoice = existingQuestion.Choices.FirstOrDefault(c => c.Id == choiceDto.Id);
                        if (existingChoice != null)
                        {

                            existingChoice.Name = choiceDto.Name;
                            existingChoice.IsCorrect = choiceDto.IsCorrect;
                        }
                        else
                        {

                            newChoices.Add(new Choice
                            {
                                Name = choiceDto.Name,
                                IsCorrect = choiceDto.IsCorrect,
                                QuestionId = existingQuestion.QuestionId
                            });
                        }
                    }


                    var choicesToRemove = existingQuestion.Choices
                        .Where(c => !questionDto.Choices.Any(cd => cd.Id == c.Id))
                        .ToList();
                    _context.Choices.RemoveRange(choicesToRemove);


                    if (newChoices.Any())
                    {
                        _context.Choices.AddRange(newChoices);
                    }
                }
                else
                {

                    var newQuestion = new Question
                    {
                        Body = questionDto.Body,
                        QuizId = currentQuiz.Id,
                        Choices = questionDto.Choices.Select(c => new Choice
                        {
                            Name = c.Name,
                            IsCorrect = c.IsCorrect
                        }).ToList()
                    };
                    newQuestions.Add(newQuestion);
                }
            }

            var questionsToRemove = currentQuiz.Question
                .Where(q => !quizDto.Question.Any(qd => qd.QuestionId == q.QuestionId))
                .ToList();
            _context.Questions.RemoveRange(questionsToRemove);

            if (newQuestions.Any())
            {
                _context.Questions.AddRange(newQuestions);
            }

            currentQuiz.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return currentQuiz;
        }


    }
}
