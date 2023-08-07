using Async_Inn.Data;
using Async_Inn.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly AsyncInnDbContext _db;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AsyncInnDbContext(
                new DbContextOptionsBuilder<AsyncInnDbContext>()
                    .UseSqlite(_connection)
                    .Options);

            _db.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
        protected async Task<Hotel> CreateAndSaveTestHotel()
        {
            var hotel = new Hotel
            {
                Name = "Test Hotel",
                StreetAddress = "street1",
                City = "Amman",
                State = "00962",
                Country = "Jordan",
                Phone = 07898231
            };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, hotel.Id);
            return hotel;
        }
        protected async Task<Room> CreateAndSaveTestRoom()
        {
            var room = new Room
            {
                Name = "TEstRoom",
                Layout = 1
            };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, room.Id);
            return room;
        }
    }
}
