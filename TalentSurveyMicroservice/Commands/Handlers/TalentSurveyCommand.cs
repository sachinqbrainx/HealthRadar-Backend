using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using TalentSurvey.CommandModel;
using TalentSurvey.Commands.Interface;
using TalentSurvey.DBContexts;


namespace TalentSurvey.Commands.Handlers
{
    public class TalentSurveyCommand : ITalentSurveyCommand
    {
        private readonly TalentSurveyDbContext _dbContext;
        public IConfiguration Configuration { get; }

        public TalentSurveyCommand(TalentSurveyDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            Configuration = configuration;
        }

        public async Task<List<UserResponse>> GetAllTotalAsync()
        {
            using var connection = new NpgsqlConnection(this.Configuration.GetConnectionString("TalentConnection"));
            return (await connection.QueryAsync<UserResponse>
            (@"SELECT ""UserId"",""UserName"", ""Total""
	            FROM public.""TalentSurveyTable""")).ToList();

        }
        public async Task<List<QuestionCommandModel>> GetAllQuestionAsync()
        {
            using var connection = new NpgsqlConnection(this.Configuration.GetConnectionString("RevenueConnection"));
           var result =  (await connection.QueryAsync<TalentSurveyQuestions>
            (@"SELECT TQ.""QuestionId"",TQ.""Question"", TP.""Text"", TP.""Id"" 
	            FROM public.""TalentSurveyQuestion"" TQ left join public.""TalentSurveyOptions"" TP on TQ.""QuestionId"" = TP.""TalentSurveyCommandModelQuestionId""")).ToList();

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
                options = gcs.Select(a => new TalentSurveyOptions
                {
                    Id = a.Id,
                    Text = a.Text,
                }).ToList()
            }).ToList();

            return QuestionList;

        }
        public async Task<List<QuestionCommandModel>> GetQuestionByIdAsync(Guid Id)
        {
            using var connection = new NpgsqlConnection(this.Configuration.GetConnectionString("RevenueConnection"));
            var result = (await connection.QueryAsync<TalentSurveyQuestions>
            (@"SELECT TQ.""QuestionId"",TQ.""Question"", TP.""Text"", TP.""Id"" 
	            FROM public.""TalentSurveyQuestion"" TQ left join public.""TalentSurveyOptions"" TP on TQ.""QuestionId"" = TP.""TalentSurveyCommandModelQuestionId"" WHERE TQ.""QuestionId"" = '" + Id+"'")).ToList();


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
               options = gcs.Select(a => new TalentSurveyOptions
               {
                   Id = a.Id,
                   Text=a.Text,
               }).ToList()
           }).ToList();

            return QuestionList;
        }

       


        public async Task<bool> AddResponseAsync(TalentSurveyCommandModel modal)
        { int total = 0;
            bool result = false;
            
            using (var connection = new NpgsqlConnection(this.Configuration.GetConnectionString("TalentConnection")))
            {
                var UserGuidId = Guid.NewGuid();

                //var Answers = (await (new NpgsqlConnection(this.Configuration.GetConnectionString("RevenueConnection"))).QueryAsync<QuestionCommandModel>
                //(@"SELECT ""QuestionId"",""Answer"" FROM public.""TalentSurveyTable""")).ToList();
             
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

                var isSuccess = await connection.ExecuteAsync(@"INSERT INTO public.""TalentSurveyTable""(""UserId"",""UserName"", ""Total"")VALUES(@UserId,@UserName,@Total )",
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
                (@"INSERT INTO public.""ResponsedAnswer""(""Id"",""QuestionId"", ""RespondedAnswer"",""TalentSurveyCommandModelUserId"") VALUES(@Id,@QuestionId, @RespondedAnswer,@TalentSurveyCommandModelUserId)",
                new
                {
                    Id = gnewId,
                    QuestionId = res.QuestionId,
                    RespondedAnswer = res.RespondedAnswer,
                    TalentSurveyCommandModelUserId = id
                });
            }
        }




    }


   
}
