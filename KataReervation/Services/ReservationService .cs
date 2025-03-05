using KataReservation.Models;
using KataReservation.Services;

public class ReservationService : IReservationService
{
    private readonly ISalleRepository _salleRepository;
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(ISalleRepository salleRepository, IReservationRepository reservationRepository)
    {
        _salleRepository = salleRepository;
        _reservationRepository = reservationRepository;
    }

    public List<Salle> ListerSalles()
    {
        return _salleRepository.GetAll().ToList();
    }

    public List<Reservation> ListerReservations()
    {
        return _reservationRepository.GetAll().ToList();
    }

    public Reservation CreerReservation(Reservation reservation)
    {
        // Vérifiez les conflits ici

        var salle = _salleRepository.GetById(reservation.Salle.Id);
        var reservations = _salleRepository.GetReservation(salle);
        var existe = reservations.Exists(r=> (r.StartTime <= reservation.StartTime) && (r.EndTime >= reservation.StartTime) ||
                                             (r.StartTime <= reservation.EndTime) && (r.EndTime >= reservation.EndTime));

        if(existe)
        {
            throw new Exception("Conflit de réservation");
        }
        _reservationRepository.Add(reservation);

        return reservation;
    }

    public void SupprimerReservation(string id)
    {
        _reservationRepository.Delete(id);
    }

    public List<string> GererConflits(string room, DateTime debut, DateTime fin)
    {
        var reservations = _reservationRepository.GetByRoomAndTime(room, debut, fin);
        // Exemple simplifié de créneaux libres
        return new List<string> { "08:00-09:00", "10:00-11:00", "15:00-16:00" }; 
    }
}
