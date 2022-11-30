using Microsoft.EntityFrameworkCore;
using TalentSurvey.CommandModel;

namespace TalentSurvey.DBContexts
{
    public class TalentSurveyDbContext : DbContext
    {
        public TalentSurveyDbContext(DbContextOptions<TalentSurveyDbContext> options) : base(options)
        {
        }
  
        public DbSet<TalentSurveyCommandModel> TalentSurveyTable { get; set; }
    }
}
