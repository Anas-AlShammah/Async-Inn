using Async_Inn.Data;
using Async_Inn.Interfaces;
using Async_Inn.Models;
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
           _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Hotel> GetHotel(int id)
        {
            var hotel =await _context.Hotels.Where(h=>h.Id == id).FirstOrDefaultAsync();
            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            var hotels = await _context.Hotels
             .Include(h => h.HotelRooms)
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

        public async Task<Hotel> PostHotel(Hotel hotel)
        {
             _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task  PutHotel(int id, Hotel hotel)
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
