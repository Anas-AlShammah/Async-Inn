namespace Async_Inn.Models.Dtos
{
    public class HotelRoomsDto
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }

        public int RoomNumber { get; set; }
        public decimal Rate { get; set; }
        public bool PetFriendly { get; set; }
        public RoomsDto Rooms { get; set; }
    }
}
