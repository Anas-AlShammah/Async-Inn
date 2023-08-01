namespace Async_Inn.Models.Dtos
{
    public class HotelsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }

        public string City { get; set; }
        public string State { get; set; }

        public int Phone { get; set; }

        public List<HotelRoomsDto> HotelRooms { get; set; } = new();
    }
}
