using System.ComponentModel.DataAnnotations;

namespace KataReservation.Models
{
    public class Salle
    {
        [Key]
        public string Id { get; set; }
        public string Nom { get; set; }

        // Liste des réservations associées à cette salle
        public List<Reservation> Reservations { get; set; }

    }

}
