using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Carpool.Database
{
    public class Ride
    {
        [Key]
        public int Id { get; set; }

        public List<Stop> Stops { get; set; } = new List<Stop>();

        [ForeignKey("User")]
        public int OfferedById { get; set; }


        [ForeignKey("User")]
        public int BookedById { get; set; }

        public bool IsBooked { get; set; }

        public DateTime Date { get; set; }

        public string TimeSlot { get; set; }

        public int Price { get; set; }

        public string? StartPoint { get; set;}

        public string? EndPoint { get; set;}
    }
}