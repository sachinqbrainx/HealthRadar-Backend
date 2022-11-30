using System.ComponentModel.DataAnnotations;


namespace DecisionMakingSurvey.CommandModel
{
    public class DecisionMakingSurveyCommandModel
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string UserName { get; set; }
   
        public List<ResponsedAnswer> Response { get; set; }

        public int Total { get; set; }
    }
    public class ResponsedAnswer
    {

        [Key]
        public Guid Id  { get; set; }
        public Guid QuestionId { get; set; }
        [Required]
        public string RespondedAnswer { get; set; }
    }

    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int Total { get; set; }
    }
}
