﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportAPI.DapperDataAccess.Repositories.Trip
{
    using Models;

    public interface ITripRepository
    {
        Task<Trip> GetById(int id);

        Task<List<Trip>> GetByDestination(int departureId, int arriveId);
        
        Task<List<Trip>> GetByAirline(int airlineId);
    }
}