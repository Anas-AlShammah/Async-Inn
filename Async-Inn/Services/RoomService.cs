using Async_Inn.Data;
using Async_Inn.Interfaces;
using Async_Inn.Models;
using Async_Inn.Models.Dtos;
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

        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            var rooms = await _context.Rooms
               .Include(e => e.Amenities)
               .ToListAsync();
            var amenity = await _context.Amenities.Where(e => e.Id == amenityId).FirstOrDefaultAsync();
            var room= rooms.Where(e=>e.Id== roomId).FirstOrDefault();
            room.Amenities.Add(amenity);
            await _context.SaveChangesAsync();

        }
        public async Task RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            var rooms = await _context.Rooms
              .Include(e => e.Amenities)
              .ToListAsync();
            var amenity = await _context.Amenities.Where(e => e.Id == amenityId).FirstOrDefaultAsync();
            var room = rooms.Where(e => e.Id == roomId).FirstOrDefault();
            room.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(int id)
        {
            var room =await _context.Rooms.Where(r=>r.Id== id).FirstOrDefaultAsync();
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RoomsDto> GetRoom(int id)
        {
            var room = await _context.Rooms
                .Select(r=>new RoomsDto
                {
                    Id = r.Id,
                    Layout = r.Layout,
                    Name = r.Name,
                    Amenities =r.Amenities.Select(a=> new AmenitiesDto
                    {
                        Id= a.Id,
                        Name= a.Name
                    }).ToList()
                }
                )
                .Where(r=>r.Id==id).FirstOrDefaultAsync();
           
            return room;
        }

        public async Task<IEnumerable<RoomsDto>> GetRoomss()
        {
            var rooms = await _context.Rooms
                .Select(r => new RoomsDto
                {
                    Id = r.Id,
                    Layout = r.Layout,
                    Name = r.Name,
                    Amenities = r.Amenities.Select(a => new AmenitiesDto
                    {
                        Id = a.Id,
                        Name = a.Name
                    }).ToList()
                }
                )
                .ToListAsync();
               
            return rooms;
        }

        public async Task<RoomsDto> PostRoom(RoomsDto room)
        {
            var roomToAdd = new Room();
            roomToAdd.Name = room.Name;
            roomToAdd.Layout = room.Layout;
            roomToAdd.Amenities=room.Amenities.Select(a=>new Amenity
            {
                Id=a.Id,
                Name=a.Name
            }).ToList();
               
            await _context.Rooms.AddAsync(roomToAdd);
            await _context.SaveChangesAsync();
            var roomToReturn = await _context.Rooms.FirstOrDefaultAsync(r=>r.Id == room.Id);
            return room;
        }

        public async Task PutRoom(int id, RoomsDto room)
        {
            var roomToUpdate = await _context.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
            roomToUpdate.Name = room.Name;
            roomToUpdate.Layout = room.Layout;
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
