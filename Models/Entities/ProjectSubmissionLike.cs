using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.Entities
{
    public class ProjectSubmissionLike
    {
        public int Id { get; set; }
        public int ProjectSubmissionID { get; set; }
        public ProjectSubmission? ProjectSubmission { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
