using System.ComponentModel.DataAnnotations;

namespace Revenue.CommandModel
{
    public class TalentSurveyCommandModel
    {
        [Key]
        public Guid QuestionId  { get; set; }
        [Required]
        [StringLength(500)]
        public string Question { get; set; }

       
        public List<TalentSurveyOptions> options { get; set; }
       
   

    }

 
    public class TalentSurveyOptions
    {
        [Key]
        public Guid Id { get; set; }

        public String Text { get; set; }
    }

}
