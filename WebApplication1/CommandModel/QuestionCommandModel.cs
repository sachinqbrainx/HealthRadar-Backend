using System.ComponentModel.DataAnnotations;

namespace DecisionMakingSurvey.CommandModel
{
    public class QuestionCommandModel
    {

        [Key]
        public Guid QuestionId { get; set; }
        [Required]
        [StringLength(500)]
        public string Question { get; set; }

        public List<DecisionMakingSurveyOptions> options { get; set; }


    }


  
    public class DecisionMakingSurveyQuestions
    {

        public Guid QuestionId { get; set; }
        public string Question { get; set; }
        public Guid Id { get; set; }
        public string Text { get; set; }

    }
  


    public class DecisionMakingSurveyOptions
    {
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }

    }
}
