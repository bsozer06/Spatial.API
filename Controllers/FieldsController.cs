using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spatial.API.Entities;


namespace Spatial.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FieldsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FieldsController(AppDbContext context)           // constructor
        {
            _context = context;
        }

        // https://localhost:5001/api/fields
        [HttpGet]
        public async Task<ActionResult> GetFields()
        {
            var fields = await _context.Fields.Select(f => new
            {
                f.Id,
                f.Name,
                f.IsActive,
                f.NeighId,
                f.Person,
                f.PersonId,
                f.CityId,
                f.CreatedTime,
                f.Wkt
            }).ToListAsync();

            return Ok(fields);   // status:200
        }

        // https://localhost:5001/api/fields/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetField(int id)
        {
            var f = await _context.Fields.FindAsync(id);

            if (f == null)
            {
                return NotFound();      // status: 404
            }
            return Ok(new
            {
                f.Id,
                f.Name,
                f.IsActive,
                f.NeighId,
                f.Person,
                f.PersonId,
                f.CityId,
                f.CreatedTime,
                f.Wkt
            });
        }

        [HttpPost]
        public async Task<ActionResult> CreateField(Field entitiy)
        {
            _context.Fields.Add(entitiy);

            await _context.SaveChangesAsync();

            // status: 201
            return CreatedAtAction( nameof(GetField),
                                    new { id = entitiy.Id },
                                    new
                                    {
                                        entitiy.Id,
                                        entitiy.Name,
                                        entitiy.IsActive,
                                        entitiy.NeighId,
                                        entitiy.Person,
                                        entitiy.PersonId,
                                        entitiy.CityId,
                                        entitiy.CreatedTime,
                                        entitiy.Wkt
                                    });
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateField(int id, Field entitiy)
        {
            if (id != entitiy.Id)
            {
                return BadRequest();    // status: 400
            }

            var field = await _context.Fields.FindAsync(id);

            if (field == null)
            {
                NotFound();
            }
            field.Id = entitiy.Id;
            field.Geom = entitiy.Geom;
            field.IsActive = entitiy.IsActive;
            field.Name = entitiy.Name;
            field.NeighId = entitiy.NeighId;
            field.PersonId = entitiy.PersonId;
            field.CityId = entitiy.CityId;
            field.Wkt = entitiy.Wkt;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }
            return NoContent();     // status: 204
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            var field = await _context.Fields.FindAsync(id);

            if (field == null)
            {
                NotFound();
            }

            _context.Fields.Remove(field);
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}