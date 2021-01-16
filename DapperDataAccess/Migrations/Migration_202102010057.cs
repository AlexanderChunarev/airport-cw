using FluentMigrator;

namespace AirportAPI.DapperDataAccess.Migrations
{
    [Migration(202102010057)]
    public class Migration_202102010057 : Migration
    {
        public override void Up()
        {
            Alter.Table("trip")
                .AddColumn("from_location_id").AsInt32().ForeignKey("airport", "id")
                .AddColumn("to_location_id").AsInt32().ForeignKey("airport", "id");
        }

        public override void Down()
        {
            Delete.Column("from_location_id").FromTable("client");
            Delete.Column("to_location_id").FromTable("client");
        }
    }
}