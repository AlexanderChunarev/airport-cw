using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AirportAPI.Models;
using Dapper;

namespace AirportAPI.DapperDataAccess.Repositories.Airport
{
    using Models;

    public class AirportRepository : IAirportRepository
    {
        private readonly IDbConnection _dbConnection;

        public AirportRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Airport> GetById(int id)
        {
            const string query = @"SELECT * FROM airport WHERE id=@Id";

            return await _dbConnection.QueryFirstAsync<Airport>(query, new {Id = id});
        }

        public async Task<List<Airport>> GetAllByPattern(string pattern)
        {
            const string query = @"SELECT * FROM airport WHERE name ilike @Pattern";
            var result = await _dbConnection.QueryAsync<Airport>(query, new {Pattern = pattern + '%'});

            return result.ToList();
        }
    }
}