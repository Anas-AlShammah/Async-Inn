using Async_Inn.Models;
using Async_Inn.Models.Dtos;

namespace Async_Inn.Interfaces
{
    public interface IAmenity
    {
        Task<IEnumerable<AmenitiesDto>> GetAmenities();

        Task<AmenitiesDto> GetAmenity(int id);
        Task PutAmenity(int id, AmenitiesDto amenity);

        Task<AmenitiesDto> PostAmenity(AmenitiesDto amenity);
        Task DeleteAmenity(int id);
        bool AmenityExists(int id);
    }
}
