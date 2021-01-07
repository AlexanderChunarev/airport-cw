namespace AirportAPI.Services.Flight
{
    using Models;
    public interface IOutputPort
    {
        void Ok(Flight flight);
        
        void Created(Flight flight);
    }
}