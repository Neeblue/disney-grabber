namespace disney_grabber.Helper;

/// <summary>
/// Helper class to build the url for the API call to get Disney dining availabilities.
/// </summary>
public static class UrlBuilder
{
    /// <summary>
    /// Don't remember what this method was used for :)
    /// </summary>
    /// <param name="restaurant">Restaurant to get the ID of</param>
    /// <returns></returns>
    public static string GetRestaurantId(Restaurant restaurant)
    {
        return Restaurants[restaurant].Id;
    }

    /// <summary>
    /// Builds the url for the API call.
    /// </summary>
    /// <param name="month">Month to look for reservation</param>
    /// <param name="date">Specific date to look for reservation</param>
    /// <param name="partySize">Number of people in the party</param>
    /// <param name="t">Reservation time of choice</param>
    /// <param name="r">Reservation restaurant of choice</param>
    /// <returns></returns>
    public static string UrlApiBuilder(int month, int date, int partySize, Time t, Restaurant r)
    {
        int validMaxDate = DateTime.DaysInMonth(2023, month);
        if (date < 1 || date > validMaxDate) return "";
        if (month < 1 || month > 12) return "";

        string url = $"https://disneyworld.disney.go.com/en_CA/finder/api/v1/explorer-service/dining-availability/%7B43C01E1D-D7BA-4FAD-A0AB-38FE74B92D4B%7D/wdw/{Restaurants[r].Id};entityType=restaurant/table-service/{partySize}/2023-{month}-{date}/?{Times[t]}";
        return url;
    }

    /// <summary>
    /// Builds a booking url using the restaurant of choice and the partial url from the API response.
    /// This url will take the user directly to the booking page for the restaurant and time of choice.
    /// </summary>
    /// <param name="bookingUrl">partial url take from the "offers" "url" in the API response</param>
    /// <param name="restaurant">Restaurant of choice</param>
    /// <returns></returns>
    public static string BookingUrl(string bookingUrl, Restaurant restaurant)
    {
        return $"https://disneyworld.disney.go.com/en_CA{bookingUrl}&offerOrigin={Restaurants[restaurant].LinkEnd}";
    }

    /// <summary>
    /// Holds various information about a restaurant needed to find reservation details.
    /// </summary>
    /// <typeparam name="Restaurant">Restaurant of choice</typeparam>
    /// <typeparam name="(string Id, string LinkEnd)">Tuple showing the ID of the restaurant and part of the url</typeparam>
    /// <typeparam name="string LinkEnd">LinkEnd: The "OfferOrigin" that is needed to make booking links work.</typeparam>
    /// 
    private static Dictionary<Restaurant, (string Id, string LinkEnd)> Restaurants = new()
    {
        { Restaurant.Space220, ("19634138", "%2Fdining%2Fepcot%2Fspace-220-lounge%2Favailability-modal%2F") },
        { Restaurant.YakAndYeti, ("215686", "%2Fdining%2Fanimal-kingdom%2Fyak-and-yeti-restaurant%2Favailability-modal%2F") },
        { Restaurant.JungleSkipper, ("18185631", "%2Fdining/magic-kingdom%2Fjungle-navigation-skipper-canteen%2Favailability-modal%2F") },
    };

    /// <summary>
    /// Used to convert from the Time enum to the string needed for the url.
    /// </summary>
    private static Dictionary<Time, string> Times = new()
    {
        { Time.Breakfast, $"{meal}712" },
        { Time.Lunch, $"{meal}717" },
        { Time.Dinner, $"{meal}714" },
        { Time.NineAM, $"{tStart}09%3A00{tEnd}" },
        { Time.HalfNineAM, $"{tStart}09%3A30{tEnd}" },
        { Time.Ten, $"{tStart}10%3A00{tEnd}" },
        { Time.HalfTen, $"{tStart}10%3A30{tEnd}" },
        { Time.Eleven, $"{tStart}11%3A00{tEnd}" },
        { Time.HalfEleven, $"{tStart}11%3A30{tEnd}" },
        { Time.Twelve, $"{tStart}12%3A00{tEnd}" },
        { Time.HalfTwelve, $"{tStart}12%3A30{tEnd}" },
        { Time.One, $"{tStart}13%3A00{tEnd}" },
        { Time.HalfOne, $"{tStart}13%3A30{tEnd}" },
        { Time.Two, $"{tStart}14%3A00{tEnd}" },
        { Time.HalfTwo, $"{tStart}14%3A30{tEnd}" },
        { Time.Three, $"{tStart}15%3A30{tEnd}" },
        { Time.HalfThree, $"{tStart}15%3A30{tEnd}" },
        { Time.Four, $"{tStart}16%3A00{tEnd}" },
        { Time.HalfFour, $"{tStart}16%3A30{tEnd}" },
        { Time.Five, $"{tStart}17%3A00{tEnd}" },
        { Time.HalfFive, $"{tStart}17%3A30{tEnd}" },
        { Time.Six, $"{tStart}18%3A00{tEnd}" },
        { Time.HalfSix, $"{tStart}18%3A30{tEnd}" },
        { Time.Seven, $"{tStart}19%3A00{tEnd}" },
        { Time.HalfSeven, $"{tStart}19%3A30{tEnd}" },
        { Time.EightPM, $"{tStart}20%3A00{tEnd}" },
        { Time.HalfEightPM, $"{tStart}20%3A30{tEnd}" },
        { Time.NinePM, $"{tStart}21%3A00{tEnd}" },
    };

    private static string meal { get => "mealPeriod=80000"; }
    private static string tStart { get => "searchTime="; }
    private static string tEnd { get => "%3A00"; }
    
    public enum Restaurant { Space220, YakAndYeti, JungleSkipper };
    public enum Time { Breakfast, Lunch, Dinner, EightAM, HalfEightAM, NineAM, HalfNineAM, Ten, HalfTen, Eleven, HalfEleven, Twelve, HalfTwelve, One, HalfOne, Two, HalfTwo, Three, HalfThree, Four, HalfFour, Five, HalfFive, Six, HalfSix, Seven, HalfSeven, EightPM, HalfEightPM, NinePM, HalfNinePM };
}