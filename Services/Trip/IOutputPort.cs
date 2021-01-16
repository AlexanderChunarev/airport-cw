﻿using System.Collections.Generic;

namespace AirportAPI.Services.Trip
{
    public interface IOutputPort
    {
        void Ok(Models.Trip trip);
        
        void Ok(List<Models.Trip> trips);
    }
}