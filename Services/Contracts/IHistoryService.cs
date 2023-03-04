using Carpool.Database;
using Carpool.Models;

namespace Carpool.Services.Contracts
{
    public interface IHistoryService
    {
        public List<Ride> GetBookedRides(int UserId);

        public List<Ride> GetOfferedRides(int UserId);

    }
}
