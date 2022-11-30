using Npgsql;
using TalentSurvey.CommandModel;

namespace TalentSurvey.Commands.Interface
{
    public interface ITalentSurveyCommand
    {
        Task<List<QuestionCommandModel>> GetAllQuestionAsync();
        Task<List<QuestionCommandModel>> GetQuestionByIdAsync(Guid Id);
        Task<List<UserResponse>> GetAllTotalAsync();
        Task<bool> AddResponseAsync(TalentSurveyCommandModel model);
       
    }
}
