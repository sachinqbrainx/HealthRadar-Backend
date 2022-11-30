using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentSurvey.CommandModel;
using TalentSurvey.Commands.Handlers;
using TalentSurvey.Commands.Interface;

namespace TalentSurvey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalentSurveyController : ControllerBase
    {
        private readonly ITalentSurveyCommand _talentSurveyCommand;

        public TalentSurveyController(ITalentSurveyCommand talentSurveyCommand )
        {
            _talentSurveyCommand = talentSurveyCommand;
        }


        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TalentSurveyCommandModel Response)
        {
            try
            {
                var result = await _talentSurveyCommand.AddResponseAsync(Response);
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
        public async Task<ActionResult<TalentSurveyCommandModel>> GetAllTotal()
        {
            try
            {
                var Total = await _talentSurveyCommand.GetAllTotalAsync();
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
                var Questions = await _talentSurveyCommand.GetAllQuestionAsync();
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

        [HttpGet]
        [Route("{QuestionId}")]
        public async Task<ActionResult<QuestionCommandModel>> GetQuestionById(Guid QuestionId)
        {

            try
            {
                var Questions = await _talentSurveyCommand.GetQuestionByIdAsync(QuestionId);
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

        //[HttpGet]
        //[Route("Answers")]
        //public async Task<ActionResult<QuestionCommandModel>> GetQuestionwithAnswer()
        //{
        //    try
        //    {
        //        var QuestionwithAnswer = await _talentSurveyCommand.GetQuestionwithAnswerAsync();
        //        if (QuestionwithAnswer != null && QuestionwithAnswer.Count > 0)
        //        {
        //            return Ok(QuestionwithAnswer);
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status400BadRequest);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

    }
}
