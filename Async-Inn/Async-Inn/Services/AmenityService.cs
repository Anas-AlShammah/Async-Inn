using Async_Inn.Data;
using Async_Inn.Interfaces;
using Async_Inn.Models;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Services
{
    public class AmenityService : IAmenity
    {
        private readonly AsyncInnDbContext _context;
        public AmenityService(AsyncInnDbContext context)
        {
            _context = context;
        }
        public  bool AmenityExists(int id)
        {
            var amenity = GetAmenity(id);
            if (amenity == null)
            {
                return false;
            }
            return true;
        }

        public async Task DeleteAmenity(int id)
        {
            var amenity = await GetAmenity(id);
             _context.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Amenity>> GetAmenities()
        {
            var amenities = await _context.Amenities
                .Include(e=>e.Rooms)
                .ToListAsync();
            return amenities;
        }

        public async Task<Amenity> GetAmenity(int id)
        {
            var amenity =await _context.Amenities.FindAsync(id);
            return amenity;
            
        }

        public async Task<Amenity> PostAmenity(Amenity amenity)
        {
            await _context.Amenities.AddAsync(amenity);
            var amentiy = await GetAmenity(amenity.Id);
            await _context.SaveChangesAsync();
            return amentiy;
        }

        public async Task PutAmenity(int id, Amenity amenity)
        {
            var amentiyToUpdata = await GetAmenity(id);
            amentiyToUpdata.Name = amenity.Name;
            await _context.SaveChangesAsync();

        }
    }
}
