using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class SubjectVM
    {
        public SubjectVM() { }
        public SubjectVM(Subject subject)
        {
            Id = subject.Id;
            Name = subject.Name;
            Abbreviation = subject.Abbreviation;
            Description = subject.Description;
            PageBackground = subject.PageBackground;
            InstitutionID = subject.InstitutionID;
            Institution = subject.Institution;
            CreatedDate = subject.CreatedDate;
        }

        public SubjectVM(Subject subject,
                         List<UserOnSubject>? users,
                         List<Forum> forums,
                         List<ForumPost> forumPosts,
                         List<ProjectTask> projectTasks,
                         List<ProjectSubmission> submissions,
                         List<ProjectSubmissionLike> submissionsLikes,
                         string userId) : this(subject)
        {
            GetUsers(users);
            GetForums(forums, forumPosts);
            GetProjectTasks(projectTasks, submissions, users, submissionsLikes, userId);
            GetMainPerson(users);
        }

        public int? Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Abbreviation { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? PageBackground { get; set; } = string.Empty;
        public int? InstitutionID { get; set; }
        public Institution? Institution { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<UserOnSubjectVM>? Users { get; set; }
        public List<ForumVM>? Forums { get; set; }
        public List<ProjectTaskVM>? ProjectTasks { get; set; }
        public UserOnSubjectVM? MainPerson { get; set; }


        private void GetUsers(List<UserOnSubject>? users)
        {
            if (users == null) return;
            var subjectMembers = users.Where(p => p.SubjectID == Id).ToList();
            Users = subjectMembers.Select(p => new UserOnSubjectVM(p)).ToList();
        }

        private void GetForums(List<Forum> forums, List<ForumPost>? forumPosts)
        {
            var subjectForums = forums.Where(p => p.SubjectID == Id).ToList();
            Forums = subjectForums.Select(p => new ForumVM(p, forumPosts)).ToList();
        }

        private void GetProjectTasks(List<ProjectTask> projectTasks,
                                     List<ProjectSubmission>? projectSubmissions,
                                     List<UserOnSubject>? users,
                                     List<ProjectSubmissionLike> projectSubmissionLikes,
                                     string userId)
        {
            if (projectTasks == null) return;
            if (projectSubmissions == null) return;
            var thisSubjectTasks = projectTasks.Where(p => p.SubjectID == Id).ToList();
            var userVMs = users.Select(p => new UserVM_short(p)).ToList();
            ProjectTasks = thisSubjectTasks.Select(p => new ProjectTaskVM(p, projectSubmissions, userVMs, projectSubmissionLikes, userId)).ToList();
        }

        private void GetMainPerson(List<UserOnSubject>? users)
        {
            if (users == null) return;
            var mainPerson = users.Where(p => p.HasAdminRights)
                                  .OrderBy(p => p.JoinedDate)
                                  .FirstOrDefault();
            if (mainPerson == null) return;
            MainPerson = new UserOnSubjectVM(mainPerson);
        }
    }
}
