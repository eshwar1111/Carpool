using Carpool.Database;
using Carpool.Models;
using Carpool.Services.Contracts;

namespace Carpool.Services
{
    public class HistoryService : IHistoryService
    {
        DatabaseContext CarpoolDb;
        public HistoryService(DatabaseContext CarpoolDb)
        {
            this.CarpoolDb = CarpoolDb;
        }
        
        public List<Ride> GetBookedRides(int userId)
        {
            try
            {
                List<Ride> bookedrides = CarpoolDb.Rides.Where(r => r.IsBooked && r.BookedById == userId).ToList();

                return bookedrides;
            }
            catch (Exception ex)
            {
                return new List<Ride>();
            }
        }

        public List<Ride> GetOfferedRides(int userId)
        {
            try
            {
                List<Ride> offeredRides = CarpoolDb.Rides.Where(r => r.IsBooked && r.OfferedById == userId).ToList();
                


                return offeredRides;
            }
            catch (Exception ex)
            {
                return new List<Ride>();
            }
        }
    }
}
