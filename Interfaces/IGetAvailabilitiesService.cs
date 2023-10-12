namespace disney_grabber.Interfaces;

public interface IGetAvailabilitiesService
{
    Task<List<Reservation>> GetAvailabilities(string url);    
}