using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class ProjectSubmissionVM
    {
        public ProjectSubmissionVM() { }
        public ProjectSubmissionVM(ProjectSubmission submission)
        {
            Id = submission.Id;
            SubjectID = submission.SubjectID;
            Subject = submission.Subject;
            SubjectName = submission.Subject?.Name;
            InstitutionName = submission.Subject?.Institution?.Name;
            InstitutionAbbreviation = submission.Subject?.Institution?.Abbreviation;
            ProjectTaskID = submission.ProjectTaskID;
            ProjectTask = submission.ProjectTask;
            ProjectTaskTitle = submission.ProjectTask?.Title;
            UserEmail = submission.User.Email;
            UserFirstName = submission.User.FirstName;
            UserLastName = submission.User.LastName;
            UserImage = submission.User.Image;
            Title = submission.Title;
            Description = submission.Description;
            Link = submission.Link;
            ImageLink = submission.ImageLink;
            Grade = submission.Grade;
            UploadDate = submission.UploadDate;
        }
        public ProjectSubmissionVM(ProjectSubmission submission,
                                   List<UserVM_short>? users,
                                   List<ProjectSubmissionLike> allLikes,
                                   string UserId) : this(submission)
        {
            GetComments(users);
            GetLikes(allLikes, UserId);
        }

        public int? Id { get; set; }
        public int? SubjectID { get; set; }
        public Subject? Subject { get; set; }
        public string? SubjectName { get; set; }
        public string? InstitutionName { get; set; }
        public string? InstitutionAbbreviation { get; set; }
        public int? ProjectTaskID { get; set; }
        public ProjectTask? ProjectTask { get; set; }
        public string? ProjectTaskTitle { get; set; }
        public string? UserEmail { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public string? UserImage { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string? ImageLink { get; set; }
        public float? Grade { get; set; }
        public DateTime? UploadDate { get; set; }

        public List<CommentVM>? Comments { get; set; }
        public int LikeCount { get; set; }
        public bool UserLiked { get; set; }

        private void GetComments(List<UserVM_short>? users)
        {
            if (users == null || users.Count == 0) return;
            Random rnd = new Random();
            int rndnum1 = rnd.Next(0, users.Count);
            int rndnum2 = rnd.Next(0, users.Count);
            int rndnum3 = rnd.Next(0, users.Count);
            Comments = new List<CommentVM>()
            {
                new CommentVM(1, "Ovo je dobro, samo sam drugačije zamislio prvi dio.", users[rndnum1]),
                new CommentVM(2, "Imao sam istu ideju!", users[rndnum2]),
                new CommentVM(3, "Ovo bi moglo biti korisno na drugoj godini diplomskog.", users[rndnum3]),
            };
        }

        private void GetLikes(List<ProjectSubmissionLike> allLikes, string userId)
        {
            var thisPostLikes = allLikes.Where(p => p.ProjectSubmissionID == Id).ToList();
            LikeCount = thisPostLikes.Count;
            var userLike = allLikes.FirstOrDefault(p => p.UserId == userId && p.ProjectSubmissionID == Id);
            if (userLike == null) UserLiked = false;
            else UserLiked = true;
        }
    }
}
