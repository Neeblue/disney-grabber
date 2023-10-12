using static disney_grabber.Helper.UrlBuilder;

public class Reservation
{
    /// <summary>
    /// Object that holds the restaurant name, reservation date, and a direct link to the booking page
    /// </summary>
    public Restaurant? Location { get; set; }
    public DateTime Date { get; set; }
    public string? Url { get; set; }
}