using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class ProjectTaskVM
    {
        public ProjectTaskVM() { }
        public ProjectTaskVM(ProjectTask projectTask)
        {
            Id = projectTask.Id;
            SubjectID = projectTask.SubjectID;
            Subject = projectTask.Subject;
            Title = projectTask.Title;
            Description = projectTask.Description;
            Criteria = projectTask.Criteria;
            CreatedDate = projectTask.CreatedDate;
            MaxGrade = projectTask.MaxGrade;
        }

        public ProjectTaskVM(ProjectTask projectTask, List<ProjectSubmission>? submissions, List<UserVM_short> users) : this(projectTask)
        {
            GetProjectSubmissions(submissions, users);
        }

        public int? Id { get; set; }
        public int? SubjectID { get; set; }
        public Subject? Subject { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Criteria { get; set; }
        public DateTime? CreatedDate { get; set; }
        public float? MaxGrade { get; set; }

        public List<ProjectSubmissionVM>? Submissions { get; set; }

        private void GetProjectSubmissions(List<ProjectSubmission>? submissions, List<UserVM_short>? users)
        {
            if (submissions == null) return;
            if (users == null || users.Count() == 0) return;
            var thisProjectSubmissions = submissions.Where(s => s.ProjectTaskID == Id);
            Submissions = thisProjectSubmissions.Select(p => new ProjectSubmissionVM(p, users)).ToList();
        }

        public ProjectTask GetProjectTask()
        {
            var projectTask = new ProjectTask();
            projectTask.Id = Id.GetValueOrDefault();
            projectTask.SubjectID = SubjectID.GetValueOrDefault();
            projectTask.Subject = Subject;
            projectTask.Title = Title;
            projectTask.Description = Description;
            projectTask.Criteria = Criteria;
            projectTask.CreatedDate = CreatedDate.GetValueOrDefault();
            projectTask.MaxGrade = MaxGrade.GetValueOrDefault();
            return projectTask;
        }
    }
}
