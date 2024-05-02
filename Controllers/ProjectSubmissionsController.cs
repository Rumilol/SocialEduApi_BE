using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialEduApi.Data;
using SocialEduApi.Migrations;
using SocialEduApi.Models.Entities;
using SocialEduApi.Models.ViewModels;

namespace SocialEduApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectSubmissionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectSubmissionsController(ApplicationDbContext context)
        {
            _context = context;
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

        // POST: api/ProjectSubmissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectSubmission>> PostProjectSubmission(ProjectSubmissionVM vm)
        {
            if (vm.Title == null) return BadRequest("Fali naslov");
            var user = _context.Users.FirstOrDefault(p => p.Email == vm.UserEmail);
            if (user == null) return NotFound("Nije pronađen korisnik s tim emailom.");

            var projectSubmission = new ProjectSubmission()
            {
                SubjectID = vm.SubjectID,
                ProjectTaskID = vm.ProjectTaskID,
                UserID = user.Id,
                Title = vm.Title,
                Description = vm.Description,
                Link = vm.Link,
                ImageLink = vm.ImageLink,
                Grade = vm.Grade,
                UploadDate = DateTime.Now,
            };

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
    }
}
