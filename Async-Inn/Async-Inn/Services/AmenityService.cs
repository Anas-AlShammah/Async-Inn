using Async_Inn.Data;
using Async_Inn.Interfaces;
using Async_Inn.Models;
using Async_Inn.Models.Dtos;
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
            //var amenity = await GetAmenity(id);
            // _context.Amenities.Remove(amenity);
            //await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AmenitiesDto>> GetAmenities()
        {
            var amenities = await _context.Amenities
                .Select(a=>new AmenitiesDto
                {
                    Id = a.Id,
                    Name = a.Name,
                }
                )
                .ToListAsync();
            return amenities;
        }

        public async Task<AmenitiesDto> GetAmenity(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);
            var amenityDto = new AmenitiesDto
            {
                Id = amenity.Id,
                Name = amenity.Name
            };
            return amenityDto;
            
            
        }

        public async Task<AmenitiesDto> PostAmenity(AmenitiesDto amenity)
        {
            var amentiyadd = new Amenity
            {
                Name = amenity.Name,
            };
            await _context.Amenities.AddAsync(amentiyadd);
            //var amentiy = await GetAmenity(amenity.Id);
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task PutAmenity(int id, AmenitiesDto amenity)
        {
            var amentiyToUpdata = await _context.Amenities.FindAsync(id);

            amentiyToUpdata.Name = amenity.Name;
            await _context.SaveChangesAsync();
            //_context.Entry(amentiyToUpdata).Reload();
            //var amentiyUpdata = await _context.Amenities.FindAsync(id);
            //Console.WriteLine(amentiyUpdata.Name);
            //Console.WriteLine(amentiyToUpdata.Name);
            //_context.Entry(amentiyToUpdata).State = EntityState.Modified;
            //await _context.SaveChangesAsync();

        }
    }
}
