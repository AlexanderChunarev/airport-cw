using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace AirportAPI.DapperDataAccess.Repositories.Country
{
    using Models;

    public class CountryRepository : ICountryRepository
    {
        private readonly IDbConnection _dbConnection;

        public CountryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<Country>> GetAllByPattern(string pattern)
        {
            const string query = @"SELECT * FROM country WHERE name ilike @Pattern";
            var result = await _dbConnection.QueryAsync<Country>(query, new {Pattern = pattern + '%'});

            return result.ToList();
        }
    }
}