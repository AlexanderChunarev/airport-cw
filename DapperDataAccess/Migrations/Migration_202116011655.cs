using FluentMigrator;

namespace AirportAPI.DapperDataAccess.Migrations
{
    [Migration(202116011655)]
    public class Migration_202116011655 : Migration
    {
        public override void Up()
        {
            Rename.Column("from_location_id").OnTable("flight").To("departure_airport_id");
            Rename.Column("to_location_id").OnTable("flight").To("arrive_airport_id");
        }

        public override void Down()
        {
        }
    }
}