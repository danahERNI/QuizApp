namespace QuizApp.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public List<string> Choices { get; set; }
    }
}
