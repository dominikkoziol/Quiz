using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QuizLogic.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string QuestionContent { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public int CorrectAnswer { get; set; }
        public QuestionLevelEnum QuestionLevel { get; set; }
        /// <summary>
        /// Zaznaczona odpowiedź posiada atrybut [NotMapped] ponieważ nie będzie przechowywana w bazie danych
        /// </summary>
        [NotMapped]
        public int MarkedAnswer { get; set; }
        /// <summary>
        /// Poziom trudności pytań
        /// </summary>
        public enum QuestionLevelEnum
        {
            [Description("Najprostsze")]
            Easy = 0,
            [Description("Średnio trudne")]
            Medium = 1,
            [Description("Trudne")]
            Hard = 2
        }
    }
}
