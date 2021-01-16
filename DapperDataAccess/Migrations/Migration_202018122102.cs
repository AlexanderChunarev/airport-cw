using System.IO;
using FluentMigrator;
using Microsoft.AspNetCore.Hosting;

namespace AirportAPI.DapperDataAccess.Migrations
{
    [Migration(202018122102)]
    public class Migration_202018122102 : Migration
    {
        private IWebHostEnvironment _hostEnvironment;

        public Migration_202018122102(IWebHostEnvironment environment) {
            _hostEnvironment = environment;
        }
        
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
            
            Execute.Sql(@"INSERT INTO country(name, code)
            VALUES ('Italy', 'IT'),
            ('China', 'CN'),
            ('United Kingdom', 'GB'),
            ('United Arab Emirates', 'AE'),
            ('Spain', 'ES'),
            ('Singapore', 'SG'),
            ('United States', 'US'),
            ('Germany', 'DE'),
            ('France', 'FR');
       
            INSERT INTO airport(name,city,country_id)
            VALUES ('Hartsfield Jackson Atlanta Intl','Atlanta',7),
            ('Chicago Ohare Intl','Chicago',7),
            ('Capital Intl','Beijing',2),
            ('Heathrow','London', 3),
            ('Charles De Gaulle','Paris', 9),
            ('Los Angeles Intl','Los Angeles', 7),
            ('Frankfurt Main','Frankfurt', 8),
            ('Dallas Fort Worth Intl','Dallas-Fort Worth', 7),
            ('John F Kennedy Intl','New York', 7),
            ('Pudong','Shanghai', 2),
            ('Changi Intl','6', 6),
            ('Barcelona','Barcelona', 5),
            ('Denver Intl','Denver', 7),
            ('Miami Intl','Miami', 7),
            ('Dubai Intl','Dubai', 4),
            ('Gatwick','London', 3),
            ('Baiyun Intl','Guangzhou', 2),
            ('Fiumicino','Rome', 1);");
           
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