using Async_Inn.Data;
using Async_Inn.Interfaces;
using Async_Inn.Models;
using Async_Inn.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Services
{
    public class HotelService : IHotel
    {
        private readonly AsyncInnDbContext _context;

        public HotelService(AsyncInnDbContext context)
        {
            _context=context;
           

        }
        public async Task DeleteHotel(int id)
        {
            var hotel = await GetHotel(id);
            if(hotel != null) { 
           //_context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<HotelsDto> GetHotel(int id)
        {

            var hotel = await _context.Hotels
       .Where(h => h.Id == id) // Filter hotels by the given id
       .Include(h => h.HotelRooms)
           .ThenInclude(hr => hr.Room)
           .ThenInclude(hrr => hrr.Amenities)// Eager load related entities
                                      // Eager load related layout information if needed
       .Select(h => new HotelsDto
       {
           Id = h.Id,
           Name = h.Name,
           StreetAddress = h.StreetAddress,
           City = h.City,
           State = h.State,
           Phone = h.Phone,
           HotelRooms = h.HotelRooms.Select(hr => new HotelRoomsDto
           {
               HotelId = hr.HotelId,
               RoomId = hr.RoomId,
               RoomNumber = hr.RoomNumber,
               Rate = hr.Rate,
               PetFriendly = hr.PetFriendly,
               Rooms = new RoomsDto
               {
                   Id = hr.Room.Id,
                   Name = hr.Room.Name,
                   Layout = hr.Room.Layout,
                   Amenities=hr.Room.Amenities.Select(a=>new AmenitiesDto
                   { Id = a.Id, Name = a.Name 
                   }).ToList()
               }
           }).ToList()
       })
       .FirstOrDefaultAsync();






            return hotel;
        }

        public async Task<IEnumerable<HotelsDto>> GetHotels()
        {
            var hotels = await _context.Hotels
             .Select(h => new HotelsDto
             {
                 Id = h.Id,
                 Name = h.Name,
                 StreetAddress = h.StreetAddress,
                 City = h.City,
                 State = h.State,
                 Phone = h.Phone
             })

                .ToListAsync();
            return hotels;
        }

        public bool HotelExists(int id)
        {
            var hotel = GetHotel(id);
            if (hotel != null)
            {
                return true;
            }
            return false;
        }

        public async Task<HotelsDto> PostHotel(HotelsDto hotel)
        {
             //_context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task  PutHotel(int id, HotelsDto hotel)
        {
            var hotelupdata = await GetHotel(id);
            if (hotelupdata != null)
            {
                hotelupdata.State = hotel.State;
                hotelupdata.StreetAddress = hotel.StreetAddress;
                hotelupdata.City = hotel.City;
                hotelupdata.Phone = hotel.Phone;
                await _context.SaveChangesAsync();
            }
          
        }
    }
}
