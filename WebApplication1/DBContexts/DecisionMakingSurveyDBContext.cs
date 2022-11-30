using Microsoft.EntityFrameworkCore;
using DecisionMakingSurvey.CommandModel;

namespace DecisionMakingSurvey.DBContexts
{
    public class DecisionMakingSurveyDBContext : DbContext
    {
        public DecisionMakingSurveyDBContext(DbContextOptions<DecisionMakingSurveyDBContext> options) : base(options)
        {
        }
       
        public DbSet<DecisionMakingSurveyCommandModel> DecisionMakingSurveyTable { get; set; }
    }
}
