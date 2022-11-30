using UserManagement.CommandModel;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.DbContexts
{
    public class DataDbContext : DbContext
    {

        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {

        }
        public DbSet<RegistrationCommandModel> UserManagement { get; set; }

    }
}
