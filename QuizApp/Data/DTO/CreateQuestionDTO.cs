using QuizApp.Entities;

namespace QuizApp.Data.DTO
{
    public class CreateQuestionDTO
    {
        public string Body { get; set; }
        //public int ChoiceId {  get; set; }
        public List<CreateChoiceDTO> Choices { get; set; }
    }
}
