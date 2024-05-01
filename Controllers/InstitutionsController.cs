using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class InstitutionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public InstitutionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Institutions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Institution>>> GetInstitutions()
        {
            return await _context.Institutions.ToListAsync();
        }

        // GET: api/Institutions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionVM>> GetInstitution(int id)
        {
            var institution = await _context.Institutions.FindAsync(id);
            if (institution == null)
            {
                return NotFound();
            }

            var vm = new InstitutionVM();
            await vm.Init(institution, _userManager, _context);
            return vm;
        }

        // PUT: api/Institutions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitution(int id, Institution institution)
        {
            if (id != institution.Id)
            {
                return BadRequest("IDjevi se ne podudaraju");
            }

            _context.Entry(institution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitutionExists(id))
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

        // POST: api/Institutions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Institution>> PostInstitution(Institution institution)
        {
            institution.Id = 0;
            _context.Institutions.Add(institution);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstitution", new { id = institution.Id }, institution);
        }

        // DELETE: api/Institutions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitution(int id)
        {
            var institution = await _context.Institutions.FindAsync(id);
            if (institution == null)
            {
                return NotFound();
            }

            _context.Institutions.Remove(institution);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Institutions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("user")]
        public async Task<ActionResult> PostInstitutionBelongs(InstitutionBelongsVM belongs)
        {
            var user = await _userManager.FindByEmailAsync(belongs.UserEmail);
            if (user == null) user = await _userManager.FindByNameAsync(belongs.UserEmail);
            if (user == null) return NotFound("User nije pronađen po emailu.");

            var institution = await _context.Institutions.FirstOrDefaultAsync(p => p.Id == belongs.InstitutionID);
            if (institution == null) return NotFound("Institucija nije pronađena");

            if (belongs.RoleName == null) return NotFound("Nije pronađena rola");
            var role = await _context.InstitutionRoles.FirstOrDefaultAsync(p => p.Name.ToLower() ==  belongs.RoleName.ToLower());
            if (role == null) return NotFound("Nije pronađena rola");

            

            var belongsDB = new UserInstitutionBelongs()
            {
                UserID = user.Id,
                InstitutionID = institution.Id,
                RoleID = role.Id,
            };

            var oldBelongs = _context.UserInstitutionBelongses.FirstOrDefault(p => p.InstitutionID == institution.Id && p.UserID == user.Id);
            if (oldBelongs != null)
            {
                oldBelongs.RoleID = belongsDB.RoleID;
            }
            else
            {
                await _context.UserInstitutionBelongses.AddAsync(belongsDB);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Institutions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpDelete("user")]
        public async Task<ActionResult> DeleteInstitutionBelongs(InstitutionBelongsVM belongs)
        {
            var user = await _userManager.FindByEmailAsync(belongs.UserEmail);
            if (user == null) user = await _userManager.FindByNameAsync(belongs.UserEmail);
            if (user == null) return NotFound("User nije pronađen po emailu.");

            var institution = await _context.Institutions.FirstOrDefaultAsync(p => p.Id == belongs.InstitutionID);
            if (institution == null) return NotFound("Institucija nije pronađena");

            var oldBelongs = _context.UserInstitutionBelongses.FirstOrDefault(p => p.InstitutionID == institution.Id && p.UserID == user.Id);
            if (oldBelongs == null) return NotFound("Korisnik nije ni pripadao instituciji");
            _context.UserInstitutionBelongses.Remove(oldBelongs);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool InstitutionExists(int id)
        {
            return _context.Institutions.Any(e => e.Id == id);
        }
    }
}
