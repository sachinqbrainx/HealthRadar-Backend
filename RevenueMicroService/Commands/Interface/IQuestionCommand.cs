using Microsoft.AspNetCore.Mvc;
using Revenue.CommandModel;

namespace Revenue.Commands.Interface
{
    public interface IQuestionCommand
    {
        Task<bool> AddTalentSurveyQuestionAsync(TalentSurveyCommandModel model);
        Task<bool> AddDecisionMakingSurveyQuestionAsync(DecisionMakingSurveyCommandModel model);
        Task<List<Chart>> GetPercentageAsync();
    }
}
