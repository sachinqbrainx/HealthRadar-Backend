using System.ComponentModel.DataAnnotations;

namespace Revenue.CommandModel
{
    public class DecisionMakingSurveyCommandModel
    {
        [Key]
        public Guid QuestionId  { get; set; }
        [Required]
        [StringLength(500)]
        public string Question { get; set; }
    
        public List<DecisionMakingSurveyOptions> options { get; set; }

    }

    public class DecisionMakingSurveyOptions
    {
        [Key]
        public Guid Id { get; set; }
       
        public string Text { get; set; }
    }

   
    
}
