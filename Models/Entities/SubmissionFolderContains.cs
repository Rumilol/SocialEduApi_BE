

namespace SocialEduApi.Models.Entities
{
    public class SubmissionFolderContains
    {
        public int Id { get; set; }
        public int SubmissionID { get; set; }
        public ProjectSubmission? Submission { get; set; }
        public int FolderID { get; set; }
        public SubmissionFolder? Folder { get; set; }


    }
}
