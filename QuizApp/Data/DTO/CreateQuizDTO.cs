using QuizApp.Entities;

namespace QuizApp.Data.DTO
{
    public class CreateQuizDTO
    {
        public string Title { get; set; }
        public List<CreateQuestionDTO> QuestionDTO { get; set; }
    }
}
