using Xunit;
using Moq;
using KataReservation.Models;
using KataReservation.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KataReservation.Tests
{
    public class ReservationServiceTests
    {
        private Mock<ISalleRepository> _mockSalleRepository;
        private Mock<IReservationRepository> _mockReservationRepository;
        private ReservationService _reservationService;

        public ReservationServiceTests()
        {
            _mockSalleRepository = new Mock<ISalleRepository>();
            _mockReservationRepository = new Mock<IReservationRepository>();
            _reservationService = new ReservationService(_mockSalleRepository.Object, _mockReservationRepository.Object);
        }

        [Fact]
        public void ListerSalles_ShouldReturnAllRooms()
        {
            // Arrange
            var salles = new List<Salle> { new Salle { Id = "1", Nom = "Room1" }, new Salle { Id = "2", Nom = "Room2" } };
            _mockSalleRepository.Setup(repo => repo.GetAll()).Returns(salles.AsQueryable());

            // Act
            var result = _reservationService.ListerSalles();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ListerReservations_ShouldReturnAllReservations()
        {
            // Arrange
            var reservations = new List<Reservation> { 
                new Reservation { Id = "1", StartTime = DateTime.Now, EndTime= DateTime.Now, 
                    Salle= new Salle{ Nom="S1" , Id="1"}
                }, 
                new Reservation { Id = "2", StartTime = DateTime.Now, EndTime= DateTime.Now,
                    Salle= new Salle{ Nom="S2" , Id="2"}
                } 
            };
            _mockReservationRepository.Setup(repo => repo.GetAll()).Returns(reservations.AsQueryable());

            // Act
            var result = _reservationService.ListerReservations();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void CreerReservation_ShouldAddReservation() 
        {
            // Arrange
            var reservation = new Reservation { Id = "1", StartTime = DateTime.Now, EndTime = DateTime.Now, Salle = new Salle { Nom = "S1", Id = "1" } };

            // Act
            var result = _reservationService.CreerReservation(reservation);

            // Assert
            _mockReservationRepository.Verify(repo => repo.Add(reservation), Times.Once);
            Assert.Equal(reservation, result);
        }

        [Fact]
        public void SupprimerReservation_ShouldDeleteReservation()
        {
            // Arrange
            var reservationId = "1";

            // Act
            _reservationService.SupprimerReservation(reservationId);

            // Assert
            _mockReservationRepository.Verify(repo => repo.Delete(reservationId), Times.Once);
        }

        [Fact]
        public void GererConflits_ShouldReturnFreeSlots()
        {
            // Arrange
            var room = "Room1";
            var debut = DateTime.Now;
            var fin = DateTime.Now.AddHours(1);
            var expectedSlots = new List<string> { "08:00-09:00", "10:00-11:00", "15:00-16:00" };
            _mockReservationRepository.Setup(repo => repo.GetByRoomAndTime(room, debut, fin)).Returns(new List<Reservation>());

            // Act
            var result = _reservationService.GererConflits(room, debut, fin);

            // Assert
            Assert.Equal(expectedSlots, result);
        }
    }
}
