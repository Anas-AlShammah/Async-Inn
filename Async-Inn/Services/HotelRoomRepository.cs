
using Async_Inn.Data;
using Async_Inn.Interfaces;
using Async_Inn.Models;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Services
{
    public class HotelRoomRepository : IHoteRoom
    {
        private readonly AsyncInnDbContext _context;
        public HotelRoomRepository(AsyncInnDbContext context)
        {
            _context= context;
        }

        public async Task DeleteRoom(int HotelId, int roomNumber)
        {
            var room = await GetRoom(HotelId, roomNumber);
            var rooms = await _context.hotelRooms
           .Where(hr => hr.HotelId == HotelId)
           .Select(hr => hr.Room)
           .ToListAsync();
            rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Room>> GetAllRooms(int hotelId)
        {
            //var hotel =await _context.Hotels.Where(h => h.Id == hotelId).Include(h=>h.Rooms).FirstOrDefaultAsync();
            //var rooms =  hotel.Rooms;
            //return  rooms;
            //return hotel;
            var rooms = await _context.hotelRooms
                  .Include(r => r.Room)
            .Where(hr => hr.HotelId == hotelId)
           
            .Select(hr => hr.Room )
           
            .ToListAsync();
            return rooms;
        }

        public async Task<Room> GetRoom( int hotelId,int roomNum)
        {
            var room = await _context.hotelRooms
       .Where(hr => hr.HotelId == hotelId && hr.RoomNumber == roomNum)
       .Select(hr => hr.Room )
       .FirstOrDefaultAsync();

            return room;

        }

        public async Task PostRoom(int hotelId, HotelRoom hotelRoom)
        {
            var hotel = await _context.Hotels
                .Where(h => h.Id == hotelId)
                .Include(h => h.HotelRooms)
                .FirstOrDefaultAsync();
            var hotelToAdd = await _context.Hotels
               .Where(h => h.Id == hotelId)
               .FirstOrDefaultAsync();
            var roomToADD = await _context.Rooms
                .Where(h => h.Id == hotelRoom.RoomId)
                .FirstOrDefaultAsync();
            hotelRoom.Hotel = hotelToAdd;
            hotelRoom.Room = roomToADD;
            hotel.HotelRooms.Add(hotelRoom);
           
            
            await _context.SaveChangesAsync();
           
        }

        public async Task putRoom(int roomId, int roomNumber,Room room)
        {
            var roomToUpdata =await GetRoom(roomId, roomNumber);
            roomToUpdata.Name = room.Name;
            roomToUpdata.Layout = room.Layout;
            await _context.SaveChangesAsync();

        }
    }
}
