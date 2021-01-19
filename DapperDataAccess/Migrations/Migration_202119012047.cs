using FluentMigrator;

namespace AirportAPI.DapperDataAccess.Migrations
{
    [Migration(202119012047)]
    public class Migration_202119012047 : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"INSERT INTO airline(name, description)
            VALUES ('Turkish Airlines®', 'Some description.'),
            ('AIRWAYS', 'Some description.'),
            ('KLM Royal Dutch Airlines', 'Some description.')");
        }

        public override void Down()
        {
        }
    }
}