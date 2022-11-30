using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revenue.CommandModel;
using Revenue.Commands.Interface;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionCommand _questionCommand;

        public QuestionController(IQuestionCommand questionCommand)
        {
           _questionCommand = questionCommand;
        }

        [HttpPost]
        [Route("TalentSurvey")]
        public async Task<IActionResult> PostTalent([FromBody] TalentSurveyCommandModel Question)
        {
            try
            {
                var result = await _questionCommand.AddTalentSurveyQuestionAsync(Question);
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

        [HttpPost]
        [Route("DecisionMakingSurvey")]
        public async Task<IActionResult> PostDecisionMaking([FromBody] DecisionMakingSurveyCommandModel Question)
        {
            try
            {
                var result = await _questionCommand.AddDecisionMakingSurveyQuestionAsync(Question);
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
        public async Task<ActionResult<PieCommandModel>> GetPercentage()
        {
            try
            {
                var Total = await _questionCommand.GetPercentageAsync();
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


    }
}
