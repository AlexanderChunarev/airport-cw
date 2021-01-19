using System.Collections.Generic;

namespace AirportAPI.Services.Flight
{
    using Models;
    public interface IOutputPort
    {
        void Ok(Flight flight);
        
        void Ok(List<Flight> flights);
        
        void Created(Flight flight);
    }
}