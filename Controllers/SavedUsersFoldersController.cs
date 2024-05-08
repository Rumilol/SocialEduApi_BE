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
    public class SavedUsersFoldersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SavedUsersFoldersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/SavedUsersFolders
        [HttpGet("foruser/{email}")]
        public async Task<ActionResult<IEnumerable<SavedUsersFolderVM>>> GetSavedUsersFolders(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) user = await _userManager.FindByNameAsync(email);
            if (user == null) return NotFound("Ulogirani user nije pronađen");

            var folders = _context.SavedUsersFolders.Where(p => p.UserID == user.Id).ToList();
            var vms = new List<SavedUsersFolderVM>();
            foreach (var fold in folders)
            {
                fold.GetUsers(_context);
                vms.Add(new SavedUsersFolderVM(fold, email));
            }
            return vms;
        }

        // GET: api/SavedUsersFolders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SavedUsersFolderVM>> GetSavedUsersFolder(int id)
        {
            var savedUsersFolder = await _context.SavedUsersFolders.FindAsync(id);

            if (savedUsersFolder == null)
            {
                return NotFound("Nije pronađen folder");
            }
            savedUsersFolder.GetUsers(_context);
            var user = await _userManager.FindByIdAsync(savedUsersFolder.UserID);

            return new SavedUsersFolderVM(savedUsersFolder, user.Email);
        }

        // PUT: api/SavedUsersFolders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSavedUsersFolder(int id, [FromBody]SavedUsersFolderVM savedUsersFolder)
        {
            if (id != savedUsersFolder.Id)
            {
                return BadRequest("Id u queryu i bodyju se ne podudaraju");
            }

            var folder = _context.SavedUsersFolders.FirstOrDefault(p => p.Id == id);
            if (folder == null) return NotFound("Nije pronađen folder s navedenim idjem");
            folder.GetUsers(_context);

            if (savedUsersFolder.Name != null) folder.Name = savedUsersFolder.Name;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("addusers/{id}")]
        public async Task<IActionResult> PutAddSavedUsers(int id, [FromBody]List<UserVM_short> users)
        {
            var folder = _context.SavedUsersFolders.FirstOrDefault(p => p.Id == id);
            if (folder == null) return NotFound("Nije pronađen folder");
            folder.GetUsers(_context);
            var existingUsers = folder.Users.Select(p => p.Email).ToList();
            var newUsers = users.Select(p => p.Email).Except(existingUsers).ToList();

            var allUsers = _context.AspNetUsers.ToList();
            var message = "";
            foreach (var email in newUsers)
            {
                var user = allUsers.FirstOrDefault(p => p.Email == email);
                if (user == null)
                {
                    message += $"korisnik {email} nije pronađen\n";
                    continue;
                }
                var savedUser = new SavedUser()
                {
                    UserID = user.Id,
                    FolderID = folder.Id,
                };
                _context.SavedUsers.Add(savedUser);
            }
            _context.SaveChanges();
            return Ok(message);
        }

        [HttpPut("deleteusers/{id}")]
        public async Task<IActionResult> PutDeleteSavedUsers(int id, List<UserVM_short> users)
        {
            var folder = _context.SavedUsersFolders.FirstOrDefault(p => p.Id == id);
            if (folder == null) return NotFound("Nije pronađen folder");
            folder.GetUsers(_context);
            var deletedUsers = users.Select(p => p.Email).ToList();

            var allUsers = _context.AspNetUsers.ToList();
            var relations = _context.SavedUsers.Where(p => p.FolderID == folder.Id).ToList();
            var message = "";
            foreach (var email in deletedUsers)
            {
                var user = allUsers.FirstOrDefault(p => p.Email == email);
                if (user == null)
                {
                    message += $"korisnik {email} nije pronađen\n";
                    continue;
                }
                var relation = relations.FirstOrDefault(p => p.UserID == user.Id);
                if (relation != null)
                {
                    _context.SavedUsers.Remove(relation);
                }
            }
            _context.SaveChanges();
            return Ok(message);
        }

        // POST: api/SavedUsersFolders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SavedUsersFolder>> PostSavedUsersFolder(FolderInput input)
        {
            var userEmail = User.Identities.FirstOrDefault()?.Name;
            if (string.IsNullOrEmpty(userEmail)) userEmail = input.UserEmail;
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null) return NotFound($"Korisnik s emailom {userEmail} nije pronađen.");

            var savedUsersFolder = new SavedUsersFolder()
            {
                UserID = user.Id,
                Name = input.Name
            };

            _context.SavedUsersFolders.Add(savedUsersFolder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSavedUsersFolder", new { id = savedUsersFolder.Id }, savedUsersFolder);
        }

        // DELETE: api/SavedUsersFolders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSavedUsersFolder(int id)
        {
            var savedUsersFolder = await _context.SavedUsersFolders.FindAsync(id);
            if (savedUsersFolder == null)
            {
                return NotFound();
            }

            _context.SavedUsersFolders.Remove(savedUsersFolder);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
