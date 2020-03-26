using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Model;

namespace API.Controllers
{
    [Route("api/People")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly Context _context;

        public PeopleController(Context context)
        {
            _context = context;
        }

        // GET: api/People
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<People>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<People>> GetPeople(int id)
        {
            var people = await _context.Users.FindAsync(id);

            if (people == null)
            {
                return NotFound();
            }

            return people;
        }



        // PUT: api/People/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeople(int id, People people)
        {
            if (id != people.Id)
            {
                return BadRequest();
            }

            _context.Entry(people).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeopleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<People>> PostPeople(People people)
        {
            _context.Users.Add(people);
            await _context.SaveChangesAsync();
             
            return CreatedAtAction(nameof(GetPeople), new { id = people.Id }, people);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<People>> DeletePeople(int id)
        {
            var people = await _context.Users.FindAsync(id);
            if (people == null)
            {
                return NotFound();
            }

            _context.Users.Remove(people);
            await _context.SaveChangesAsync();

            return people;
        }

        private bool PeopleExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
