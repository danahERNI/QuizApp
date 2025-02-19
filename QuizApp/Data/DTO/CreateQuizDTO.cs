using QuizApp.Entities;

namespace QuizApp.Data.DTO
{
    public class CreateQuizDTO
    {
        //public string Title { get; set; }
        //public int UserId { get; set; }
        //public List<CreateQuestionDTO> QuestionDTO { get; set; }
        public int? Id { get; set; }  // Nullable for new quizzes
        public string Title { get; set; }
        public int UserId { get; set; }
        public List<CreateQuestionDTO> QuestionDTO { get; set; }

    }
}
