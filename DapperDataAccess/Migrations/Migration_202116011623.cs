using FluentMigrator;

namespace AirportAPI.DapperDataAccess.Migrations
{
    [Migration(202116011623)]
    public class Migration_202116011623 : Migration
    {
        public override void Up()
        {
            Alter.Table("trip")
                .AddColumn("departure_country_id").AsInt32()
                .AddColumn("arrive_country_id").AsInt32();
            Rename.Column("from_location_id").OnTable("trip").To("departure_airport_id");
            Rename.Column("to_location_id").OnTable("trip").To("arrive_airport_id");
        }

        public override void Down()
        {
            Delete.Column("departure_country_id").FromTable("trip");
            Delete.Column("arrive_country_id").FromTable("trip");
        }
    }
}