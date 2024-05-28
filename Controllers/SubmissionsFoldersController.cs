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
using SocialEduApi.Models.InputModels;
using SocialEduApi.Models.ViewModels;

namespace SocialEduApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsFoldersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubmissionsFoldersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet("foruser/{email}")]
        public async Task<ActionResult<IEnumerable<SubmissionsFolderVM>>> GetSubmissionFolders(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) user = await _userManager.FindByNameAsync(email);
            if (user == null) return NotFound("Ulogirani user nije pronađen");

            var folders = _context.SubmissionFolders.Where(p => p.UserID == user.Id).ToList();
            var vms = new List<SubmissionsFolderVM>();
            foreach (var fold in folders)
            {
                fold.GetSubmissions(_context);
                vms.Add(new SubmissionsFolderVM(fold, email));
            }
            return vms;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubmissionsFolderVM>> GetSubmissionFolder(int id)
        {
            var submissionsFolder = await _context.SubmissionFolders.FindAsync(id);

            if (submissionsFolder == null)
            {
                return NotFound("Nije pronađen folder");
            }
            var user = await _userManager.FindByIdAsync(submissionsFolder.UserID);
            submissionsFolder.GetSubmissions(_context);

            return new SubmissionsFolderVM(submissionsFolder, user.Email);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubmissionsFolder(int id, [FromBody]SubmissionsFolderVM submissionsFolder)
        {
            if (id != submissionsFolder.Id)
            {
                return BadRequest("Id u queryu i bodyju se ne podudaraju");
            }

            var folder = _context.SubmissionFolders.FirstOrDefault(p => p.Id == id);
            if (folder == null) return NotFound("Nije pronađen folder s navedenim idjem");
            folder.GetSubmissions(_context);

            if (submissionsFolder.Name != null) folder.Name = submissionsFolder.Name;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("addsubmissions/{id}")]
        public async Task<IActionResult> PutAddSubmissions(int id, [FromBody]List<int> submissionIDs)
        {
            var folder = _context.SubmissionFolders.FirstOrDefault(p => p.Id == id);
            if (folder == null) return NotFound("Nije pronađen folder");
            folder.GetSubmissions(_context);
            var existingSubmissions = folder.Submissions.Select(p => p.Id.Value).ToList();
            var newSubmissions = submissionIDs.Except(existingSubmissions).ToList();

            var allSubs = _context.ProjectSubmissions.ToList();
            var message = "";
            foreach (var subId in newSubmissions)
            {
                var sub = allSubs.FirstOrDefault(p => p.Id == subId);
                if (sub == null)
                {
                    message += $"projekt s idjem {subId} nije pronađen\n";
                    continue;
                }
                var savedSub = new SubmissionFolderContains()
                {
                    SubmissionID = sub.Id,
                    FolderID = folder.Id,
                };
                _context.SubmissionFolderContainses.Add(savedSub);
            }
            _context.SaveChanges();
            return Ok(message);
        }

        [HttpPut("deletesubmissions/{id}")]
        public async Task<IActionResult> PutDeleteSubmissions(int id, List<int> submissionIDs)
        {
            var folder = _context.SubmissionFolders.FirstOrDefault(p => p.Id == id);
            if (folder == null) return NotFound("Nije pronađen folder");
            folder.GetSubmissions(_context);

            var allSubmissions = _context.ProjectSubmissions.ToList();
            var relations = _context.SubmissionFolderContainses.Where(p => p.FolderID == folder.Id).ToList();
            var message = "";
            foreach (var subId in submissionIDs)
            {
                var submission = allSubmissions.FirstOrDefault(p => p.Id == subId);
                if (submission == null)
                {
                    message += $"Projekt s idjem {subId} nije pronađen\n";
                    continue;
                }
                var relation = relations.FirstOrDefault(p => p.SubmissionID == submission.Id);
                if (relation != null)
                {
                    _context.SubmissionFolderContainses.Remove(relation);
                }
            }
            _context.SaveChanges();
            return Ok(message);
        }

        [HttpPost]
        public async Task<ActionResult<SubmissionFolder>> PostSubmissionsFolder(FolderInput input)
        {
            var userEmail = User.Identities.FirstOrDefault()?.Name;
            if (string.IsNullOrEmpty(userEmail)) userEmail = input.UserEmail;
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null) return NotFound($"Korisnik s emailom {userEmail} nije pronađen.");

            var submissionsFolder = new SubmissionFolder()
            {
                UserID = user.Id,
                Name = input.Name
            };

            _context.SubmissionFolders.Add(submissionsFolder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubmissionFolder", new { id = submissionsFolder.Id }, submissionsFolder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubmissionsFolder(int id)
        {
            var submissionsFolder = await _context.SubmissionFolders.FindAsync(id);
            if (submissionsFolder == null)
            {
                return NotFound();
            }

            _context.SubmissionFolders.Remove(submissionsFolder);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
