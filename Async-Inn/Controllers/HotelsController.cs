using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Interfaces;
using Async_Inn.Models.Dtos;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _context;
        private readonly IHoteRoom _hotelRoom;

        public HotelsController(IHotel context, IHoteRoom hotelRoom)
        {
            _context = context;
            _hotelRoom = hotelRoom;
        }

        // GET: api/Hotels
        [HttpGet("{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelsDto>>> GetHotelRooms(int hotelId)
        {
            var rooms = await _hotelRoom.GetAllRooms(hotelId);
            return Ok(rooms);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetHotels()
        {
            var rooms = await _context.GetHotels();
            return Ok(rooms);
        }
        [HttpGet("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<Room>> GetHotelRoom(int hotelId,int roomNumber)
        {
            var room = await _hotelRoom.GetRoom(hotelId,roomNumber);
            return Ok(room);
        }
        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelsDto>> GetHotel(int id)
        {
          var hotel = await _context.GetHotel(id);
            if (hotel != null) { 
            return Ok(hotel);

            }
            return NotFound();
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelsDto hotel)
        {
          
            return Ok(_context.PutHotel(id, hotel));
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelsDto>> PostHotel(HotelsDto hotel)
        {
          await _context.PostHotel(hotel);
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }
        [HttpPost("{hotelId}/rooms")]
        public async Task<ActionResult> PostRoom (int id, HotelRoom hotelRoom)
        {
            await _hotelRoom.PostRoom(id, hotelRoom);
            return Ok();
        }
        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _context.DeleteHotel(id);
            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return _context.HotelExists(id);
        }
    }
}
