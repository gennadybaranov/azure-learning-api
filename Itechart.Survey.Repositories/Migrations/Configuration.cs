using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Itechart.Common;
using Itechart.Survey.DomainModel;

namespace Itechart.Survey.Repositories.Migrations
{
    [UsedImplicitly]
    internal sealed class Configuration : DbMigrationsConfiguration<SurveyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        protected override void Seed(SurveyDbContext context)
        {
            var adminRole = new Role
            {
                Name = Role.BuiltIn.Admin
            };

            var userRole = new Role
            {
                Name = Role.BuiltIn.User
            };

            var admin = new User
            {
                DisplayName = "Admin",
                UserName = "admin@email.com",
                Password = "AMjLyviIiTd1y3kc9RhS6GpDqb7udEL0zdL2Q9F49OnxizNEVzN9uvNaZapC9GtzCw==",
                SignUpDate = DateTime.UtcNow,
                Roles = new HashSet<Role>
                {
                    adminRole,
                    userRole
                }
            };

            context.Roles.AddOrUpdate(r => r.Name, adminRole, userRole);
            if (!context.Users.Any(u => u.UserName == admin.UserName))
            {
                context.Users.Add(admin);
            }

            base.Seed(context);
        }
    }
}
