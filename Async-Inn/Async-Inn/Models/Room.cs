using System.Text.Json.Serialization;

namespace Async_Inn.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Layout { get; set; }
        [JsonIgnore]
        public List<Amenity> Amenities { get; set; } = new();
        [JsonIgnore]
        public List<HotelRoom>? HotelRooms { get; set; } = new();
        //[JsonIgnore]
        //public List<Hotel>? Hotels { get; set; }
    }
}
