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
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenity _context;

        public AmenitiesController(IAmenity context)
        {
            _context = context;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenitiesDto>>> GetAmenities()
        {
          var  amenities= await _context.GetAmenities();
            return Ok(amenities);
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenitiesDto>> GetAmenity(int id)
        {
          var amenity = await _context.GetAmenity(id);
            return Ok(amenity);
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity(int id, AmenitiesDto amenity)
        {
           await _context.PutAmenity(id, amenity);
            return Ok();
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Amenity>> PostAmenity(AmenitiesDto amenity)
        {
          await _context.PostAmenity(amenity);

            return CreatedAtAction("GetAmenity", new { id = amenity.Id }, amenity);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            await _context.DeleteAmenity(id);

            return NoContent();
        }

        private bool AmenityExists(int id)
        {
            return _context.AmenityExists(id);
        }
    }
}
