using FluentMigrator;

namespace AirportAPI.DapperDataAccess.Migrations
{
    [Migration(202116021675)]
    public class Migration_202116021675 : Migration
    {
        public override void Up()
        {
            Alter.Table("trip")
                .AddColumn("departure_date").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Column("departure_date").FromTable("trip");
        }
    }
}