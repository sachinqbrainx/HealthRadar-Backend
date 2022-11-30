using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DecisionMakingSurvey.CommandModel;
using DecisionMakingSurvey.Commands.Handlers;
using DecisionMakingSurvey.Commands.Interface;

namespace DecisionMakingSurvey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecisionMakingSurveyController : ControllerBase
    {
        private readonly IDecisionMakingSurveyCommand _decisionMakingSurveyCommand;

        public DecisionMakingSurveyController(IDecisionMakingSurveyCommand decisionMakingSurveyCommand)
        {
            _decisionMakingSurveyCommand = decisionMakingSurveyCommand;
        }


        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DecisionMakingSurveyCommandModel Response)
        {
            try
            {
                var result = await _decisionMakingSurveyCommand.AddResponseAsync(Response);
                if (result)
                    return StatusCode(StatusCodes.Status200OK);
                else
                    return StatusCode(StatusCodes.Status304NotModified);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult<DecisionMakingSurveyCommandModel>> GetAllTotal()
        {
            try
            {
                var Total = await _decisionMakingSurveyCommand.GetAllTotalAsync();
                if (Total != null && Total.Count > 0)
                {
                    return Ok(Total);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("AllQuestions")]
        public async Task<ActionResult<List<QuestionCommandModel>>> GetAllQuestion()
        {

            try
            {
                var Questions = await _decisionMakingSurveyCommand.GetAllQuestionAsync();
                return Ok(Questions);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{QuestionById}")]
        public async Task<ActionResult<QuestionCommandModel>> GetQuestionById(Guid QuestionById)
        {

            try
            {
                var Questions = await _decisionMakingSurveyCommand.GetQuestionByIdAsync(QuestionById);
                if (Questions != null && Questions.Count > 0)
                {
                    return Ok(Questions);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
