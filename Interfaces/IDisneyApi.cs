namespace disney_grabber.Interfaces;

public interface IDisneyApi
{
    Task<string?> GetDiningAvailability(string url);
}