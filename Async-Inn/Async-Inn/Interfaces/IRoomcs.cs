using Async_Inn.Models;

namespace Async_Inn.Interfaces
{
    public interface IRoomcs
    {
        Task<IEnumerable<Room>> GetRoomss();

        Task<Room> GetRoom(int id);
        Task PutRoom(int id, Room room);

        Task<Room> PostRoom(Room room);
        Task DeleteRoom(int id);
        Task AddAmenityToRoom(int roomId, int amenityId);
        Task RemoveAmentityFromRoom(int roomId, int amenityId);
        bool RoomExists(int id);
    }
}
