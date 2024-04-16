using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialEduApi.Data;
using SocialEduApi.Models.Entities;

namespace SocialEduApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _UserOnSubjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public _UserOnSubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserOnSubjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserOnSubject>>> GetUsersOnSubjects()
        {
            return await _context.UsersOnSubjects.ToListAsync();
        }

        // GET: api/UserOnSubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserOnSubject>> GetUserOnSubject(int id)
        {
            var userOnSubject = await _context.UsersOnSubjects.FindAsync(id);

            if (userOnSubject == null)
            {
                return NotFound();
            }

            return userOnSubject;
        }

        // PUT: api/UserOnSubjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserOnSubject(int id, UserOnSubject userOnSubject)
        {
            if (id != userOnSubject.Id)
            {
                return BadRequest();
            }

            _context.Entry(userOnSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserOnSubjectExists(id))
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

        // POST: api/UserOnSubjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserOnSubject>> PostUserOnSubject(UserOnSubject userOnSubject)
        {
            _context.UsersOnSubjects.Add(userOnSubject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserOnSubject", new { id = userOnSubject.Id }, userOnSubject);
        }

        // DELETE: api/UserOnSubjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserOnSubject(int id)
        {
            var userOnSubject = await _context.UsersOnSubjects.FindAsync(id);
            if (userOnSubject == null)
            {
                return NotFound();
            }

            _context.UsersOnSubjects.Remove(userOnSubject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserOnSubjectExists(int id)
        {
            return _context.UsersOnSubjects.Any(e => e.Id == id);
        }
    }
}
