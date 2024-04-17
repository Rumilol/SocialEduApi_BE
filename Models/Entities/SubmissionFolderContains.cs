

using Bogus;

namespace SocialEduApi.Models.Entities
{
    public class SubmissionFolderContains
    {
        public int Id { get; set; }
        public int SubmissionID { get; set; }
        public ProjectSubmission? Submission { get; set; }
        public int FolderID { get; set; }
        public SubmissionFolder? Folder { get; set; }

        public static Faker<SubmissionFolderContains> GetFaker(List<int> submissionIDs, SubmissionFolder folder)
        {
            return new Faker<SubmissionFolderContains>("hr")
                .RuleFor(x => x.SubmissionID, y => y.PickRandom(submissionIDs))
                .RuleFor(x => x.Folder, y => folder);
        }
    }
}
