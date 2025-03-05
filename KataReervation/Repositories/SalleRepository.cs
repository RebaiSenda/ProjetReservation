using System.Collections.Generic;
using System.Linq;
using KataReservation.Models;
using Microsoft.EntityFrameworkCore;

public class SalleRepository : ISalleRepository
{
    private readonly ReservationContext _context;

    public SalleRepository(ReservationContext context)
    {
        _context = context;
    }

    public IEnumerable<Salle> GetAll()
    {
        return _context.Salles.ToList();
    }

    public Salle GetById(string id)
    {
        return _context.Salles.Find(id);
    }

    public void Add(Salle salle)
    {
        _context.Salles.Add(salle);
        _context.SaveChanges();
    }

    public void Update(Salle salle)
    {
        _context.Entry(salle).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public List<Reservation> GetReservation(Salle salle)
    {
        return _context.Reservations.Where(r => r.Salle.Id == salle.Id).ToList();
    }
    public void Delete(string id)
    {
        var salle = _context.Salles.Find(id);
        if (salle != null)
        {
            _context.Salles.Remove(salle);
            _context.SaveChanges();
        }
    }
}
