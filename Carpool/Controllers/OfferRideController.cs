using Carpool.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Carpool.Database;
using Carpool.Models;
using Carpool.Services.Contracts;

namespace Carpool.Api.Controllers
{
    //  [Authorize]
    [ApiController]
    [Route("api/offerride")]
    public class OfferRideController : Controller
    {
        private readonly DatabaseContext CarpoolDb;
        private readonly IOfferRideService offerRideService;
        public OfferRideController(DatabaseContext CarpoolDb, IOfferRideService offerRideService)
        {
            this.CarpoolDb = CarpoolDb;
            this.offerRideService = offerRideService;
        }


        [HttpPost]
        public async Task<IActionResult> OfferTheRide(int UserId, OfferRide ride)
        {
            try
            {
                offerRideService.OfferTheRide(UserId, ride);
                return Ok(ride);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
