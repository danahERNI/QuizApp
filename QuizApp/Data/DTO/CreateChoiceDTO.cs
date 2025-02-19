namespace QuizApp.Data.DTO
{
    public class CreateChoiceDTO
    {
        //public string Name { get; set; }
        //public bool IsCorrect { get; set; }
        ////public int QuestionId { get; set; }
        public int? Id { get; set; }  // Nullable for new choices
        public string Name { get; set; }
        public bool IsCorrect { get; set; }
    }
}
