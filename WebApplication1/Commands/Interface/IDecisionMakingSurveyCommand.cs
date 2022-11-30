using Npgsql;
using DecisionMakingSurvey.CommandModel;

namespace DecisionMakingSurvey.Commands.Interface
{
    public interface IDecisionMakingSurveyCommand
    {

        Task<List<QuestionCommandModel>> GetAllQuestionAsync();
        Task<List<QuestionCommandModel>> GetQuestionByIdAsync(Guid Id);
        Task<List<UserResponse>> GetAllTotalAsync();
        Task<bool> AddResponseAsync(DecisionMakingSurveyCommandModel model);
       
    }
}
