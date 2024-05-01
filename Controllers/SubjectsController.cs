using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialEduApi.Data;
using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;
using SocialEduApi.Models.ViewModels;

namespace SocialEduApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubjectsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            return await _context.Subjects.Include(p => p.Institution).ToListAsync();
        }

        [HttpGet("foruser/{email}")]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjectsForUser(string email, bool admin = false)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) user = await _userManager.FindByNameAsync(email);
            if (user == null) return NotFound($"Nije pronađen korisnik {email}");

            var memberships = await _context.UsersOnSubjects.Include(p => p.Subject)
                                                 .ThenInclude(p => p.Institution)
                                                 .Where(p => p.UserID == user.Id)
                                                 .ToListAsync();
            if (admin) memberships = memberships.Where(p => p.HasAdminRights).ToList();
            return memberships.Select(p => p.Subject).ToList();
        }

        [HttpGet("forinstitution/{id}")]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjectsForInstitution(int id)
        {
            var institution = _context.Institutions.FirstOrDefault(p => p.Id == id);
            if (institution == null) return NotFound($"Nije pronađena institucija s idjem {id}");

            return _context.Subjects.Where(p => p.InstitutionID == id).ToList();
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectVM>> GetSubject(int id)
        {
            var subject = await _context.Subjects.Include(p => p.Institution).FirstOrDefaultAsync(p => p.Id == id);
            if (subject == null)
            {
                return NotFound($"Nije pronađen predmet s IDjem {id}");
            }

            var members = _context.UsersOnSubjects
                .Include(p => p.User)
                .Where(p => p.SubjectID == subject.Id)
                .ToList();
            members.ForEach(p => { p.Subject = null; });

            var projectTasks = _context.ProjectTasks.AsNoTracking().Where(p => p.SubjectID == subject.Id).ToList();
            var projectTaskIDs = projectTasks.Select(p => p.Id).ToList();
            var projectTaskSubmissions = _context.ProjectSubmissions.AsNoTracking()
                                                                    .Include(p => p.User)
                                                                    .Where(p => projectTaskIDs.Contains(p.ProjectTaskID.GetValueOrDefault(-1)))
                                                                    .ToList();

            var forums = _context.Forums.AsNoTracking().Where(p => p.SubjectID == subject.Id).ToList();
            var forumIDs = forums.Select(p => p.Id).ToList();
            var forumPosts = _context.ForumPosts.AsNoTracking().Include(p => p.User)
                                                .Where(p => forumIDs.Contains(p.ForumID))
                                                .ToList();

            var vm = new SubjectVM(subject, members, forums, forumPosts, projectTasks, projectTaskSubmissions);

            return vm;
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, SubjectVM? input)
        {
            if (input == null) return BadRequest();
            if (id != input.Id)
            {
                return BadRequest($"ID u adresi {id} se ne podudara s idjem u bodyju {input.Id}");
            }

            var subject = _context.Subjects.FirstOrDefault(p => p.Id == id);
            if (subject == null) return NotFound($"Nije pronađen predmet s idjem {id}");

            subject.Name = input.Name;
            subject.Abbreviation = input.Abbreviation;
            subject.Description = input.Description;
            subject.PageBackground = input.PageBackground;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject? subject)
        {
            if (subject == null) return BadRequest();
            subject.Id = 0;
            subject.Institution = null;
            subject.CreatedDate = DateTime.Now;

            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubject", new { id = subject.Id }, subject);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
