using System.Collections.Generic;

namespace AirportAPI.Services.Airport
{
    public interface IOutputPort
    {
        void Ok(List<Models.Airport> airports);
    }
}