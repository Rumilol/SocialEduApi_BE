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
    public class ProjectSubmissionCommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectSubmissionCommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/ProjectSubmissionComments
        [HttpGet("forSubmission/{id}")]
        public async Task<ActionResult<IEnumerable<CommentVM>>> GetCommentsForSubmission(int id)
        {
            var submission = _context.ProjectSubmissions.FirstOrDefault(p => p.Id == id);
            if (submission == null) { return NotFound($"Nije pronađen submission s IDjem {id}"); }

            var users = _context.Users.ToList();
            var comments = await _context.ProjectSubmissionComments.Where(p => p.ProjectSubmissionID == submission.Id).ToListAsync();

            return comments.Select(p => new CommentVM(p, users)).ToList();
        }

        // POST: api/ProjectSubmissionComments/forSubmission/postId
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("forSubmission/{postId}")]
        [Authorize]
        public async Task<ActionResult<ProjectSubmissionComment>> PostProjectSubmissionComment(int postId, CommentVM cmt)
        {
            var comment = new ProjectSubmissionComment();
            comment.Text = cmt.Text;

            var projectSubmission = _context.ProjectSubmissions.FirstOrDefault(p => p.Id == postId);
            if (projectSubmission == null) return NotFound($"Nije pronađen submission s IDjem {postId}");
            comment.ProjectSubmissionID = projectSubmission.Id;

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            comment.UserID = user.Id;

            _context.ProjectSubmissionComments.Add(comment);
            await _context.SaveChangesAsync();
            return Created();
        }

        // POST: api/ProjectSubmissionComments/forSubmission/postId
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectSubmissionComment>> PutProjectSubmissionComment(int id, CommentVM cmt)
        {
            var comment = _context.ProjectSubmissionComments.FirstOrDefault(p => p.ID == id);
            if (comment == null) return NotFound($"Nije pronađen komentar s IDjem {id}");

            if (string.IsNullOrEmpty(cmt.Text))
            {
                _context.ProjectSubmissionComments.Remove(comment);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            comment.Text = cmt.Text;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
