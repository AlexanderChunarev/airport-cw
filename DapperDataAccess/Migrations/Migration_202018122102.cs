using System.IO;
using FluentMigrator;
using Microsoft.AspNetCore.Hosting;

namespace AirportAPI.DapperDataAccess.Migrations
{
    [Migration(202018122102)]
    public class Migration_202018122102 : Migration
    {
        private IWebHostEnvironment _hostEnvironment;

        public Migration_202018122102(IWebHostEnvironment environment)
        {
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
       
            INSERT INTO airport(name,city,country_id, latitude, longitude)
            VALUES ('Hartsfield Jackson Atlanta Intl','Atlanta',7, 33.636719,-84.428067),
                ('Chicago Ohare Intl','Chicago',7, 41.978603,-87.904842),
                ('Capital Intl','Beijing',2, 40.080111,116.584556),
                ('Heathrow','London', 3, 51.4775,-0.461389),
                ('Charles De Gaulle','Paris', 9, 49.012779,2.55),
                ('Los Angeles Intl','Los Angeles', 7, 33.942536,-118.40807),
                ('Frankfurt Main','Frankfurt', 8, 50.026421,8.543125),
                ('Dallas Fort Worth Intl','Dallas-Fort Worth', 7, 32.896828,-97.037997),
                ('John F Kennedy Intl','New York', 7, 40.639751,-73.778925),
                ('Pudong','Shanghai', 2, 31.143378,121.805214),
                ('Changi Intl','6', 6, 1.350189,103.994433),
                ('Barcelona','Barcelona', 5, 41.297078,2.078464 ),
                ('Denver Intl','Denver', 7, 39.861656,-104.67317),
                ('Miami Intl','Miami', 7, 25.79325,-80.290556),
                ('Dubai Intl','Dubai', 4, 25.252778,55.364444),
                ('Gatwick','London', 3, 51.148056,-0.190278),
                ('Baiyun Intl','Guangzhou', 2, 23.392436,113.298786),
                ('Fiumicino','Rome', 1, 41.804475,12.250797);");
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