using System.Collections.Generic;
using KataReservation.Models;

public interface ISalleRepository
{
    IEnumerable<Salle> GetAll();
    Salle GetById(string id);
    void Add(Salle salle);
    void Update(Salle salle);
    void Delete(string id);
    List<Reservation> GetReservation(Salle salle);

}
