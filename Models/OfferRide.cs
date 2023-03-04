namespace Carpool.Models
{
    public class OfferRide
    {
        public List<string> Path { get; set; }

        public DateTime Date { get; set; }

        public string TimeSlot { get; set; }

        public int AvailableSeats { get; set; }

        public int Price { get; set; }

    }
}
