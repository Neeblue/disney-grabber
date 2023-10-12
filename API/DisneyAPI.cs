using Newtonsoft.Json.Linq;

namespace disney_grabber;

/// <summary>
/// Service for retrieving availability information for Disney dining reservations.
/// </summary>
public class DisneyApi
{
    private readonly HttpClient httpClient;

    public DisneyApi()
    {
        httpClient = new HttpClient();
    }

    /// <summary>
    /// Retrieves the dining availability data from the specified URL.
    /// Don't call directly. Use GetAvaiabilityService instead.
    /// </summary>
    /// <param name="url">The URL to retrieve the data from.</param>
    /// <returns>The response body as a string, or null if an error occurred.</returns>
    public async Task<string?> GetDiningAvailability(string url)
    {
        try
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                
                return responseBody;
            }
            else
            {
                Console.WriteLine($"Failed to fetch data. Status code: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
}