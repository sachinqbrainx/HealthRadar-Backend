using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Npgsql;
using Revenue.CommandModel;
using Revenue.Commands.Interface;
using Revenue.DBContexts;
using System.Collections.Generic;

namespace Revenue.Commands.Handlers
{
    public class QuestionCommand: IQuestionCommand
    {
        private readonly RevenueDBContext _dbContext;
        public IConfiguration Configuration { get; }
        public QuestionCommand(RevenueDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            Configuration = configuration;
        }

     

        public async Task<bool> AddDecisionMakingSurveyQuestionAsync(DecisionMakingSurveyCommandModel model)
        {
            bool result = false;
            using (var connection = new NpgsqlConnection(this.Configuration.GetConnectionString("RevenueConnection")))
            {
                var QuestionGuid = Guid.NewGuid();
                var OptionGuid = Guid.NewGuid();
                var isSuccess = await connection.ExecuteAsync
                (@"INSERT INTO public.""DecisionMakingSurveyQuestion""(""QuestionId"", ""Question"") VALUES (@QuestionId, @Question)",
                new
                {
                    QuestionId = QuestionGuid,
                    Question = model.Question,
                    

                });


                if (isSuccess == 1)
                {
                    await InsertDecisionMakingSurveyOption(connection, model.options, QuestionGuid);
                    result = true;
                }
                else
                    result = false;
            }

            return result;
        }
        public async Task InsertDecisionMakingSurveyOption(NpgsqlConnection? connection, List<DecisionMakingSurveyOptions> options, Guid QuestionGuid)
        {

            foreach(DecisionMakingSurveyOptions option in options)
            { 

            await connection.ExecuteAsync
                (@"INSERT INTO public.""DecisionMakingSurveyOptions""(""Id"", ""Text"",""DecisionMakingSurveyCommandModelQuestionId"") VALUES (@Id, @Text,@DecisionMakingSurveyCommandModelQuestionId);",
                new
                {
                    Id = Guid.NewGuid(),
                    Text = option.Text,
                    DecisionMakingSurveyCommandModelQuestionId = QuestionGuid,
                });
             }

        }

        public async Task<bool> AddTalentSurveyQuestionAsync(TalentSurveyCommandModel model)
        {
            bool result = false;
            using (var connection = new NpgsqlConnection(this.Configuration.GetConnectionString("RevenueConnection")))
            {
               
                var QuestionGuid = Guid.NewGuid();
                var isSuccess = await connection.ExecuteAsync
                (@"INSERT INTO public.""TalentSurveyQuestion""(""QuestionId"", ""Question"") VALUES (@QuestionId, @Question)",
                new
                {
                    QuestionId = QuestionGuid,
                    Question = model.Question,
                   

                });


                if (isSuccess == 1)
                {
                    await InsertTalentSurveyQuestion(connection, model.options, QuestionGuid);
                    result = true;
                }
                else
                    result = false;
            }

            return result;
        }

        public async Task InsertTalentSurveyQuestion(NpgsqlConnection? connection, List<TalentSurveyOptions> options, Guid QuestionGuid)
        {


            foreach (TalentSurveyOptions option in options)
            {

                await connection.ExecuteAsync
                (@"INSERT INTO public.""TalentSurveyOptions""(""Id"", ""Text"",""TalentSurveyCommandModelQuestionId"") VALUES (@Id, @Text,@TalentSurveyCommandModelQuestionId);",
                new
                {
                    Id = Guid.NewGuid(),
                    Text=option.Text,
                    TalentSurveyCommandModelQuestionId=QuestionGuid,    

                });
            }

        }


        public async Task<List<Chart>> GetPercentageAsync()
        {
            //           using var connection = new NpgsqlConnection(this.Configuration.GetConnectionString("TalentConnection"));

            //           var NoOfDotnet = (await connection.QuerySingle
            //              (@"SELECT ""RespondedAnswer"", COUNT(""RespondedAnswer"")
            //FROM public.""ResponsedAnswer"" GROUP BY ""RespondedAnswer"" HAVING ""RespondedAnswer"" = '.Net'"));

            //           var NoOfReact = (await connection.QuerySingle
            //              (@"SELECT ""RespondedAnswer"", COUNT(""RespondedAnswer"")
            //FROM public.""ResponsedAnswer"" GROUP BY ""RespondedAnswer"" HAVING ""RespondedAnswer"" = 'React'"));
            //           var NoOfScrum = (await connection.QuerySingle
            //              (@"SELECT ""RespondedAnswer"", COUNT(""RespondedAnswer"")
            //FROM public.""ResponsedAnswer"" GROUP BY ""RespondedAnswer"" HAVING ""RespondedAnswer"" = 'Scrum Master'"));

            //           var TotalResourse = (NoOfDotnet + NoOfReact + NoOfScrum);

            //           var PercentageOfnet = (NoOfDotnet / TotalResourse) * 100;

            //           var PercentageOfReact = (NoOfReact / TotalResourse) * 100;

            //           var PercentageOfScrum = (NoOfScrum / TotalResourse) * 100;

            //           var analysis = new List<Chart>() {
            //               new Chart(){ Department=".NET",
            //               NoOfPerson= Convert.ToString(NoOfDotnet),
            //               Text = Convert.ToString(PercentageOfnet)},
            //               new Chart(){  Department = "React",
            //               NoOfPerson = Convert.ToString(NoOfReact),
            //               Text = Convert.ToString(PercentageOfReact)},
            //               new Chart(){Department = "Scrum Master",
            //               NoOfPerson = Convert.ToString(NoOfScrum),
            //               Text = Convert.ToString(PercentageOfScrum)}

            //           };

            ////  PieCommandModel result =(
            ////  new PieCommandModel()
            ////  {
            ////      Department=".NET",
            ////      NoOfPerson= Convert.ToString(NoOfDotnet),
            ////      Text = Convert.ToString(PercentageOfnet)
            ////  },
            ////  new PieCommandModel()
            ////  {
            ////      Department = "React",
            ////      NoOfPerson = Convert.ToString(NoOfReact),
            ////      Text = Convert.ToString(PercentageOfReact)
            ////  },
            //// new PieCommandModel()
            ////  {
            ////      Department = "Scrum Master",
            ////      NoOfPerson = Convert.ToString(NoOfScrum),
            ////      Text = Convert.ToString(PercentageOfScrum)
            ////  }
            ////);




            //return analysis;

            return new List<Chart>();
        }

    }
}
