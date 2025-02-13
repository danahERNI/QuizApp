using System.ComponentModel.DataAnnotations;

namespace QuizApp.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Body { get; set; }

        [Required]
        public int QuizId { get; set; }
        public List<Choice> Choices { get; set; }
        //public Quiz Quiz { get; set; }
    }
}
