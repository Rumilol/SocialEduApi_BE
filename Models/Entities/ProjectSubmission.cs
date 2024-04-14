

using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.Entities
{
    public class ProjectSubmission
    {
        public int Id { get; set; }
        public int? SubjectID { get; set; }
        public Subject? Subject { get; set; }
        public int? ProjectTaskID { get; set; }
        public ProjectTask? ProjectTask { get; set; }
        public string UserID { get; set; }
        public ApplicationUser? User { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string? ImageLink { get; set; }
        public float? Grade { get; set; }
        public DateTime UploadDate { get; set; }


    }
}
