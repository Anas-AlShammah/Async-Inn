using Async_Inn.Models;
using Microsoft.AspNetCore.Mvc;

namespace Async_Inn.Interfaces
{
    public interface IHotel
    {
        Task<IEnumerable<Hotel>> GetHotels();

        Task<Hotel> GetHotel(int id);
        Task PutHotel(int id, Hotel hotel);

        Task<Hotel> PostHotel(Hotel hotel);
        Task DeleteHotel(int id);
        bool HotelExists(int id);

    }
}
