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

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomcs _context;

        public RoomsController(IRoomcs context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
         var Rooms =await _context.GetRoomss();
            return Ok(Rooms);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
          var room = await _context.GetRoom(id);
            return Ok(room);
        }
        [HttpPost("{roomId}/Amenity/{amenityId}")]
        public async Task<ActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
            await _context.AddAmenityToRoom(roomId, amenityId);
            return Ok();
        }
        [HttpDelete("{roomId}/Amenity/{amenityId}")]
        public async Task<ActionResult> RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            await _context.RemoveAmentityFromRoom(roomId, amenityId);
            return NoContent();
        }
        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            await _context.PutRoom(id, room);

            return NoContent();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
          await _context.PostRoom(room);

            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _context.DeleteRoom(id);

            return NoContent();
        }

        private bool RoomExists(int id)
        {
            return _context.RoomExists(id);
        }
    }
}
