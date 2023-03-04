using Carpool.Database;

namespace Carpool.Models
{
    public class CurrentRide
    {
        public int Id { get; set; }

        public List<Stop> Stops { get; set; } = new List<Stop>();

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public int OfferedById { get; set; }

        public DateTime Date { get; set; }

        public string TimeSlot { get; set; }

        public int Price { get; set; }

        public int AvailableSeats { get; set; }
    }
}
