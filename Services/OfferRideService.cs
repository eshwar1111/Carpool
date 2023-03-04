using Carpool.Database;
using Carpool.Models;
using Carpool.Services.Contracts;
using System.Globalization;

namespace Carpool.Services
{
    public class OfferRideService : IOfferRideService
    {
        DatabaseContext CarpoolDb;
        public OfferRideService(DatabaseContext CarpoolDb)
        {
            this.CarpoolDb = CarpoolDb;
        }

        public int GetLocationId(string locationName)
        {
            try
            {
                foreach (var location in CarpoolDb.Locations)
                {
                    if (location.Name == locationName)
                    {
                        return location.Id;
                    }
                }
                return -1;
            }
            catch { return -1; }
        }

        public void OfferTheRide(int UserId, OfferRide ride)
        {
            try
            {
                Ride newRide = new Ride()
                {
                    BookedById = -1,
                    IsBooked = false,
                    OfferedById = UserId,
                    Date=ride.Date,
                    /*Date = DateTime.ParseExact(ride.Date, "MM-dd-yyyy", CultureInfo.InvariantCulture),*/
                    TimeSlot = ride.TimeSlot,
                    Price = ride.Price,
                    StartPoint = ride.Path[0],
                    EndPoint= ride.Path[ride.Path.Count-1],

                };
                for (int i = 0; i < ride.Path.Count; i++)
                {
                    Stop newStop = new Stop();
                    newStop.StopIndex = i;
                    int LocationId = GetLocationId(ride.Path[i]);
                    newStop.SeatsAtStop = ride.AvailableSeats;

                    if (LocationId > -1)
                    {
                        newStop.LocationId = LocationId;
                    }
                    else
                    {
                        Location newLocation = new Location();
                        newLocation.Name = ride.Path[i];
                        CarpoolDb.Locations.Add(newLocation);
                        CarpoolDb.SaveChanges();
                        newStop.LocationId = GetLocationId(ride.Path[i]);
                    }
                    newRide.Stops.Add(newStop);
                }
                CarpoolDb.Rides.Add(newRide);
                CarpoolDb.SaveChanges();
            }
            catch(Exception ex)
            {}
        
        }
    }
}