

namespace SocialEduApi.Models.Entities
{
    public class Forum
    {
        public int Id { get; set; }
        public int SubjectID { get; set; }
        public Subject? Subject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

    }
}
