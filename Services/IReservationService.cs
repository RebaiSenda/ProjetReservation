using KataReservation.Models;

namespace KataReservation.Services
{
    public interface IReservationService
    {
        List<Salle> ListerSalles();
        Reservation CreerReservation(Reservation reservation);
        void SupprimerReservation(string id);
        List<string> GererConflits(string room, DateTime debut, DateTime fin);
        List<Reservation> ListerReservations();

    }
}