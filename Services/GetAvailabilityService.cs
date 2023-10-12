using System.Globalization;
using disney_grabber.Interfaces;
using Newtonsoft.Json.Linq;

namespace disney_grabber.Services;

/// <summary>
/// Service for retrieving availability information for Disney dining reservations.
/// </summary>
/// <param name="url">Url of the restaurant that you would like to check the availability of.</param>
/// <param name="disneyApi">An instance of <see cref="DisneyApi"/> for making requests to the Disney API.</param>
/// <returns>A <see cref="Reservation"/> object containing the location, time, and direct booking link of the reservation.</returns>
public class GetAvailabilitiesService : IGetAvailabilitiesService
{
    DisneyApi _disneyApi;
    
    public GetAvailabilitiesService(DisneyApi disneyApi)
    {
        _disneyApi = disneyApi;
    }

    /// <summary>
    /// Gets the availabilities.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns>A list of reservations.</returns>
    public async Task<List<Reservation>> GetAvailabilities(string url)
    {
        string? response = await _disneyApi.GetDiningAvailability(url);
        List<Reservation> reservations = new();

        if (response != null)
        {
            JObject result = JObject.Parse(response);

            if (result.ContainsKey("offers"))
            {
                JArray? offers = (JArray?)result["offers"];
                if (offers is null)
                    return reservations;
                foreach (JToken offer in offers)
                {
                    Reservation reservation = new();

                    //Grab the time of the reservation from the JObject
                    DateTime time;
                    if (DateTime.TryParseExact(offer["time"]?.ToString() ?? "", "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                        reservation.Date = time;
                    
                    //Grab the partial url needed to build a reservation link (missing base beginning and restaurant specific end)
                    reservation.Url = offer["url"]?.ToString() ?? "";
                    
                    //Grab the date of the reservation from the URL
                    DateTime date = GetDateFromUrl(url);

                    //Combine both date and time into a single DateTime object
                    DateTime combined = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
                    reservation.Date = combined;                    

                    reservations.Add(reservation);
                }
            }
            else if (result.ContainsKey("unavailableReason"))
            {
                string reason = result["unavailableReason"]?.ToString() ?? "";
                Console.WriteLine($"No reservations available: {reason}");
            }
        }
        return reservations;
    }

    /// <summary>
    /// Pulls the date from the URL and returns it as a DateTime object.
    /// </summary>
    /// <param name="url">The URL </param>
    /// <returns>The date.</returns>
    private DateTime GetDateFromUrl(string url)
    {
        string[] urlParts = url.Split('/');
        string dateString = urlParts[urlParts.Length - 2];
        DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
        return date;
    }
}