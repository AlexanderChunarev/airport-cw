﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportAPI.DapperDataAccess.Repositories.Trip
{
    using Models;

    public interface ITripRepository
    {
        Task<Trip> GetById(int id);

        Task<List<Trip>> GetData(Airport airport);
        
        Task<List<Trip>> GetData(Airline airline);
    }
}