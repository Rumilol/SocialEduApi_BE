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
    public class _ProjectSubmissionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public _ProjectSubmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectSubmissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectSubmission>>> GetProjectSubmissions()
        {
            return await _context.ProjectSubmissions.ToListAsync();
        }

        // GET: api/ProjectSubmissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectSubmission>> GetProjectSubmission(int id)
        {
            var projectSubmission = await _context.ProjectSubmissions.FindAsync(id);

            if (projectSubmission == null)
            {
                return NotFound();
            }

            return projectSubmission;
        }

        // PUT: api/ProjectSubmissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectSubmission(int id, ProjectSubmission projectSubmission)
        {
            if (id != projectSubmission.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectSubmission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectSubmissionExists(id))
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

        // POST: api/ProjectSubmissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectSubmission>> PostProjectSubmission(ProjectSubmission projectSubmission)
        {
            _context.ProjectSubmissions.Add(projectSubmission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectSubmission", new { id = projectSubmission.Id }, projectSubmission);
        }

        // DELETE: api/ProjectSubmissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectSubmission(int id)
        {
            var projectSubmission = await _context.ProjectSubmissions.FindAsync(id);
            if (projectSubmission == null)
            {
                return NotFound();
            }

            _context.ProjectSubmissions.Remove(projectSubmission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectSubmissionExists(int id)
        {
            return _context.ProjectSubmissions.Any(e => e.Id == id);
        }
    }
}
