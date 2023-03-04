using Carpool.Models;

namespace Carpool.Services.Contracts
{
    public interface IOfferRideService
    {
        public void OfferTheRide(int UserId, OfferRide ride);
        public int GetLocationId(string locationName);

    }
}