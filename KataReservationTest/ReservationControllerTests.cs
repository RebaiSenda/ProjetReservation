using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KataReservation.Models;
using KataReservation.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace KataReservationTest
{
    public class ReservationControllerTests
    {
        private readonly Mock<IReservationService> _mockReservationService;
        private readonly ReservationController _controller;

        public ReservationControllerTests()
        {
            _mockReservationService = new Mock<IReservationService>();
            _controller = new ReservationController(_mockReservationService.Object);
        }

        [Fact]
        public void ListerSalles_RetourneListeDeSalles()
        {
            // Arrange
            var salles = new List<Salle>
            {
                new Salle { Id = "1", Nom = "Salle A" },
                new Salle { Id = "2", Nom = "Salle B" }
            };
            _mockReservationService.Setup(service => service.ListerSalles()).Returns(salles);

            // Act
            var result = _controller.ListerSalles();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Salle>>>(result);
            var returnValue = Assert.IsType<List<Salle>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void CreerReservation_RetourneReservationCreee()
        {
            // Arrange
            var reservation = new Reservation
            {
                Id = "1",
                User = "John Doe",
                Salle =new Salle { Id="1", Nom="S1"},
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            };
            _mockReservationService.Setup(service => service.CreerReservation(It.IsAny<Reservation>())).Returns(reservation);

            // Act
            var result = _controller.CreerReservation(reservation);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Reservation>(actionResult.Value);
            Assert.Equal(reservation.Id, returnValue.Id);
        }

        [Fact]
        public void SupprimerReservation_RetourneNoContent()
        {
            // Arrange
            var reservationId = "1";
            _mockReservationService.Setup(service => service.SupprimerReservation(reservationId));

            // Act
            var result = _controller.SupprimerReservation(reservationId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GererConflits_RetourneListeDeCreneauxLibres()
        {
            // Arrange
            var reservation = new Reservation
            {
                Salle = new Salle { Id = "1", Nom = "S1" },
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            };
            var creneauxLibres = new List<string> { "08:00-09:00", "10:00-11:00", "15:00-16:00" };
            _mockReservationService.Setup(service => service.GererConflits(
                reservation.Salle.Id, reservation.StartTime, reservation.EndTime)).Returns(creneauxLibres);


            // Act
            var result = _controller.GererConflits(reservation);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<string>>>(result);
            var returnValue = Assert.IsType<List<string>>(actionResult.Value);
            Assert.Equal(3, returnValue.Count);
        }
    }

}
