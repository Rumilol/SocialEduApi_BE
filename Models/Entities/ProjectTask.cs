

namespace SocialEduApi.Models.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public int SubjectID { get; set; }
        public Subject? Subject { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Criteria { get; set; }
        public DateTime CreatedDate { get; set; }
        public float MaxGrade { get; set; }


    }
}
