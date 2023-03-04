using Carpool.Database;
using Carpool.Models;

namespace Carpool.Services.Contracts
{
    public interface IBookRideService
    {
        public List<Stop> GetUpdatedStops(List<Stop> Stops, int StartPoint, int PathLength);
        public int GetAvailableSeats(List<Stop> Stops, int StartPoint, int EndPoint);



        public bool BookTheRide(CurrentRide currentRide, int UserId);
        public List<CurrentRide> GetAvailableRides(BookRide ride);
        public List<int> IsPathExists(Ride ride,  string StartPoint, string Endpoint);

        public string GetLocationName(int LocationId);

    }
}
