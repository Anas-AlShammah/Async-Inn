using Async_Inn.Models;
using Async_Inn.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Async_Inn.Interfaces
{
    public interface IHotel
    {
        Task<IEnumerable<HotelsDto>> GetHotels();

        Task<HotelsDto> GetHotel(int id);
        Task PutHotel(int id, HotelsDto hotel);

        Task<HotelsDto> PostHotel(HotelsDto hotel);
        Task DeleteHotel(int id);
        bool HotelExists(int id);

    }
}
