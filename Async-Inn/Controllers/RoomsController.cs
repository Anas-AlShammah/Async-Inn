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
using Microsoft.AspNetCore.Authorization;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomcs _context;

        public RoomsController(IRoomcs context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RoomsDto>>> GetRooms()
        {
         var Rooms =await _context.GetRoomss();
            return Ok(Rooms);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RoomsDto>> GetRoom(int id)
        {
          var room = await _context.GetRoom(id);
            return Ok(room);
        }
        [HttpPost("{roomId}/Amenity/{amenityId}")]
        [Authorize(Roles = "DistrictManager,PropertyManager,Agent")]
        public async Task<ActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
            await _context.AddAmenityToRoom(roomId, amenityId);
            return Ok();
        }
        [HttpDelete("{roomId}/Amenity/{amenityId}")]
        [Authorize(Roles = "DistrictManager,Agent")]
        public async Task<ActionResult> RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            await _context.RemoveAmentityFromRoom(roomId, amenityId);
            return NoContent();
        }
        // PUT: api/Rooms/5
      
        [HttpPut("{id}")]
        [Authorize(Roles = "DistrictManager, PropertyManager,Agent")]
        public async Task<IActionResult> PutRoom(int id, RoomsDto room)
        {
            await _context.PutRoom(id, room);

            return NoContent();
        }

        // POST: api/Rooms
       
        [HttpPost]
        [Authorize(Policy = "create")]
        public async Task<ActionResult<RoomsDto>> PostRoom(RoomsDto room)
        {
          await _context.PostRoom(room);

            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "delete")]
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
