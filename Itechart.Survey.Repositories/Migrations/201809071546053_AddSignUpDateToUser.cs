using System.Data.Entity.Migrations;
using Itechart.Common;

namespace Itechart.Survey.Repositories.Migrations
{
    [UsedImplicitly]
    public partial class AddSignUpDateToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SignUpDate", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "SignUpDate");
        }
    }
}
