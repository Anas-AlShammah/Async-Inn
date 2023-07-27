using Async_Inn.Models;

namespace Async_Inn.Interfaces
{
    public interface IHoteRoom
    {
        Task<IEnumerable<Room>> GetAllRooms(int hotelId);
        Task PostRoom(int hotelId, HotelRoom hotelRoom); 

        Task<Room> GetRoom(int roomId,int hotelId);

        Task putRoom (int HotelId,int roomNumber,Room room);
        Task DeleteRoom (int HotelId, int roomNumber);
       
    }
}
