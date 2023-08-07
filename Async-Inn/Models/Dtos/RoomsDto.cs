namespace Async_Inn.Models.Dtos
{
    public class RoomsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Layout { get; set; }
       
        public List<AmenitiesDto> Amenities { get; set; } = new();
      
    }
}
