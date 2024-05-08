using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialEduApi.Data;
using SocialEduApi.Models.Identity;
using SocialEduApi.Models.InputModels;
using SocialEduApi.Models.ViewModels;

namespace SocialEduApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Institutions/5
        [HttpGet("{email}")]
        public async Task<ActionResult<UserDetails>> GetUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) user = await _userManager.FindByNameAsync(email);
            if (user == null) return NotFound("User nije pronađen");

            var returned = new UserDetails(user, _context);
            await returned.FetchRoles(_userManager);
            return returned;
        }

        [Authorize]
        public async Task<ActionResult<UserVM_short>> EditUser(UserEditModel userDetails)
        {
            var email = User.Identities.FirstOrDefault()?.Name;
            if (email == null) return NotFound("Ulogirani user nije pronađen");
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) user = await _userManager.FindByNameAsync(email);
            if (user == null) return NotFound("Ulogirani user nije pronađen");

            //if (user.Email != userDetails.Email)
            //{
            //    await _userManager.SetEmailAsync(user, userDetails.Email);
            //    await _userManager.SetUserNameAsync(user, userDetails.Email);
            //}

            user.FirstName = userDetails.FirstName;
            user.LastName = userDetails.LastName;
            if (!string.IsNullOrEmpty(userDetails.Gender)) user.Gender = userDetails.Gender;
            if (!string.IsNullOrEmpty(userDetails.Location)) user.Location = userDetails.Location;
            if (!string.IsNullOrEmpty(userDetails.Description)) user.Description = userDetails.Description;
            if (!string.IsNullOrEmpty(userDetails.Image)) user.Image = userDetails.Image;
            if (!string.IsNullOrEmpty(userDetails.UserpageBackground)) user.UserpageBackground = userDetails.UserpageBackground;
            if (userDetails.General != null) user.General = string.Join(";;", userDetails.General);
            if (userDetails.Interests != null) user.Interests = string.Join(";;", userDetails.Interests);
            if (userDetails.Education != null) user.Education = string.Join(";;", userDetails.Education);
            if (userDetails.Experience != null) user.Experience = string.Join(";;", userDetails.Experience);
            if (userDetails.Links != null) user.Links = string.Join(";;", userDetails.Links);
            await _userManager.UpdateAsync(user);

            return NoContent();
        }
    }
}
