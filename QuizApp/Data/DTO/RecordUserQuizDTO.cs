using QuizApp.Entities;

namespace QuizApp.Data.DTO
{
    public class RecordUserQuizDTO
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        //public Quiz Quiz { get; set; }
        //public User User { get; set; }
    }
}
