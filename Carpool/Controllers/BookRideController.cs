using Carpool.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Carpool.Database;
using Carpool.Models;
using Carpool.Services.Contracts;

namespace Carpool.Api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/bookRide")]
    public class BookRideController : Controller
    {
        private readonly DatabaseContext CarpoolDb;
        private readonly IBookRideService bookRideService;
        public BookRideController(DatabaseContext carpoolDb, IBookRideService bookRideService)
        {
            CarpoolDb = carpoolDb;
            this.bookRideService = bookRideService;
        }

        [Authorize]
        [HttpPost]
        [Route("availableRides")]
        public async Task<List<CurrentRide>> GetAvailableRides(BookRide ride)
        {
            try
            {
                return (List<CurrentRide>)bookRideService.GetAvailableRides(ride);
            }
            catch (Exception ex)
            {
                return new List<CurrentRide>();
            }
        }

        [HttpPost]
        [Route("bookTheRide")]
        public async Task<bool> BookTheRide(CurrentRide currentRide, int UserId)
        {
            try
            {
                if(bookRideService.BookTheRide(currentRide, UserId)) {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;   
            }

        }
    }
}
