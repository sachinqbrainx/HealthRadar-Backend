using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using DecisionMakingSurvey.CommandModel;
using DecisionMakingSurvey.Commands.Interface;
using DecisionMakingSurvey.DBContexts;

namespace DecisionMakingSurvey.Commands.Handlers
{
    public class DecisionMakingSurveyCommand : IDecisionMakingSurveyCommand
    {
        private readonly DecisionMakingSurveyDBContext _dbContext;
        public IConfiguration Configuration { get; }

        public DecisionMakingSurveyCommand(DecisionMakingSurveyDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            Configuration = configuration;
        }

        public async Task<List<UserResponse>> GetAllTotalAsync()
        {
            using var connection = new NpgsqlConnection(this.Configuration.GetConnectionString("DecisionMakingConnection"));
            return (await connection.QueryAsync<UserResponse>
            (@"SELECT ""UserId"",""UserName"", ""Total""
	            FROM public.""DecisionMakingSurveyTable""")).ToList();

        }


        public async Task<bool> AddResponseAsync(DecisionMakingSurveyCommandModel modal)
        { int total = 0;
            bool result = false;
            using (var connection = new NpgsqlConnection(this.Configuration.GetConnectionString("DecisionMakingConnection")))
            {
                var UserGuidId = Guid.NewGuid();
                //var Answers = (await(new NpgsqlConnection(this.Configuration.GetConnectionString("RevenueConnection"))).QueryAsync<QuestionCommandModel>
                //(@"SELECT ""QuestionId"",""Answer"" FROM public.""DecisionMakingSurveyQuestion""")).ToList();
             
                //foreach (var Answer in Answers)
                //{
                //    foreach (var response in modal.Response)
                //    {
                      
                //        if ((Answer.QuestionId == response.QuestionId) && (Answer.Answer == response.RespondedAnswer))
                //        {
                //            total++;
                //        }
                //    }
                //}

                var isSuccess = await connection.ExecuteAsync(@"INSERT INTO public.""DecisionMakingSurveyTable""(""UserId"",""UserName"", ""Total"")VALUES(@UserId,@UserName,@Total )",
                new
                {
                    UserId = UserGuidId,
                    UserName=modal.UserName,
                    Total = total
                });

                if (isSuccess == 1)
                {
                    await InsertResponseData(connection, modal.Response, UserGuidId);
                    result = true;
                }
                else
                    result = false;
            }

            return result;
        }

        public async Task InsertResponseData(NpgsqlConnection? connection, List<ResponsedAnswer> response,Guid id)
        {
            foreach (var res in response)
            {
                var gnewId = Guid.NewGuid();
                await connection.ExecuteAsync
                (@"INSERT INTO public.""ResponsedAnswer""(""Id"",""QuestionId"", ""RespondedAnswer"",""DecisionMakingSurveyCommandModelUserId"") VALUES(@Id,@QuestionId, @RespondedAnswer,@DecisionMakingSurveyCommandModelUserId)",
                new
                {
                    Id = gnewId,
                    QuestionId = res.QuestionId,
                    RespondedAnswer = res.RespondedAnswer,
                    DecisionMakingSurveyCommandModelUserId = id
                });
            }
        }

        public async Task<List<QuestionCommandModel>> GetAllQuestionAsync()
        {
            using var connection = new NpgsqlConnection(this.Configuration.GetConnectionString("RevenueConnection"));

            List<DecisionMakingSurveyQuestions> result = (await connection.QueryAsync<DecisionMakingSurveyQuestions>
            (@"SELECT DQ.""QuestionId"",DQ.""Question"",DP.""Text"",DP.""Id""
	            FROM public.""DecisionMakingSurveyQuestion"" DQ left join public.""DecisionMakingSurveyOptions"" DP on DQ.""QuestionId""= DP.""DecisionMakingSurveyCommandModelQuestionId""")).ToList();


            List<QuestionCommandModel> QuestionList =
            (from c in result
             group c by new
             {
                 c.QuestionId,
                 c.Question,
             } into gcs
             select new QuestionCommandModel()
             {
                 QuestionId = gcs.Key.QuestionId,
                 Question = gcs.Key.Question,
                 options = gcs.Select(a => new DecisionMakingSurveyOptions
                 {
                     Id=a.Id,   
                     Text=a.Text,   
                 }).ToList()
             }).ToList();

            return QuestionList;
        }

 
        public async Task<List<QuestionCommandModel>> GetQuestionByIdAsync(Guid Id)
        {
            using var connection = new NpgsqlConnection(this.Configuration.GetConnectionString("RevenueConnection"));
            var result = (await connection.QueryAsync<DecisionMakingSurveyQuestions>
            (@"SELECT DQ.""QuestionId"",DQ.""Question"",DP.""Text"",DP.""Id""
	            FROM public.""DecisionMakingSurveyQuestion"" DQ left join public.""DecisionMakingSurveyOptions"" DP on DQ.""QuestionId""= DP.""DecisionMakingSurveyCommandModelQuestionId"" WHERE DQ.""QuestionId"" = '" + Id + "'")).ToList();

            List<QuestionCommandModel> QuestionList =
           (from c in result
            group c by new
            {
                c.QuestionId,
                c.Question,
            } into gcs
            select new QuestionCommandModel()
            {
                QuestionId = gcs.Key.QuestionId,
                Question = gcs.Key.Question,
                options = gcs.Select(a => new DecisionMakingSurveyOptions
                {
                   Id = a.Id,
                   Text = a.Text,   
                }).ToList()
            }).ToList();

            return QuestionList;
        }
    }


   
}
