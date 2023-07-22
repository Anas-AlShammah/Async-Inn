using Async_Inn.Data;
using Async_Inn.Interfaces;
using Async_Inn.Models;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Services
{
    public class RoomService : IRoomcs
    {
        private readonly AsyncInnDbContext _context;
        public RoomService(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task DeleteRoom(int id)
        {
            var room =await GetRoom(id);
            if (room != null) { 
             _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Room> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            return room;
        }

        public async Task<IEnumerable<Room>> GetRoomss()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return rooms;
        }

        public async Task<Room> PostRoom(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            var roomToReturn = await _context.Rooms.FirstOrDefaultAsync(r=>r.Id == room.Id);
            return roomToReturn;
        }

        public async Task PutRoom(int id, Room room)
        {
            var roomUpdate = await GetRoom(id);
            roomUpdate.Name = room.Name;
            roomUpdate.Layout = room.Layout;
            await _context.SaveChangesAsync();
        }

        public bool RoomExists(int id)
        {
            var room = GetRoom(id);
            if (room == null)
            {
                return false;
            }
            return true;
        }
    }
}
