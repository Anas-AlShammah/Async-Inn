using Async_Inn.Models;

namespace Async_Inn.Interfaces
{
    public interface IAmenity
    {
        Task<IEnumerable<Amenity>> GetAmenities();

        Task<Amenity> GetAmenity(int id);
        Task PutAmenity(int id, Amenity amenity);

        Task<Amenity> PostAmenity(Amenity amenity);
        Task DeleteAmenity(int id);
        bool AmenityExists(int id);
    }
}
