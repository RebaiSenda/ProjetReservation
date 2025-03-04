using KataReservation.Models;
using KataReservation.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet("salles")]
    public ActionResult<List<Salle>> ListerSalles()
    {
        return _reservationService.ListerSalles();
    }

    [HttpPost("reservations")]
    public ActionResult<Reservation> CreerReservation([FromBody] Reservation reservation)
    {
        var result = _reservationService.CreerReservation(reservation);
        return CreatedAtAction(nameof(CreerReservation), new { id = result.Id }, result);
    }

    [HttpDelete("reservations/{id}")]
    public IActionResult SupprimerReservation(string id)
    {
        _reservationService.SupprimerReservation(id);
        return NoContent();
    }

    [HttpPost("reservations/conflits")]
    public ActionResult<List<string>> GererConflits([FromBody] Reservation reservation)
    {
        return _reservationService.GererConflits(reservation.Salle.Id, reservation.StartTime, reservation.EndTime);
    }
}
