using Humanizer;
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
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<ProjectSubmissionVM>>> GetHomePageSubmissions(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("Nije pronađen user s tim emailom. ");
            }

            //getting submissions from users saved by the user
            var savedUserFolderIDs = _context.SavedUsersFolders.AsNoTracking()
                                                       .Where(p => p.UserID == user.Id)
                                                       .Select(p => p.Id)
                                                       .ToList();
            var savedUsersIDs = _context.SavedUsers.Where(p => savedUserFolderIDs.Contains(p.FolderID))
                                                   .Select(p => p.UserID)
                                                   .ToList();
            var followedUsersSubmissions = _context.ProjectSubmissions.AsNoTracking()
                                                         .Include(p => p.User)
                                                         .Include(p => p.Subject)
                                                         .Include(p => p.ProjectTask)
                                                         .Where(p => savedUsersIDs.Contains(p.UserID))
                                                         .ToList();

            //getting submissions from subject the user is on
            var userSubjectIDs = _context.UsersOnSubjects.Where(p => p.UserID == user.Id)
                                                         .Select(p => p.SubjectID)
                                                         .ToList();
            var relatedProjectSubmissions = _context.ProjectSubmissions
                                                    .AsNoTracking()
                                                    .Include(p => p.Subject)
                                                    .Include(p => p.User)
                                                    .Include(p => p.ProjectTask)
                                                    .Where(p => userSubjectIDs.Contains(p.SubjectID.GetValueOrDefault(-1)) || 
                                                       userSubjectIDs.Contains(p.ProjectTask != null ? p.ProjectTask.SubjectID : -1))
                                                    .Where(p => p.UserID != user.Id);

            var submissions = followedUsersSubmissions.Union(relatedProjectSubmissions)
                                                      .OrderByDescending(p => p.UploadDate);

            var users = _context.Users.AsNoTracking().ToList().Select(p => new UserVM_short(p)).ToList();
            var allLikes = _context.ProjectSubmissionLikes.AsNoTracking().ToList();

            var vms = submissions.DistinctBy(p => p.Id)
                                 .OrderByDescending(p => p.UploadDate)
                                 .Select(p => new ProjectSubmissionVM(p, users, allLikes, user.Id))
                                 .Take(5)
                                 .ToList();

            return vms;
        }

        [HttpPost("searchresults")]
        public async Task<ActionResult<IEnumerable<SearchResultVM>>> GetSearchResults(SearchRequest request)
        {
            return SearchResultVM.GetSearchResults(request.SearchString, _context, request.UserEmail);
        }
    }
}
