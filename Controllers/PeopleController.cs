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
    public class PeopleController: ControllerBase
    {
        private readonly AppDbContext _context; 

        public PeopleController(AppDbContext context)       // constructor
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult> GetPeople()
        {
            var people = await _context.People.Select(p => new {
                p.Id,
                p.Name,
                p.Gender,
                p.Age
            }).ToListAsync(); 
            
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPerson(int Id)
        {
            var person = await _context.People.FindAsync(Id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(new {
                person.Id,
                person.Name,
                person.Gender,
                person.Age
            });
        }
        
        [HttpPost]
        public async Task<ActionResult> CreatePerson(Person entity)
        {
            _context.People.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction( 
                nameof(GetPerson),
                new { id = entity.Id },
                new {
                    entity.Id,
                    entity.Name,
                    entity.Gender,
                    entity.Age
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePerson(int id, Person entity)
        {
            if(id != entity.Id)
            {
                return BadRequest();
            }

            var person = await _context.People.FindAsync(id);

            if(person == null)
            {
                return NotFound();
            }

            person.Id = entity.Id;
            person.Name = entity.Name;
            person.Gender = entity.Gender;
            person.Age = entity.Age;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);
            
            if(person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}