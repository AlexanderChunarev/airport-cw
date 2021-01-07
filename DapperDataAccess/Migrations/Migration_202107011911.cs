using FluentMigrator;

namespace AirportAPI.DapperDataAccess.Migrations
{
    [Migration(202107011911)]
    public class Migration_202107011911 : Migration
    {
        public override void Up()
        {
            Alter.Table("flight")
                .AddColumn("flight_time").AsDouble().Nullable();
        }

        public override void Down()
        {
            Delete.Column("flight_time").FromTable("flight");
        }
    }
}