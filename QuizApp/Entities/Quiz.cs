namespace QuizApp.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<UserQuiz> UserQuiz { get; set; }
    }
}
