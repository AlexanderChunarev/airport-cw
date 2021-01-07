using FluentMigrator;

namespace AirportAPI.DapperDataAccess.Migrations
{
    [Migration(202018122102)]
    public class Migration_202018122102 : Migration
    {
        public override void Up()
        {
            Create.Table("country")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString()
                .WithColumn("code").AsString();
            
            Create.Table("airline")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString()
                .WithColumn("description").AsString().Nullable();
            
            Create.Table("plane")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString()
                .WithColumn("description").AsString().Nullable();

            Create.Table("airport")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString()
                .WithColumn("city").AsString()
                .WithColumn("latitude").AsDouble()
                .WithColumn("longitude").AsDouble()
                .WithColumn("country_id").AsInt32().ForeignKey("country", "id");
            
            Create.Table("trip")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("airline_id").AsInt32().ForeignKey("airline", "id");

            Create.Table("flight")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("trip_id").AsInt32()
                .WithColumn("from_location_id").AsInt32().ForeignKey("airport", "id")
                .WithColumn("to_location_id").AsInt32().ForeignKey("airport", "id")
                .WithColumn("departure_date").AsDateTime()
                .WithColumn("arrive_date").AsDateTime()
                .WithColumn("flight_code").AsString()
                .WithColumn("plane_id").AsInt32().ForeignKey("plane", "id");
            
            Execute.Script("DapperDataAccess/Migrations/initial_data.sql");
        }

        public override void Down()
        {
            Delete.Table("trip");
            Delete.Table("flight");
            Delete.Table("airport");
            Delete.Table("country");
            Delete.Table("airline");
            Delete.Table("plane");
        }
    }
}