using Async_Inn.Models;
using Async_Inn.Models.Dtos;

namespace Async_Inn.Interfaces
{
    public interface IRoomcs
    {
        Task<IEnumerable<RoomsDto>> GetRoomss();

        Task<RoomsDto> GetRoom(int id);
        Task PutRoom(int id, RoomsDto room);

        Task<RoomsDto> PostRoom(RoomsDto room);
        Task DeleteRoom(int id);
        Task AddAmenityToRoom(int roomId, int amenityId);
        Task RemoveAmentityFromRoom(int roomId, int amenityId);
        bool RoomExists(int id);
    }
}
