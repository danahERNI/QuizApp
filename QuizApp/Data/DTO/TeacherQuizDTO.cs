using QuizApp.Entities;

namespace QuizApp.Data.DTO
{
    public class TeacherQuizDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        //public int UserId { get; set; }

        //public ICollection<Question> Question { get; set; }
        //public List<UserQuiz> UserQuiz { get; set; }
    }
}
