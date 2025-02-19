namespace QuizApp.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
       
        public ICollection<Question> Question { get; set; }
        public List<UserQuiz> UserQuiz { get; set; }
    }
}
