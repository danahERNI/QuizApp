﻿namespace QuizApp.Entities
{
    public class Choice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        //public Question Question { get; set; }
    }
}
