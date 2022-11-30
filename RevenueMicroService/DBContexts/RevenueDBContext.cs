using Microsoft.EntityFrameworkCore;
using Revenue.CommandModel;

namespace Revenue.DBContexts
{
    public class RevenueDBContext : DbContext
    {
        public RevenueDBContext(DbContextOptions<RevenueDBContext> options) : base(options)
        {
        }
        public DbSet<DecisionMakingSurveyCommandModel> DecisionMakingSurveyQuestion { get; set; }
        public DbSet<TalentSurveyCommandModel> TalentSurveyQuestion { get; set; }
        
    }
}
