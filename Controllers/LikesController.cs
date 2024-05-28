using Microsoft.AspNetCore.Authorization;
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
    public class LikesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LikesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: api/Likes/submission/ID
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost("submission/{submissionId}")]
        public async Task<ActionResult<ProjectSubmissionLike>> PostProjectSubmissionLike(int submissionId)
        {
            var submission = _context.ProjectSubmissions.FirstOrDefault(p => p.Id == submissionId);
            if (submission == null)
            {
                return NotFound($"Nije pronađen project submission s IDjem {submissionId}");
            }
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound("Nije pronađen prijavljeni korisnik.");
            }

            var like = new ProjectSubmissionLike()
            {
                ProjectSubmissionID = submission.Id,
                UserId = user.Id
            };

            var existingLike = await _context.ProjectSubmissionLikes
                                    .FirstOrDefaultAsync(p => p.ProjectSubmissionID == submission.Id && 
                                                         p.UserId == user.Id);
            if (existingLike != null) return Ok(existingLike);

            _context.ProjectSubmissionLikes.Add(like);
            await _context.SaveChangesAsync();
            return Ok(like);
        }

        [Authorize]
        [HttpPost("submission/{submissionId}/remove")]
        public async Task<ActionResult<ProjectSubmissionLike>> RemoveProjectSubmissionLike(int submissionId)
        {
            var submission = _context.ProjectSubmissions.FirstOrDefault(p => p.Id == submissionId);
            if (submission == null)
            {
                return NotFound($"Nije pronađen project submission s IDjem {submissionId}");
            }
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound("Nije pronađen prijavljeni korisnik.");
            }

            var existingLikes = await _context.ProjectSubmissionLikes
                                    .Where(p => p.ProjectSubmissionID == submission.Id &&
                                                         p.UserId == user.Id)
                                    .ToListAsync();
            if (!existingLikes.Any()) return Ok();

            _context.ProjectSubmissionLikes.RemoveRange(existingLikes);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Likes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost("forumpost/{forumPostId}")]
        public async Task<ActionResult<ProjectSubmissionLike>> PostForumPostLike(int forumPostId)
        {
            var post = _context.ForumPosts.FirstOrDefault(p => p.Id == forumPostId);
            if (post == null)
            {
                return NotFound($"Nije pronađen forum post s IDjem {forumPostId}");
            }
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound("Nije pronađen prijavljeni korisnik.");
            }

            var like = new ForumPostLike()
            {
                ForumPostID = post.Id,
                UserId = user.Id
            };

            var existingLike = await _context.ForumPostLikes
                                    .FirstOrDefaultAsync(p => p.ForumPostID == post.Id &&
                                                         p.UserId == user.Id);
            if (existingLike != null) return Ok(existingLike);

            _context.ForumPostLikes.Add(like);
            await _context.SaveChangesAsync();
            return Ok(like);
        }

        [Authorize]
        [HttpPost("forumpost/{forumPostId}/remove")]
        public async Task<ActionResult<ProjectSubmissionLike>> RemoveForumPostLike(int forumPostId)
        {
            var post = _context.ForumPosts.FirstOrDefault(p => p.Id == forumPostId);
            if (post == null)
            {
                return NotFound($"Nije pronađen forum post s IDjem {forumPostId}");
            }
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound("Nije pronađen prijavljeni korisnik.");
            }

            var existingLikes = await _context.ForumPostLikes
                                    .Where(p => p.ForumPostID == post.Id &&
                                                         p.UserId == user.Id)
                                    .ToListAsync();
            if (!existingLikes.Any()) return Ok();

            _context.ForumPostLikes.RemoveRange(existingLikes);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
