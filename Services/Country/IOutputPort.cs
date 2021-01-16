using System.Collections.Generic;

namespace AirportAPI.Services.Country
{
    public interface IOutputPort
    {
        void Ok(List<Models.Country> countries);
    }
}