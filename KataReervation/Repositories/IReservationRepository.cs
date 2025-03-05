using System.Collections.Generic;
using KataReservation.Models;

public interface IReservationRepository
{
    IEnumerable<Reservation> GetAll();
    Reservation GetById(string id);
    void Add(Reservation reservation);
    void Update(Reservation reservation);
    void Delete(string id);
    IEnumerable<Reservation> GetByRoomAndTime(string room, DateTime debut, DateTime fin);
}
