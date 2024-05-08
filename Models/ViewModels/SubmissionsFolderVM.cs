using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class SubmissionsFolderVM
    {
        public SubmissionsFolderVM() { }
        public SubmissionsFolderVM(SubmissionFolder folder, string email)
        {
            Id = folder.Id;
            UserEmail = folder.User.Email;
            Name = folder.Name;
            Description = folder.Description;
            Submissions = folder.Submissions;
        }
        public int? Id { get; set; }
        public string? UserEmail { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public List<ProjectSubmissionVM>? Submissions { get; set; }
    }
}
