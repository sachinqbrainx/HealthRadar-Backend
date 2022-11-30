using System.ComponentModel.DataAnnotations;


namespace Revenue.CommandModel
{
    public class UserResponseCommandModel
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public List<ResponsedAnswer> Response { get; set; }

        [Required]
        public int Total { get; set; }
    }
    public class ResponsedAnswer
    {

        [Key]
        public Guid Id  { get; set; }
        public Guid QuestionId { get; set; }
        public string RespondedAnswer { get; set; }
    }

    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int Total { get; set; }
    }
}
