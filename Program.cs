using disney_grabber;
using disney_grabber.Services;
using static disney_grabber.Helper.UrlBuilder;

string link = UrlApiBuilder(
    ModifyThisCode.month, 
    ModifyThisCode.date, 
    ModifyThisCode.partySize, 
    ModifyThisCode.time, 
    ModifyThisCode.restaurant);

GetAvailabilitiesService service = new(new DisneyApi());
List<Reservation> reservations = await service.GetAvailabilities(link);

if (reservations.Count > 0)
{
    Console.WriteLine($"Dates found for {ModifyThisCode.restaurant}:");
    
    foreach (Reservation res in reservations)
    {
        res.Url = BookingUrl(res.Url ?? "", ModifyThisCode.restaurant);
        Console.WriteLine($"\tDate: {res.Date}");
        Console.WriteLine($"\tBooking url: {res.Url}\n");
    }
}