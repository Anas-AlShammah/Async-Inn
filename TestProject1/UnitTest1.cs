using Async_Inn.Models.Dtos;
using Async_Inn.Services;
using NuGet.Protocol.Core.Types;

namespace TestProject1
{
    public class UnitTest1 : Mock
    {
        [Fact]
        public async void Create_Read()
        {
            var hotel = await CreateAndSaveTestHotel();
            var hotelDto = new HotelsDto()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                City = hotel.City,
                StreetAddress = hotel.StreetAddress,
                State = hotel.State,
                Phone = hotel.Phone,
            };
            var room = await CreateAndSaveTestRoom();
            var repository = new HotelService(_db);
            repository.PostHotel(hotelDto);
            var actHotel = await repository.GetHotel(hotelDto.Id);
            Assert.Equal(hotelDto.Id, actHotel.Id);
            Assert.Equal(hotelDto.Name, actHotel.Name);
            Assert.Equal(hotelDto.City, actHotel.City);
            Assert.Equal(hotelDto.StreetAddress, actHotel.StreetAddress);
            Assert.Equal(hotelDto.State, actHotel.State);
            Assert.Equal(hotelDto.Phone, actHotel.Phone);

        }
        [Fact]
        public async void UpdateHotel()
        {
            var hotel = await CreateAndSaveTestHotel();
            var hotelDto = new HotelsDto()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                City = hotel.City,
                StreetAddress = hotel.StreetAddress,
                State = hotel.State,
                Phone = hotel.Phone,
            };
            var room = await CreateAndSaveTestRoom();
            var repository = new HotelService(_db);
            var hotelDtoToUpdate = new HotelsDto()
            {
                Name = "Updated Name",
                City = "Updated City",
                StreetAddress = hotel.StreetAddress,
                State = hotel.State,
                Phone = hotel.Phone,
            };
            hotelDtoToUpdate.HotelRooms = hotelDto.HotelRooms;
            var actHotel= await repository.PutHotel(hotelDto.Id, hotelDtoToUpdate);
             
          
            
            Assert.Equal(hotelDtoToUpdate.City, actHotel.City);
            Assert.Equal(hotelDtoToUpdate.StreetAddress, actHotel.StreetAddress);
            Assert.Equal(hotelDtoToUpdate.State, actHotel.State);
            Assert.Equal(hotelDtoToUpdate.Phone, actHotel.Phone);

        }
        [Fact]
        public async void DeleteHotel()
        {
            var hotel = await CreateAndSaveTestHotel();
            var hotelDto = new HotelsDto()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                City = hotel.City,
                StreetAddress = hotel.StreetAddress,
                State = hotel.State,
                Phone = hotel.Phone,
            };
            var repository = new HotelService(_db);
            repository.PostHotel(hotelDto);
            var actHotel = await repository.GetHotel(hotelDto.Id);
            await repository.DeleteHotel(hotelDto.Id);
            var deletedHotel = await repository.GetHotel(hotelDto.Id);
            Assert.Null(deletedHotel);
        }
        [Fact]
        public async void Create_ReadRoom()
        {
            var room = await CreateAndSaveTestRoom();
           
            var roomDto = new RoomsDto()
            {
                Name = room.Name,
                Layout = room.Layout
            };
            var repository = new RoomService(_db);
            var actRoom = await repository.GetRoom(room.Id);
            Assert.Equal(roomDto.Name, actRoom.Name);
            Assert.Equal(roomDto.Layout, actRoom.Layout);
        }
        [Fact]
        public async void UpdateRoom()
        {
            var room = await CreateAndSaveTestRoom();

            var roomDto = new RoomsDto()
            {
                Name = room.Name,
                Layout = room.Layout
            };
            var repository = new RoomService(_db);
            var roomDtoToUpdate = new RoomsDto()
            {
                Name = "Updated Name",
                Layout = 1,
            };

            await repository.PutRoom(room.Id, roomDtoToUpdate);
            var actRoom = await repository.GetRoom(room.Id);

            Assert.Equal(roomDtoToUpdate.Name, room.Name);
            Assert.Equal(roomDtoToUpdate.Layout, room.Layout);
        }
        [Fact]
        public async void DeleteRoom()
        {
            var room = await CreateAndSaveTestRoom();

            var roomDto = new RoomsDto()
            {
                Name = room.Name,
                Layout = room.Layout
            };
            var repository = new RoomService(_db);
                 
            await repository.DeleteRoom(room.Id);
            var deletedHotel = await repository.GetRoom(room.Id);
            Assert.Null(deletedHotel);
        }
    }
}