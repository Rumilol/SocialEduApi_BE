using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.Entities
{
    public class ProjectSubmissionComment
    {
        public int? ID { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public int ProjectSubmissionID { get; set; }
        public ProjectSubmission ProjectSubmission { get; set; }
    }
}
