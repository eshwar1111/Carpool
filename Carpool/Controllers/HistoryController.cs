using Carpool.Services;
using Microsoft.AspNetCore.Mvc;
using Carpool.Models;
using Microsoft.AspNetCore.Authorization;
using Carpool.Database;
using Carpool.Services.Contracts;

namespace Carpool.Api.Controllers
{
  /*  [Authorize]*/
    [ApiController]
    [Route("api/history")]
    public class HistoryController : Controller
    {
        private readonly IHistoryService historyService;
        private readonly DatabaseContext CarpoolDb;
        public HistoryController(DatabaseContext Carpool, IHistoryService historyService)
        {

            CarpoolDb = Carpool;
            this.historyService = historyService;

        }

        [HttpGet]
        [Route("BookedRides")]
        public async Task<List<Ride>> GetBookedRides(int UserId)
        {
            try
            {
                return historyService.GetBookedRides(UserId);
            }
            catch (Exception ex)
            {
                return new List<Ride>();
            }
        }

        [HttpGet]
        [Route("OfferedRides")]
        public async Task<List<Ride>> GetOfferedRides(int UserId)
        {
            try
            {
                return historyService.GetOfferedRides(UserId);
            }
            catch (Exception ex) 
            {
                return new List<Ride>();
            }
        }
    }
}
