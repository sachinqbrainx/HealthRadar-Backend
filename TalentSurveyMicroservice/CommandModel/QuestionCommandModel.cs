using System.ComponentModel.DataAnnotations;

namespace TalentSurvey.CommandModel
{
    public class QuestionCommandModel
    {
         
        [Key]
        public Guid QuestionId { get; set; }
        [Required]
        [StringLength(500)]
        public string Question { get; set; }

  
        public List<TalentSurveyOptions> options { get; set; }
       

    }


    
    
    public class TalentSurveyQuestions
    {

        public Guid QuestionId { get; set; }
        public string Question { get; set; }
        public Guid Id { get; set; }
        public string Text { get; set; }


    }
  
    public class TalentSurveyOptions
    {
        [Key]
        public Guid Id { get; set; }
        public string Text { get; set; }

    }
}
