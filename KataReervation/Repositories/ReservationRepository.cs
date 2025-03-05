using System.Collections.Generic;
using System.Linq;
using KataReservation.Models;
using Microsoft.EntityFrameworkCore;
public class ReservationRepository : IReservationRepository
{
    private readonly ReservationContext _context;

    public ReservationRepository(ReservationContext context)
    {
        _context = context;
    }

    public IEnumerable<Reservation> GetAll()
    {
        return _context.Reservations.ToList();
    }

    public Reservation GetById(string id)
    {
        return _context.Reservations.Find(id);
    }

    public void Add(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        _context.SaveChanges();
    }

    public void Update(Reservation reservation)
    {
        _context.Entry(reservation).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var reservation = _context.Reservations.Find(id);
        if (reservation != null)
        {
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Reservation> GetByRoomAndTime(string room, DateTime debut, DateTime fin)
    {
        return _context.Reservations
            .Where(r => r.Salle.Id== room && r.StartTime < fin && r.EndTime > debut)
            .ToList();
    }
}
