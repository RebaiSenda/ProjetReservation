using System.ComponentModel.DataAnnotations;

namespace KataReservation.Models
{
    public class Reservation
    {
        [Key]
        public string Id { get; set; }
        public string User { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Salle Salle { get; set; }

    }

}
