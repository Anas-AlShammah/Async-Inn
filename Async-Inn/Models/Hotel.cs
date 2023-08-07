using System.Text.Json.Serialization;

namespace Async_Inn.Models
{
    public class Hotel
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public int Phone { get; set; }
      
        public List<HotelRoom>? HotelRooms { get; set; } = new();
        
        //public List<Room>? Rooms { get; set; }
    }
}
