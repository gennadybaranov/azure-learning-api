using System.Data.Entity.Migrations;
using Itechart.Common;

namespace Itechart.Survey.Repositories.Migrations
{
    [UsedImplicitly]
    public partial class ChangingModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserName", c => c.String());
            AddColumn("dbo.Users", "DisplayName", c => c.String());
            AddColumn("dbo.Users", "Password", c => c.String());
            DropColumn("dbo.Users", "Name");
        }

        public override void Down()
        {
            AddColumn("dbo.Users", "Name", c => c.String());
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "DisplayName");
            DropColumn("dbo.Users", "UserName");
        }
    }
}
