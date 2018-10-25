using System.Data.Entity;
using Itechart.Common;
using Itechart.Survey.DomainModel;
using Itechart.Survey.Repositories.Migrations;

namespace Itechart.Survey.Repositories
{
    [UsedImplicitly]
    public class SurveyDbContext : DbContext
    {
        private const string DefaultConnection = "defaultConnection";


        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }


        static SurveyDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SurveyDbContext, Configuration>());
        }

        public SurveyDbContext()
            : base(DefaultConnection)
        {

        }
    }
}
